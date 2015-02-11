namespace Restaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        WaiterId = c.Int(nullable: false),
                        Table_TableId = c.Int(),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Waiters", t => t.WaiterId, cascadeDelete: true)
                .ForeignKey("dbo.Tables", t => t.Table_TableId)
                .Index(t => t.WaiterId)
                .Index(t => t.Table_TableId);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        MealId = c.Int(nullable: false, identity: true),
                        Meal_name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.MealId);
            
            CreateTable(
                "dbo.Waiters",
                c => new
                    {
                        WaiterId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.WaiterId);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        isEmpty = c.Boolean(nullable: false),
                        Chair_number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TableId);
            
            CreateTable(
                "dbo.MealBills",
                c => new
                    {
                        Meal_MealId = c.Int(nullable: false),
                        Bill_BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_MealId, t.Bill_BillId })
                .ForeignKey("dbo.Meals", t => t.Meal_MealId, cascadeDelete: true)
                .ForeignKey("dbo.Bills", t => t.Bill_BillId, cascadeDelete: true)
                .Index(t => t.Meal_MealId)
                .Index(t => t.Bill_BillId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "Table_TableId", "dbo.Tables");
            DropForeignKey("dbo.Bills", "WaiterId", "dbo.Waiters");
            DropForeignKey("dbo.MealBills", "Bill_BillId", "dbo.Bills");
            DropForeignKey("dbo.MealBills", "Meal_MealId", "dbo.Meals");
            DropIndex("dbo.MealBills", new[] { "Bill_BillId" });
            DropIndex("dbo.MealBills", new[] { "Meal_MealId" });
            DropIndex("dbo.Bills", new[] { "Table_TableId" });
            DropIndex("dbo.Bills", new[] { "WaiterId" });
            DropTable("dbo.MealBills");
            DropTable("dbo.Tables");
            DropTable("dbo.Waiters");
            DropTable("dbo.Meals");
            DropTable("dbo.Bills");
        }
    }
}
