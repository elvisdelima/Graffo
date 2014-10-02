namespace Graffo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ColumnFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Columns", "Position", c => c.Int(nullable: false));
            AddColumn("dbo.Columns", "IsArchived", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Columns", "IsArchived");
            DropColumn("dbo.Columns", "Position");
        }
    }
}
