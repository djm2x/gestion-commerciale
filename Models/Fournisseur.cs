
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class Fournisseur
    {
        public Fournisseur()
        {
            Fournitures = new HashSet<Fourniture>();
            Achats = new HashSet<Achat>();
        }
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Societe { get; set; }
        public string Telephone { get; set; }
        public virtual ICollection<Achat> Achats { get; set; }
        public virtual ICollection<Fourniture> Fournitures { get; set; }
        
    }
}