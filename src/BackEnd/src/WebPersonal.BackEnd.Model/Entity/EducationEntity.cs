using System;

namespace WebPersonal.BackEnd.Model.Entity
{
    public class EducationEntity
    {
        public readonly int Id;
        public readonly int UserId;
        public readonly DateTime StartDate;
        public readonly DateTime? EndDate; 
        public readonly string CourseName;
        public readonly string? CollegeName;
        public readonly string? City;
        public readonly string? Country;

        private EducationEntity(int id, int userId, DateTime startDate, DateTime? endDate, string courseName, string? collegeName, string? city, string? country)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            CourseName = courseName;
            CollegeName = collegeName;
            UserId = userId;
            City = city;
            Country = country;
        }

        public static EducationEntity Create(int id, int userId, DateTime startDate, DateTime? endDate, string courseName, string collegeName, string? city, string? country)
        => new EducationEntity(id, userId, startDate, endDate, courseName, collegeName, city, country);
    }
}
