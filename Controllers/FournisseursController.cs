using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GestionCommerciale.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCommerciale.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FournisseursController : SuperController<Fournisseur>
    {
        public FournisseursController(MyDbContext context) : base(context) { }


    }
}