namespace WebPersonal.BackEnd.Model.Entity
{
    public class UserIdEntity
    {
        public readonly string UserName;
        public readonly int UserId;

        protected UserIdEntity(int userId, string userName)
        {
            UserName = userName;
            UserId = userId;
        }

        public static UserIdEntity Create(string userName, int userId) =>
            new UserIdEntity(userId, userName);

        public static UserIdEntity UpdateUserId(int userId, UserIdEntity userIdEntity) =>
            new UserIdEntity(userId, userIdEntity.UserName);
    }
}
