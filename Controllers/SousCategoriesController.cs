using System.Linq;
using System.Threading.Tasks;
using GestionCommerciale.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Providers;

namespace GestionCommerciale.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SousCategoriesController : SuperController<SousCategorie>
    {
        public SousCategoriesController(MyDbContext context) : base(context) { }

         [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}")]
        public override async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir)
        {
            // int i = typeof(T).FullName.LastIndexOf('.');
            // string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            var list = await _context.SousCategories
                .OrderByName<SousCategorie>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                .Include(e => e.Categorie)
                .ToListAsync()
                ;
            int count = await _context.SousCategories.CountAsync();

            return Ok(new { list = list, count = count });
        }

         [HttpGet("{idCategorie}")]
        public async Task<IActionResult> GetByCat(int idCategorie)
        {
            return Ok(await _context.SousCategories
                .Where(e => idCategorie == 0 ? false : e.IdCategorie == idCategorie)
                .ToListAsync()
                )
            ;
        }
    }
}