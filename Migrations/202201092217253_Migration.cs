namespace AspApp_VenteVetements.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commandes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        utilisateurId = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        AdresseLivraison = c.String(),
                        Vetement_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Utilisateurs", t => t.utilisateurId, cascadeDelete: true)
                .ForeignKey("dbo.Vetements", t => t.Vetement_id)
                .Index(t => t.utilisateurId)
                .Index(t => t.Vetement_id);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        statutId = c.Int(nullable: false),
                        nom = c.String(nullable: false, maxLength: 150),
                        adresseMail = c.String(nullable: false, maxLength: 100),
                        motDePasse = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Statuts", t => t.statutId, cascadeDelete: true)
                .Index(t => t.statutId);
            
            CreateTable(
                "dbo.Statuts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nom = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Vetements",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nom = c.String(),
                        marque = c.String(),
                        prix = c.Single(nullable: false),
                        couleur = c.String(),
                        taille = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.VetementCommandes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        commandeId = c.Int(nullable: false),
                        vetementId = c.Int(nullable: false),
                        quantite = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Vetements", t => t.vetementId, cascadeDelete: true)
                .ForeignKey("dbo.Commandes", t => t.commandeId, cascadeDelete: true)
                .Index(t => t.commandeId)
                .Index(t => t.vetementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VetementCommandes", "commandeId", "dbo.Commandes");
            DropForeignKey("dbo.Commandes", "Vetement_id", "dbo.Vetements");
            DropForeignKey("dbo.VetementCommandes", "vetementId", "dbo.Vetements");
            DropForeignKey("dbo.Utilisateurs", "statutId", "dbo.Statuts");
            DropForeignKey("dbo.Commandes", "utilisateurId", "dbo.Utilisateurs");
            DropIndex("dbo.VetementCommandes", new[] { "vetementId" });
            DropIndex("dbo.VetementCommandes", new[] { "commandeId" });
            DropIndex("dbo.Utilisateurs", new[] { "statutId" });
            DropIndex("dbo.Commandes", new[] { "Vetement_id" });
            DropIndex("dbo.Commandes", new[] { "utilisateurId" });
            DropTable("dbo.VetementCommandes");
            DropTable("dbo.Vetements");
            DropTable("dbo.Statuts");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Commandes");
        }
    }
}
