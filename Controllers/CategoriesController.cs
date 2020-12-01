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
    public class CategoriesController : SuperController<Categorie>
    {
        public CategoriesController(MyDbContext context) : base(context) { }

        
    }
}