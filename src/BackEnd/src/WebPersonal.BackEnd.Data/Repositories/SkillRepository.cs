using Dapper;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class SkillRepository : BaseRepository<SkillEntity>
    {
        public override string TableName => TableNames.Skill;

        public SkillRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }

        public override async Task<SkillEntity> InsertSingle(SkillEntity obj)
        {
            string sql = $"insert into {TableName} ({nameof(SkillEntity.UserId).ToLower()}, {nameof(SkillEntity.Name).ToLower()}, " +
                    $"{nameof(SkillEntity.Punctuation).ToLower()}) " +
                    $"values (@{nameof(SkillEntity.UserId)}, @{nameof(SkillEntity.Name)}, @{nameof(SkillEntity.Punctuation)});" +
                    $"SELECT LAST_INSERT_ID();";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, new
            {
                Userid = obj.UserId,
                Name = obj.Name,
                Punctuation = obj.Punctuation
            })).First();
            return SkillEntity.UpdateId(obj, newId);

        }

        public override async Task<SkillEntity> UpdateSingle(SkillEntity obj)
        {
            string sql = $"Update {TableName} " +
                 $"set {nameof(SkillEntity.Name).ToLower()} = @{nameof(SkillEntity.Name)}, " +
                     $"{nameof(SkillEntity.Punctuation).ToLower()} = @{nameof(SkillEntity.Punctuation)} " +
                     $"Where {nameof(SkillEntity.Id).ToLower()} = @{nameof(SkillEntity.Id)}";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, new
            {
                Name = obj.Name,
                Punctuation = obj.Punctuation,
                Id = obj.Id
            });
            return obj;
        }
    }
}
