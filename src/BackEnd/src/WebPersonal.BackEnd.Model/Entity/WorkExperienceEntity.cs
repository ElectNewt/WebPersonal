using System;
using System.Collections.Generic;

namespace WebPersonal.BackEnd.Model.Entity
{
    public class WorkExperienceEntity
    {

        public readonly int Id;
        public readonly int UserId;
        public readonly string Position;
        public readonly string CompanyName;
        public readonly string City;
        public readonly string Country;
        public readonly DateTime? StartDate;
        public readonly DateTime? EndDate;
        public readonly string? Environment;

        public WorkExperienceEntity(int id, int userId, string position, string companyName, string city, string country, 
            DateTime? startDate, DateTime? endDate, string? environment)
        {
            Id = id;
            UserId = userId;
            Position = position;
            CompanyName = companyName;
            City = city;
            Country = country;
            StartDate = startDate;
            EndDate = endDate;
            Environment = environment;
        }

        public static WorkExperienceEntity Create(int id, int userId, string position, string companyName, string city, string country,
            DateTime? startDate, DateTime? endDate, string? environment) =>
            new WorkExperienceEntity(id, userId, position, companyName, city, country, startDate, endDate, environment);

        public static WorkExperienceEntity UpdateId(int id, WorkExperienceEntity entity) =>
             new WorkExperienceEntity(id, entity.UserId, entity.Position, entity.CompanyName, entity.City, entity.Country, entity.StartDate, entity.EndDate, entity.Environment);

    }
}
