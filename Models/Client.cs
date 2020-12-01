
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class Client
    {
        public Client()
        {
            Commandes = new HashSet<Commande>();
        }
        public int Id { get; set; }
        public string Nom { get; set; }
         public string Prenom { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public string Ice { get; set; }
        public virtual ICollection<Commande> Commandes { get; set; }
    }
}
