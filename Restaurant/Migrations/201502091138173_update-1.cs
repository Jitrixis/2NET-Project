namespace Restaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MealBills", "Meal_MealId", "dbo.Meals");
            DropForeignKey("dbo.MealBills", "Bill_BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "Table_TableId", "dbo.Tables");
            DropIndex("dbo.Bills", new[] { "Table_TableId" });
            DropIndex("dbo.MealBills", new[] { "Meal_MealId" });
            DropIndex("dbo.MealBills", new[] { "Bill_BillId" });
            RenameColumn(table: "dbo.Bills", name: "Table_TableId", newName: "TableId");
            CreateTable(
                "dbo.MealStrings",
                c => new
                    {
                        MealStringId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Bill_BillId = c.Int(),
                    })
                .PrimaryKey(t => t.MealStringId)
                .ForeignKey("dbo.Bills", t => t.Bill_BillId)
                .Index(t => t.Bill_BillId);
            
            AddColumn("dbo.Bills", "BillPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Bills", "TableId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bills", "TableId");
            AddForeignKey("dbo.Bills", "TableId", "dbo.Tables", "TableId", cascadeDelete: true);
            DropTable("dbo.MealBills");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MealBills",
                c => new
                    {
                        Meal_MealId = c.Int(nullable: false),
                        Bill_BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_MealId, t.Bill_BillId });
            
            DropForeignKey("dbo.Bills", "TableId", "dbo.Tables");
            DropForeignKey("dbo.MealStrings", "Bill_BillId", "dbo.Bills");
            DropIndex("dbo.MealStrings", new[] { "Bill_BillId" });
            DropIndex("dbo.Bills", new[] { "TableId" });
            AlterColumn("dbo.Bills", "TableId", c => c.Int());
            DropColumn("dbo.Bills", "BillPrice");
            DropTable("dbo.MealStrings");
            RenameColumn(table: "dbo.Bills", name: "TableId", newName: "Table_TableId");
            CreateIndex("dbo.MealBills", "Bill_BillId");
            CreateIndex("dbo.MealBills", "Meal_MealId");
            CreateIndex("dbo.Bills", "Table_TableId");
            AddForeignKey("dbo.Bills", "Table_TableId", "dbo.Tables", "TableId");
            AddForeignKey("dbo.MealBills", "Bill_BillId", "dbo.Bills", "BillId", cascadeDelete: true);
            AddForeignKey("dbo.MealBills", "Meal_MealId", "dbo.Meals", "MealId", cascadeDelete: true);
        }
    }
}
