namespace WebPersonal.BackEnd.Model.Entity
{
    public class SkillEntity
    {
        public readonly int? UserId;
        public readonly int? Id;
        public readonly string Name;
        public readonly decimal? Punctuation;

        protected SkillEntity(int? id, int? userid, string name, decimal? punctuation)
        {
            UserId = userid;
            Id = id;
            Name = name;
            Punctuation = punctuation;
        }

        public static SkillEntity Create(int? userId, int? id, string name, decimal? punctuation)
            => new SkillEntity(id, userId, name, punctuation);

        public static SkillEntity UpdateId(SkillEntity skill, int id) =>
            new SkillEntity(id, skill.UserId, skill.Name, skill.Punctuation);
    }
}
