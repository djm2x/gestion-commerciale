
using System;


namespace GestionCommerciale.Models
{
    public partial class Fourniture
    {
        public int Id { get; set; }
        public int IdArticle { get; set; }
        public int IdFournisseur { get; set; }
        public decimal Qte { get; set; }
        public decimal PrixAchat { get; set; }
        public DateTime DateAchat { get; set; }
        public int IdAchat { get; set; }
        public Article Article { get; set; }
        public Achat Achat { get; set; }
        public Fournisseur Fournisseur { get; set; }
    }
}
