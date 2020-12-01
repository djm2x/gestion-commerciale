
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class Article
    {
        public Article()
        {
            Fournitures = new HashSet<Fourniture>();
            DetailCmds = new HashSet<DetailCmd>();
            DevisAcrticles = new HashSet<DevisArticle>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string TitreFr { get; set; }
        public string TitreAr { get; set; }
        public string EmplacementMagasin { get; set; }
        public string EmplacementDepot { get; set; }
        public DateTime DateDernierAchat { get; set; }
        public decimal PrixUnitaire { get; set; }
        public decimal QteStock { get; set; }
        public decimal? StockMin { get; set; }
        public string Image { get; set; }
        public string Unite { get; set; }
        public string Constructeur { get; set; }
        public bool BestSell { get; set; }
        public string Description { get; set; }
        public int IdSousCategorie { get; set; }
        public SousCategorie SousCategorie { get; set; }
        public virtual ICollection<Fourniture> Fournitures { get; set; }
        public virtual ICollection<DetailCmd> DetailCmds { get; set; }
         public virtual ICollection<DevisArticle> DevisAcrticles { get; set; }
    }
}
