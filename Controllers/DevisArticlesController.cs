using System;
using System.Collections.Generic;
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
    public class DevisArticlesController : SuperController<DevisArticle>
    {
        public DevisArticlesController(MyDbContext context) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{id}")]
        public async Task<IActionResult> GetListByDevis(int startIndex, int pageSize, string sortBy, string sortDir, int id)
        {
            // int i = typeof(T).FullName.LastIndexOf('.');
            // string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            var q = _context.DevisAcrticles
                .Where(e => e.IdDevis == id)
                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<DevisArticle>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                .Include(e => e.Article)
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

    }

   
}