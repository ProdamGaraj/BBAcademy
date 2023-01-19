namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "MediaPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "MediaPath");
        }
    }
}
