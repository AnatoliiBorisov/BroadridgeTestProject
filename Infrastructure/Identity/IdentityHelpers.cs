using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace BroadridgeTestProject.Infrastructure.Identity
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            var mgr = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();

            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }

        public static MvcHtmlString GetCurrentUserName(this HtmlHelper html)
        {
            return new MvcHtmlString(HttpContext.Current.User.Identity.Name);           
        }
    }
}