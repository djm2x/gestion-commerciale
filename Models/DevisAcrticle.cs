
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class DevisArticle
    {
        public DevisArticle()
        {
        }
        public int Id { get; set; }
        public decimal Qte { get; set; }
        public decimal Pu { get; set; }
        public decimal Remise { get; set; }
        public decimal Total { get; set; }
        public int IdArticle { get; set; }
        public int IdDevis { get; set; }
        public Article Article { get; set; }
        public Devis Devis { get; set; }
        
    }
}