using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IUser
    {
        bool UsernameExists(string username);
        bool EmailExists(string email);
        bool CreateUser(User user);
        bool UsernameEmailExists(string usernameEmail);
        string FetchPassword(string usernameEmail);
        bool EditPassword(User user, string newPassword);
        bool EditEmail(User user, string newEmail);
        User GetUserDetails(Guid userGuid);
        User GetUserDetails(string usernameEmail);
    }
}