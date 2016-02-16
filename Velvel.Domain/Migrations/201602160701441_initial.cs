namespace Velvel.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accessories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        ProjectId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Changes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Approval = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Description = c.String(),
                        MeasurementUnitId = c.Int(nullable: false),
                        Quanitty = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        CommentGroupId = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        ProjectId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommentGroups", t => t.CommentGroupId, cascadeDelete: true)
                .ForeignKey("dbo.MeasurementUnits", t => t.MeasurementUnitId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.MeasurementUnitId)
                .Index(t => t.RoomId)
                .Index(t => t.CommentGroupId);
            
            CreateTable(
                "dbo.CommentGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MeasurementUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(),
                        ProjectId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentGroupId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                        Read = c.DateTime(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommentGroups", t => t.CommentGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CommentGroupId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Defects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Approval = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Description = c.String(),
                        MeasurementUnitId = c.Int(nullable: false),
                        Quanitty = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        CommentGroupId = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        ProjectId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommentGroups", t => t.CommentGroupId, cascadeDelete: true)
                .ForeignKey("dbo.MeasurementUnits", t => t.MeasurementUnitId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.MeasurementUnitId)
                .Index(t => t.RoomId)
                .Index(t => t.CommentGroupId);
            
            CreateTable(
                "dbo.Floorings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModelId = c.Int(nullable: false),
                        GroutId = c.Int(nullable: false),
                        Date = c.DateTime(),
                        Approval = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Description = c.String(),
                        MeasurementUnitId = c.Int(nullable: false),
                        Quanitty = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        CommentGroupId = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        ProjectId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommentGroups", t => t.CommentGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Grouts", t => t.GroutId, cascadeDelete: true)
                .ForeignKey("dbo.MeasurementUnits", t => t.MeasurementUnitId, cascadeDelete: true)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.ModelId)
                .Index(t => t.GroutId)
                .Index(t => t.StatusId)
                .Index(t => t.MeasurementUnitId)
                .Index(t => t.RoomId)
                .Index(t => t.CommentGroupId);
            
            CreateTable(
                "dbo.Grouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        ProjectId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        ProjectId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plumbings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessoryId = c.Int(nullable: false),
                        Date = c.DateTime(),
                        Approval = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Description = c.String(),
                        MeasurementUnitId = c.Int(nullable: false),
                        Quanitty = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        CommentGroupId = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        ProjectId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accessories", t => t.AccessoryId, cascadeDelete: true)
                .ForeignKey("dbo.CommentGroups", t => t.CommentGroupId, cascadeDelete: true)
                .ForeignKey("dbo.MeasurementUnits", t => t.MeasurementUnitId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.AccessoryId)
                .Index(t => t.StatusId)
                .Index(t => t.MeasurementUnitId)
                .Index(t => t.RoomId)
                .Index(t => t.CommentGroupId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Phone = c.String(),
                        Phone2 = c.String(),
                        ManagerId = c.Int(),
                        CustomerId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .Index(t => t.ManagerId)
                .Index(t => t.CustomerId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Managers", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Managers", "Id", "dbo.Users");
            DropForeignKey("dbo.Customers", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Customers", "Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Plumbings", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Plumbings", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Plumbings", "MeasurementUnitId", "dbo.MeasurementUnits");
            DropForeignKey("dbo.Plumbings", "CommentGroupId", "dbo.CommentGroups");
            DropForeignKey("dbo.Plumbings", "AccessoryId", "dbo.Accessories");
            DropForeignKey("dbo.Floorings", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Floorings", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Floorings", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Floorings", "MeasurementUnitId", "dbo.MeasurementUnits");
            DropForeignKey("dbo.Floorings", "GroutId", "dbo.Grouts");
            DropForeignKey("dbo.Floorings", "CommentGroupId", "dbo.CommentGroups");
            DropForeignKey("dbo.Defects", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Defects", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Defects", "MeasurementUnitId", "dbo.MeasurementUnits");
            DropForeignKey("dbo.Defects", "CommentGroupId", "dbo.CommentGroups");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Projects", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "CommentGroupId", "dbo.CommentGroups");
            DropForeignKey("dbo.Changes", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Changes", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Changes", "MeasurementUnitId", "dbo.MeasurementUnits");
            DropForeignKey("dbo.Changes", "CommentGroupId", "dbo.CommentGroups");
            DropIndex("dbo.Managers", new[] { "Project_Id" });
            DropIndex("dbo.Managers", new[] { "Id" });
            DropIndex("dbo.Customers", new[] { "Project_Id" });
            DropIndex("dbo.Customers", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CustomerId" });
            DropIndex("dbo.AspNetUsers", new[] { "ManagerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Plumbings", new[] { "CommentGroupId" });
            DropIndex("dbo.Plumbings", new[] { "RoomId" });
            DropIndex("dbo.Plumbings", new[] { "MeasurementUnitId" });
            DropIndex("dbo.Plumbings", new[] { "StatusId" });
            DropIndex("dbo.Plumbings", new[] { "AccessoryId" });
            DropIndex("dbo.Floorings", new[] { "CommentGroupId" });
            DropIndex("dbo.Floorings", new[] { "RoomId" });
            DropIndex("dbo.Floorings", new[] { "MeasurementUnitId" });
            DropIndex("dbo.Floorings", new[] { "StatusId" });
            DropIndex("dbo.Floorings", new[] { "GroutId" });
            DropIndex("dbo.Floorings", new[] { "ModelId" });
            DropIndex("dbo.Defects", new[] { "CommentGroupId" });
            DropIndex("dbo.Defects", new[] { "RoomId" });
            DropIndex("dbo.Defects", new[] { "MeasurementUnitId" });
            DropIndex("dbo.Defects", new[] { "StatusId" });
            DropIndex("dbo.Projects", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "CommentGroupId" });
            DropIndex("dbo.Changes", new[] { "CommentGroupId" });
            DropIndex("dbo.Changes", new[] { "RoomId" });
            DropIndex("dbo.Changes", new[] { "MeasurementUnitId" });
            DropIndex("dbo.Changes", new[] { "StatusId" });
            DropTable("dbo.Managers");
            DropTable("dbo.Customers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Plumbings");
            DropTable("dbo.Models");
            DropTable("dbo.Grouts");
            DropTable("dbo.Floorings");
            DropTable("dbo.Defects");
            DropTable("dbo.Projects");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Status");
            DropTable("dbo.Rooms");
            DropTable("dbo.MeasurementUnits");
            DropTable("dbo.CommentGroups");
            DropTable("dbo.Changes");
            DropTable("dbo.Accessories");
        }
    }
}
