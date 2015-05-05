namespace Telephony_selling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        PasswordHelp = c.String(),
                        Family = c.String(),
                        Name = c.String(),
                        Second = c.String(),
                        RoleID = c.Int(nullable: false),
                        Phone = c.String(),
                        Mail = c.String(),
                    })
                .PrimaryKey(t => t.ClientID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Loads",
                c => new
                    {
                        LoadID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Crew = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ClientID = c.Int(nullable: false),
                        DiscountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoadID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Discounts", t => t.DiscountID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.DiscountID);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        DiscountID = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Mincost = c.Double(nullable: false),
                        Maxcost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DiscountID);
            
            CreateTable(
                "dbo.LoadLists",
                c => new
                    {
                        LoadListID = c.Int(nullable: false, identity: true),
                        count = c.Int(nullable: false),
                        ItemsID = c.Int(nullable: false),
                        LoadID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoadListID)
                .ForeignKey("dbo.Items", t => t.ItemsID, cascadeDelete: true)
                .ForeignKey("dbo.Loads", t => t.LoadID, cascadeDelete: true)
                .Index(t => t.ItemsID)
                .Index(t => t.LoadID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemsID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        count = c.Int(nullable: false),
                        RateTID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ItemsID)
                .ForeignKey("dbo.RateTs", t => t.RateTID, cascadeDelete: true)
                .Index(t => t.RateTID);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        ParametersID = c.Int(nullable: false, identity: true),
                        ParamValue = c.String(),
                        ItemsID = c.Int(nullable: false),
                        ParamClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParametersID)
                .ForeignKey("dbo.Items", t => t.ItemsID, cascadeDelete: true)
                .ForeignKey("dbo.ParamClasses", t => t.ParamClassID, cascadeDelete: true)
                .Index(t => t.ItemsID)
                .Index(t => t.ParamClassID);
            
            CreateTable(
                "dbo.ParamClasses",
                c => new
                    {
                        ParamClassID = c.Int(nullable: false, identity: true),
                        ParamClassName = c.String(),
                        Format = c.Int(nullable: false),
                        ParamRateID = c.Int(nullable: false),
                        GroupOfParameterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParamClassID)
                .ForeignKey("dbo.GroupOfParameters", t => t.GroupOfParameterID, cascadeDelete: true)
                .ForeignKey("dbo.ParamRates", t => t.ParamRateID, cascadeDelete: true)
                .Index(t => t.ParamRateID)
                .Index(t => t.GroupOfParameterID);
            
            CreateTable(
                "dbo.GroupOfParameters",
                c => new
                    {
                        GroupOfParameterID = c.Int(nullable: false, identity: true),
                        NameParameter = c.String(),
                    })
                .PrimaryKey(t => t.GroupOfParameterID);
            
            CreateTable(
                "dbo.ParamLists",
                c => new
                    {
                        ParamListID = c.Int(nullable: false, identity: true),
                        RateTID = c.Int(nullable: false),
                        ParamClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParamListID)
                .ForeignKey("dbo.ParamClasses", t => t.ParamClassID, cascadeDelete: true)
                .ForeignKey("dbo.RateTs", t => t.RateTID, cascadeDelete: true)
                .Index(t => t.RateTID)
                .Index(t => t.ParamClassID);
            
            CreateTable(
                "dbo.RateTs",
                c => new
                    {
                        RateTID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RateTID);
            
            CreateTable(
                "dbo.ParamRates",
                c => new
                    {
                        ParamRateID = c.Int(nullable: false, identity: true),
                        ParamRateName = c.String(),
                    })
                .PrimaryKey(t => t.ParamRateID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        Buy = c.Boolean(nullable: false),
                        EditItems = c.Boolean(nullable: false),
                        Library = c.Boolean(nullable: false),
                        Autentification = c.Boolean(nullable: false),
                        Content = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.StatusLists",
                c => new
                    {
                        StatusListID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        LoadID = c.Int(nullable: false),
                        StatusTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatusListID)
                .ForeignKey("dbo.Loads", t => t.LoadID, cascadeDelete: true)
                .ForeignKey("dbo.StatusTypes", t => t.StatusTypeID, cascadeDelete: true)
                .Index(t => t.LoadID)
                .Index(t => t.StatusTypeID);
            
            CreateTable(
                "dbo.StatusTypes",
                c => new
                    {
                        StatusTypeID = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.StatusTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusLists", "StatusTypeID", "dbo.StatusTypes");
            DropForeignKey("dbo.StatusLists", "LoadID", "dbo.Loads");
            DropForeignKey("dbo.Clients", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.LoadLists", "LoadID", "dbo.Loads");
            DropForeignKey("dbo.ParamClasses", "ParamRateID", "dbo.ParamRates");
            DropForeignKey("dbo.ParamLists", "RateTID", "dbo.RateTs");
            DropForeignKey("dbo.Items", "RateTID", "dbo.RateTs");
            DropForeignKey("dbo.ParamLists", "ParamClassID", "dbo.ParamClasses");
            DropForeignKey("dbo.Parameters", "ParamClassID", "dbo.ParamClasses");
            DropForeignKey("dbo.ParamClasses", "GroupOfParameterID", "dbo.GroupOfParameters");
            DropForeignKey("dbo.Parameters", "ItemsID", "dbo.Items");
            DropForeignKey("dbo.LoadLists", "ItemsID", "dbo.Items");
            DropForeignKey("dbo.Loads", "DiscountID", "dbo.Discounts");
            DropForeignKey("dbo.Loads", "ClientID", "dbo.Clients");
            DropIndex("dbo.StatusLists", new[] { "StatusTypeID" });
            DropIndex("dbo.StatusLists", new[] { "LoadID" });
            DropIndex("dbo.ParamLists", new[] { "ParamClassID" });
            DropIndex("dbo.ParamLists", new[] { "RateTID" });
            DropIndex("dbo.ParamClasses", new[] { "GroupOfParameterID" });
            DropIndex("dbo.ParamClasses", new[] { "ParamRateID" });
            DropIndex("dbo.Parameters", new[] { "ParamClassID" });
            DropIndex("dbo.Parameters", new[] { "ItemsID" });
            DropIndex("dbo.Items", new[] { "RateTID" });
            DropIndex("dbo.LoadLists", new[] { "LoadID" });
            DropIndex("dbo.LoadLists", new[] { "ItemsID" });
            DropIndex("dbo.Loads", new[] { "DiscountID" });
            DropIndex("dbo.Loads", new[] { "ClientID" });
            DropIndex("dbo.Clients", new[] { "RoleID" });
            DropTable("dbo.StatusTypes");
            DropTable("dbo.StatusLists");
            DropTable("dbo.Roles");
            DropTable("dbo.ParamRates");
            DropTable("dbo.RateTs");
            DropTable("dbo.ParamLists");
            DropTable("dbo.GroupOfParameters");
            DropTable("dbo.ParamClasses");
            DropTable("dbo.Parameters");
            DropTable("dbo.Items");
            DropTable("dbo.LoadLists");
            DropTable("dbo.Discounts");
            DropTable("dbo.Loads");
            DropTable("dbo.Clients");
        }
    }
}
