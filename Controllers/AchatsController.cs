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
    public class AchatsController : SuperController<Achat>
    {
        public AchatsController(MyDbContext context) : base(context) { }

        [HttpPost]
        public async Task<IActionResult> GetAll(Params data)
        {
            // int i = typeof(T).FullName.LastIndexOf('.');
            // string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            var q = _context.Achats
                .Where(e => data.Id == 0 ? true : e.IdFournisseur == data.Id)
                .Where(e => data.IdCategorie == 0 ? true : e.Fournitures.Any(f => f.Article.SousCategorie.IdCategorie == data.IdCategorie))
                // .Where(e => DateTime.Compare(e.Date, data.Date) == 0)
                // .Where(e => data.Date == null ? true : (e.Date.Year == data.Date.Year && e.Date.Month == data.Date.Month && e.Date.Day == data.Date.Day))
                .OrderByName<Achat>(data.SortBy, data.SortDir == "desc")
                ;

            int count = await q.CountAsync();

            var list = await q
                .Skip(data.StartIndex)
                .Take(data.PageSize)
                .Include(e => e.Fournisseur)
                .Select(e => new
                {
                    id = e.Id,
                    date = e.Date,
                    fournisseur = e.Fournisseur.Societe,
                    montant = e.Montant,
                    modePayement = e.ModePayement,
                    credit = e.Credit,
                    avance = e.Avance,
                    categorie = e.Fournitures.Select(c => new { libelle = c.Article.SousCategorie.Categorie.Libelle }),
                })
                // .Include(e => e.)
                .ToListAsync()
                ;


            return Ok(new { list = list, count = count });
        }

         [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Achats.Where(e => e.Id == id).Include(e => e.Fournitures).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }

            model.Fournitures.ToList().ForEach( e => {
                var a = _context.Articles.Find(e.IdArticle);
                a.QteStock -= e.Qte;
            }); 

            _context.Achats.Remove(model);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoAchat(int id)
        {
            // var list = await _context.Achats
            //                 .Where(e => e.Id == id)
            //                 .Include(e => e.Fournitures)
            //                 .ThenInclude(e => e.Article)
            //                 .FirstOrDefaultAsync()
            //                 ;

            return Ok(await _context.Achats
                            .Where(e => e.Id == id)
                            .Include(e => e.Fournitures)
                            .ThenInclude(e => e.Article)
                            .FirstOrDefaultAsync()
                            );
        }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{idFournisseur}")]
        public async Task<ActionResult<Article>> GetAchatsWithCredit(
            int startIndex, int pageSize, string sortBy, string sortDir, int idFournisseur)
        {

            var q = _context.Achats
                .Where(r => r.IdFournisseur == idFournisseur)
                .Where(r => r.Credit > 0)
                .OrderByName<Achat>(sortBy, sortDir == "desc")
                ;

            int count = await q.CountAsync();

            var list = await q.Skip(startIndex).Take(pageSize).ToListAsync();

            return Ok(new { list = list, count = count });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreditByFournisseur(int id)
        {
            var q = _context.Achats
                    .Where(e => id == 0 ? true : e.IdFournisseur == id)
                    ;

            decimal credit = await q
                            .SumAsync(e => e.Credit)
                            ;

            decimal avance = await q
                            .SumAsync(e => e.Avance)
                            ;

            return Ok(new { credit, avance });
        }

         [HttpGet]
        public async Task<IActionResult> GetCreditFournisseurs()
        {
            return Ok(await _context.Achats.SumAsync(e => e.Credit));
        }


    }

    public class Params
    {
        public int StartIndex { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortDir { get; set; }
        public int Id { get; set; }
        public int IdCategorie { get; set; }
    }
}