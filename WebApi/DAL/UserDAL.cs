using WebApi.DAL.DB;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.DAL
{
    public class UserDAL : IUser
    {
        private DatabaseContext dc {  get; set; }

        public UserDAL(DatabaseContext context) { 
            dc = context;
        }

        public bool CreateUser(User user)
        {
            dc.users.Add(user);

            if (dc.SaveChanges() > 0)
                return true;
            return false;    
        }

        public bool EditEmail(User user, string newEmail)
        {
            User? searchedUser = dc.users
            .FirstOrDefault(findUser =>
                findUser.userId.Equals(user.userId)
            ) ?? throw new KeyNotFoundException("User not found.");

            searchedUser.email = newEmail;

            if (dc.SaveChanges() > 0)
                return true;
            return false;
        }

        public bool EditPassword(User user, string newPassword)
        {
            User? searchedUser = dc.users
            .FirstOrDefault(findUser =>
                findUser.userId.Equals(user.userId)
            ) ?? throw new KeyNotFoundException("User not found.");

            searchedUser.passwordHash = newPassword;

            if (dc.SaveChanges() > 0)
                return true;
            return false;
        }

        public bool EmailExists(string email)
        {
            IEnumerable<User> users = [.. dc.users.Where(
                 user =>
                 user.email.ToLower().Equals(email.ToLower())
            )];

            if (users.Count() == 1)
            {
                return true;
            }
            return false;
        }

        public string FetchPassword(string usernameEmail)
        {
            User? user = dc.users
            .FirstOrDefault(findUser =>
                findUser.username.ToLower().Equals(usernameEmail.ToLower()) ||
                findUser.email.ToLower().Equals(usernameEmail.ToLower())
            );

            return user?.passwordHash ?? string.Empty;
        }

        public User GetUserDetails(Guid userGuid)
        {
            User? user = dc.users
            .FirstOrDefault(findUser =>
                findUser.userId.Equals(userGuid)
            ) ?? throw new KeyNotFoundException("User not found.");

            return user;
        }

        public User GetUserDetails(string usernameEmail)
        {
            User? user = dc.users
            .FirstOrDefault(findUser =>
                findUser.username.ToLower().Equals(usernameEmail.ToLower()) ||
                findUser.email.ToLower().Equals(usernameEmail.ToLower())
            ) ?? throw new KeyNotFoundException("User not found.");

            return user;
        }

        public bool UsernameEmailExists(string usernameEmail)
        {
            IEnumerable<User> users = [.. dc.users.Where(
                 user =>
                 user.username.ToLower().Equals(usernameEmail.ToLower()) ||
                 user.email.ToLower().Equals(usernameEmail.ToLower())
            )];

            if (users.Count() == 1)
            {
                return true;
            }
            return false;
        }

        public bool UsernameExists(string username)
        {
            IEnumerable<User> users = [.. dc.users.Where(
                 user =>
                 user.username.ToLower().Equals(username.ToLower())
            )];

            if (users.Count() == 1)
            {
                return true;
            }
            return false;
        }
    }
}