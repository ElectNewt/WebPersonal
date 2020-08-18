using System;
using System.Data.Common;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class InterestsRepository : BaseRepository<InterestEntity>
    {
        public InterestsRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }

        public override string TableName => TableNames.Interest;
    }
}
