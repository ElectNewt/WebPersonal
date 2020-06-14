using System.Collections.Generic;

namespace WebPersonal.Shared.Dto
{
    public class AcademicProjectsDto
    {

        public List<AcademicProjectDto> Projects { get; set; }
    }
    public class AcademicProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public List<string> Environment { get; set; }
    }
}
