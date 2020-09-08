using System;
using System.Collections.Generic;
using System.Linq;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.Service.Mappers
{
    public static class InterestDtoMapper
    {
        public static List<InterestEntity> Map(this List<InterestDto> interests, int userId)
        {
            return interests.Select(a 
                => InterestEntity.Create(a.Id, userId, a.Interest)).ToList();
        }
    }
}
