namespace Graffo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdOrganization = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        IdImportation = c.Long(nullable: false),
                        IdTrello = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardMembers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdCard = c.String(),
                        IdMember = c.String(),
                        IdImportation = c.Long(nullable: false),
                        IdTrello = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdColumn = c.String(),
                        Name = c.String(),
                        IdImportation = c.Long(nullable: false),
                        IdTrello = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Columns",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdBoard = c.String(),
                        Name = c.String(),
                        IdImportation = c.Long(nullable: false),
                        IdTrello = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        IdImportation = c.Long(nullable: false),
                        IdTrello = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IdImportation = c.Long(nullable: false),
                        IdTrello = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Organizations");
            DropTable("dbo.Members");
            DropTable("dbo.Columns");
            DropTable("dbo.Cards");
            DropTable("dbo.CardMembers");
            DropTable("dbo.Boards");
        }
    }
}
