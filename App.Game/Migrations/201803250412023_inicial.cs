namespace App.Game.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.emprestimo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Game_id = c.Int(nullable: false),
                        Data_emprestimo = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Data_devolucao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Data_devolvido = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PessoaSolicitada_Id = c.Int(),
                        PessoaSolicitante_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.game", t => t.Game_id, cascadeDelete: true)
                .ForeignKey("dbo.pessoa", t => t.PessoaSolicitada_Id)
                .ForeignKey("dbo.pessoa", t => t.PessoaSolicitante_Id)
                .Index(t => t.Game_id)
                .Index(t => t.PessoaSolicitada_Id)
                .Index(t => t.PessoaSolicitante_Id);
            
            CreateTable(
                "dbo.game",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.pessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserID = c.String(maxLength: 128),
                        Nome = c.String(nullable: false, maxLength: 50),
                        sobrenome = c.String(nullable: false, maxLength: 50),
                        Idade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
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
                "dbo.pessoa_game",
                c => new
                    {
                        pessoa_pessoa_id = c.Int(nullable: false),
                        game_game_id = c.Int(nullable: false),
                        Emprestado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.pessoa_pessoa_id, t.game_game_id })
                .ForeignKey("dbo.game", t => t.game_game_id, cascadeDelete: true)
                .ForeignKey("dbo.pessoa", t => t.pessoa_pessoa_id, cascadeDelete: true)
                .Index(t => t.pessoa_pessoa_id)
                .Index(t => t.game_game_id);
            
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
                "dbo.PessoaModelPessoaModels",
                c => new
                    {
                        PessoaModel_Id = c.Int(nullable: false),
                        PessoaModel_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PessoaModel_Id, t.PessoaModel_Id1 })
                .ForeignKey("dbo.pessoa", t => t.PessoaModel_Id)
                .ForeignKey("dbo.pessoa", t => t.PessoaModel_Id1)
                .Index(t => t.PessoaModel_Id)
                .Index(t => t.PessoaModel_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.pessoa_game", "pessoa_pessoa_id", "dbo.pessoa");
            DropForeignKey("dbo.pessoa_game", "game_game_id", "dbo.game");
            DropForeignKey("dbo.emprestimo", "PessoaSolicitante_Id", "dbo.pessoa");
            DropForeignKey("dbo.emprestimo", "PessoaSolicitada_Id", "dbo.pessoa");
            DropForeignKey("dbo.PessoaModelPessoaModels", "PessoaModel_Id1", "dbo.pessoa");
            DropForeignKey("dbo.PessoaModelPessoaModels", "PessoaModel_Id", "dbo.pessoa");
            DropForeignKey("dbo.pessoa", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.emprestimo", "Game_id", "dbo.game");
            DropIndex("dbo.PessoaModelPessoaModels", new[] { "PessoaModel_Id1" });
            DropIndex("dbo.PessoaModelPessoaModels", new[] { "PessoaModel_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.pessoa_game", new[] { "game_game_id" });
            DropIndex("dbo.pessoa_game", new[] { "pessoa_pessoa_id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.pessoa", new[] { "ApplicationUserID" });
            DropIndex("dbo.emprestimo", new[] { "PessoaSolicitante_Id" });
            DropIndex("dbo.emprestimo", new[] { "PessoaSolicitada_Id" });
            DropIndex("dbo.emprestimo", new[] { "Game_id" });
            DropTable("dbo.PessoaModelPessoaModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.pessoa_game");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.pessoa");
            DropTable("dbo.game");
            DropTable("dbo.emprestimo");
        }
    }
}
