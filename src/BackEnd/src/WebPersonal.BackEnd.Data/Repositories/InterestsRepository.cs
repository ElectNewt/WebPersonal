using System;
using System.Data.Common;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class InterestsRepository : BaseRepository<InterestEntity>
    {
        public InterestsRepository(DbConnection conexion) : base(conexion)
        {
        }

        public override string TableName => TableNames.Interest;
    }
}
