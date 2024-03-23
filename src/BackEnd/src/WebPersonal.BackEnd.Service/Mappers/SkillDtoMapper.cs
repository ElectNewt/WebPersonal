using System.Collections.Generic;
using System.Linq;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.Service.Mappers
{
    public static class SkillDtoMapper
    {
        public static List<SkillEntity> Map(this List<SkillDto> skills, int userId)
        {
            return  skills.Select(a => SkillEntity.Create(userId, a.Id, a.Name, a.Punctuation)).ToList();
        }
    }
}
