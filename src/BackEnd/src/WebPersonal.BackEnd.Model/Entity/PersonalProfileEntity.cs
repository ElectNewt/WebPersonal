
namespace WebPersonal.BackEnd.Model.Entity
{
    public class PersonalProfileEntity
    {
        public readonly int UserId;
        public readonly int? Id;
        public readonly string FirstName;
        public readonly string LastName;
        public readonly string Description;
        public readonly string Phone;
        public readonly string Email;
        public readonly string Website;
        public readonly string GitHub;

        protected PersonalProfileEntity(int? id, int userid, string firstname, string description, string phone, string email,
            string lastname, string website, string github)
        {
            UserId = userid;
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            Description = description;
            Phone = phone;
            Email = email;
            Website = website;
            GitHub = github;
        }
 
        public static PersonalProfileEntity Create(int userId, int? id, string firstName, string lastName, string description,
            string phone, string email, string website, string gitHub)
            => new PersonalProfileEntity(id, userId, firstName, description, phone, email, lastName, website, gitHub);

        public static PersonalProfileEntity UpdateId(PersonalProfileEntity perfilPersonal, int id)=> 
            new PersonalProfileEntity(id, perfilPersonal.UserId, perfilPersonal.FirstName, perfilPersonal.Description, perfilPersonal.Phone,
                perfilPersonal.Email, perfilPersonal.LastName, perfilPersonal.Website, perfilPersonal.GitHub);
    }
}
