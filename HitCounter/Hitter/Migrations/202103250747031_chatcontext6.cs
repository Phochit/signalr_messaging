namespace Hitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatcontext6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginStatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        loginid = c.Int(nullable: false),
                        LastLoginTime = c.DateTime(nullable: false),
                        loginstatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginStatus");
        }
    }
}
