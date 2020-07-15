namespace WebPersonal.BackEnd.Model.Entity
{
    public class SkillEntity
    {
        public readonly int UserId;
        public readonly int Id;
        public readonly string Name;
        public readonly int Punctuation;

        private SkillEntity(int userId, int id, string name, int punctuation)
        {
            UserId = userId;
            Id = id;
            Name = name;
            Punctuation = punctuation;
        }

        public static SkillEntity Create(int userId, int id, string name, int punctuation)
            => new SkillEntity(userId, id, name, punctuation);
    }
}
