namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VersionPresaentation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyAppointment = c.DateTime(nullable: false),
                        Contract_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.Contract_Id)
                .Index(t => t.Contract_Id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IntContractNum = c.String(),
                        ExtContractNum = c.Int(),
                        ContractBegin = c.DateTime(),
                        ContractEnd = c.DateTime(),
                        Description = c.String(nullable: false, maxLength: 100),
                        OwnerId = c.String(nullable: false, maxLength: 128),
                        DispatcherId = c.String(maxLength: 128),
                        CoordinatorId = c.String(maxLength: 128),
                        DepartmentId = c.Int(),
                        UDepartmentId = c.Int(),
                        ContractKindId = c.Int(),
                        ContractTypeId = c.Int(),
                        ContractStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractsKinds", t => t.ContractKindId)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .ForeignKey("dbo.ContractsStatus", t => t.ContractStatusId)
                .ForeignKey("dbo.ContractsTypes", t => t.ContractTypeId)
                .ForeignKey("dbo.AspNetUsers", t => t.CoordinatorId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.AspNetUsers", t => t.DispatcherId)
                .ForeignKey("dbo.Departments", t => t.UDepartmentId)
                .Index(t => t.OwnerId)
                .Index(t => t.DispatcherId)
                .Index(t => t.CoordinatorId)
                .Index(t => t.DepartmentId)
                .Index(t => t.UDepartmentId)
                .Index(t => t.ContractKindId)
                .Index(t => t.ContractTypeId)
                .Index(t => t.ContractStatusId);
            
            CreateTable(
                "dbo.ContractFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileType = c.String(),
                        FileUrl = c.String(),
                        Contract_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.Contract_Id)
                .Index(t => t.Contract_Id);
            
            CreateTable(
                "dbo.ContractsKinds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        DispatcherId = c.String(maxLength: 128),
                        StvDispatcherId = c.String(maxLength: 128),
                        Mandant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DispatcherId)
                .ForeignKey("dbo.Mandants", t => t.Mandant_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.StvDispatcherId)
                .Index(t => t.DispatcherId)
                .Index(t => t.StvDispatcherId)
                .Index(t => t.Mandant_Id);
            
            CreateTable(
                "dbo.DU_Relation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Mandants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MandantName = c.String(),
                        BuchungsKreis = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MU_Relation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MandantID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mandants", t => t.MandantID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.MandantID)
                .Index(t => t.UserID);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ContractsStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractsTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Contracts", "UDepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Contracts", "DispatcherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Contracts", "CoordinatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "ContractTypeId", "dbo.ContractsTypes");
            DropForeignKey("dbo.Contracts", "ContractStatusId", "dbo.ContractsStatus");
            DropForeignKey("dbo.Contracts", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Departments", "StvDispatcherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MU_Relation", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MU_Relation", "MandantID", "dbo.Mandants");
            DropForeignKey("dbo.Departments", "Mandant_Id", "dbo.Mandants");
            DropForeignKey("dbo.DU_Relation", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.DU_Relation", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "DispatcherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "ContractKindId", "dbo.ContractsKinds");
            DropForeignKey("dbo.ContractFiles", "Contract_Id", "dbo.Contracts");
            DropForeignKey("dbo.Appointments", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.MU_Relation", new[] { "UserID" });
            DropIndex("dbo.MU_Relation", new[] { "MandantID" });
            DropIndex("dbo.DU_Relation", new[] { "DepartmentID" });
            DropIndex("dbo.DU_Relation", new[] { "UserID" });
            DropIndex("dbo.Departments", new[] { "Mandant_Id" });
            DropIndex("dbo.Departments", new[] { "StvDispatcherId" });
            DropIndex("dbo.Departments", new[] { "DispatcherId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ContractFiles", new[] { "Contract_Id" });
            DropIndex("dbo.Contracts", new[] { "ContractStatusId" });
            DropIndex("dbo.Contracts", new[] { "ContractTypeId" });
            DropIndex("dbo.Contracts", new[] { "ContractKindId" });
            DropIndex("dbo.Contracts", new[] { "UDepartmentId" });
            DropIndex("dbo.Contracts", new[] { "DepartmentId" });
            DropIndex("dbo.Contracts", new[] { "CoordinatorId" });
            DropIndex("dbo.Contracts", new[] { "DispatcherId" });
            DropIndex("dbo.Contracts", new[] { "OwnerId" });
            DropIndex("dbo.Appointments", new[] { "Contract_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ContractsTypes");
            DropTable("dbo.ContractsStatus");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.MU_Relation");
            DropTable("dbo.Mandants");
            DropTable("dbo.DU_Relation");
            DropTable("dbo.Departments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ContractsKinds");
            DropTable("dbo.ContractFiles");
            DropTable("dbo.Contracts");
            DropTable("dbo.Appointments");
        }
    }
}
