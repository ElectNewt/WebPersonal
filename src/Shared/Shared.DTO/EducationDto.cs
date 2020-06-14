using System;

namespace WebPersonal.Shared.Dto
{
    public class EducationDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CourseName { get; set; }
        public string UniversityName { get; set; }
    }
}
