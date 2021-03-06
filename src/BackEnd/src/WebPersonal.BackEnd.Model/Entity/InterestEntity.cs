﻿namespace WebPersonal.BackEnd.Model.Entity
{
    public class InterestEntity
    {
        public readonly int? Id;
        public readonly int? UserId;
        public readonly string Description;

        protected InterestEntity(int? id, int? userid, string description)
        {
            UserId = userid;
            Description = description;
            Id = id;
        }

        public static InterestEntity Create(int? id, int? userId, string description) =>
            new InterestEntity(id, userId, description);

        public static InterestEntity UpdateId(InterestEntity entity, int id ) =>
            new InterestEntity(id, entity.UserId, entity.Description);
    }
}
