using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Containers
{
    public class UserContainer
    {
        IUser userDAL;

        public UserContainer(IUser userDAL)
        {
            this.userDAL = userDAL;
        }

        public bool UsernameExists(string username)
        {
            return userDAL.UsernameExists(username);
        }

        public bool EmailExists(string email)
        {
            return userDAL.EmailExists(email);
        }

        public bool CreateUser(User user)
        {
            return userDAL.CreateUser(user);
        }

        public bool UsernameEmailExists(string usernameEmail)
        {
            return userDAL.UsernameEmailExists(usernameEmail);
        }

        public string FetchPassword(string usernameEmail)
        {
            return userDAL.FetchPassword(usernameEmail);
        }

        public bool EditPassword(User user, string newPassword)
        {
            return userDAL.EditPassword(user, newPassword);
        }

        public bool EditEmail(User user, string newEmail)
        {
            return userDAL.EditEmail(user, newEmail);
        }

        public User GetUserDetails(Guid userGuid)
        {
            return userDAL.GetUserDetails(userGuid);
        }
        public User GetUserDetails(string usernameEmail)
        {
            return userDAL.GetUserDetails(usernameEmail);
        }
    }
}