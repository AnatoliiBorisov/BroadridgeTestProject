using BroadridgeTestProject.DAL.Identity;
using BroadridgeTestProject.Infrastructure.Identity;
using BroadridgeTestProject.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using BroadridgeTestProject.Models;

namespace BroadridgeTestProject.Migrations
{    
    internal sealed class Configuration : DbMigrationsConfiguration<AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            ContextKey = "BroadridgeTestProject.DAL.Identity.AppIdentityDbContext";
        }

        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
        }

        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            const string roleAdmin = "Administrators";
            const string roleUser = "Users";
            const string roleEmployees = "Employees";

            const string userAdmin = "Admin";
            const string passwordAdmin = "P@ssw0rd";
            const string emailAdmin = "admin@example.com";

            const string userJoe = "Joe";
            const string passwordJoe = "P@ssw0rd";
            const string emailJoe = "joe@example.com";

            const string userSteve = "Steve";
            const string passworSteve = "P@ssw0rd";
            const string emailSteve = "steve@example.com";

            const string userAlice = "Alice";
            const string passworAlice = "P@ssw0rd";
            const string emailAlice = "alice@example.com";

            const string userBob = "Bob";
            const string passworBob = "P@ssw0rd";
            const string emailBob = "bob@example.com";

            var userMgr = new AppUserManager(new UserStore<AppUser>(context));
            var roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            RoleCreate(roleMgr, roleAdmin);
            RoleCreate(roleMgr, roleUser);
            RoleCreate(roleMgr, roleEmployees);

            var admin = UserCreate(userMgr, new UserDto(userAdmin, passwordAdmin, emailAdmin));

            var joe = UserCreate(userMgr, new UserDto(userJoe, passwordJoe, emailJoe));
            var steve = UserCreate(userMgr, new UserDto(userSteve, passworSteve, emailSteve));
            var alice = UserCreate(userMgr, new UserDto(userAlice, passworAlice, emailAlice));
            var bob = UserCreate(userMgr, new UserDto(userBob, passworBob, emailBob));

            AddUserToRole(userMgr, admin, roleAdmin);

            AddUserToRole(userMgr, joe, roleUser);
            AddUserToRole(userMgr, steve, roleUser);
            AddUserToRole(userMgr, alice, roleUser);

            AddUserToRole(userMgr, alice, roleEmployees);
            AddUserToRole(userMgr, bob, roleEmployees);
        }

        private void RoleCreate(AppRoleManager roleManager, string roleName)
        {
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new AppRole(roleName));
            }
        }

        private AppUser UserCreate(AppUserManager userManager, UserDto userDto)
        {
            var user = userManager.FindByName(userDto.Name);

            if (user == null)
            {
                userManager.Create(new AppUser
                {
                    UserName = userDto.Name,

                    Email = userDto.Email
                }
                                    , userDto.Password);

                user = userManager.FindByName(userDto.Name);
            }

            return user;
        }

        private void AddUserToRole(AppUserManager userManager, AppUser user, string roleName)
        {
            if (!userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, roleName);
            }
        }

        private class UserDto
        {
            public string Name { get; }

            public string Password { get; }

            public string Email { get; }
           

            public UserDto(string name, string password, string email)
            {
                Name = name;

                Password = password;

                Email = email;                
            }
        }
    }
}
