using System;

namespace WebPersonal.BackEnd.Model.Entity
{
    public class EducationEntity
    {
        public readonly int Id;
        public readonly DateTime StartDate;
        public readonly DateTime? EndDate; //Monad maybe?
        public readonly string CourseName;
        public readonly string UniversityName;
        private EducationEntity(int id, DateTime startDate, DateTime? endDate, string courseName, string universityName)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            CourseName = courseName ;
            UniversityName = universityName;
        }

        public EducationEntity UpdateEndDate(DateTime? endDate)
        {
            return new EducationEntity(Id, StartDate, endDate, CourseName, UniversityName);
        }
    }
}
