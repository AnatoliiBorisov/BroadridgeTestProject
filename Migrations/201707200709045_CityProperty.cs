using BroadridgeTestProject.Models;

namespace BroadridgeTestProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "City", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "City");
        }
    }
}
