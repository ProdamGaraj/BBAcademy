namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPriceToCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Price");
        }
    }
}
