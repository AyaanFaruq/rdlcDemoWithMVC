namespace rdlcDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalaryCoumnAddedToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Salary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Salary");
        }
    }
}
