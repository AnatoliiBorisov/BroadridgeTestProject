using System.Collections.Generic;
using BroadridgeTestProject.Models.Identity;

namespace BroadridgeTestProject.Models.Web
{
    public class AdminIndexModel
    {
        public IEnumerable<AppUser> Users { get; set; }

        public IEnumerable<AppRole> Roles { get; set; }
    }
}