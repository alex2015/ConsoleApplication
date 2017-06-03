namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillingDetail_TPH",
                c => new
                    {
                        BillingDetailId = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        Number = c.String(),
                        BankName = c.String(),
                        Swift = c.String(),
                        CardType = c.Int(),
                        ExpiryMonth = c.String(),
                        ExpiryYear = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BillingDetailId);
            
            CreateTable(
                "dbo.BillingDetail_TPT",
                c => new
                    {
                        BillingDetailId = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.BillingDetailId);
            
            CreateTable(
                "dbo.User_TPT",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BillingDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.BillingDetail_TPT", t => t.BillingDetailId, cascadeDelete: true)
                .Index(t => t.BillingDetailId);
            
            CreateTable(
                "dbo.BankAccount_TPC",
                c => new
                    {
                        BillingDetailId = c.Int(nullable: false),
                        Owner = c.String(),
                        Number = c.String(),
                        BankName = c.String(),
                        Swift = c.String(),
                    })
                .PrimaryKey(t => t.BillingDetailId);
            
            CreateTable(
                "dbo.CreditCard_TPC",
                c => new
                    {
                        BillingDetailId = c.Int(nullable: false),
                        Owner = c.String(),
                        Number = c.String(),
                        CardType = c.Int(nullable: false),
                        ExpiryMonth = c.String(),
                        ExpiryYear = c.String(),
                    })
                .PrimaryKey(t => t.BillingDetailId);
            
            CreateTable(
                "dbo.BankAccount_TPT",
                c => new
                    {
                        BillingDetailId = c.Int(nullable: false),
                        BankName = c.String(),
                        Swift = c.String(),
                    })
                .PrimaryKey(t => t.BillingDetailId)
                .ForeignKey("dbo.BillingDetail_TPT", t => t.BillingDetailId)
                .Index(t => t.BillingDetailId);
            
            CreateTable(
                "dbo.CreditCard_TPT",
                c => new
                    {
                        BillingDetailId = c.Int(nullable: false),
                        CardType = c.Int(nullable: false),
                        ExpiryMonth = c.String(),
                        ExpiryYear = c.String(),
                    })
                .PrimaryKey(t => t.BillingDetailId)
                .ForeignKey("dbo.BillingDetail_TPT", t => t.BillingDetailId)
                .Index(t => t.BillingDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCard_TPT", "BillingDetailId", "dbo.BillingDetail_TPT");
            DropForeignKey("dbo.BankAccount_TPT", "BillingDetailId", "dbo.BillingDetail_TPT");
            DropForeignKey("dbo.User_TPT", "BillingDetailId", "dbo.BillingDetail_TPT");
            DropIndex("dbo.CreditCard_TPT", new[] { "BillingDetailId" });
            DropIndex("dbo.BankAccount_TPT", new[] { "BillingDetailId" });
            DropIndex("dbo.User_TPT", new[] { "BillingDetailId" });
            DropTable("dbo.CreditCard_TPT");
            DropTable("dbo.BankAccount_TPT");
            DropTable("dbo.CreditCard_TPC");
            DropTable("dbo.BankAccount_TPC");
            DropTable("dbo.User_TPT");
            DropTable("dbo.BillingDetail_TPT");
            DropTable("dbo.BillingDetail_TPH");
        }
    }
}
