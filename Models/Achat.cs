
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class Achat
    {
        public Achat()
        {
            Fournitures = new HashSet<Fourniture>();
        }
        public int Id { get; set; }
        public int IdFournisseur { get; set; }
        public decimal Montant { get; set; }
        public string ModePayement { get; set; }
        public string NumCheque { get; set; }
        public decimal Credit { get; set; }
        public DateTime Date { get; set; }
        public decimal Avance { get; set; }
        public Fournisseur Fournisseur { get; set; }
        public virtual ICollection<Fourniture> Fournitures { get; set; }
    }
}
