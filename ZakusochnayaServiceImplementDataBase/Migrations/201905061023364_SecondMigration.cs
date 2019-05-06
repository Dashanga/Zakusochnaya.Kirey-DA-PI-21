namespace ZakusochnayaServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Executors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExecutorFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Zakazs", "ExecutorId", c => c.Int());
            CreateIndex("dbo.Zakazs", "ExecutorId");
            AddForeignKey("dbo.Zakazs", "ExecutorId", "dbo.Executors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zakazs", "ExecutorId", "dbo.Executors");
            DropIndex("dbo.Zakazs", new[] { "ExecutorId" });
            DropColumn("dbo.Zakazs", "ExecutorId");
            DropTable("dbo.Executors");
        }
    }
}
