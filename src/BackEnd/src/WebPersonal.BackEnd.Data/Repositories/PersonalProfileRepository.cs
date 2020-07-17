using System;
using System.Data.Common;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class PersonalProfileRepository : BaseRepository<PersonalProfileEntity>
    {
        public override string TableName => TableNames.PersonalProfile;

        public override PersonalProfileEntity? Create(DbDataReader reader)
        {
            return PersonalProfileEntity.Create(
                Convert.ToInt32(reader[nameof(PersonalProfileEntity.UserId)]),
                Convert.ToInt32(reader[nameof(PersonalProfileEntity.Id)]),
                reader[nameof(PersonalProfileEntity.FirstName)].ToString() ?? "",
                reader[nameof(PersonalProfileEntity.LastName)].ToString() ?? "",
                reader[nameof(PersonalProfileEntity.Description)].ToString() ?? "",
                reader[nameof(PersonalProfileEntity.Phone)].ToString() ?? "",
                reader[nameof(PersonalProfileEntity.Email)].ToString() ?? "",
                reader[nameof(PersonalProfileEntity.Website)].ToString() ?? "",
                reader[nameof(PersonalProfileEntity.GitHub)].ToString() ?? ""
                );
        }


    }
}
