namespace WebPersonal.BackEnd.Model.Entity
{
    public class UserIdEntity
    {
        public readonly string UserName;
        public readonly int UserId;

        private UserIdEntity(string userName, int userId)
        {
            UserName = userName;
            UserId = userId;
        }

        public static UserIdEntity Create(string userName, int userId) =>
            new UserIdEntity(userName, userId);
    }
}
