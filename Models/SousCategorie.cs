
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class SousCategorie
    {
        public SousCategorie()
        {
            Articles = new HashSet<Article>();
        }
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int IdCategorie { get; set; }
        public Categorie Categorie { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}