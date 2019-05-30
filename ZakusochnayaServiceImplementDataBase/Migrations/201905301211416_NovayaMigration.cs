namespace ZakusochnayaServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovayaMigration : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pokupatels", t => t.PokupatelId)
                .Index(t => t.PokupatelId);
            
            AddColumn("dbo.Pokupatels", "Mail", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageInfoes", "PokupatelId", "dbo.Pokupatels");
            DropIndex("dbo.MessageInfoes", new[] { "PokupatelId" });
            DropColumn("dbo.Pokupatels", "Mail");
            DropTable("dbo.MessageInfoes");
        }
    }
}
