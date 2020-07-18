namespace WebPersonal.BackEnd.Model.Entity
{
    public class InterestEntity
    {
        public readonly int? Id;
        public readonly int? UserId;
        public readonly string Description;

        private InterestEntity(int? id, int? userId, string description)
        {
            UserId = userId;
            Description = description;
            Id = id;
        }

        public static InterestEntity Create(int? id, int? userId, string description) =>
            new InterestEntity(id, userId, description);
    }
}
