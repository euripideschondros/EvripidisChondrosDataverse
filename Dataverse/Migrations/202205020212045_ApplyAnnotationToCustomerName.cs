namespace Dataverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyAnnotationToCustomerName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Lname", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Customers", "Fname", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Fname", c => c.String());
            AlterColumn("dbo.Customers", "Lname", c => c.String());
        }
    }
}
