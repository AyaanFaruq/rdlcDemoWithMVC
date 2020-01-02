namespace rdlcDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatrixDemoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatrixDemoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalesPerson = c.String(),
                        Product = c.String(),
                        SalesPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MatrixDemoes");
        }
    }
}
