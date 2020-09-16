using System;

namespace WebPersonal.BackEnd.Model.Entity
{
    public class WorkProjectEntity
    {
        public readonly int Id;
        public readonly int WorkId;
        public readonly int UserId;
        public readonly string Name;
        public readonly string Details;
        public readonly string? Environment;
        public readonly DateTime? Date;


        private WorkProjectEntity(int id, int workId, int userId, string name, string details, string? environment, DateTime? date)
        {
            Id = id;
            WorkId = workId;
            Name = name;
            Details = details;
            Environment = environment;
            UserId = userId;
            Date = date;
        }

        public static WorkProjectEntity Create(int id, int workId, int userId, string name, string details, string? environment, DateTime? date) => 
            new WorkProjectEntity(id, workId, userId, name, details, environment, date);

        public static WorkProjectEntity UpdateId(int id, WorkProjectEntity entity) =>
             new WorkProjectEntity(id, entity.WorkId, entity.UserId, entity.Name, entity.Details, entity.Environment, entity.Date);
    }
}
