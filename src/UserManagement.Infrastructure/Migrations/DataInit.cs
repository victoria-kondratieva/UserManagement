using UserManagement.Domain.Entities;
using UserManagement.Domain.Constants;
using UserManagement.Infrastructure.Helpers;

namespace UserManagement.Infrastructure.Migrations;

public static class DataInit
{
    public static void Seed(this DataContext context)
    {
        if (!context.Roles.Any())
        {
            context.Roles.Add(new Role
            {
                Name = "User"
            });
            context.Roles.Add(new Role
            {
                Name = "Admin"
            });
            context.Roles.Add(new Role
            {
                Name = "Support"
            });
            context.Roles.Add(new Role
            {
                Name = "SuperAdmin"
            });

            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            var userRole = context.Roles.Find(Constants.UserRoleId);

            context.Users.Add(new User
            {
                Name = "Victoire",
                Age = 12,
                Email = "victoire.david@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Wiktor",
                Age = 20,
                Email = "wiktor.skaare@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Mikola",
                Age = 42,
                Email = "mikola.nikitenko@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Jules",
                Age = 65,
                Email = "jules.robert@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Amalie",
                Age = 15,
                Email = "amalie.moller@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Thn",
                Age = 9,
                Email = "thn.shylyrd@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Tommaso",
                Age = 37,
                Email = "tommaso.vincent@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Ceyhun",
                Age = 41,
                Email = "ceyhun.yilmazer@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Logan",
                Age = 18,
                Email = "logan.mckinney@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Elmer",
                Age = 84,
                Email = "elmer.sutton@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Taylor",
                Age = 30,
                Email = "taylor.castillo@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Marlise",
                Age = 10,
                Email = "marlise.vanseventer@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Deniz",
                Age = 23,
                Email = "deniz.gonultas@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Alain",
                Age = 19,
                Email = "alain.roger@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Aldo",
                Age = 54,
                Email = "aldo.benavidez@example.com",
                Roles = new List<Role> { userRole }
            });
            context.Users.Add(new User
            {
                Name = "Jacob",
                Age = 48,
                Email = "jacob.andrews@example.com",
                Roles = new List<Role> { userRole }
            });

            context.SaveChanges();
        }
    }
}
