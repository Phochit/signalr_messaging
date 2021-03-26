namespace Hitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatcontext5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.V2_Conversation",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sender_id = c.Int(nullable: false),
                        receiver_id = c.Int(nullable: false),
                        message = c.String(),
                        status = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.V2_Conversation");
        }
    }
}
