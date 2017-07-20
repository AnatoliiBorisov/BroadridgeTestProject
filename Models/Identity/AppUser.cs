using Microsoft.AspNet.Identity.EntityFramework;

namespace BroadridgeTestProject.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public City? City { get; set; }
    }
}