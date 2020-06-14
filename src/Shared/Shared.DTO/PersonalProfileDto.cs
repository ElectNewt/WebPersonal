using System.Collections.Generic;

namespace WebPersonal.Shared.Dto
{
    public class PersonalProfileDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string GitHub { get; set; }
        public List<string> Interests { get; set; }
        public List<SkillDto> Skills { get; set; }
    }

    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// How good do you consider yourself at this skill.
        /// </summary>
        public int Punctuation { get; set; }
    }
}
