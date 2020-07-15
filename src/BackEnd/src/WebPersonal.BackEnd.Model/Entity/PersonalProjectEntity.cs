using System;
using System.Collections.Generic;

namespace WebPersonal.BackEnd.Model.Entity
{
    public class PersonalProjectEntity
    {
        public readonly int Id;
        public readonly int UserId;
        public readonly string Name;
        public readonly string Details;
        public readonly string? Environment;
        public readonly DateTime? Date;
        //Todo: group them by category?


        private PersonalProjectEntity(int id, int userId, string name, string details, string? environment, DateTime? date)
        {
            Id = id;
            Name = name;
            Details = details;
            Environment = environment;
            UserId = userId;
            Date = date;
        }

        public static PersonalProjectEntity Create(int id, int userId, string name, string details, string? environment, DateTime? date)
            => new PersonalProjectEntity(id, userId, name, details, environment, date);
    }
}
