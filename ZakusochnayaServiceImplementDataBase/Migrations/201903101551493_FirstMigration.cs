namespace ZakusochnayaServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Elements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ElementName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OutputElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutputId = c.Int(nullable: false),
                        ElementId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Elements", t => t.ElementId, cascadeDelete: true)
                .Index(t => t.ElementId);
            
            CreateTable(
                "dbo.SkladElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkladId = c.Int(nullable: false),
                        ElementId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Elements", t => t.ElementId, cascadeDelete: true)
                .ForeignKey("dbo.Sklads", t => t.SkladId, cascadeDelete: true)
                .Index(t => t.SkladId)
                .Index(t => t.ElementId);
            
            CreateTable(
                "dbo.Sklads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkladName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Outputs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutputName = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zakazs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PokupatelId = c.Int(nullable: false),
                        OutputId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Summa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Outputs", t => t.OutputId, cascadeDelete: true)
                .ForeignKey("dbo.Pokupatels", t => t.PokupatelId, cascadeDelete: true)
                .Index(t => t.PokupatelId)
                .Index(t => t.OutputId);
            
            CreateTable(
                "dbo.Pokupatels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PokupatelFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zakazs", "PokupatelId", "dbo.Pokupatels");
            DropForeignKey("dbo.Zakazs", "OutputId", "dbo.Outputs");
            DropForeignKey("dbo.SkladElements", "SkladId", "dbo.Sklads");
            DropForeignKey("dbo.SkladElements", "ElementId", "dbo.Elements");
            DropForeignKey("dbo.OutputElements", "ElementId", "dbo.Elements");
            DropIndex("dbo.Zakazs", new[] { "OutputId" });
            DropIndex("dbo.Zakazs", new[] { "PokupatelId" });
            DropIndex("dbo.SkladElements", new[] { "ElementId" });
            DropIndex("dbo.SkladElements", new[] { "SkladId" });
            DropIndex("dbo.OutputElements", new[] { "ElementId" });
            DropTable("dbo.Pokupatels");
            DropTable("dbo.Zakazs");
            DropTable("dbo.Outputs");
            DropTable("dbo.Sklads");
            DropTable("dbo.SkladElements");
            DropTable("dbo.OutputElements");
            DropTable("dbo.Elements");
        }
    }
}
