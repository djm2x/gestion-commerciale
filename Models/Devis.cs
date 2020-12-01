
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class Devis
    {
        public Devis()
        {
            DevisActicles = new HashSet<DevisArticle>();
        }
        public int Id { get; set; }
        public string Client { get; set; }
        public DateTime Date { get; set; }
        public decimal Montant { get; set; }
        public virtual ICollection<DevisArticle> DevisActicles { get; set; }
    }
}
