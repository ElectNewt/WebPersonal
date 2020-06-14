using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.Model.Mappers
{
    public static class EducationMapper
    {
        public static EducationDto Map(this EducationEntity entity)
        {
            return new EducationDto()
            {
                Id = entity.Id,
                CourseName = entity.CourseName,
                EndDate = entity.EndDate,
                StartDate = entity.StartDate,
                UniversityName = entity.UniversityName
            };
        }

    }
}
