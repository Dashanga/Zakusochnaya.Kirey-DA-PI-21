namespace ZakusochnayaServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Zakazs", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.MessageInfoes", "PokupatelId", "dbo.Pokupatels");
            DropIndex("dbo.Zakazs", new[] { "ExecutorId" });
            DropIndex("dbo.MessageInfoes", new[] { "PokupatelId" });
            DropColumn("dbo.Zakazs", "ExecutorId");
            DropColumn("dbo.Pokupatels", "Mail");
            DropTable("dbo.Executors");
            DropTable("dbo.MessageInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        PokupatelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Executors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExecutorFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Pokupatels", "Mail", c => c.String());
            AddColumn("dbo.Zakazs", "ExecutorId", c => c.Int());
            CreateIndex("dbo.MessageInfoes", "PokupatelId");
            CreateIndex("dbo.Zakazs", "ExecutorId");
            AddForeignKey("dbo.MessageInfoes", "PokupatelId", "dbo.Pokupatels", "Id");
            AddForeignKey("dbo.Zakazs", "ExecutorId", "dbo.Executors", "Id");
        }
    }
}
