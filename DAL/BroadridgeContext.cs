using System.Data.Entity;
using BroadridgeTestProject.Models;

namespace BroadridgeTestProject.DAL
{
    public class BroadridgeContext : DbContext
    {
        public BroadridgeContext(): base("BroadridgeDb")
        {
            
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}