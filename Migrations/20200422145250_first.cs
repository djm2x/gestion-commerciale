using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionCommerciale.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Libelle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Adresse = table.Column<string>(nullable: true),
                    Ice = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deviss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Client = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Montant = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deviss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    Societe = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SousCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Libelle = table.Column<string>(nullable: false),
                    IdCategorie = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SousCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SousCategories_Categories_IdCategorie",
                        column: x => x.IdCategorie,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModePayement = table.Column<string>(nullable: true),
                    NumCheque = table.Column<string>(nullable: true),
                    Credit = table.Column<decimal>(nullable: false),
                    Avance = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    NomClient = table.Column<string>(nullable: false),
                    IdClient = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commandes_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdFournisseur = table.Column<int>(nullable: false),
                    Montant = table.Column<decimal>(nullable: false),
                    ModePayement = table.Column<string>(nullable: false),
                    NumCheque = table.Column<string>(nullable: true),
                    Credit = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Avance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achats_Fournisseurs_IdFournisseur",
                        column: x => x.IdFournisseur,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomComplete = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Tel = table.Column<string>(nullable: true),
                    IdRole = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: false),
                    TitreFr = table.Column<string>(nullable: false),
                    TitreAr = table.Column<string>(nullable: false),
                    EmplacementMagasin = table.Column<string>(nullable: true),
                    EmplacementDepot = table.Column<string>(nullable: true),
                    DateDernierAchat = table.Column<DateTime>(nullable: false),
                    PrixUnitaire = table.Column<decimal>(nullable: false),
                    QteStock = table.Column<decimal>(nullable: false),
                    StockMin = table.Column<decimal>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Unite = table.Column<string>(nullable: true),
                    Constructeur = table.Column<string>(nullable: true),
                    BestSell = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdSousCategorie = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_SousCategories_IdSousCategorie",
                        column: x => x.IdSousCategorie,
                        principalTable: "SousCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailCmds",
                columns: table => new
                {
                    IdArticle = table.Column<int>(nullable: false),
                    IdCommande = table.Column<int>(nullable: false),
                    PrixVente = table.Column<decimal>(nullable: false),
                    QtePrise = table.Column<decimal>(nullable: false),
                    Remise = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailCmds", x => new { x.IdArticle, x.IdCommande });
                    table.ForeignKey(
                        name: "FK_DetailCmds_Articles_IdArticle",
                        column: x => x.IdArticle,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailCmds_Commandes_IdCommande",
                        column: x => x.IdCommande,
                        principalTable: "Commandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevisAcrticles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qte = table.Column<decimal>(nullable: false),
                    Pu = table.Column<decimal>(nullable: false),
                    Remise = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    IdArticle = table.Column<int>(nullable: false),
                    IdDevis = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevisAcrticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevisAcrticles_Articles_IdArticle",
                        column: x => x.IdArticle,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevisAcrticles_Deviss_IdDevis",
                        column: x => x.IdDevis,
                        principalTable: "Deviss",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fournitures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdArticle = table.Column<int>(nullable: false),
                    IdFournisseur = table.Column<int>(nullable: false),
                    Qte = table.Column<decimal>(nullable: false),
                    PrixAchat = table.Column<decimal>(nullable: false),
                    DateAchat = table.Column<DateTime>(nullable: false),
                    IdAchat = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournitures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fournitures_Achats_IdAchat",
                        column: x => x.IdAchat,
                        principalTable: "Achats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fournitures_Articles_IdArticle",
                        column: x => x.IdArticle,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fournitures_Fournisseurs_IdFournisseur",
                        column: x => x.IdFournisseur,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Libelle" },
                values: new object[] { 1, "plombie" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Libelle" },
                values: new object[] { 2, "electricite" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Libelle" },
                values: new object[] { 3, "quincaillerie" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Libelle" },
                values: new object[] { 4, "peinture" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Libelle" },
                values: new object[] { 5, "divers" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Libelle" },
                values: new object[] { 6, "outillage" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 21, null, null, "", "Paul", null, "(+212)6 08 46-09-61" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 20, null, null, "", "Benjamin", null, "(+212)6 40 50-87-79" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 19, null, null, "", "Noémie", null, "(+212)6 71 10-64-81" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 18, null, null, "", "Romain", null, "(+212)6 34 59-57-57" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 17, null, null, "", "Anaïs", null, "(+212)6 45 98-92-90" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 16, null, null, "", "Charlotte", null, "(+212)6 61 29-90-00" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 15, null, null, "", "Lucas", null, "(+212)6 73 99-99-53" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 14, null, null, "", "Océane", null, "(+212)6 95 18-83-36" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 13, null, null, "", "Marie", null, "(+212)6 00 01-77-76" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 12, null, null, "", "Benjamin", null, "(+212)6 34 29-45-42" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 11, null, null, "", "Adrien", null, "(+212)6 67 57-85-08" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 10, null, null, "", "Maeva", null, "(+212)6 06 53-36-17" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 9, null, null, "", "Quentin", null, "(+212)6 07 50-80-62" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 8, null, null, "", "Léa", null, "(+212)6 94 59-08-37" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 7, null, null, "", "Lucie", null, "(+212)6 18 14-96-72" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 6, null, null, "", "Emilie", null, "(+212)6 42 77-37-34" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 5, null, null, "", "Charlotte", null, "(+212)6 14 35-85-89" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 4, null, null, "", "Charlotte", null, "(+212)6 38 16-17-83" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 3, null, null, "", "Mael", null, "(+212)6 33 56-96-60" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 2, null, null, "", "Thomas", null, "(+212)6 17 78-11-97" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Adresse", "Email", "Ice", "Nom", "Prenom", "Tel" },
                values: new object[] { 1, null, null, "00", "Client caisse 1", null, "00" });

            migrationBuilder.InsertData(
                table: "Fournisseurs",
                columns: new[] { "Id", "Nom", "Prenom", "Societe", "Telephone" },
                values: new object[] { 1, "Manon", "Gaillard", "Perez, Dufour and Brunet", "(+212)6 39 56-70-15" });

            migrationBuilder.InsertData(
                table: "Fournisseurs",
                columns: new[] { "Id", "Nom", "Prenom", "Societe", "Telephone" },
                values: new object[] { 2, "Marie", "Berger", "Cousin, Blanc and Marie", "(+212)6 77 52-54-17" });

            migrationBuilder.InsertData(
                table: "Fournisseurs",
                columns: new[] { "Id", "Nom", "Prenom", "Societe", "Telephone" },
                values: new object[] { 3, "Justine", "Rousseau", "Brun EI", "(+212)6 04 22-62-06" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nom" },
                values: new object[] { 1, "Commercial" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nom" },
                values: new object[] { 2, "Manager" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nom" },
                values: new object[] { 3, "Administrateur" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 6, 0m, 0m, new DateTime(2019, 8, 5, 14, 50, 23, 12, DateTimeKind.Local).AddTicks(8037), 3, "chèque", 87m, "" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 5, 0m, 0m, new DateTime(2019, 10, 14, 11, 6, 42, 730, DateTimeKind.Local).AddTicks(8083), 1, "chèque", 21840m, "" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 9, 0m, 0m, new DateTime(2020, 1, 19, 21, 17, 29, 964, DateTimeKind.Local).AddTicks(4917), 1, "crédit", 837m, "" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 10, 0m, 0m, new DateTime(2019, 12, 2, 16, 5, 7, 517, DateTimeKind.Local).AddTicks(6854), 1, "éspece", 22436m, "" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 4, 0m, 0m, new DateTime(2019, 12, 5, 11, 24, 38, 559, DateTimeKind.Local).AddTicks(6907), 2, "crédit", 73069m, "" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 2, 0m, 0m, new DateTime(2019, 7, 6, 7, 36, 46, 550, DateTimeKind.Local).AddTicks(8372), 1, "chèque", 64519m, "" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 8, 0m, 0m, new DateTime(2020, 2, 6, 5, 5, 15, 198, DateTimeKind.Local).AddTicks(9170), 2, "chèque", 19224m, "" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 1, 0m, 0m, new DateTime(2019, 8, 22, 9, 7, 55, 567, DateTimeKind.Local).AddTicks(2095), 3, "crédit", 820m, "" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 3, 0m, 0m, new DateTime(2019, 8, 25, 6, 26, 48, 935, DateTimeKind.Local).AddTicks(4186), 3, "éspece", 29614m, "" });

            migrationBuilder.InsertData(
                table: "Achats",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdFournisseur", "ModePayement", "Montant", "NumCheque" },
                values: new object[] { 7, 0m, 0m, new DateTime(2020, 4, 13, 3, 17, 52, 910, DateTimeKind.Local).AddTicks(5786), 2, "crédit", 28735m, "" });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 4, 0m, 0m, new DateTime(2020, 3, 10, 2, 17, 12, 656, DateTimeKind.Local).AddTicks(2547), 2, "éspece", "", "", "00:00", 2772m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 5, 0m, 0m, new DateTime(2019, 11, 12, 9, 18, 27, 537, DateTimeKind.Local).AddTicks(6032), 1, "crédit", "", "", "00:00", 15255m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 9, 0m, 0m, new DateTime(2019, 9, 8, 13, 43, 57, 467, DateTimeKind.Local).AddTicks(5889), 1, "éspece", "", "", "00:00", 10727m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 12, 0m, 0m, new DateTime(2020, 4, 11, 17, 10, 5, 634, DateTimeKind.Local).AddTicks(9145), 1, "éspece", "", "", "00:00", 2316m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 13, 0m, 0m, new DateTime(2019, 5, 9, 14, 45, 20, 203, DateTimeKind.Local).AddTicks(2860), 1, "chèque", "", "", "00:00", 36945m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 15, 0m, 0m, new DateTime(2019, 5, 16, 5, 38, 5, 394, DateTimeKind.Local).AddTicks(56), 1, "éspece", "", "", "00:00", 11362m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 19, 0m, 0m, new DateTime(2019, 8, 10, 10, 29, 41, 636, DateTimeKind.Local).AddTicks(354), 1, "chèque", "", "", "00:00", 21643m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 1, 0m, 0m, new DateTime(2020, 1, 13, 2, 8, 11, 493, DateTimeKind.Local).AddTicks(1849), 2, "crédit", "", "", "00:00", 1185m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 3, 0m, 0m, new DateTime(2019, 10, 11, 0, 34, 40, 199, DateTimeKind.Local).AddTicks(8598), 2, "crédit", "", "", "00:00", 19513m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 6, 0m, 0m, new DateTime(2019, 10, 7, 0, 12, 46, 282, DateTimeKind.Local).AddTicks(9533), 2, "crédit", "", "", "00:00", 9353m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 11, 0m, 0m, new DateTime(2019, 5, 7, 1, 32, 12, 43, DateTimeKind.Local).AddTicks(1147), 2, "crédit", "", "", "00:00", 56800m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 10, 0m, 0m, new DateTime(2019, 7, 19, 2, 13, 33, 68, DateTimeKind.Local).AddTicks(3611), 2, "crédit", "", "", "00:00", 40120m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 20, 0m, 0m, new DateTime(2019, 11, 29, 0, 28, 20, 650, DateTimeKind.Local).AddTicks(6767), 2, "chèque", "", "", "00:00", 4620m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 2, 0m, 0m, new DateTime(2019, 8, 21, 15, 10, 57, 947, DateTimeKind.Local).AddTicks(3997), 3, "chèque", "", "", "00:00", 24365m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 8, 0m, 0m, new DateTime(2019, 7, 22, 21, 39, 32, 893, DateTimeKind.Local).AddTicks(5107), 3, "éspece", "", "", "00:00", 15620m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 14, 0m, 0m, new DateTime(2019, 8, 18, 19, 48, 20, 129, DateTimeKind.Local).AddTicks(3507), 3, "éspece", "", "", "00:00", 390m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 16, 0m, 0m, new DateTime(2019, 10, 21, 4, 48, 13, 560, DateTimeKind.Local).AddTicks(3046), 3, "éspece", "", "", "00:00", 13650m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 17, 0m, 0m, new DateTime(2019, 4, 26, 23, 42, 31, 950, DateTimeKind.Local).AddTicks(7918), 3, "crédit", "", "", "00:00", 59866m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 18, 0m, 0m, new DateTime(2020, 2, 28, 4, 59, 46, 746, DateTimeKind.Local).AddTicks(2492), 3, "chèque", "", "", "00:00", 19880m });

            migrationBuilder.InsertData(
                table: "Commandes",
                columns: new[] { "Id", "Avance", "Credit", "Date", "IdClient", "ModePayement", "NomClient", "NumCheque", "Time", "Total" },
                values: new object[] { 7, 0m, 0m, new DateTime(2019, 12, 31, 13, 13, 9, 938, DateTimeKind.Local).AddTicks(6794), 2, "éspece", "", "", "00:00", 5723m });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 18, 6, "Julie" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 40, 6, "Océane" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 35, 6, "Enzo" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 30, 6, "Noa" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 29, 6, "Noa" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 1, 1, "Generale" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 4, 6, "Rayan" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 41, 5, "Zoe" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 37, 2, "Lilou" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 34, 2, "Romain" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 32, 2, "Mattéo" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 26, 2, "Lina" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 22, 2, "Ambre" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 17, 2, "Mattéo" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 9, 3, "Evan" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 16, 2, "Romane" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 12, 2, "Quentin" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 8, 2, "Louna" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 5, 2, "Manon" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 39, 1, "Julie" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 27, 1, "Ethan" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 21, 1, "Quentin" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 15, 2, "Marie" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 2, 6, "Gabriel" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 10, 3, "Charlotte" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 25, 3, "Paul" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 33, 5, "Jade" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 28, 5, "Emilie" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 19, 5, "Clément" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 14, 5, "Noa" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 7, 5, "Chloé" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 36, 4, "Maxime" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 13, 3, "Lola" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 31, 4, "Paul" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 23, 4, "Lisa" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 20, 4, "Juliette" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 11, 4, "Chloé" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 6, 4, "Mélissa" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 3, 4, "Rayan" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 38, 3, "Thomas" });

            migrationBuilder.InsertData(
                table: "SousCategories",
                columns: new[] { "Id", "IdCategorie", "Libelle" },
                values: new object[] { 24, 4, "Laura" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IdRole", "NomComplete", "Password", "Tel" },
                values: new object[] { 1, "admin@angular.io", 3, "admin", "123", "" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 7, false, "Code-7", "Constructeur2", new DateTime(2020, 1, 14, 8, 54, 36, 846, DateTimeKind.Local).AddTicks(1872), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 1, "", 386m, -259m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-7", "titre long pour un produit avec la nature de super hasardouze-7", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 79, false, "Code-79", "Constructeur1", new DateTime(2020, 1, 9, 3, 38, 24, 32, DateTimeKind.Local).AddTicks(314), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 6, "", 710m, -8m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-79", "titre long pour un produit avec la nature de super hasardouze-79", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 59, false, "Code-59", "Constructeur1", new DateTime(2019, 8, 9, 4, 10, 34, 47, DateTimeKind.Local).AddTicks(4291), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 6, "", 59m, -141m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-59", "titre long pour un produit avec la nature de super hasardouze-59", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 52, false, "Code-52", "Constructeur3", new DateTime(2020, 4, 17, 5, 33, 43, 180, DateTimeKind.Local).AddTicks(7511), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 6, "", 563m, -74m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-52", "titre long pour un produit avec la nature de super hasardouze-52", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 51, false, "Code-51", "Constructeur1", new DateTime(2019, 8, 10, 5, 4, 25, 937, DateTimeKind.Local).AddTicks(8540), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 6, "", 963m, -197m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-51", "titre long pour un produit avec la nature de super hasardouze-51", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 25, false, "Code-25", "Constructeur3", new DateTime(2020, 1, 20, 18, 32, 52, 266, DateTimeKind.Local).AddTicks(590), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 6, "", 565m, -161m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-25", "titre long pour un produit avec la nature de super hasardouze-25", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 91, true, "Code-91", "Constructeur1", new DateTime(2019, 6, 17, 6, 44, 17, 681, DateTimeKind.Local).AddTicks(9867), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 247m, -353m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-91", "titre long pour un produit avec la nature de super hasardouze-91", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 74, false, "Code-74", "Constructeur1", new DateTime(2019, 5, 21, 9, 30, 32, 217, DateTimeKind.Local).AddTicks(9215), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 915m, -355m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-74", "titre long pour un produit avec la nature de super hasardouze-74", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 68, false, "Code-68", "Constructeur2", new DateTime(2020, 2, 27, 3, 40, 17, 162, DateTimeKind.Local).AddTicks(7757), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 568m, -382m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-68", "titre long pour un produit avec la nature de super hasardouze-68", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 66, false, "Code-66", "Constructeur2", new DateTime(2019, 5, 24, 9, 12, 29, 685, DateTimeKind.Local).AddTicks(8025), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 850m, -183m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-66", "titre long pour un produit avec la nature de super hasardouze-66", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 62, false, "Code-62", "Constructeur2", new DateTime(2019, 10, 19, 7, 54, 44, 386, DateTimeKind.Local).AddTicks(1184), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 634m, -392m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-62", "titre long pour un produit avec la nature de super hasardouze-62", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 50, false, "Code-50", "Constructeur2", new DateTime(2019, 5, 16, 0, 14, 31, 550, DateTimeKind.Local).AddTicks(9189), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 784m, -114m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-50", "titre long pour un produit avec la nature de super hasardouze-50", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 48, false, "Code-48", "Constructeur3", new DateTime(2020, 1, 17, 20, 26, 23, 672, DateTimeKind.Local).AddTicks(1816), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 28m, -260m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-48", "titre long pour un produit avec la nature de super hasardouze-48", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 39, false, "Code-39", "Constructeur1", new DateTime(2020, 2, 19, 7, 6, 35, 777, DateTimeKind.Local).AddTicks(5494), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 926m, -346m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-39", "titre long pour un produit avec la nature de super hasardouze-39", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 30, false, "Code-30", "Constructeur3", new DateTime(2019, 8, 27, 6, 1, 30, 854, DateTimeKind.Local).AddTicks(6561), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 599m, -106m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-30", "titre long pour un produit avec la nature de super hasardouze-30", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 10, false, "Code-10", "Constructeur2", new DateTime(2019, 6, 17, 11, 15, 1, 749, DateTimeKind.Local).AddTicks(9734), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 969m, -186m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-10", "titre long pour un produit avec la nature de super hasardouze-10", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 2, false, "Code-2", "Constructeur2", new DateTime(2020, 1, 21, 21, 58, 39, 399, DateTimeKind.Local).AddTicks(1448), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 3, "", 284m, -382m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-2", "titre long pour un produit avec la nature de super hasardouze-2", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 99, true, "Code-99", "Constructeur1", new DateTime(2020, 3, 24, 15, 39, 46, 652, DateTimeKind.Local).AddTicks(7664), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 472m, -155m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-99", "titre long pour un produit avec la nature de super hasardouze-99", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 98, false, "Code-98", "Constructeur1", new DateTime(2019, 4, 22, 23, 21, 0, 577, DateTimeKind.Local).AddTicks(7864), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 311m, -259m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-98", "titre long pour un produit avec la nature de super hasardouze-98", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 96, false, "Code-96", "Constructeur2", new DateTime(2019, 9, 20, 0, 37, 29, 464, DateTimeKind.Local).AddTicks(4177), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 757m, -72m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-96", "titre long pour un produit avec la nature de super hasardouze-96", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 85, true, "Code-85", "Constructeur1", new DateTime(2019, 6, 8, 15, 14, 15, 615, DateTimeKind.Local).AddTicks(5916), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 771m, -130m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-85", "titre long pour un produit avec la nature de super hasardouze-85", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 83, true, "Code-83", "Constructeur2", new DateTime(2020, 2, 1, 11, 43, 24, 354, DateTimeKind.Local).AddTicks(6478), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 302m, -247m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-83", "titre long pour un produit avec la nature de super hasardouze-83", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 92, false, "Code-92", "Constructeur2", new DateTime(2019, 11, 5, 8, 53, 53, 334, DateTimeKind.Local).AddTicks(5713), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 6, "", 237m, -119m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-92", "titre long pour un produit avec la nature de super hasardouze-92", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 81, true, "Code-81", "Constructeur2", new DateTime(2020, 1, 28, 22, 53, 55, 328, DateTimeKind.Local).AddTicks(1528), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 148m, -300m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-81", "titre long pour un produit avec la nature de super hasardouze-81", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 95, true, "Code-95", "Constructeur3", new DateTime(2019, 6, 2, 1, 28, 37, 919, DateTimeKind.Local).AddTicks(7896), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 6, "", 347m, -330m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-95", "titre long pour un produit avec la nature de super hasardouze-95", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 5, false, "Code-5", "Constructeur1", new DateTime(2019, 8, 27, 11, 18, 34, 416, DateTimeKind.Local).AddTicks(7925), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 245m, -42m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-5", "titre long pour un produit avec la nature de super hasardouze-5", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 75, false, "Code-75", "Constructeur2", new DateTime(2020, 4, 5, 3, 5, 43, 482, DateTimeKind.Local).AddTicks(9637), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 4, "", 763m, -231m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-75", "titre long pour un produit avec la nature de super hasardouze-75", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 72, false, "Code-72", "Constructeur1", new DateTime(2019, 6, 15, 1, 21, 34, 617, DateTimeKind.Local).AddTicks(1820), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 4, "", 868m, -355m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-72", "titre long pour un produit avec la nature de super hasardouze-72", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 61, false, "Code-61", "Constructeur1", new DateTime(2020, 1, 27, 8, 38, 7, 914, DateTimeKind.Local).AddTicks(6214), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 4, "", 961m, -387m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-61", "titre long pour un produit avec la nature de super hasardouze-61", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 58, false, "Code-58", "Constructeur3", new DateTime(2020, 3, 10, 23, 11, 36, 130, DateTimeKind.Local).AddTicks(9937), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 4, "", 26m, -355m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-58", "titre long pour un produit avec la nature de super hasardouze-58", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 20, false, "Code-20", "Constructeur3", new DateTime(2019, 12, 20, 9, 7, 40, 490, DateTimeKind.Local).AddTicks(7973), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 4, "", 171m, -345m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-20", "titre long pour un produit avec la nature de super hasardouze-20", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 9, false, "Code-9", "Constructeur3", new DateTime(2020, 4, 3, 16, 55, 22, 236, DateTimeKind.Local).AddTicks(787), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 4, "", 67m, -317m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-9", "titre long pour un produit avec la nature de super hasardouze-9", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 6, false, "Code-6", "Constructeur1", new DateTime(2019, 5, 5, 0, 52, 12, 481, DateTimeKind.Local).AddTicks(971), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 4, "", 26m, -370m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-6", "titre long pour un produit avec la nature de super hasardouze-6", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 80, false, "Code-80", "Constructeur3", new DateTime(2019, 4, 24, 14, 21, 31, 63, DateTimeKind.Local).AddTicks(6321), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 2, "", 886m, -306m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-80", "titre long pour un produit avec la nature de super hasardouze-80", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 60, false, "Code-60", "Constructeur2", new DateTime(2019, 12, 15, 14, 46, 31, 843, DateTimeKind.Local).AddTicks(2969), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 2, "", 974m, -158m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-60", "titre long pour un produit avec la nature de super hasardouze-60", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 31, false, "Code-31", "Constructeur2", new DateTime(2019, 8, 26, 15, 45, 42, 253, DateTimeKind.Local).AddTicks(4946), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 2, "", 120m, -220m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-31", "titre long pour un produit avec la nature de super hasardouze-31", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 29, false, "Code-29", "Constructeur3", new DateTime(2019, 7, 2, 13, 33, 2, 863, DateTimeKind.Local).AddTicks(7414), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 2, "", 176m, -171m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-29", "titre long pour un produit avec la nature de super hasardouze-29", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 100, false, "Code-100", "Constructeur1", new DateTime(2020, 1, 2, 5, 36, 9, 783, DateTimeKind.Local).AddTicks(3335), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 584m, -284m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-100", "titre long pour un produit avec la nature de super hasardouze-100", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 94, false, "Code-94", "Constructeur1", new DateTime(2020, 3, 29, 17, 22, 50, 652, DateTimeKind.Local).AddTicks(8033), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 462m, -337m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-94", "titre long pour un produit avec la nature de super hasardouze-94", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 89, true, "Code-89", "Constructeur3", new DateTime(2020, 3, 8, 16, 49, 34, 396, DateTimeKind.Local).AddTicks(4991), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 289m, -301m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-89", "titre long pour un produit avec la nature de super hasardouze-89", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 84, false, "Code-84", "Constructeur2", new DateTime(2020, 2, 11, 18, 29, 57, 970, DateTimeKind.Local).AddTicks(1682), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 155m, -402m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-84", "titre long pour un produit avec la nature de super hasardouze-84", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 77, false, "Code-77", "Constructeur2", new DateTime(2019, 11, 10, 11, 54, 31, 259, DateTimeKind.Local).AddTicks(5953), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 473m, -198m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-77", "titre long pour un produit avec la nature de super hasardouze-77", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 76, false, "Code-76", "Constructeur2", new DateTime(2020, 3, 8, 12, 29, 55, 515, DateTimeKind.Local).AddTicks(3407), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 636m, -217m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-76", "titre long pour un produit avec la nature de super hasardouze-76", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 55, false, "Code-55", "Constructeur1", new DateTime(2019, 5, 6, 5, 24, 6, 58, DateTimeKind.Local).AddTicks(2033), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 199m, -374m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-55", "titre long pour un produit avec la nature de super hasardouze-55", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 35, false, "Code-35", "Constructeur1", new DateTime(2019, 6, 28, 3, 55, 45, 324, DateTimeKind.Local).AddTicks(5698), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 470m, 21m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-35", "titre long pour un produit avec la nature de super hasardouze-35", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 21, false, "Code-21", "Constructeur3", new DateTime(2020, 1, 7, 10, 35, 7, 789, DateTimeKind.Local).AddTicks(8010), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 132m, -167m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-21", "titre long pour un produit avec la nature de super hasardouze-21", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 18, false, "Code-18", "Constructeur2", new DateTime(2019, 10, 10, 23, 40, 13, 16, DateTimeKind.Local).AddTicks(9498), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 7, "", 679m, -300m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-18", "titre long pour un produit avec la nature de super hasardouze-18", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 97, true, "Code-97", "Constructeur3", new DateTime(2019, 5, 27, 1, 9, 35, 616, DateTimeKind.Local).AddTicks(5175), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 6, "", 47m, 20m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-97", "titre long pour un produit avec la nature de super hasardouze-97", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 70, false, "Code-70", "Constructeur3", new DateTime(2020, 2, 29, 11, 54, 55, 289, DateTimeKind.Local).AddTicks(9724), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 470m, -248m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-70", "titre long pour un produit avec la nature de super hasardouze-70", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 56, false, "Code-56", "Constructeur2", new DateTime(2019, 12, 2, 19, 59, 36, 245, DateTimeKind.Local).AddTicks(7), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 661m, -378m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-56", "titre long pour un produit avec la nature de super hasardouze-56", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 53, false, "Code-53", "Constructeur2", new DateTime(2019, 8, 27, 20, 52, 18, 35, DateTimeKind.Local).AddTicks(9686), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 712m, -456m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-53", "titre long pour un produit avec la nature de super hasardouze-53", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 63, false, "Code-63", "Constructeur3", new DateTime(2019, 12, 13, 18, 3, 7, 218, DateTimeKind.Local).AddTicks(1598), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 563m, -407m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-63", "titre long pour un produit avec la nature de super hasardouze-63", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 54, false, "Code-54", "Constructeur1", new DateTime(2020, 1, 4, 8, 15, 9, 832, DateTimeKind.Local).AddTicks(2253), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 704m, -317m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-54", "titre long pour un produit avec la nature de super hasardouze-54", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 49, false, "Code-49", "Constructeur1", new DateTime(2020, 3, 31, 3, 57, 14, 598, DateTimeKind.Local).AddTicks(4557), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 443m, -275m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-49", "titre long pour un produit avec la nature de super hasardouze-49", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 47, false, "Code-47", "Constructeur1", new DateTime(2020, 3, 24, 15, 21, 39, 84, DateTimeKind.Local).AddTicks(362), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 36m, -136m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-47", "titre long pour un produit avec la nature de super hasardouze-47", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 46, false, "Code-46", "Constructeur1", new DateTime(2019, 5, 31, 16, 33, 23, 123, DateTimeKind.Local).AddTicks(8556), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 543m, -202m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-46", "titre long pour un produit avec la nature de super hasardouze-46", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 41, false, "Code-41", "Constructeur3", new DateTime(2019, 11, 9, 3, 50, 51, 948, DateTimeKind.Local).AddTicks(4359), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 888m, -99m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-41", "titre long pour un produit avec la nature de super hasardouze-41", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 40, false, "Code-40", "Constructeur3", new DateTime(2020, 2, 16, 23, 17, 31, 676, DateTimeKind.Local).AddTicks(849), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 16m, -77m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-40", "titre long pour un produit avec la nature de super hasardouze-40", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 37, false, "Code-37", "Constructeur1", new DateTime(2020, 2, 28, 10, 18, 33, 885, DateTimeKind.Local).AddTicks(6217), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 796m, -225m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-37", "titre long pour un produit avec la nature de super hasardouze-37", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 27, false, "Code-27", "Constructeur2", new DateTime(2019, 11, 16, 8, 45, 26, 264, DateTimeKind.Local).AddTicks(658), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 739m, -420m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-27", "titre long pour un produit avec la nature de super hasardouze-27", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 24, false, "Code-24", "Constructeur1", new DateTime(2019, 9, 6, 22, 6, 7, 508, DateTimeKind.Local).AddTicks(8298), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 687m, -267m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-24", "titre long pour un produit avec la nature de super hasardouze-24", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 22, false, "Code-22", "Constructeur3", new DateTime(2019, 5, 29, 1, 34, 1, 76, DateTimeKind.Local).AddTicks(1335), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 729m, -306m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-22", "titre long pour un produit avec la nature de super hasardouze-22", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 15, false, "Code-15", "Constructeur1", new DateTime(2019, 9, 29, 22, 39, 53, 196, DateTimeKind.Local).AddTicks(3373), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 495m, -368m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-15", "titre long pour un produit avec la nature de super hasardouze-15", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 4, false, "Code-4", "Constructeur1", new DateTime(2019, 10, 17, 10, 56, 48, 890, DateTimeKind.Local).AddTicks(1257), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 661m, -96m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-4", "titre long pour un produit avec la nature de super hasardouze-4", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 69, false, "Code-69", "Constructeur2", new DateTime(2019, 10, 27, 10, 24, 37, 448, DateTimeKind.Local).AddTicks(9445), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 1, "", 166m, -322m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-69", "titre long pour un produit avec la nature de super hasardouze-69", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 42, false, "Code-42", "Constructeur3", new DateTime(2020, 2, 12, 19, 18, 9, 999, DateTimeKind.Local).AddTicks(8904), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 1, "", 267m, -332m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-42", "titre long pour un produit avec la nature de super hasardouze-42", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 34, false, "Code-34", "Constructeur2", new DateTime(2019, 7, 17, 17, 51, 14, 999, DateTimeKind.Local).AddTicks(7767), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 1, "", 210m, -152m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-34", "titre long pour un produit avec la nature de super hasardouze-34", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 33, false, "Code-33", "Constructeur3", new DateTime(2019, 7, 13, 21, 14, 58, 503, DateTimeKind.Local).AddTicks(4637), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 1, "", 631m, -315m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-33", "titre long pour un produit avec la nature de super hasardouze-33", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 28, false, "Code-28", "Constructeur1", new DateTime(2019, 6, 7, 17, 28, 58, 666, DateTimeKind.Local).AddTicks(5442), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 1, "", 809m, -354m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-28", "titre long pour un produit avec la nature de super hasardouze-28", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 26, false, "Code-26", "Constructeur2", new DateTime(2019, 7, 25, 20, 42, 41, 417, DateTimeKind.Local).AddTicks(5010), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 1, "", 134m, -325m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-26", "titre long pour un produit avec la nature de super hasardouze-26", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 11, false, "Code-11", "Constructeur1", new DateTime(2019, 12, 1, 17, 22, 12, 342, DateTimeKind.Local).AddTicks(4217), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 1, "", 742m, -226m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-11", "titre long pour un produit avec la nature de super hasardouze-11", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 8, false, "Code-8", "Constructeur3", new DateTime(2020, 3, 10, 9, 46, 58, 761, DateTimeKind.Local).AddTicks(7197), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 1, "", 953m, -44m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-8", "titre long pour un produit avec la nature de super hasardouze-8", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 87, true, "Code-87", "Constructeur2", new DateTime(2020, 1, 26, 2, 34, 5, 507, DateTimeKind.Local).AddTicks(4675), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 164m, -371m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-87", "titre long pour un produit avec la nature de super hasardouze-87", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 93, true, "Code-93", "Constructeur3", new DateTime(2019, 7, 4, 4, 39, 47, 19, DateTimeKind.Local).AddTicks(8397), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 5, "", 821m, -394m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-93", "titre long pour un produit avec la nature de super hasardouze-93", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 14, false, "Code-14", "Constructeur2", new DateTime(2019, 5, 18, 9, 21, 35, 405, DateTimeKind.Local).AddTicks(4976), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 562m, -184m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-14", "titre long pour un produit avec la nature de super hasardouze-14", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 16, false, "Code-16", "Constructeur3", new DateTime(2019, 9, 17, 17, 33, 56, 204, DateTimeKind.Local).AddTicks(490), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 860m, -445m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-16", "titre long pour un produit avec la nature de super hasardouze-16", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 45, false, "Code-45", "Constructeur3", new DateTime(2019, 6, 25, 15, 31, 46, 853, DateTimeKind.Local).AddTicks(6957), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 443m, -261m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-45", "titre long pour un produit avec la nature de super hasardouze-45", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 32, false, "Code-32", "Constructeur1", new DateTime(2019, 7, 30, 10, 55, 21, 142, DateTimeKind.Local).AddTicks(3105), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 704m, -305m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-32", "titre long pour un produit avec la nature de super hasardouze-32", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 23, false, "Code-23", "Constructeur1", new DateTime(2019, 10, 22, 10, 50, 36, 983, DateTimeKind.Local).AddTicks(424), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 560m, -25m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-23", "titre long pour un produit avec la nature de super hasardouze-23", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 17, false, "Code-17", "Constructeur3", new DateTime(2019, 4, 23, 11, 3, 34, 934, DateTimeKind.Local).AddTicks(4599), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 613m, -575m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-17", "titre long pour un produit avec la nature de super hasardouze-17", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 13, false, "Code-13", "Constructeur1", new DateTime(2020, 1, 27, 20, 22, 59, 585, DateTimeKind.Local).AddTicks(1131), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 715m, -390m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-13", "titre long pour un produit avec la nature de super hasardouze-13", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 12, false, "Code-12", "Constructeur3", new DateTime(2019, 12, 13, 11, 54, 36, 284, DateTimeKind.Local).AddTicks(6386), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 910m, -24m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-12", "titre long pour un produit avec la nature de super hasardouze-12", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 1, false, "Code-1", "Constructeur2", new DateTime(2019, 9, 17, 10, 41, 14, 910, DateTimeKind.Local).AddTicks(6460), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 10, "", 212m, -253m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-1", "titre long pour un produit avec la nature de super hasardouze-1", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 78, false, "Code-78", "Constructeur2", new DateTime(2020, 3, 1, 12, 6, 40, 63, DateTimeKind.Local).AddTicks(9343), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 9, "", 59m, 14m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-78", "titre long pour un produit avec la nature de super hasardouze-78", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 71, false, "Code-71", "Constructeur2", new DateTime(2019, 8, 29, 22, 54, 19, 449, DateTimeKind.Local).AddTicks(4861), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 9, "", 279m, -237m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-71", "titre long pour un produit avec la nature de super hasardouze-71", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 64, false, "Code-64", "Constructeur3", new DateTime(2019, 9, 12, 1, 5, 11, 368, DateTimeKind.Local).AddTicks(6854), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 9, "", 709m, -90m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-64", "titre long pour un produit avec la nature de super hasardouze-64", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 86, false, "Code-86", "Constructeur1", new DateTime(2019, 12, 19, 21, 53, 42, 886, DateTimeKind.Local).AddTicks(3713), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 4, "", 93m, -168m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-86", "titre long pour un produit avec la nature de super hasardouze-86", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 36, false, "Code-36", "Constructeur3", new DateTime(2020, 3, 22, 9, 38, 52, 915, DateTimeKind.Local).AddTicks(8317), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 9, "", 411m, -42m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-36", "titre long pour un produit avec la nature de super hasardouze-36", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 3, false, "Code-3", "Constructeur2", new DateTime(2019, 12, 31, 7, 24, 42, 19, DateTimeKind.Local).AddTicks(4943), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 9, "", 802m, -206m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-3", "titre long pour un produit avec la nature de super hasardouze-3", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 88, false, "Code-88", "Constructeur3", new DateTime(2019, 12, 31, 11, 27, 37, 826, DateTimeKind.Local).AddTicks(3862), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 311m, -392m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-88", "titre long pour un produit avec la nature de super hasardouze-88", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 82, false, "Code-82", "Constructeur1", new DateTime(2019, 6, 3, 4, 24, 4, 369, DateTimeKind.Local).AddTicks(3023), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 29m, -236m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-82", "titre long pour un produit avec la nature de super hasardouze-82", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 73, false, "Code-73", "Constructeur1", new DateTime(2019, 8, 24, 17, 25, 51, 958, DateTimeKind.Local).AddTicks(9940), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 497m, -217m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-73", "titre long pour un produit avec la nature de super hasardouze-73", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 67, false, "Code-67", "Constructeur1", new DateTime(2019, 11, 23, 2, 32, 5, 277, DateTimeKind.Local).AddTicks(1793), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 442m, -319m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-67", "titre long pour un produit avec la nature de super hasardouze-67", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 65, false, "Code-65", "Constructeur2", new DateTime(2020, 1, 10, 14, 53, 10, 294, DateTimeKind.Local).AddTicks(1817), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 388m, -213m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-65", "titre long pour un produit avec la nature de super hasardouze-65", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 57, false, "Code-57", "Constructeur3", new DateTime(2020, 3, 12, 20, 36, 31, 677, DateTimeKind.Local).AddTicks(8122), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 51m, -488m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-57", "titre long pour un produit avec la nature de super hasardouze-57", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 44, false, "Code-44", "Constructeur2", new DateTime(2019, 4, 29, 12, 22, 44, 118, DateTimeKind.Local).AddTicks(4691), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 484m, -215m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-44", "titre long pour un produit avec la nature de super hasardouze-44", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 43, false, "Code-43", "Constructeur2", new DateTime(2019, 12, 25, 14, 6, 16, 56, DateTimeKind.Local).AddTicks(2892), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 244m, -36m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-43", "titre long pour un produit avec la nature de super hasardouze-43", "U" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 38, false, "Code-38", "Constructeur2", new DateTime(2019, 12, 27, 1, 1, 18, 549, DateTimeKind.Local).AddTicks(4030), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 8, "", 941m, -212m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-38", "titre long pour un produit avec la nature de super hasardouze-38", "Kg" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 19, false, "Code-19", "Constructeur3", new DateTime(2020, 2, 5, 22, 55, 44, 674, DateTimeKind.Local).AddTicks(2341), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 9, "", 822m, -300m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-19", "titre long pour un produit avec la nature de super hasardouze-19", "L" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "BestSell", "Code", "Constructeur", "DateDernierAchat", "Description", "EmplacementDepot", "EmplacementMagasin", "IdSousCategorie", "Image", "PrixUnitaire", "QteStock", "StockMin", "TitreAr", "TitreFr", "Unite" },
                values: new object[] { 90, false, "Code-90", "Constructeur1", new DateTime(2019, 4, 26, 7, 36, 28, 496, DateTimeKind.Local).AddTicks(3043), "Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit rerum, voluptates debitis, blanditiis quibusdam consequatur maxime repudiandae placeat a molestias dolores molestiae incidunt, nobis tempore doloribus expedita maiores. Atque, magnam!", null, null, 4, "", 440m, -318m, 10m, "عنوان طويل لمنتج ذي طبيعة فائقة الخطورة-90", "titre long pour un produit avec la nature de super hasardouze-90", "U" });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 7, 11, 386m, 57m, 0m, 22002m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 68, 5, 568m, 4m, 0m, 2272m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 68, 20, 568m, 42m, 0m, 23856m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 68, 16, 568m, 25m, 0m, 14200m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 68, 11, 568m, 100m, 0m, 56800m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 74, 20, 915m, 3m, 0m, 2745m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 74, 6, 915m, 31m, 0m, 28365m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 74, 12, 915m, 86m, 0m, 78690m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 74, 18, 915m, 71m, 0m, 64965m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 74, 13, 915m, 59m, 0m, 53985m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 74, 1, 915m, 96m, 0m, 87840m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 74, 11, 915m, 2m, 0m, 1830m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 74, 8, 915m, 7m, 0m, 6405m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 11, 247m, 2m, 0m, 494m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 18, 247m, 82m, 0m, 20254m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 14, 247m, 7m, 0m, 1729m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 68, 4, 568m, 50m, 0m, 28400m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 68, 9, 568m, 90m, 0m, 51120m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 68, 19, 568m, 52m, 0m, 29536m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 68, 14, 568m, 19m, 0m, 10792m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 48, 6, 28m, 20m, 0m, 560m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 48, 4, 28m, 99m, 0m, 2772m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 50, 6, 784m, 24m, 0m, 18816m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 50, 16, 784m, 90m, 0m, 70560m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 62, 11, 634m, 37m, 0m, 23458m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 62, 19, 634m, 98m, 0m, 62132m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 62, 10, 634m, 55m, 0m, 34870m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 7, 247m, 39m, 0m, 9633m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 62, 7, 634m, 98m, 0m, 62132m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 62, 14, 634m, 8m, 0m, 5072m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 66, 20, 850m, 7m, 0m, 5950m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 66, 15, 850m, 46m, 0m, 39100m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 66, 17, 850m, 46m, 0m, 39100m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 66, 14, 850m, 18m, 0m, 15300m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 66, 18, 850m, 34m, 0m, 28900m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 66, 10, 850m, 88m, 0m, 74800m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 62, 17, 634m, 96m, 0m, 60864m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 10, 247m, 82m, 0m, 20254m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 8, 247m, 9m, 0m, 2223m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 2, 247m, 1m, 0m, 247m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 59, 20, 59m, 22m, 0m, 1298m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 59, 7, 59m, 97m, 0m, 5723m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 79, 2, 710m, 34m, 0m, 24140m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 79, 1, 710m, 31m, 0m, 22010m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 92, 20, 237m, 94m, 0m, 22278m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 92, 15, 237m, 87m, 0m, 20619m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 92, 18, 237m, 6m, 0m, 1422m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 59, 11, 59m, 56m, 0m, 3304m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 92, 10, 237m, 51m, 0m, 12087m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 95, 8, 347m, 1m, 0m, 347m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 95, 5, 347m, 48m, 0m, 16656m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 95, 19, 347m, 80m, 0m, 27760m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 95, 12, 347m, 33m, 0m, 11451m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 95, 15, 347m, 83m, 0m, 28801m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 95, 11, 347m, 22m, 0m, 7634m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 95, 9, 347m, 16m, 0m, 5552m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 92, 1, 237m, 5m, 0m, 1185m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 48, 18, 28m, 19m, 0m, 532m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 52, 6, 563m, 81m, 0m, 45603m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 52, 3, 563m, 43m, 0m, 24209m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 20, 247m, 48m, 0m, 11856m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 15, 247m, 46m, 0m, 11362m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 91, 3, 247m, 79m, 0m, 19513m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 25, 3, 565m, 9m, 0m, 5085m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 25, 1, 565m, 18m, 0m, 10170m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 25, 12, 565m, 23m, 0m, 12995m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 25, 15, 565m, 29m, 0m, 16385m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 52, 12, 563m, 63m, 0m, 35469m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 25, 18, 565m, 83m, 0m, 46895m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 51, 17, 963m, 40m, 0m, 38520m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 51, 19, 963m, 86m, 0m, 82818m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 51, 11, 963m, 41m, 0m, 39483m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 51, 9, 963m, 56m, 0m, 53928m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 51, 12, 963m, 32m, 0m, 30816m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 52, 10, 563m, 63m, 0m, 35469m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 52, 8, 563m, 2m, 0m, 1126m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 25, 5, 565m, 27m, 0m, 15255m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 48, 15, 28m, 30m, 0m, 840m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 48, 7, 28m, 77m, 0m, 2156m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 48, 12, 28m, 59m, 0m, 1652m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 70, 7, 470m, 40m, 0m, 18800m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 81, 9, 148m, 72m, 0m, 10656m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 81, 7, 148m, 79m, 0m, 11692m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 81, 1, 148m, 13m, 0m, 1924m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 81, 8, 148m, 76m, 0m, 11248m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 81, 20, 148m, 54m, 0m, 7992m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 81, 15, 148m, 41m, 0m, 6068m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 90, 13, 440m, 2m, 0m, 880m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 81, 17, 148m, 33m, 0m, 4884m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 83, 14, 302m, 54m, 0m, 16308m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 83, 15, 302m, 56m, 0m, 16912m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 83, 6, 302m, 63m, 0m, 19026m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 83, 9, 302m, 11m, 0m, 3322m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 83, 13, 302m, 16m, 0m, 4832m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 83, 3, 302m, 10m, 0m, 3020m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 85, 13, 771m, 68m, 0m, 52428m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 83, 4, 302m, 37m, 0m, 11174m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 85, 17, 771m, 43m, 0m, 33153m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 70, 20, 470m, 20m, 0m, 9400m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 70, 15, 470m, 88m, 0m, 41360m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 45, 1, 443m, 68m, 0m, 30124m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 53, 18, 712m, 89m, 0m, 63368m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 53, 10, 712m, 69m, 0m, 49128m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 53, 4, 712m, 41m, 0m, 29192m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 53, 11, 712m, 87m, 0m, 61944m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 53, 14, 712m, 75m, 0m, 53400m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 53, 13, 712m, 71m, 0m, 50552m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 70, 4, 470m, 15m, 0m, 7050m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 53, 8, 712m, 51m, 0m, 36312m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 56, 7, 661m, 76m, 0m, 50236m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 56, 18, 661m, 59m, 0m, 38999m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 56, 5, 661m, 1m, 0m, 661m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 56, 16, 661m, 38m, 0m, 25118m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 56, 1, 661m, 97m, 0m, 64117m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 56, 10, 661m, 51m, 0m, 33711m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 56, 13, 661m, 85m, 0m, 56185m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 56, 17, 661m, 20m, 0m, 13220m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 95, 18, 347m, 47m, 0m, 16309m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 85, 18, 771m, 15m, 0m, 11565m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 96, 4, 757m, 36m, 0m, 27252m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 10, 11, 969m, 55m, 0m, 53295m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 10, 16, 969m, 45m, 0m, 43605m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 10, 2, 969m, 86m, 0m, 83334m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 30, 13, 599m, 19m, 0m, 11381m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 30, 8, 599m, 81m, 0m, 48519m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 30, 4, 599m, 38m, 0m, 22762m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 30, 2, 599m, 59m, 0m, 35341m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 2, 8, 284m, 55m, 0m, 15620m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 39, 12, 926m, 49m, 0m, 45374m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 39, 5, 926m, 47m, 0m, 43522m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 39, 17, 926m, 36m, 0m, 33336m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 39, 11, 926m, 54m, 0m, 50004m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 39, 7, 926m, 15m, 0m, 13890m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 39, 14, 926m, 84m, 0m, 77784m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 39, 10, 926m, 28m, 0m, 25928m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 48, 5, 28m, 100m, 0m, 2800m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 39, 20, 926m, 76m, 0m, 70376m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 85, 16, 771m, 4m, 0m, 3084m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 2, 18, 284m, 70m, 0m, 19880m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 2, 12, 284m, 37m, 0m, 10508m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 96, 9, 757m, 60m, 0m, 45420m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 98, 6, 311m, 43m, 0m, 13373m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 98, 17, 311m, 68m, 0m, 21148m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 98, 12, 311m, 24m, 0m, 7464m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 98, 5, 311m, 24m, 0m, 7464m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 98, 16, 311m, 54m, 0m, 16794m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 98, 15, 311m, 44m, 0m, 13684m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 2, 5, 284m, 55m, 0m, 15620m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 98, 18, 311m, 92m, 0m, 28612m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 99, 13, 472m, 6m, 0m, 2832m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 99, 2, 472m, 19m, 0m, 8968m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 99, 10, 472m, 85m, 0m, 40120m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 2, 4, 284m, 89m, 0m, 25276m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 2, 1, 284m, 89m, 0m, 25276m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 2, 13, 284m, 48m, 0m, 13632m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 2, 15, 284m, 34m, 0m, 9656m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 99, 17, 472m, 52m, 0m, 24544m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 45, 8, 443m, 29m, 0m, 12847m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 97, 11, 47m, 37m, 0m, 1739m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 97, 18, 47m, 53m, 0m, 2491m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 6, 9, 26m, 76m, 0m, 1976m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 6, 20, 26m, 3m, 0m, 78m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 9, 15, 67m, 89m, 0m, 5963m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 9, 9, 67m, 46m, 0m, 3082m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 9, 16, 67m, 94m, 0m, 6298m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 9, 19, 67m, 10m, 0m, 670m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 9, 1, 67m, 78m, 0m, 5226m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 20, 14, 171m, 93m, 0m, 15903m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 20, 19, 171m, 6m, 0m, 1026m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 20, 4, 171m, 90m, 0m, 15390m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 20, 18, 171m, 43m, 0m, 7353m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 20, 17, 171m, 99m, 0m, 16929m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 20, 6, 171m, 14m, 0m, 2394m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 58, 7, 26m, 48m, 0m, 1248m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 58, 8, 26m, 44m, 0m, 1144m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 6, 16, 26m, 63m, 0m, 1638m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 6, 13, 26m, 73m, 0m, 1898m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 6, 12, 26m, 13m, 0m, 338m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 6, 2, 26m, 43m, 0m, 1118m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 31, 16, 120m, 21m, 0m, 2520m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 31, 15, 120m, 44m, 0m, 5280m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 31, 3, 120m, 29m, 0m, 3480m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 31, 18, 120m, 2m, 0m, 240m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 31, 11, 120m, 31m, 0m, 3720m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 31, 2, 120m, 9m, 0m, 1080m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 60, 5, 974m, 68m, 0m, 66232m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 58, 13, 26m, 72m, 0m, 1872m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 60, 2, 974m, 58m, 0m, 56492m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 80, 7, 886m, 27m, 0m, 23922m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 80, 9, 886m, 98m, 0m, 86828m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 80, 2, 886m, 46m, 0m, 40756m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 80, 8, 886m, 10m, 0m, 8860m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 80, 19, 886m, 74m, 0m, 65564m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 80, 5, 886m, 51m, 0m, 45186m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 6, 10, 26m, 99m, 0m, 2574m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 60, 12, 974m, 32m, 0m, 31168m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 58, 12, 26m, 29m, 0m, 754m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 58, 6, 26m, 86m, 0m, 2236m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 58, 2, 26m, 58m, 0m, 1508m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 75, 19, 763m, 59m, 0m, 45017m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 75, 14, 763m, 25m, 0m, 19075m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 75, 16, 763m, 4m, 0m, 3052m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 86, 3, 93m, 14m, 0m, 1302m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 86, 17, 93m, 53m, 0m, 4929m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 86, 13, 93m, 35m, 0m, 3255m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 86, 16, 93m, 97m, 0m, 9021m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 75, 5, 763m, 70m, 0m, 53410m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 86, 5, 93m, 11m, 0m, 1023m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 90, 5, 440m, 88m, 0m, 38720m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 90, 6, 440m, 50m, 0m, 22000m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 90, 15, 440m, 57m, 0m, 25080m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 90, 7, 440m, 47m, 0m, 20680m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 90, 3, 440m, 18m, 0m, 7920m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 90, 16, 440m, 21m, 0m, 9240m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 90, 9, 440m, 90m, 0m, 39600m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 86, 2, 93m, 27m, 0m, 2511m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 31, 6, 120m, 84m, 0m, 10080m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 75, 18, 763m, 22m, 0m, 16786m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 72, 13, 868m, 81m, 0m, 70308m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 58, 15, 26m, 83m, 0m, 2158m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 58, 11, 26m, 66m, 0m, 1716m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 58, 14, 26m, 15m, 0m, 390m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 61, 16, 961m, 21m, 0m, 20181m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 61, 12, 961m, 87m, 0m, 83607m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 61, 1, 961m, 67m, 0m, 64387m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 61, 7, 961m, 55m, 0m, 52855m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 75, 15, 763m, 51m, 0m, 38913m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 61, 13, 961m, 95m, 0m, 91295m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 61, 18, 961m, 29m, 0m, 27869m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 72, 12, 868m, 76m, 0m, 65968m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 72, 19, 868m, 53m, 0m, 46004m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 72, 3, 868m, 8m, 0m, 6944m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 72, 20, 868m, 36m, 0m, 31248m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 72, 8, 868m, 74m, 0m, 64232m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 72, 9, 868m, 27m, 0m, 23436m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 61, 9, 961m, 33m, 0m, 31713m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 29, 18, 176m, 52m, 0m, 9152m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 29, 2, 176m, 39m, 0m, 6864m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 29, 14, 176m, 68m, 0m, 11968m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 35, 10, 470m, 8m, 0m, 3760m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 55, 14, 199m, 28m, 0m, 5572m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 55, 15, 199m, 71m, 0m, 14129m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 55, 18, 199m, 22m, 0m, 4378m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 55, 4, 199m, 36m, 0m, 7164m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 55, 8, 199m, 64m, 0m, 12736m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 55, 19, 199m, 83m, 0m, 16517m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 35, 17, 470m, 36m, 0m, 16920m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 55, 9, 199m, 13m, 0m, 2587m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 55, 6, 199m, 47m, 0m, 9353m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 76, 5, 636m, 31m, 0m, 19716m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 76, 13, 636m, 66m, 0m, 41976m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 76, 16, 636m, 26m, 0m, 16536m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 76, 15, 636m, 13m, 0m, 8268m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 76, 9, 636m, 33m, 0m, 20988m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 76, 2, 636m, 47m, 0m, 29892m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 55, 3, 199m, 10m, 0m, 1990m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 76, 12, 636m, 56m, 0m, 35616m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 35, 20, 470m, 53m, 0m, 24910m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 35, 19, 470m, 8m, 0m, 3760m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 5, 9, 245m, 36m, 0m, 8820m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 5, 3, 245m, 79m, 0m, 19355m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 5, 5, 245m, 16m, 0m, 3920m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 5, 2, 245m, 41m, 0m, 10045m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 5, 16, 245m, 21m, 0m, 5145m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 5, 4, 245m, 23m, 0m, 5635m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 18, 12, 679m, 89m, 0m, 60431m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 35, 6, 470m, 9m, 0m, 4230m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 18, 10, 679m, 37m, 0m, 25123m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 18, 3, 679m, 13m, 0m, 8827m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 18, 5, 679m, 99m, 0m, 67221m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 21, 7, 132m, 58m, 0m, 7656m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 21, 16, 132m, 88m, 0m, 11616m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 21, 3, 132m, 27m, 0m, 3564m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 21, 14, 132m, 42m, 0m, 5544m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 21, 12, 132m, 33m, 0m, 4356m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 18, 14, 679m, 62m, 0m, 42098m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 97, 4, 47m, 17m, 0m, 799m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 77, 18, 473m, 94m, 0m, 44462m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 77, 8, 473m, 79m, 0m, 37367m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 8, 462m, 9m, 0m, 4158m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 10, 462m, 15m, 0m, 6930m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 15, 462m, 90m, 0m, 41580m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 2, 462m, 65m, 0m, 30030m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 11, 462m, 77m, 0m, 35574m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 20, 462m, 10m, 0m, 4620m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 100, 8, 584m, 19m, 0m, 11096m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 4, 462m, 1m, 0m, 462m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 100, 20, 584m, 87m, 0m, 50808m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 100, 3, 584m, 52m, 0m, 30368m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 100, 11, 584m, 8m, 0m, 4672m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 100, 1, 584m, 50m, 0m, 29200m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 29, 13, 176m, 24m, 0m, 4224m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 29, 15, 176m, 22m, 0m, 3872m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 29, 19, 176m, 50m, 0m, 8800m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 29, 10, 176m, 37m, 0m, 6512m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 100, 18, 584m, 99m, 0m, 57816m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 77, 16, 473m, 42m, 0m, 19866m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 14, 462m, 52m, 0m, 24024m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 7, 462m, 8m, 0m, 3696m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 77, 7, 473m, 54m, 0m, 25542m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 77, 20, 473m, 23m, 0m, 10879m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 84, 16, 155m, 91m, 0m, 14105m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 84, 6, 155m, 74m, 0m, 11470m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 84, 4, 155m, 80m, 0m, 12400m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 84, 14, 155m, 92m, 0m, 14260m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 84, 1, 155m, 55m, 0m, 8525m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 94, 19, 462m, 75m, 0m, 34650m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 84, 3, 155m, 80m, 0m, 12400m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 89, 12, 289m, 45m, 0m, 13005m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 89, 9, 289m, 96m, 0m, 27744m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 89, 17, 289m, 39m, 0m, 11271m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 89, 6, 289m, 71m, 0m, 20519m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 89, 13, 289m, 33m, 0m, 9537m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 89, 4, 289m, 9m, 0m, 2601m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 89, 3, 289m, 6m, 0m, 1734m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 89, 2, 289m, 97m, 0m, 28033m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 45, 9, 443m, 22m, 0m, 9746m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 70, 1, 470m, 85m, 0m, 39950m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 45, 18, 443m, 29m, 0m, 12847m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 42, 1, 267m, 21m, 0m, 5607m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 42, 12, 267m, 34m, 0m, 9078m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 42, 18, 267m, 80m, 0m, 21360m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 14, 9, 562m, 66m, 0m, 37092m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 14, 10, 562m, 70m, 0m, 39340m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 14, 4, 562m, 60m, 0m, 33720m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 14, 17, 562m, 81m, 0m, 45522m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 42, 19, 267m, 93m, 0m, 24831m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 45, 14, 443m, 45m, 0m, 19935m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 42, 4, 267m, 75m, 0m, 20025m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 42, 5, 267m, 93m, 0m, 24831m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 13, 821m, 45m, 0m, 36945m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 19, 821m, 11m, 0m, 9031m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 20, 821m, 11m, 0m, 9031m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 34, 16, 210m, 65m, 0m, 13650m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 69, 5, 166m, 14m, 0m, 2324m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 2, 821m, 59m, 0m, 48439m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 4, 821m, 92m, 0m, 75532m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 8, 821m, 66m, 0m, 54186m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 15, 821m, 68m, 0m, 55828m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 3, 821m, 88m, 0m, 72248m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 69, 1, 166m, 68m, 0m, 11288m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 69, 16, 166m, 72m, 0m, 11952m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 69, 20, 166m, 76m, 0m, 12616m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 14, 821m, 97m, 0m, 79637m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 69, 11, 166m, 49m, 0m, 8134m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 87, 20, 164m, 100m, 0m, 16400m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 87, 9, 164m, 78m, 0m, 12792m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 87, 8, 164m, 100m, 0m, 16400m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 87, 10, 164m, 98m, 0m, 16072m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 93, 11, 821m, 28m, 0m, 22988m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 69, 8, 166m, 73m, 0m, 12118m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 34, 9, 210m, 5m, 0m, 1050m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 16, 6, 860m, 92m, 0m, 79120m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 22, 14, 729m, 87m, 0m, 63423m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 57, 10, 51m, 92m, 0m, 4692m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 57, 3, 51m, 18m, 0m, 918m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 57, 17, 51m, 24m, 0m, 1224m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 44, 6, 484m, 48m, 0m, 23232m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 44, 11, 484m, 18m, 0m, 8712m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 44, 20, 484m, 20m, 0m, 9680m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 33, 4, 631m, 13m, 0m, 8203m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 44, 18, 484m, 37m, 0m, 17908m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 44, 8, 484m, 92m, 0m, 44528m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 33, 19, 631m, 51m, 0m, 32181m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 43, 5, 244m, 12m, 0m, 2928m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 43, 11, 244m, 50m, 0m, 12200m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 33, 14, 631m, 78m, 0m, 49218m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 34, 14, 210m, 62m, 0m, 13020m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 33, 17, 631m, 28m, 0m, 17668m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 38, 11, 941m, 76m, 0m, 71516m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 38, 6, 941m, 74m, 0m, 69634m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 38, 12, 941m, 39m, 0m, 36699m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 33, 18, 631m, 53m, 0m, 33443m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 33, 13, 631m, 89m, 0m, 56159m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 33, 5, 631m, 44m, 0m, 27764m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 33, 1, 631m, 1m, 0m, 631m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 33, 9, 631m, 17m, 0m, 10727m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 16, 10, 860m, 88m, 0m, 75680m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 16, 19, 860m, 99m, 0m, 85140m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 16, 18, 860m, 98m, 0m, 84280m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 16, 16, 860m, 27m, 0m, 23220m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 34, 5, 210m, 20m, 0m, 4200m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 16, 5, 860m, 55m, 0m, 47300m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 38, 19, 941m, 23m, 0m, 21643m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 63, 19, 563m, 29m, 0m, 16327m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 63, 12, 563m, 9m, 0m, 5067m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 63, 18, 563m, 86m, 0m, 48418m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 40, 17, 16m, 8m, 0m, 128m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 15, 18, 495m, 54m, 0m, 26730m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 40, 20, 16m, 5m, 0m, 80m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 22, 4, 729m, 48m, 0m, 34992m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 37, 7, 796m, 37m, 0m, 29452m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 37, 17, 796m, 73m, 0m, 58108m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 37, 16, 796m, 36m, 0m, 28656m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 37, 3, 796m, 6m, 0m, 4776m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 37, 9, 796m, 71m, 0m, 56516m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 37, 11, 796m, 4m, 0m, 3184m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 22, 19, 729m, 15m, 0m, 10935m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 37, 14, 796m, 38m, 0m, 30248m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 27, 19, 739m, 79m, 0m, 58381m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 27, 2, 739m, 86m, 0m, 63554m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 40, 18, 16m, 64m, 0m, 1024m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 27, 11, 739m, 50m, 0m, 36950m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 27, 1, 739m, 51m, 0m, 37689m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 27, 5, 739m, 78m, 0m, 57642m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 27, 10, 739m, 37m, 0m, 27343m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 22, 18, 729m, 31m, 0m, 22599m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 22, 3, 729m, 50m, 0m, 36450m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 24, 4, 687m, 38m, 0m, 26106m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 24, 6, 687m, 90m, 0m, 61830m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 24, 1, 687m, 80m, 0m, 54960m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 24, 19, 687m, 39m, 0m, 26793m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 24, 9, 687m, 98m, 0m, 67326m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 22, 7, 729m, 39m, 0m, 28431m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 24, 13, 687m, 54m, 0m, 37098m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 24, 16, 687m, 69m, 0m, 47403m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 24, 15, 687m, 52m, 0m, 35724m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 27, 4, 739m, 39m, 0m, 28821m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 41, 10, 888m, 29m, 0m, 25752m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 41, 19, 888m, 39m, 0m, 34632m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 41, 2, 888m, 54m, 0m, 47952m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 63, 2, 563m, 33m, 0m, 18579m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 63, 8, 563m, 23m, 0m, 12949m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 63, 5, 563m, 98m, 0m, 55174m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 63, 1, 563m, 95m, 0m, 53485m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 63, 7, 563m, 72m, 0m, 40536m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 4, 16, 661m, 34m, 0m, 22474m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 4, 10, 661m, 62m, 0m, 40982m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 54, 8, 704m, 74m, 0m, 52096m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 54, 18, 704m, 65m, 0m, 45760m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 54, 2, 704m, 85m, 0m, 59840m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 54, 14, 704m, 93m, 0m, 65472m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 49, 2, 443m, 55m, 0m, 24365m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 49, 5, 443m, 12m, 0m, 5316m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 49, 17, 443m, 21m, 0m, 9303m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 49, 10, 443m, 79m, 0m, 34997m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 49, 4, 443m, 8m, 0m, 3544m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 49, 1, 443m, 100m, 0m, 44300m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 15, 16, 495m, 11m, 0m, 5445m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 15, 12, 495m, 62m, 0m, 30690m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 46, 1, 543m, 99m, 0m, 53757m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 46, 5, 543m, 54m, 0m, 29322m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 46, 20, 543m, 6m, 0m, 3258m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 46, 16, 543m, 76m, 0m, 41268m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 57, 5, 51m, 85m, 0m, 4335m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 15, 4, 495m, 62m, 0m, 30690m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 47, 13, 36m, 45m, 0m, 1620m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 47, 17, 36m, 95m, 0m, 3420m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 47, 14, 36m, 52m, 0m, 1872m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 47, 1, 36m, 11m, 0m, 396m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 15, 10, 495m, 7m, 0m, 3465m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 15, 1, 495m, 89m, 0m, 44055m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 15, 11, 495m, 83m, 0m, 41085m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 57, 1, 51m, 69m, 0m, 3519m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 28, 8, 809m, 45m, 0m, 36405m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 57, 2, 51m, 70m, 0m, 3570m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 13, 5, 715m, 38m, 0m, 27170m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 13, 9, 715m, 47m, 0m, 33605m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 8, 10, 953m, 24m, 0m, 22872m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 12, 19, 910m, 33m, 0m, 30030m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 12, 20, 910m, 32m, 0m, 29120m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 12, 12, 910m, 10m, 0m, 9100m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 8, 8, 953m, 22m, 0m, 20966m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 8, 13, 953m, 79m, 0m, 75287m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 1, 8, 212m, 91m, 0m, 19292m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 1, 17, 212m, 78m, 0m, 16536m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 1, 9, 212m, 11m, 0m, 2332m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 1, 13, 212m, 84m, 0m, 17808m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 1, 1, 212m, 19m, 0m, 4028m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 71, 16, 279m, 6m, 0m, 1674m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 71, 12, 279m, 28m, 0m, 7812m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 11, 2, 742m, 12m, 0m, 8904m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 71, 19, 279m, 64m, 0m, 17856m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 71, 14, 279m, 36m, 0m, 10044m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 71, 9, 279m, 87m, 0m, 24273m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 71, 11, 279m, 16m, 0m, 4464m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 11, 4, 742m, 73m, 0m, 54166m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 11, 11, 742m, 30m, 0m, 22260m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 11, 17, 742m, 22m, 0m, 16324m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 64, 15, 709m, 59m, 0m, 41831m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 64, 7, 709m, 93m, 0m, 65937m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 64, 20, 709m, 30m, 0m, 21270m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 11, 1, 742m, 82m, 0m, 60844m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 57, 15, 51m, 52m, 0m, 2652m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 36, 3, 411m, 16m, 0m, 6576m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 13, 13, 715m, 67m, 0m, 47905m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 13, 14, 715m, 64m, 0m, 45760m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 13, 11, 715m, 82m, 0m, 58630m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 13, 15, 715m, 70m, 0m, 50050m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 45, 19, 443m, 29m, 0m, 12847m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 45, 10, 443m, 39m, 0m, 17277m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 7, 2, 386m, 52m, 0m, 20072m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 7, 4, 386m, 96m, 0m, 37056m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 32, 6, 704m, 90m, 0m, 63360m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 32, 9, 704m, 48m, 0m, 33792m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 32, 7, 704m, 42m, 0m, 29568m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 32, 4, 704m, 79m, 0m, 55616m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 7, 10, 386m, 48m, 0m, 18528m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 32, 20, 704m, 88m, 0m, 61952m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 32, 10, 704m, 40m, 0m, 28160m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 7, 12, 386m, 6m, 0m, 2316m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 8, 11, 953m, 30m, 0m, 28590m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 23, 7, 560m, 79m, 0m, 44240m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 11, 20, 742m, 13m, 0m, 9646m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 23, 15, 560m, 12m, 0m, 6720m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 10, 613m, 52m, 0m, 31876m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 8, 3, 953m, 38m, 0m, 36214m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 8, 17, 953m, 51m, 0m, 48603m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 18, 613m, 74m, 0m, 45362m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 19, 613m, 4m, 0m, 2452m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 7, 613m, 81m, 0m, 49653m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 15, 613m, 76m, 0m, 46588m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 13, 613m, 74m, 0m, 45362m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 3, 613m, 97m, 0m, 59461m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 6, 613m, 84m, 0m, 51492m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 14, 613m, 5m, 0m, 3065m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 17, 8, 613m, 28m, 0m, 17164m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 13, 20, 715m, 2m, 0m, 1430m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 13, 10, 715m, 20m, 0m, 14300m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 23, 11, 560m, 52m, 0m, 29120m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 36, 17, 411m, 77m, 0m, 31647m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 11, 8, 742m, 56m, 0m, 41552m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 82, 1, 29m, 49m, 0m, 1421m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 36, 12, 411m, 41m, 0m, 16851m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 82, 20, 29m, 13m, 0m, 377m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 82, 4, 29m, 59m, 0m, 1711m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 82, 17, 29m, 95m, 0m, 2755m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 82, 14, 29m, 57m, 0m, 1653m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 82, 15, 29m, 55m, 0m, 1595m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 82, 10, 29m, 5m, 0m, 145m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 28, 19, 809m, 68m, 0m, 55012m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 73, 15, 497m, 51m, 0m, 25347m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 28, 11, 809m, 22m, 0m, 17798m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 73, 3, 497m, 72m, 0m, 35784m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 73, 9, 497m, 79m, 0m, 39263m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 73, 8, 497m, 35m, 0m, 17395m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 73, 16, 497m, 76m, 0m, 37772m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 28, 2, 809m, 93m, 0m, 75237m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 28, 12, 809m, 52m, 0m, 42068m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 67, 18, 442m, 31m, 0m, 13702m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 67, 8, 442m, 95m, 0m, 41990m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 67, 1, 442m, 6m, 0m, 2652m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 67, 9, 442m, 80m, 0m, 35360m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 67, 3, 442m, 94m, 0m, 41548m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 67, 7, 442m, 88m, 0m, 38896m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 28, 17, 809m, 74m, 0m, 59866m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 65, 10, 388m, 36m, 0m, 13968m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 65, 2, 388m, 29m, 0m, 11252m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 65, 13, 388m, 62m, 0m, 24056m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 65, 16, 388m, 29m, 0m, 11252m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 65, 4, 388m, 84m, 0m, 32592m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 65, 19, 388m, 74m, 0m, 28712m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 57, 14, 51m, 78m, 0m, 3978m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 67, 16, 442m, 30m, 0m, 13260m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 88, 12, 311m, 96m, 0m, 29856m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 22, 10, 729m, 83m, 0m, 60507m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 88, 13, 311m, 94m, 0m, 29234m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 19, 4, 822m, 30m, 0m, 24660m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 26, 18, 134m, 33m, 0m, 4422m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 3, 7, 802m, 8m, 0m, 6416m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 3, 20, 802m, 71m, 0m, 56942m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 3, 11, 802m, 23m, 0m, 18446m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 19, 11, 822m, 10m, 0m, 8220m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 26, 4, 134m, 31m, 0m, 4154m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 3, 5, 802m, 73m, 0m, 58546m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 26, 20, 134m, 27m, 0m, 3618m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 26, 3, 134m, 96m, 0m, 12864m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 19, 9, 822m, 37m, 0m, 30414m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 3, 12, 802m, 15m, 0m, 12030m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 19, 1, 822m, 39m, 0m, 32058m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 3, 1, 802m, 56m, 0m, 44912m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 88, 6, 311m, 63m, 0m, 19593m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 19, 8, 822m, 9m, 0m, 7398m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 19, 13, 822m, 36m, 0m, 29592m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 88, 14, 311m, 72m, 0m, 22392m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 88, 19, 311m, 61m, 0m, 18971m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 88, 8, 311m, 17m, 0m, 5287m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 19, 5, 822m, 40m, 0m, 32880m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 26, 11, 134m, 17m, 0m, 2278m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 26, 9, 134m, 65m, 0m, 8710m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 88, 3, 311m, 46m, 0m, 14306m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 19, 10, 822m, 99m, 0m, 81378m });

            migrationBuilder.InsertData(
                table: "DetailCmds",
                columns: new[] { "IdArticle", "IdCommande", "PrixVente", "QtePrise", "Remise", "Total" },
                values: new object[] { 26, 5, 134m, 71m, 0m, 9514m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 50, new DateTime(2019, 6, 6, 20, 10, 28, 912, DateTimeKind.Local).AddTicks(5749), 5, 69, 2, 166m, 30m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 3, new DateTime(2019, 6, 18, 21, 7, 27, 74, DateTimeKind.Local).AddTicks(7037), 9, 58, 1, 26m, 74m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 70, new DateTime(2019, 7, 22, 15, 56, 24, 410, DateTimeKind.Local).AddTicks(4), 6, 58, 1, 26m, 72m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 16, new DateTime(2020, 2, 3, 4, 2, 30, 6, DateTimeKind.Local).AddTicks(8680), 8, 29, 2, 176m, 71m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 32, new DateTime(2019, 9, 19, 13, 55, 20, 584, DateTimeKind.Local).AddTicks(7435), 3, 42, 1, 267m, 64m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 87, new DateTime(2019, 8, 24, 16, 51, 30, 176, DateTimeKind.Local).AddTicks(4266), 9, 86, 2, 93m, 60m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 58, new DateTime(2019, 10, 4, 1, 42, 17, 609, DateTimeKind.Local).AddTicks(59), 4, 8, 3, 953m, 93m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 43, new DateTime(2019, 12, 7, 19, 12, 10, 172, DateTimeKind.Local).AddTicks(4079), 8, 8, 2, 953m, 86m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 7, new DateTime(2019, 7, 4, 4, 9, 49, 703, DateTimeKind.Local).AddTicks(8340), 5, 8, 3, 953m, 21m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 18, new DateTime(2019, 8, 19, 1, 10, 12, 718, DateTimeKind.Local).AddTicks(3402), 3, 33, 2, 631m, 59m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 99, new DateTime(2020, 4, 14, 15, 23, 18, 761, DateTimeKind.Local).AddTicks(3494), 9, 86, 1, 93m, 9m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 77, new DateTime(2019, 9, 21, 11, 47, 42, 290, DateTimeKind.Local).AddTicks(7897), 4, 26, 3, 134m, 15m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 2, new DateTime(2020, 2, 10, 13, 22, 42, 928, DateTimeKind.Local).AddTicks(6382), 6, 29, 2, 176m, 50m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 79, new DateTime(2019, 7, 27, 0, 27, 26, 22, DateTimeKind.Local).AddTicks(4451), 9, 11, 3, 742m, 62m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 89, new DateTime(2019, 9, 4, 9, 43, 4, 610, DateTimeKind.Local).AddTicks(3968), 7, 93, 1, 821m, 35m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 83, new DateTime(2019, 12, 3, 19, 49, 18, 845, DateTimeKind.Local).AddTicks(4489), 4, 100, 3, 584m, 31m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 93, new DateTime(2019, 8, 21, 4, 55, 33, 539, DateTimeKind.Local).AddTicks(539), 10, 2, 1, 284m, 79m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 66, new DateTime(2020, 3, 1, 8, 32, 12, 909, DateTimeKind.Local).AddTicks(5955), 1, 30, 2, 599m, 91m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 15, new DateTime(2019, 5, 24, 9, 24, 23, 67, DateTimeKind.Local).AddTicks(7195), 10, 3, 2, 802m, 40m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 68, new DateTime(2019, 12, 9, 23, 37, 17, 526, DateTimeKind.Local).AddTicks(8033), 1, 39, 2, 926m, 18m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 80, new DateTime(2019, 8, 15, 15, 37, 35, 878, DateTimeKind.Local).AddTicks(5499), 7, 39, 1, 926m, 25m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 72, new DateTime(2019, 9, 29, 22, 55, 20, 994, DateTimeKind.Local).AddTicks(1339), 7, 88, 1, 311m, 57m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 9, new DateTime(2019, 9, 3, 10, 15, 6, 702, DateTimeKind.Local).AddTicks(1306), 5, 48, 1, 28m, 71m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 30, new DateTime(2020, 2, 20, 8, 9, 18, 87, DateTimeKind.Local).AddTicks(8900), 8, 48, 2, 28m, 73m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 74, new DateTime(2019, 11, 10, 9, 13, 56, 167, DateTimeKind.Local).AddTicks(4144), 6, 82, 1, 29m, 3m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 14, new DateTime(2019, 11, 29, 10, 44, 8, 880, DateTimeKind.Local).AddTicks(8672), 4, 82, 3, 29m, 94m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 62, new DateTime(2019, 12, 13, 3, 6, 46, 389, DateTimeKind.Local).AddTicks(3311), 5, 73, 1, 497m, 96m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 71, new DateTime(2019, 8, 15, 23, 24, 11, 37, DateTimeKind.Local).AddTicks(9770), 8, 66, 2, 850m, 56m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 100, new DateTime(2019, 7, 31, 1, 42, 50, 605, DateTimeKind.Local).AddTicks(6536), 3, 67, 2, 442m, 67m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 46, new DateTime(2019, 7, 31, 23, 22, 7, 151, DateTimeKind.Local).AddTicks(9120), 7, 67, 1, 442m, 38m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 21, new DateTime(2020, 1, 10, 11, 29, 26, 91, DateTimeKind.Local).AddTicks(7384), 3, 65, 1, 388m, 82m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 11, new DateTime(2019, 9, 8, 10, 50, 55, 505, DateTimeKind.Local).AddTicks(9717), 2, 65, 3, 388m, 19m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 36, new DateTime(2019, 7, 10, 7, 27, 59, 537, DateTimeKind.Local).AddTicks(8087), 3, 91, 2, 247m, 42m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 6, new DateTime(2019, 12, 4, 6, 25, 4, 217, DateTimeKind.Local).AddTicks(938), 6, 2, 1, 284m, 16m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 81, new DateTime(2020, 1, 26, 17, 30, 12, 727, DateTimeKind.Local).AddTicks(7643), 5, 36, 1, 411m, 27m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 94, new DateTime(2019, 9, 29, 1, 1, 57, 711, DateTimeKind.Local).AddTicks(9950), 2, 36, 2, 411m, 65m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 19, new DateTime(2019, 11, 7, 3, 53, 4, 406, DateTimeKind.Local).AddTicks(2426), 10, 64, 1, 709m, 1m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 61, new DateTime(2019, 12, 29, 3, 44, 48, 497, DateTimeKind.Local).AddTicks(9731), 10, 32, 3, 704m, 25m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 1, new DateTime(2020, 1, 25, 17, 20, 46, 165, DateTimeKind.Local).AddTicks(1410), 1, 32, 1, 704m, 57m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 82, new DateTime(2019, 4, 27, 10, 41, 3, 80, DateTimeKind.Local).AddTicks(2664), 8, 53, 2, 712m, 27m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 25, new DateTime(2020, 2, 19, 7, 33, 18, 363, DateTimeKind.Local).AddTicks(2384), 7, 23, 1, 560m, 26m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 12, new DateTime(2020, 4, 12, 13, 39, 59, 682, DateTimeKind.Local).AddTicks(6607), 1, 23, 3, 560m, 92m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 5, new DateTime(2019, 8, 29, 19, 7, 10, 686, DateTimeKind.Local).AddTicks(3293), 3, 56, 1, 661m, 34m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 40, new DateTime(2020, 3, 5, 21, 14, 39, 736, DateTimeKind.Local).AddTicks(4410), 9, 56, 3, 661m, 15m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 59, new DateTime(2019, 8, 30, 4, 34, 29, 355, DateTimeKind.Local).AddTicks(9508), 2, 81, 1, 148m, 68m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 86, new DateTime(2019, 10, 30, 9, 59, 6, 87, DateTimeKind.Local).AddTicks(7728), 4, 25, 2, 565m, 28m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 90, new DateTime(2019, 12, 10, 18, 42, 15, 376, DateTimeKind.Local).AddTicks(3626), 5, 12, 2, 910m, 24m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 76, new DateTime(2020, 1, 17, 3, 36, 10, 689, DateTimeKind.Local).AddTicks(796), 2, 1, 3, 212m, 28m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 67, new DateTime(2020, 2, 27, 15, 57, 15, 965, DateTimeKind.Local).AddTicks(3755), 6, 1, 1, 212m, 2m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 52, new DateTime(2019, 7, 8, 17, 33, 3, 688, DateTimeKind.Local).AddTicks(8930), 10, 96, 2, 757m, 6m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 63, new DateTime(2019, 5, 7, 1, 0, 14, 391, DateTimeKind.Local).AddTicks(3225), 9, 96, 1, 757m, 18m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 44, new DateTime(2019, 6, 12, 17, 53, 10, 416, DateTimeKind.Local).AddTicks(5040), 8, 78, 2, 59m, 14m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 29, new DateTime(2019, 6, 21, 0, 25, 56, 738, DateTimeKind.Local).AddTicks(2879), 4, 98, 1, 311m, 90m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 20, new DateTime(2020, 3, 9, 11, 43, 43, 859, DateTimeKind.Local).AddTicks(6), 7, 99, 1, 472m, 7m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 97, new DateTime(2020, 3, 27, 3, 52, 23, 604, DateTimeKind.Local).AddTicks(8876), 2, 64, 1, 709m, 91m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 22, new DateTime(2019, 8, 12, 19, 20, 54, 247, DateTimeKind.Local).AddTicks(3503), 9, 12, 2, 910m, 27m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 48, new DateTime(2019, 9, 19, 17, 43, 36, 876, DateTimeKind.Local).AddTicks(9584), 6, 22, 1, 729m, 47m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 28, new DateTime(2019, 5, 14, 4, 49, 58, 643, DateTimeKind.Local).AddTicks(5138), 8, 43, 1, 244m, 26m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 55, new DateTime(2019, 12, 24, 13, 24, 53, 124, DateTimeKind.Local).AddTicks(3588), 1, 51, 1, 963m, 55m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 64, new DateTime(2019, 12, 29, 17, 0, 39, 745, DateTimeKind.Local).AddTicks(8180), 5, 63, 1, 563m, 38m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 73, new DateTime(2019, 12, 26, 18, 41, 52, 779, DateTimeKind.Local).AddTicks(7780), 5, 21, 2, 132m, 81m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 39, new DateTime(2019, 8, 28, 9, 27, 17, 14, DateTimeKind.Local).AddTicks(9503), 3, 35, 2, 470m, 67m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 53, new DateTime(2020, 3, 31, 11, 32, 42, 390, DateTimeKind.Local).AddTicks(2285), 6, 35, 3, 470m, 68m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 92, new DateTime(2019, 6, 23, 7, 8, 21, 691, DateTimeKind.Local).AddTicks(3291), 2, 47, 2, 36m, 46m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 10, new DateTime(2019, 9, 11, 18, 6, 12, 656, DateTimeKind.Local).AddTicks(4144), 3, 47, 1, 36m, 21m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 17, new DateTime(2020, 2, 27, 11, 37, 59, 437, DateTimeKind.Local).AddTicks(1628), 10, 76, 3, 636m, 55m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 78, new DateTime(2019, 12, 2, 11, 56, 16, 964, DateTimeKind.Local).AddTicks(4041), 7, 46, 2, 543m, 33m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 49, new DateTime(2019, 8, 2, 15, 30, 33, 593, DateTimeKind.Local).AddTicks(7657), 7, 77, 2, 473m, 94m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 84, new DateTime(2019, 5, 13, 21, 1, 17, 199, DateTimeKind.Local).AddTicks(8219), 10, 41, 1, 888m, 23m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 23, new DateTime(2020, 1, 11, 0, 4, 4, 353, DateTimeKind.Local).AddTicks(3550), 3, 84, 1, 155m, 70m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 42, new DateTime(2019, 8, 4, 0, 36, 38, 41, DateTimeKind.Local).AddTicks(4725), 5, 37, 1, 796m, 40m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 54, new DateTime(2020, 4, 4, 13, 57, 17, 218, DateTimeKind.Local).AddTicks(5799), 6, 89, 2, 289m, 95m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 88, new DateTime(2020, 2, 16, 8, 34, 27, 855, DateTimeKind.Local).AddTicks(7753), 9, 24, 2, 687m, 92m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 85, new DateTime(2019, 7, 1, 20, 12, 15, 266, DateTimeKind.Local).AddTicks(9171), 5, 24, 3, 687m, 64m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 38, new DateTime(2020, 2, 7, 10, 57, 38, 492, DateTimeKind.Local).AddTicks(8059), 7, 94, 3, 462m, 65m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 24, new DateTime(2019, 7, 30, 7, 58, 48, 889, DateTimeKind.Local).AddTicks(1531), 2, 24, 3, 687m, 97m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 95, new DateTime(2020, 3, 20, 18, 45, 43, 938, DateTimeKind.Local).AddTicks(4720), 1, 87, 2, 164m, 5m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 57, new DateTime(2020, 1, 6, 17, 43, 43, 197, DateTimeKind.Local).AddTicks(4882), 8, 5, 3, 245m, 50m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 31, new DateTime(2019, 5, 17, 7, 54, 20, 913, DateTimeKind.Local).AddTicks(6400), 4, 5, 1, 245m, 34m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 4, new DateTime(2020, 4, 4, 15, 20, 26, 822, DateTimeKind.Local).AddTicks(272), 4, 5, 2, 245m, 90m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 75, new DateTime(2020, 1, 6, 22, 5, 32, 574, DateTimeKind.Local).AddTicks(5019), 7, 16, 1, 860m, 2m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 34, new DateTime(2020, 2, 17, 6, 48, 35, 737, DateTimeKind.Local).AddTicks(2258), 9, 52, 2, 563m, 11m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 37, new DateTime(2020, 2, 10, 12, 44, 0, 832, DateTimeKind.Local).AddTicks(9615), 9, 52, 3, 563m, 77m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 45, new DateTime(2019, 10, 26, 4, 29, 23, 841, DateTimeKind.Local).AddTicks(6969), 6, 52, 3, 563m, 90m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 13, new DateTime(2019, 9, 1, 7, 39, 11, 573, DateTimeKind.Local).AddTicks(7213), 6, 16, 2, 860m, 12m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 33, new DateTime(2019, 7, 21, 13, 19, 33, 801, DateTimeKind.Local).AddTicks(4663), 8, 59, 2, 59m, 34m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 69, new DateTime(2020, 3, 10, 13, 11, 55, 410, DateTimeKind.Local).AddTicks(6164), 5, 79, 3, 710m, 57m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 91, new DateTime(2019, 8, 6, 13, 20, 21, 651, DateTimeKind.Local).AddTicks(607), 4, 14, 3, 562m, 31m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 8, new DateTime(2019, 10, 22, 11, 39, 1, 199, DateTimeKind.Local).AddTicks(3450), 2, 51, 1, 963m, 3m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 35, new DateTime(2019, 5, 16, 4, 53, 3, 191, DateTimeKind.Local).AddTicks(4039), 3, 14, 3, 562m, 3m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 26, new DateTime(2019, 5, 16, 4, 21, 51, 42, DateTimeKind.Local).AddTicks(6831), 10, 92, 2, 237m, 94m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 51, new DateTime(2019, 12, 12, 6, 58, 27, 829, DateTimeKind.Local).AddTicks(6967), 4, 92, 3, 237m, 29m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 96, new DateTime(2020, 2, 24, 3, 26, 55, 205, DateTimeKind.Local).AddTicks(2837), 3, 92, 2, 237m, 1m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 98, new DateTime(2020, 1, 6, 9, 30, 30, 619, DateTimeKind.Local).AddTicks(9075), 4, 93, 1, 821m, 89m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 56, new DateTime(2019, 6, 5, 19, 34, 28, 628, DateTimeKind.Local).AddTicks(3917), 9, 93, 1, 821m, 18m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 47, new DateTime(2019, 5, 26, 5, 17, 41, 163, DateTimeKind.Local).AddTicks(9489), 4, 93, 1, 821m, 29m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 60, new DateTime(2020, 2, 22, 21, 46, 53, 759, DateTimeKind.Local).AddTicks(5622), 8, 97, 3, 47m, 87m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 65, new DateTime(2019, 8, 17, 8, 35, 41, 254, DateTimeKind.Local).AddTicks(1072), 8, 97, 2, 47m, 40m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 27, new DateTime(2019, 9, 16, 14, 2, 29, 135, DateTimeKind.Local).AddTicks(5731), 2, 14, 1, 562m, 59m });

            migrationBuilder.InsertData(
                table: "Fournitures",
                columns: new[] { "Id", "DateAchat", "IdAchat", "IdArticle", "IdFournisseur", "PrixAchat", "Qte" },
                values: new object[] { 41, new DateTime(2020, 1, 1, 12, 41, 51, 711, DateTimeKind.Local).AddTicks(5649), 7, 90, 1, 440m, 55m });

            migrationBuilder.CreateIndex(
                name: "IX_Achats_IdFournisseur",
                table: "Achats",
                column: "IdFournisseur");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_IdSousCategorie",
                table: "Articles",
                column: "IdSousCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_IdClient",
                table: "Commandes",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_DetailCmds_IdCommande",
                table: "DetailCmds",
                column: "IdCommande");

            migrationBuilder.CreateIndex(
                name: "IX_DevisAcrticles_IdArticle",
                table: "DevisAcrticles",
                column: "IdArticle");

            migrationBuilder.CreateIndex(
                name: "IX_DevisAcrticles_IdDevis",
                table: "DevisAcrticles",
                column: "IdDevis");

            migrationBuilder.CreateIndex(
                name: "IX_Fournitures_IdAchat",
                table: "Fournitures",
                column: "IdAchat");

            migrationBuilder.CreateIndex(
                name: "IX_Fournitures_IdArticle",
                table: "Fournitures",
                column: "IdArticle");

            migrationBuilder.CreateIndex(
                name: "IX_Fournitures_IdFournisseur",
                table: "Fournitures",
                column: "IdFournisseur");

            migrationBuilder.CreateIndex(
                name: "IX_SousCategories_IdCategorie",
                table: "SousCategories",
                column: "IdCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRole",
                table: "Users",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailCmds");

            migrationBuilder.DropTable(
                name: "DevisAcrticles");

            migrationBuilder.DropTable(
                name: "Fournitures");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Deviss");

            migrationBuilder.DropTable(
                name: "Achats");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "SousCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
