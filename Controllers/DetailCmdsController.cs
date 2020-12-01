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
    public class DetailCmdsController : SuperController<DetailCmd>
    {
        public DetailCmdsController(MyDbContext context) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{idCommande}")]
        public async Task<IActionResult> GetListByCommande(int startIndex, int pageSize, string sortBy, string sortDir, int idCommande)
        {
            // int i = typeof(T).FullName.LastIndexOf('.');
            // string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            var q = _context.DetailCmds
                .Where(e => e.IdCommande == idCommande)
                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<DetailCmd>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                .Include(e => e.Article)
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

        [HttpPost]
        public override async Task<IActionResult> Update(DetailCmd model)
        {

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        [HttpDelete("{idArticle}/{idCommande}")]
        public async Task<ActionResult> Delete(int idArticle, int idCommande)
        {
            var model = await _context.DetailCmds.FindAsync(idArticle, idCommande);
            if (model == null)
            {
                return NotFound();
            }

            _context.DetailCmds.Remove(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();
        }

        

    }
}