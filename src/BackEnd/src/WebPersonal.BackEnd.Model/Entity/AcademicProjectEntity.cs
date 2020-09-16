using System;
using System.Collections.Generic;

namespace WebPersonal.BackEnd.Model.Entity
{
    public class AcademicProjectEntity
    {
        public readonly int Id;
        public readonly int EducationId;
        public readonly int UserId;
        public readonly string Name;
        public readonly string Details;
        public readonly List<string> Environment;
        public readonly DateTime? Date;


        private AcademicProjectEntity(int id, int educationId, int userId, string name, string details, List<string> environment, DateTime? date)
        {
            Id = id;
            EducationId = educationId;
            Name = name;
            Details = details;
            Environment = environment;
            UserId = userId;
            Date = date;
        }

        public static AcademicProjectEntity Create(int id, int educationId, int userId, string name, string details, List<string> environment, DateTime? date)
            => new AcademicProjectEntity(id, educationId, userId, name, details, environment, date);

        public static AcademicProjectEntity UpdateId(int id, AcademicProjectEntity academicProjectEntity)
            => new AcademicProjectEntity(id, academicProjectEntity.EducationId, academicProjectEntity.UserId, academicProjectEntity.Name, academicProjectEntity.Details, 
                academicProjectEntity.Environment, academicProjectEntity.Date);

    }
}
