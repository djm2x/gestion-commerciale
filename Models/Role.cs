
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
