namespace Hitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatcontext3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        empId = c.Int(nullable: false, identity: true),
                        empName = c.String(),
                        Salary = c.Int(nullable: false),
                        DeptName = c.String(),
                        Designation = c.String(),
                    })
                .PrimaryKey(t => t.empId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
