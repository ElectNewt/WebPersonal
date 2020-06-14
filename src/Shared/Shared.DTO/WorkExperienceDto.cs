using System;
using System.Collections.Generic;

namespace WebPersonal.Shared.DTO
{
    public class WorkExperienceDto
    {
        public List<PositionDto> Positions { get; set; }

    }

    public class PositionDto
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<string> Environment { get; set; }
        public List<WorkProjectDto> MainProjects { get; set; }
    }

    public class WorkProjectDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set; }

    }
}
