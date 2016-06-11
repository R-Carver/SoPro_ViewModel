namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kleinesErModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abteilungs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bezeichnung = c.String(),
                        DispatcherId = c.String(nullable: false, maxLength: 128),
                        Stv_DispatcherId = c.String(nullable: false, maxLength: 128),
                        Mandant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mandants", t => t.Mandant_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DispatcherId)
                .ForeignKey("dbo.AspNetUsers", t => t.Stv_DispatcherId)
                .Index(t => t.DispatcherId)
                .Index(t => t.Stv_DispatcherId)
                .Index(t => t.Mandant_Id);
            
            CreateTable(
                "dbo.AbteilungToUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AbteilungsId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Abteilung_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Abteilungs", t => t.Abteilung_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Abteilung_Id);
            
            CreateTable(
                "dbo.Vertragsartens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bezeichnung = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vertragsstatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bezeichnung = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vertragstypens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bezeichnung = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MandantToUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MandantId = c.Int(nullable: false),
                        UserId = c.String(),
                        Koordinator_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Koordinator_Id)
                .ForeignKey("dbo.Mandants", t => t.MandantId, cascadeDelete: true)
                .Index(t => t.MandantId)
                .Index(t => t.Koordinator_Id);
            
            CreateTable(
                "dbo.Mandants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bezeichnung = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Contracts", "IntNr", c => c.Int(nullable: false));
            AddColumn("dbo.Contracts", "ExNr", c => c.Int(nullable: false));
            AddColumn("dbo.Contracts", "Vertragsbeginn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contracts", "Vertragsende", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contracts", "AbteilungsId", c => c.Int(nullable: false));
            AddColumn("dbo.Contracts", "UeAbteilungsId", c => c.Int(nullable: false));
            AddColumn("dbo.Contracts", "KoordinatorId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Contracts", "Art_Id", c => c.Int());
            AddColumn("dbo.Contracts", "Status_Id", c => c.Int());
            AddColumn("dbo.Contracts", "Typ_Id", c => c.Int());
            CreateIndex("dbo.Contracts", "AbteilungsId");
            CreateIndex("dbo.Contracts", "UeAbteilungsId");
            CreateIndex("dbo.Contracts", "KoordinatorId");
            CreateIndex("dbo.Contracts", "Art_Id");
            CreateIndex("dbo.Contracts", "Status_Id");
            CreateIndex("dbo.Contracts", "Typ_Id");
            AddForeignKey("dbo.Contracts", "AbteilungsId", "dbo.Abteilungs", "Id");
            AddForeignKey("dbo.Contracts", "Art_Id", "dbo.Vertragsartens", "Id");
            AddForeignKey("dbo.Contracts", "KoordinatorId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Contracts", "Status_Id", "dbo.Vertragsstatus", "Id");
            AddForeignKey("dbo.Contracts", "Typ_Id", "dbo.Vertragstypens", "Id");
            AddForeignKey("dbo.Contracts", "UeAbteilungsId", "dbo.Abteilungs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Abteilungs", "Stv_DispatcherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Abteilungs", "DispatcherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MandantToUsers", "MandantId", "dbo.Mandants");
            DropForeignKey("dbo.Abteilungs", "Mandant_Id", "dbo.Mandants");
            DropForeignKey("dbo.MandantToUsers", "Koordinator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "UeAbteilungsId", "dbo.Abteilungs");
            DropForeignKey("dbo.Contracts", "Typ_Id", "dbo.Vertragstypens");
            DropForeignKey("dbo.Contracts", "Status_Id", "dbo.Vertragsstatus");
            DropForeignKey("dbo.Contracts", "KoordinatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "Art_Id", "dbo.Vertragsartens");
            DropForeignKey("dbo.Contracts", "AbteilungsId", "dbo.Abteilungs");
            DropForeignKey("dbo.AbteilungToUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AbteilungToUsers", "Abteilung_Id", "dbo.Abteilungs");
            DropIndex("dbo.MandantToUsers", new[] { "Koordinator_Id" });
            DropIndex("dbo.MandantToUsers", new[] { "MandantId" });
            DropIndex("dbo.Contracts", new[] { "Typ_Id" });
            DropIndex("dbo.Contracts", new[] { "Status_Id" });
            DropIndex("dbo.Contracts", new[] { "Art_Id" });
            DropIndex("dbo.Contracts", new[] { "KoordinatorId" });
            DropIndex("dbo.Contracts", new[] { "UeAbteilungsId" });
            DropIndex("dbo.Contracts", new[] { "AbteilungsId" });
            DropIndex("dbo.AbteilungToUsers", new[] { "Abteilung_Id" });
            DropIndex("dbo.AbteilungToUsers", new[] { "UserId" });
            DropIndex("dbo.Abteilungs", new[] { "Mandant_Id" });
            DropIndex("dbo.Abteilungs", new[] { "Stv_DispatcherId" });
            DropIndex("dbo.Abteilungs", new[] { "DispatcherId" });
            DropColumn("dbo.Contracts", "Typ_Id");
            DropColumn("dbo.Contracts", "Status_Id");
            DropColumn("dbo.Contracts", "Art_Id");
            DropColumn("dbo.Contracts", "KoordinatorId");
            DropColumn("dbo.Contracts", "UeAbteilungsId");
            DropColumn("dbo.Contracts", "AbteilungsId");
            DropColumn("dbo.Contracts", "Vertragsende");
            DropColumn("dbo.Contracts", "Vertragsbeginn");
            DropColumn("dbo.Contracts", "ExNr");
            DropColumn("dbo.Contracts", "IntNr");
            DropTable("dbo.Mandants");
            DropTable("dbo.MandantToUsers");
            DropTable("dbo.Vertragstypens");
            DropTable("dbo.Vertragsstatus");
            DropTable("dbo.Vertragsartens");
            DropTable("dbo.AbteilungToUsers");
            DropTable("dbo.Abteilungs");
        }
    }
}
