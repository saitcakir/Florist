namespace CicekciDL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flowers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FlowerDisplays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FlowerId = c.Int(nullable: false),
                        DisplayDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Flowers", t => t.FlowerId, cascadeDelete: true)
                .Index(t => t.FlowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlowerDisplays", "FlowerId", "dbo.Flowers");
            DropIndex("dbo.FlowerDisplays", new[] { "FlowerId" });
            DropTable("dbo.FlowerDisplays");
            DropTable("dbo.Flowers");
        }
    }
}
