using System;
using System.Collections.Generic;
using System.Text;

namespace WebPersonal.Shared.Dto
{
    public class PersonalProjectsDto
    {
       public List<PersonalProjectDto> PersonalProjects { get; set; }
    }

    public  class PersonalProjectDto
    {
        public int Id { get; set; }
        public string ProjectType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Environment { get; set; }
    }


   
}
