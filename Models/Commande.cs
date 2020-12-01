
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class Commande
    {
        public Commande()
        {
            DetailCmds = new HashSet<DetailCmd>();
        }
        public int Id { get; set; }
        public string ModePayement { get; set; }
        public string NumCheque { get; set; }
        public decimal Credit { get; set; }
        public decimal Avance { get; set; }
        public decimal Total { get; set; }
        public string NomClient { get; set; }
        public int? IdClient { get; set; }
        public Client Client { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public virtual ICollection<DetailCmd> DetailCmds { get; set; }
    }
}
