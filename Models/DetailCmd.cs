
using System;


namespace GestionCommerciale.Models
{
    public partial class DetailCmd
    {
        public int IdArticle { get; set; }
        public int IdCommande { get; set; }
        public decimal PrixVente { get; set; }
        public decimal QtePrise { get; set; }
        public decimal Remise { get; set; }
        public decimal Total { get; set; }
        public Article Article { get; set; }
        public Commande Commande { get; set; }
    }
}
