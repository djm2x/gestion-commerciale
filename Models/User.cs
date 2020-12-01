
using System;
using System.Collections.Generic;

namespace GestionCommerciale.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string NomComplete { get; set; }
       
        public string Email { get; set; }
        public string Password { get; set; }
        public string Tel { get; set; }
        public int IdRole { get; set; }
        public Role Role { get; set; }
    }
}
