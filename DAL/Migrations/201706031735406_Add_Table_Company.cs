namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_Company : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.Company",
                c => new
                {
                    id = c.Int(false, true),
                    name = c.String(false, 200)
                }).PrimaryKey(c => c.id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Company");
        }
    }
}
