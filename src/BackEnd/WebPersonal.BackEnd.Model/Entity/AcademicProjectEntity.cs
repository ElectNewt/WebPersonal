using System;
using System.Collections.Generic;

namespace WebPersonal.BackEnd.Model.Entity
{
    public class AcademicProjectEntity
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Details;
        public readonly List<string> Environment;

        private AcademicProjectEntity(int id, string name, string details, List<string> environment)
        {
            Id = id;
            Name = name;
            Details = details;
            Environment = environment;
        }
      
    }
}
