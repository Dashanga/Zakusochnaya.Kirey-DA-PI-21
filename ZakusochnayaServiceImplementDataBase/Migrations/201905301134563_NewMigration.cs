namespace ZakusochnayaServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessageInfoes", "PokupatelId", "dbo.Pokupatels");
            DropIndex("dbo.MessageInfoes", new[] { "PokupatelId" });
            DropColumn("dbo.Pokupatels", "Mail");
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
            
            AddColumn("dbo.Pokupatels", "Mail", c => c.String());
            CreateIndex("dbo.MessageInfoes", "PokupatelId");
            AddForeignKey("dbo.MessageInfoes", "PokupatelId", "dbo.Pokupatels", "Id");
        }
    }
}
