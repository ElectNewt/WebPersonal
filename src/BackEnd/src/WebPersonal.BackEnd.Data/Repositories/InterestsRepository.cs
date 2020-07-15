using System;
using System.Data.Common;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class InterestsRepository : BaseRepository<InterestEntity>
    {
        public override string TableName => TableNames.Interest;

        public override InterestEntity Create(DbDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
