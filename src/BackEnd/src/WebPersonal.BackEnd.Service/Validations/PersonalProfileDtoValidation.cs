using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ROP;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.Service.Validations
{
    public static class PersonalProfileDtoValidation
    {

        public static Result<PersonalProfileDto> ValidateDto(this PersonalProfileDto profileDto)
        {

            return ValidateProfile(profileDto)
                .Bind(ValidateSkills)
                .Bind(ValidateInterests);

        }


        private static Result<PersonalProfileDto> ValidateProfile(this PersonalProfileDto profileDto)
        {
            List<Error> errors = new List<Error>();

            if (profileDto.Description.Length > 5000)
            {
                errors.Add(Error.Create("El campo descripción no puede ser mayor de 5000"));
            }

            if (profileDto.Email.Length > 50)
            {
                errors.Add(Error.Create("El campo email no puede ser mayor de 50"));
            }

            if (profileDto.FirstName.Length > 50)
            {
                errors.Add(Error.Create("El campo nombre propio no puede ser mayor de 50"));
            }

            if (profileDto.GitHub.Length > 50)
            {
                errors.Add(Error.Create("El campo Github no puede ser mayor de 50"));
            }

            if (profileDto.LastName.Length > 50)
            {
                errors.Add(Error.Create("El campo Apellido no puede ser mayor de 50"));
            }

            if (profileDto.Phone.Length > 50)
            {
                errors.Add(Error.Create("El campo Teléfono no puede ser mayor de 50"));
            }

            if (profileDto.UserName.Length > 50)
            {
                errors.Add(Error.Create("El campo Usuario no puede ser mayor de 50"));
            }

            if (string.IsNullOrWhiteSpace(profileDto.UserName))
            {
                errors.Add(Error.Create("El campo Usuario debe contener algún valor"));
            }

            if (profileDto.UserName.Length < 5)
            {
                errors.Add(Error.Create("El campo usuario debe ser de almenos de 5 caracteres"));
            }

            if (profileDto.Website.Length > 50)
            {
                errors.Add(Error.Create("El campo website debe ser almenos de 5 caracteres"));

            }


            return errors.Any() ?
                Result.Failure<PersonalProfileDto>(errors.ToImmutableArray())
                : profileDto;
        }


        private static Result<PersonalProfileDto> ValidateSkills(PersonalProfileDto profileDto)
        {
           return profileDto.Skills
                .Select(a => ValidateSkill(a))
                .Traverse()
                .Map(_=>profileDto);
        }


        private static Result<Unit> ValidateSkill(SkillDto skill)
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(skill.Name))
            {
                errors.Add(Error.Create($"El valor {skill.Name} no es válido"));
            }

            if (skill.Name.Length > 50)
            {
                errors.Add(Error.Create($"El valor {skill.Name} es demasiado largo (max 50)"));
            }

            if(skill.Punctuation != null && (skill.Punctuation<0 || skill.Punctuation > 10))
            {
                errors.Add(Error.Create($"La puntuacion de la skill {skill.Punctuation} debe estar entre 1 y 10"));
            }


            return errors.Any() ?
                Result.Failure(errors.ToImmutableArray())
                : Result.Unit;
        }

        private static Result<PersonalProfileDto> ValidateInterests(PersonalProfileDto profileDto)
        {
            return profileDto.Interests
                .Select(a => ValidateInterest(a.Interest))
                .Traverse()
                .Map(_ => profileDto);

            Result<Unit> ValidateInterest(string interest)
            {
                if(interest.Length>250 || interest.Length < 15)
                {
                    return Result.Failure(Error.Create("Los intereses deben contener entre 15 y 250 caracteres"));
                }
                return Result.Unit;
            }
        }

    }
}
