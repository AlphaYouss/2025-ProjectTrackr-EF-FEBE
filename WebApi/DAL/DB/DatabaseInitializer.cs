using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.DAL.DB
{
    public class DatabaseInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.users.Any())
                return;

            string username = "AlphaYouss";
            string password = "Welkom12345!";
            string email = "mes10@live.nl";

            string hashedPassword = new PasswordHasher<string>().HashPassword(username, password);

            List<User> accounts =
            [
                new() {
                    userId = Guid.NewGuid(),
                    username = username,
                    email= email,
                    passwordHash= hashedPassword,
                    createdAt=DateTime.Now,
                }
            ];
            accounts.ForEach(a => context.users.Add(a));
            context.SaveChanges();
        }
    }
}