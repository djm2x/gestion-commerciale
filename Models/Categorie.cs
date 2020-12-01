
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class Categorie
    {
         public Categorie()
        {
            SousCategories = new HashSet<SousCategorie>();
        }
        public int Id { get; set; }
        public string Libelle { get; set; }
       
       public virtual ICollection<SousCategorie> SousCategories { get; set; }

    }
}