namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Lang", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Lang", c => c.Int(nullable: false));
        }
    }
}
