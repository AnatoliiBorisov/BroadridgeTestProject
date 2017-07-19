using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BroadridgeTestProject.Infrastructure.Identity
{
    public static class IdentityExtensions
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {            
            return new MvcHtmlString(UserManager.FindByIdAsync(id).Result.UserName);
        }

        public static MvcHtmlString GetCurrentUserName(this HtmlHelper html)
        {
            return new MvcHtmlString(HttpContext.User.Identity.Name);           
        }

        public static MvcHtmlString GetCurrentUserRoleName(this HtmlHelper html)
        {
            var userName = HttpContext.User.Identity.Name;

            var user = UserManager.FindByName(userName);

            var userRoles = string.Empty;
            if (user?.Roles.Any() == true)
            {
                var roleNames = user.Roles
                                    .Select(ur => RoleManager.Roles.FirstOrDefault(r => ur.RoleId == r.Id))
                                    .Where(x => x != null)
                                    .Select(x => x.Name);

                userRoles = string.Join(", ", roleNames);
            }
            
            return new MvcHtmlString(userRoles);
        }

        public static bool IsUserInRole(string roleName)
        {
            var userName = HttpContext.User.Identity.Name;

            var user = UserManager.FindByName(userName);

            return user != null && UserManager.IsInRole(user.Id, roleName);
        }

        private static HttpContext HttpContext
        {
            get { return HttpContext.Current; }
        }

        private static AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        private static AppRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>(); }
        }
    }
}