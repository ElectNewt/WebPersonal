using System.Collections.Generic;

namespace WebPersonal.BackEnd.Model.Entity
{
    public class PersonalProfileEntity
    {
        public readonly int UserId;
        public readonly int Id;
        public readonly string FirstName;
        public readonly string LastName;
        public readonly string Description;
        public readonly string Phone;
        public readonly string Email;
        public readonly string Website;
        public readonly string GitHub;

        public PersonalProfileEntity(int userId, int id, string firstName, string lastName, string description, 
            string phone, string email, string website, string gitHub)
        {
            UserId = userId;
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Description = description;
            Phone = phone;
            Email = email;
            Website = website;
            GitHub = gitHub;
        }
    }
}
