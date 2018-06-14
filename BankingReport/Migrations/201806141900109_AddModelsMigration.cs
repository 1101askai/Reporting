namespace BankingReport.Migrations.DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CardId = c.Guid(nullable: false, identity: true),
                        ExpireMonth = c.Int(nullable: false),
                        ExpireYear = c.Int(nullable: false),
                        CardProvider = c.Int(nullable: false),
                        CVVCode = c.Int(nullable: false),
                        CardNumber = c.String(),
                        HolderName = c.String(),
                        User_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        AspNetUserId = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PostalCode = c.Int(nullable: false),
                        Country = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        TransactionId = c.Guid(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        TransDate = c.DateTime(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                        CreditCard_CardId = c.Guid(),
                        User_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.CreditCards", t => t.CreditCard_CardId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.CreditCard_CardId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Payments", "CreditCard_CardId", "dbo.CreditCards");
            DropForeignKey("dbo.CreditCards", "User_UserId", "dbo.Users");
            DropIndex("dbo.Payments", new[] { "User_UserId" });
            DropIndex("dbo.Payments", new[] { "CreditCard_CardId" });
            DropIndex("dbo.CreditCards", new[] { "User_UserId" });
            DropTable("dbo.Payments");
            DropTable("dbo.Users");
            DropTable("dbo.CreditCards");
        }
    }
}
