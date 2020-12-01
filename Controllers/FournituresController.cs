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
    public class FournituresController : SuperController<Fourniture>
    {
        public FournituresController(MyDbContext context) : base(context) { }

        [HttpGet("{idArticle}/{idFornisseur}")]
        public virtual async Task<IActionResult> GetByIds(int idArticle, int idFornisseur)
        {
            var model = await _context.Fournitures
                .Where(r => r.IdFournisseur == idFornisseur && r.IdArticle == idArticle)
                .FirstOrDefaultAsync();

            return Ok(model);
        }

         [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{id}")]
        public async Task<IActionResult> GetListByAchat(int startIndex, int pageSize, string sortBy, string sortDir, int id)
        {
            // int i = typeof(T).FullName.LastIndexOf('.');
            // string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            var q = _context.Fournitures
                .Where(e => e.IdAchat == id)
                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Fourniture>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                .Include(e => e.Article)
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{idArticle}")]
        public async Task<IActionResult> GetListByArticle(int startIndex, int pageSize, string sortBy, string sortDir, int idArticle)
        {
            // int i = typeof(T).FullName.LastIndexOf('.');
            // string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            var q = _context.Fournitures
                .Where(e => idArticle == 0 ? true : e.IdArticle == idArticle)
                .OrderByName<Fourniture>(sortBy, sortDir == "desc")
                ;

            int count = await q.CountAsync();

             var list = await q
                .Skip(startIndex)
                .Take(pageSize)
                .Include(e => e.Fournisseur)
                .Include(e => e.Achat)
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}")]
        public async Task<IActionResult> GetList2(int startIndex, int pageSize, string sortBy, string sortDir)
        {
            // int i = typeof(T).FullName.LastIndexOf('.');
            // string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            var list = await _context.Fournitures
                // .Include(e => e.Fournisseur)
                // .Include(e => e.Article)
                .GroupBy(e => e.IdFournisseur)
                // .OrderByName<Fourniture>(sortBy, sortDir == "desc")
                // .Skip(startIndex)
                // .Take(pageSize)
                // .Include(e => e.Article)
                .ToListAsync()
                ;
            int count = await _context.Fournitures.CountAsync();

            return Ok(new { list = list, count = count });
        }

        public override async Task<IActionResult> PostRange(List<Fourniture> models)
        {
            if (models.Count == 0)
            {
                return Ok(new { message = "count = 0" });
            }

            models.ForEach(e =>
            {
                var art = _context.Articles.Find(e.IdArticle);

                art.QteStock += e.Qte;

            });

            await _context.Fournitures.AddRangeAsync(models);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return StatusCode(250);
        }


    }
}