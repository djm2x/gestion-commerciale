using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using GestionCommerciale.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// dotnet ef dbcontext scaffold 
// "data source=DESKTOP-3550K4L\HARMONY;database=rfid;user id=sa; password=123" 
// Microsoft.EntityFrameworkCore.SqlServer 
// -o Model 
// -c "RfidContext"

// dotnet add package Bogus
namespace seed
{
    public static class DataSeeding
    {
        public static int i = 100;
        public static string lang = "fr";

        public static ModelBuilder Roles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role[]{
                new Role {Id = 1, Nom = "Commercial"},
                new Role {Id = 2, Nom = "Manager"},
                new Role {Id = 3, Nom = "Administrateur"},
            });

            return modelBuilder;
        }

        public static ModelBuilder Users(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User[]{
                new User {Id = 1, NomComplete = "admin", Email = "admin@angular.io", Password = "123", Tel = "", IdRole = 3},
            });

            //  modelBuilder.

            return modelBuilder;
        }

        public static List<Client> Clients(int count)
        {
            int id = 1;
            var l = new List<Client>(new Client[]{
                new Client
                {
                    Id = id++,
                    Nom = "Client caisse 1",
                    Tel = "00",
                    Ice = "00"
                },
            });

            var faker = new Faker<Client>(DataSeeding.lang)
                .CustomInstantiator(f => new Client { Id = id++ })
                .RuleFor(o => o.Nom, f => f.Name.FirstName())
                .RuleFor(o => o.Tel, f => f.Phone.PhoneNumber("(+212)6 ## ##-##-##"))
                .RuleFor(o => o.Ice, f => "")
                ;

            l.AddRange(faker.Generate(count));

            return l;
        }

        public static List<Commande> Commandes(int count)
        {
            int id = 1;
            var list = new[] { "éspece", "chèque", "crédit" };
            var faker = new Faker<Commande>(DataSeeding.lang)
                .CustomInstantiator(f => new Commande { Id = id++ })
                .RuleFor(o => o.IdClient, f => f.Random.Number(1, 3))
                .RuleFor(o => o.Total, f => 0)
                .RuleFor(o => o.ModePayement, f => f.PickRandom(list))
                .RuleFor(o => o.NumCheque, f => "")
                .RuleFor(o => o.NomClient, f => "")
                .RuleFor(o => o.Credit, (f, u) => 0)
                .RuleFor(o => o.Date, (f, u) => f.Date.Past())
                .RuleFor(o => o.Time, (f, u) => "00:00")
                .RuleFor(o => o.Avance, (f, u) => 0)
                ;

            return faker.Generate(count);
        }

        public static List<DetailCmd> DetailCmds(int count)
        {
            // int id = 1;
            // int idCmd = 1;
            // int cmd = 3;
            // int artInPannier = count / cmd;
            // int stockRandom = 1;
            // var list = new[] { "éspece", "chèque", "crédit" };
            var faker = new Faker<DetailCmd>(DataSeeding.lang)
                // .CustomInstantiator(f => new DetailCmd { Id = id++ })
                .RuleFor(o => o.IdArticle, f => f.Random.Number(1, 100))
                .RuleFor(o => o.IdCommande, (f, o) => f.Random.Number(1, 20))
                .RuleFor(o => o.QtePrise, f => f.Random.Number(1, 100))
                .RuleFor(o => o.PrixVente, f => 0)
                .RuleFor(o => o.Remise, f => 0)
                .RuleFor(o => o.Total, (f, o) => 0)
                ;

            var list = faker.Generate(count)
                            .GroupBy(e => new { e.IdArticle, e.IdCommande })
                            .Where(d => d.Count() == 1)
                            .Select(d => d.First())
                            .ToList()
                ;
            // Console.WriteLine("-----------------------------------------------------------------");
            //         Console.WriteLine(JsonConvert.SerializeObject(list));
            //         Console.WriteLine("---------------------------------------------------------------- - ");

            return list;
        }

        public static ModelBuilder Categories(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorie>().HasData(new Categorie[]{
                new Categorie {Id = 1, Libelle = "plombie"},
                new Categorie {Id = 2, Libelle = "electricite"},
                new Categorie {Id = 3, Libelle = "quincaillerie"},
                new Categorie {Id = 4, Libelle = "peinture"},
                new Categorie {Id = 5, Libelle = "divers"},
                new Categorie {Id = 6, Libelle = "outillage"},
            });

            return modelBuilder;
        }

        public static ModelBuilder SousCategories(this ModelBuilder modelBuilder)
        {
            int id = 1;
            var lists = new List<SousCategorie>();

            lists.Add(new SousCategorie
            {
                Id = id++,
                Libelle = "Generale",
                IdCategorie = 1,
            });

            var faker = new Faker<SousCategorie>(DataSeeding.lang)
                .CustomInstantiator(f => new SousCategorie { Id = id++ })
                .RuleFor(o => o.Libelle, f => f.Name.FirstName())
                .RuleFor(o => o.IdCategorie, f => f.Random.Number(1, 6))
                ;

            lists.AddRange(faker.Generate(DataSeeding.i - 60));

            modelBuilder.Entity<SousCategorie>().HasData(lists);

            return modelBuilder;
        }

        public static List<Article> Articles(int count)
        {
            int id = 1;
            var list = new[] { "Constructeur1", "Constructeur2", "Constructeur3" };
            var un = new[] { "U", "L", "Kg" };
            var faker = new Faker<Article>(DataSeeding.lang)
                .CustomInstantiator(f => new Article { Id = id++ })
                .RuleFor(o => o.TitreFr, f => $"titre long pour un produit avec la nature de super hasardouze-{id - 1}")
                .RuleFor(o => o.TitreAr, f => $"عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-{id - 1}")
                .RuleFor(o => o.Code, f => $"Code-{id - 1}")
                .RuleFor(o => o.DateDernierAchat, (f, u) => f.Date.Past())
                .RuleFor(o => o.PrixUnitaire, f => f.Random.Number(10, 1000))
                .RuleFor(o => o.QteStock, f => 0)
                .RuleFor(o => o.StockMin, f => 10)
                .RuleFor(o => o.Image, f => "")
                .RuleFor(o => o.Unite, f => f.PickRandom(un))
                .RuleFor(o => o.Constructeur, f => f.PickRandom(list))
                .RuleFor(o => o.BestSell, f => id > 80 && id % 2 == 0 ? true : false)
                .RuleFor(o => o.Description, f => "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!")
                .RuleFor(o => o.IdSousCategorie, (f) => f.Random.Number(1, 10))
                ;


            return faker.Generate(count);
        }

        public static List<Fournisseur> Fournisseurs(int count)
        {
            int id = 1;
            var list = new[] { "Fournisseur1", "Fournisseur2", "Fournisseur3" };
            var faker = new Faker<Fournisseur>(DataSeeding.lang)
                .CustomInstantiator(f => new Fournisseur { Id = id++ })
                .RuleFor(o => o.Nom, f => f.Name.FirstName())
                .RuleFor(o => o.Prenom, f => f.Name.LastName())
                .RuleFor(o => o.Societe, (f, u) => f.Company.CompanyName())
                .RuleFor(o => o.Telephone, f => f.Phone.PhoneNumber("(+212)6 ## ##-##-##"))
                ;

            return faker.Generate(count);
        }

        public static List<Fourniture> Fournitures(int count)
        {
            int id = 1;
            var list = new[] { "éspece", "chèque", "crédit" };
            var faker = new Faker<Fourniture>(DataSeeding.lang)
                .CustomInstantiator(f => new Fourniture { Id = id++ })
                .RuleFor(o => o.IdArticle, f => f.Random.Number(1, 100))
                .RuleFor(o => o.IdFournisseur, f => f.Random.Number(1, 3))
                .RuleFor(o => o.Qte, f => f.Random.Number(1, 100))
                .RuleFor(o => o.PrixAchat, f => 0)
                .RuleFor(o => o.DateAchat, (f, u) => f.Date.Past())
                .RuleFor(o => o.IdAchat, f => f.Random.Number(1, 10))
                ;

            return faker.Generate(count);
        }

        public static List<Achat> Achats(int count)
        {
            int id = 1;
            var list = new[] { "éspece", "chèque", "crédit" };
            var faker = new Faker<Achat>(DataSeeding.lang)
                .CustomInstantiator(f => new Achat { Id = id++ })
                .RuleFor(o => o.IdFournisseur, f => f.Random.Number(1, 3))
                .RuleFor(o => o.Montant, f => 0)
                .RuleFor(o => o.ModePayement, f => f.PickRandom(list))
                .RuleFor(o => o.NumCheque, f => "")
                .RuleFor(o => o.Credit, (f, u) => 0)
                .RuleFor(o => o.Date, (f, u) => f.Date.Past())
                .RuleFor(o => o.Avance, (f, u) => 0)
                ;

            return faker.Generate(count);
        }

        public static ModelBuilder GameOfData(this ModelBuilder modelBuilder)
        {

            var articles = DataSeeding.Articles(100);
            var fournisseurs = DataSeeding.Fournisseurs(3);
            var fournitures = DataSeeding.Fournitures(100);
            var achats = DataSeeding.Achats(10);
            //
            var commandes = DataSeeding.Commandes(20);
            var cliens = DataSeeding.Clients(20);
            var detailCmd = DataSeeding.DetailCmds(900);

            fournitures.ForEach(e =>
            {
                articles.ForEach(a =>
                {
                    if (a.Id == e.IdArticle)
                    {
                        e.PrixAchat = a.PrixUnitaire;
                        a.QteStock += e.Qte;
                    }
                });
            });

            achats.ForEach(e =>
            {
                fournitures.ForEach(f =>
                {
                    if (f.IdAchat == e.Id)
                    {
                        e.Montant = f.PrixAchat * f.Qte;
                    }
                });
            });

            detailCmd.ForEach(e =>
            {
                articles.ForEach(a =>
                {
                    if (a.Id == e.IdArticle)
                    {
                        e.PrixVente = a.PrixUnitaire;
                        a.QteStock -= e.QtePrise;
                        e.Total = e.PrixVente * e.QtePrise;
                    }
                });
            });

            commandes.ForEach(e =>
            {
                detailCmd.ForEach(f =>
                {
                    if (f.IdCommande == e.Id)
                    {
                        e.Total = f.PrixVente * f.QtePrise;
                    }
                });
            });



            modelBuilder.Entity<Article>().HasData(articles);
            modelBuilder.Entity<Fournisseur>().HasData(fournisseurs);
            modelBuilder.Entity<Achat>().HasData(achats);
            modelBuilder.Entity<Fourniture>().HasData(fournitures);
            modelBuilder.Entity<Client>().HasData(cliens);
            modelBuilder.Entity<Commande>().HasData(commandes);
            modelBuilder.Entity<DetailCmd>().HasData(detailCmd);

            return modelBuilder;
        }


    }
}