using Microsoft.EntityFrameworkCore;
using seed;

namespace GestionCommerciale.Models
{
    public partial class MyDbContext : DbContext
    {

        public MyDbContext()
        {
            // dotnet tool install -g dotnet-aspnet-codegenerator
            // dotnet tool update -g dotnet-aspnet-codegenerator
            // dotnet aspnet-codegenerator --project . controller -name HelloController -m Author -dc WebAPIDataContext
            // dotnet tool install --global dotnet-ef --version 3.0.0
            // scafolding to db
            // dotnet ef migrations add secondMG
            // dotnet ef database update
            // dotnet ef migrations remove
            // dotnet ef database update LastGoodMigration
            // dotnet ef migrations script
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Fourniture> Fournitures { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<Fournisseur> Fournisseurs { get; set; }
        public virtual DbSet<SousCategorie> SousCategories { get; set; }
        public virtual DbSet<Commande> Commandes { get; set; }
        public virtual DbSet<DetailCmd> DetailCmds { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Achat> Achats { get; set; }
        public virtual DbSet<Devis> Deviss { get; set; }
        public virtual DbSet<DevisArticle> DevisAcrticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code).IsRequired();
                // entity.HasIndex(e => e.Code).IsUnique();
                entity.Property(e => e.TitreFr).IsRequired();
                entity.Property(e => e.TitreAr).IsRequired();
                entity.Property(e => e.EmplacementMagasin);
                entity.Property(e => e.EmplacementDepot);
                entity.Property(e => e.DateDernierAchat).IsRequired();
                entity.Property(e => e.PrixUnitaire);
                entity.Property(e => e.QteStock).IsRequired();
                entity.Property(e => e.StockMin);
                entity.Property(e => e.Image);
                entity.Property(e => e.Unite);
                entity.Property(e => e.Constructeur);
                entity.Property(e => e.BestSell);
                entity.Property(e => e.Description);
                entity.Property(e => e.IdSousCategorie);

                entity.HasOne(d => d.SousCategorie).WithMany(p => p.Articles).HasForeignKey(d => d.IdSousCategorie);
                entity.HasMany(d => d.Fournitures).WithOne(p => p.Article).HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(d => d.DetailCmds).WithOne(p => p.Article).HasForeignKey(d => d.IdArticle);
            });

            modelBuilder.Entity<User>(entity =>
          {
              entity.HasKey(e => e.Id);
              entity.Property(e => e.Id).ValueGeneratedOnAdd();

              entity.Property(e => e.NomComplete).IsRequired();
              entity.Property(e => e.Email).IsRequired();
              entity.Property(e => e.Password).IsRequired();
              entity.Property(e => e.Tel);
              entity.Property(e => e.IdRole).IsRequired();

              entity.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.IdRole);
          });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nom).IsRequired();

                entity.HasMany(d => d.Users).WithOne(p => p.Role).HasForeignKey(d => d.IdRole);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nom).IsRequired();
                entity.Property(e => e.Prenom);
                entity.Property(e => e.Tel);
                entity.Property(e => e.Email);
                entity.Property(e => e.Adresse);
                entity.Property(e => e.Ice);

                entity.HasMany(d => d.Commandes).WithOne(p => p.Client).HasForeignKey(d => d.IdClient);
            });

            modelBuilder.Entity<Commande>(entity =>
           {
               entity.HasKey(e => e.Id);
               entity.Property(e => e.Id).ValueGeneratedOnAdd();

               entity.Property(e => e.NomClient).IsRequired();
               entity.Property(e => e.NumCheque);
               entity.Property(e => e.Credit);
               entity.Property(e => e.ModePayement);
               entity.Property(e => e.Avance);
               entity.Property(e => e.Total);
               entity.Property(e => e.Date).IsRequired();
               entity.Property(e => e.Time);
               entity.Property(e => e.IdClient);

               entity.HasMany(d => d.DetailCmds).WithOne(p => p.Commande).HasForeignKey(d => d.IdCommande);
               entity.HasOne(d => d.Client).WithMany(p => p.Commandes).HasForeignKey(d => d.IdClient);
           });

            modelBuilder.Entity<DetailCmd>(entity =>
           {
               entity.HasKey(e => new { e.IdArticle, e.IdCommande });

               entity.Property(e => e.PrixVente).IsRequired();
               entity.Property(e => e.Total);
               entity.Property(e => e.QtePrise);
               entity.Property(e => e.Remise);

               entity.HasOne(d => d.Commande).WithMany(p => p.DetailCmds).HasForeignKey(d => d.IdCommande);
           });

            modelBuilder.Entity<Fournisseur>(entity =>
           {
               entity.HasKey(e => e.Id);
               entity.Property(e => e.Id).ValueGeneratedOnAdd();

               entity.Property(e => e.Nom).IsRequired();
               entity.Property(e => e.Prenom).IsRequired();
               entity.Property(e => e.Societe);
               entity.Property(e => e.Telephone).IsRequired();

               entity.HasMany(d => d.Fournitures).WithOne(p => p.Fournisseur).HasForeignKey(d => d.IdFournisseur);
           });

           modelBuilder.Entity<Achat>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdFournisseur).IsRequired();
                entity.Property(e => e.Montant).IsRequired();
                entity.Property(e => e.ModePayement).IsRequired();
                entity.Property(e => e.NumCheque);
                entity.Property(e => e.Credit);
                entity.Property(e => e.Avance);
                entity.Property(e => e.Date);

                entity.HasOne(d => d.Fournisseur).WithMany(p => p.Achats).HasForeignKey(d => d.IdFournisseur);
                entity.HasMany(d => d.Fournitures).WithOne(p => p.Achat).HasForeignKey(d => d.IdAchat);
            });

             modelBuilder.Entity<Devis>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Client);
                entity.Property(e => e.Date);
                entity.Property(e => e.Montant);
            });

             modelBuilder.Entity<DevisArticle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Qte);
                entity.Property(e => e.Pu);
                entity.Property(e => e.Total);
                entity.Property(e => e.Remise);

                entity.HasOne(d => d.Article).WithMany(p => p.DevisAcrticles).HasForeignKey(d => d.IdArticle);
                entity.HasOne(d => d.Devis).WithMany(p => p.DevisActicles).HasForeignKey(d => d.IdDevis)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Fourniture>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdFournisseur).IsRequired();
                entity.Property(e => e.IdArticle).IsRequired();
                entity.Property(e => e.Qte);
                entity.Property(e => e.PrixAchat);
                entity.Property(e => e.DateAchat);

                entity.HasOne(d => d.Article).WithMany(p => p.Fournitures).HasForeignKey(d => d.IdArticle);
                entity.HasOne(d => d.Fournisseur).WithMany(p => p.Fournitures).HasForeignKey(d => d.IdFournisseur)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Libelle).IsRequired();
                entity.HasMany(d => d.SousCategories).WithOne(p => p.Categorie).HasForeignKey(d => d.IdCategorie);
            });

            modelBuilder.Entity<SousCategorie>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Libelle).IsRequired();
                entity.Property(e => e.IdCategorie).IsRequired();

                entity.HasOne(d => d.Categorie).WithMany(p => p.SousCategories).HasForeignKey(d => d.IdCategorie);
                entity.HasMany(d => d.Articles).WithOne(p => p.SousCategorie).HasForeignKey(d => d.IdSousCategorie);
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder
                .Roles()
                .Users()
                .Categories()
                .SousCategories()
                .GameOfData()
                // .Fournisseurs()
                // .Clients()
                ;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
