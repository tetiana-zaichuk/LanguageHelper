using System;
using LanguageHelper.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageHelper.DataAccessLayer
{
    public static class LanguageHelperDbInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var roles = new Role[]{
                new Role(){Id = 1, Title = "Admin"},
                new Role(){Id = 2, Title = "User"}
            };

            var users = new User[]{
                new User(){Id = 1, Name = "Nick", NickName = "Den", RoleId = 1,
                    CreatedAt = DateTime.Now, LastActive = DateTime.Now, Email = "f@gmail.com",
                    Password = "f"}
            };

            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
