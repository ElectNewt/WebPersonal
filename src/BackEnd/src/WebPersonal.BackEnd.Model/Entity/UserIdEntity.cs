namespace WebPersonal.BackEnd.Model.Entity
{
    public class UserIdEntity
    {
        public readonly string UserName;
        public readonly int UserId;

        public UserIdEntity(string userName, int userId)
        {
            UserName = userName;
            UserId = userId;
        }
    }
}
