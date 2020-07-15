using System;
using System.Data.Common;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class SkillRepository : BaseRepository<SkillEntity>
    {
        public override string TableName => TableNames.Skill;

        public override SkillEntity Create(DbDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
