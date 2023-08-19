namespace CicekciDL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlowerPciturecolumnadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flowers", "FlowerPicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Flowers", "FlowerPicture");
        }
    }
}
