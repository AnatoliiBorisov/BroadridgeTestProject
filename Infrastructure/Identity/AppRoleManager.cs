using System;
using BroadridgeTestProject.DAL.Identity;
using BroadridgeTestProject.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace BroadridgeTestProject.Infrastructure.Identity
{
    internal class AppRoleManager : RoleManager<AppRole>, IDisposable
    {
        public AppRoleManager(RoleStore<AppRole> store) : base(store)
        {
        }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            return new AppRoleManager(new
            RoleStore<AppRole>(context.Get<AppIdentityDbContext>()));
        }
    }
}