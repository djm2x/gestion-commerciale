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
    public class ArticlesController : SuperController<Article>
    {
        public ArticlesController(MyDbContext context) : base(context) { }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdCat(int id)
        {
            var list = await _context.Articles.Where(r => r.IdSousCategorie == id).ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> GetState(Params data)
        {
            // int i = typeof(T).FullName.LastIndexOf('.');
            // string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            var q = _context.Articles
                .Where(e => data.Id == 0 ? true : (data.Id == 1 ? e.QteStock > e.StockMin : e.QteStock <= e.StockMin))
                .Where(e => data.IdCategorie == 0 ? true : e.SousCategorie.IdCategorie == data.IdCategorie)
                // .Where(e => DateTime.Compare(e.Date, data.Date) == 0)
                // .Where(e => data.Date == null ? true : (e.Date.Year == data.Date.Year && e.Date.Month == data.Date.Month && e.Date.Day == data.Date.Day))
                .OrderByName<Article>(data.SortBy, data.SortDir == "desc")
                ;

            int count = await q.CountAsync();

            var list = await q
                .Skip(data.StartIndex)
                .Take(data.PageSize)
                .Select(e => new
                {
                    code = e.Code,
                    titreFr = e.TitreFr,
                    qteStock = e.QteStock,
                    stockMin = e.StockMin,
                    categorie = e.SousCategorie.Categorie.Libelle,
                })
                .ToListAsync()
                ;


            return Ok(new { list = list, count = count });
        }


        [HttpGet("{startIndex}/{pageSize}/{idCategorie}/{name}/{constructeur}/{idFournisseur}/{idSousCategorie}")]
        public async Task<ActionResult<Article>> GetAll(
            int startIndex, int pageSize, int idCategorie, string name, string constructeur, int idFournisseur, int idSousCategorie)
        {

            var q = _context.Articles
                .Where(r => name == "*" ? true : r.TitreAr.Contains(name))
                .Where(r => constructeur == "*" ? true : r.Constructeur.Equals(constructeur))
                .Where(r => idCategorie == 0 ? true : r.SousCategorie.IdCategorie == idCategorie)
                .Where(r => idSousCategorie == 0 ? true : r.IdSousCategorie == idSousCategorie)
                .Where(r => idFournisseur == 0 ? true : r.Fournitures.Any(c => c.IdFournisseur == idFournisseur))
                .OrderByDescending(o => o.Id)
                ;

            int count = await q.CountAsync();

            var list = await q.Skip(startIndex).Take(pageSize).ToListAsync();

            return Ok(new { list = list, count = count });
        }

        [HttpGet("{startIndex}/{pageSize}/{idCategorie}/{name}")]
        public async Task<ActionResult<Article>> GetProducts(int startIndex, int pageSize, int idCategorie, string name)
        {

            var q = _context.Articles
                .Where(r => name == "*" ? true : r.TitreAr.Contains(name))
                .Where(r => idCategorie == 0 ? true : r.SousCategorie.IdCategorie == idCategorie)
                .OrderByDescending(o => o.BestSell)
                .ThenByDescending(o => o.Id)
                ;

            int count = await q.CountAsync();

            var list = await q.Skip(startIndex).Take(pageSize).ToListAsync();

            return Ok(new { list = list, count = count });
        }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{idFournisseur}/{idCategorie}/{idArticle}")]
        public async Task<ActionResult<Article>> GetForCommande(
           int startIndex, int pageSize, string sortBy, string sortDir, int idFournisseur, int idCategorie, int idArticle)
        {

            var q = _context.Articles
                .Where(r => idArticle == 0 ? true : r.Id == idArticle)
                .Where(r => idFournisseur == 0 ? true : r.Fournitures.Any(c => c.IdFournisseur == idFournisseur))
                .Where(r => idCategorie == 0 ? true : r.SousCategorie.IdCategorie == idCategorie)
                .OrderByName<Article>(sortBy, sortDir == "desc")
                .Select(e => new
                {
                    id = e.Id,
                    article = e.TitreFr,
                    qteStock = e.QteStock,
                    prixMin = e.Fournitures.Min(m => m.PrixAchat),
                    qte = 0
                })
                ;

            int count = await q.CountAsync();

            var list = await q.Skip(startIndex).Take(pageSize).ToListAsync();

            return Ok(new { list = list, count = count });
        }




        [HttpPost]
        public async Task<IActionResult> GetAndSearch(Search s)
        {

            var q = _context.Articles
                .Where(e => s.Critere == "" ? true : e.TitreFr.Contains(s.Critere) || e.TitreAr.Contains(s.Critere) || e.Constructeur.Contains(s.Critere) || e.Code.Contains(s.Critere))
                .Where(e => s.Categorie == 0 ? true : e.SousCategorie.IdCategorie == s.Categorie)
                .Where(e => s.Fournisseur == "" ? true :
                    e.Fournitures.Any(f => f.Fournisseur.Nom.Contains(s.Fournisseur) || f.Fournisseur.Societe.Contains(s.Fournisseur) || f.Fournisseur.Prenom.Contains(s.Fournisseur)))
                .OrderByDescending(o => o.Id)
                ;

            int count = await q.CountAsync();

            var list = await q.Skip(s.StartIndex).Take(s.PageSize).ToListAsync();

            return Ok(new { list = list, count = count });
        }

        [HttpGet("{year}/{idCategorie}")]
        public async Task<IActionResult> GetTop50Sell(int year, int idCategorie)
        {
            var list = await _context.DetailCmds
                .Where(e => e.Commande.Date.Year == year && idCategorie == 0 ? true : e.Article.SousCategorie.IdCategorie == idCategorie)
                // .Where(e => )
                .GroupBy(e => e.Article.TitreFr)
                // .Distinct()
                .Select(e => new
                {
                    article = e.Key,
                    count = e.Count()
                    // count = e.Sum(c => c.DetailCmds.Sum(s => s.PrixVente * s.QtePrise))
                })
                .OrderByDescending(e => e.count)
                .Take(50)
                .ToListAsync()
                ;

            return Ok(new { list, count = list.Count });
        }



        [HttpGet("{year}/{idCategorie}")]
        public async Task<IActionResult> GetTop50Sell2(int year, int idCategorie)
        {
            var list = await _context.Articles
                .Where(e => e.DetailCmds.Any(d => d.Commande.Date.Year == year) && idCategorie == 0 ? true : e.SousCategorie.IdCategorie == idCategorie)
                // .Where(e => )
                // .GroupBy(e => e.Id)
                .Distinct()
                .Select(e => new
                {
                    article = e.TitreFr,
                    count = e.DetailCmds.Count
                    // count = e.Sum(c => c.DetailCmds.Sum(s => s.PrixVente * s.QtePrise))
                })
                .OrderByDescending(e => e.count)
                .Take(50)
                .ToListAsync()
                ;

            return Ok(new { list, count = list.Count });
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> ChiffreParCategorie(int year)
        {
            var list = await _context.Categories
                .Where(e => e.SousCategories.Any(s => s.Articles.Any(a => a.DetailCmds.Any(d => d.Commande.Date.Year == year))))
                .Select(e => new
                {
                    categorie = e.Libelle,
                    sum = e.SousCategories.SelectMany(s => s.Articles.Select(a => a.DetailCmds.Sum(d => d.PrixVente * d.QtePrise)))
                    // count = e.Sum(c => c.DetailCmds.Sum(s => s.PrixVente * s.QtePrise))
                })
                .ToListAsync()
                ;

            return Ok(list);
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> ChiffreParCategorie2(int year)
        {
            var list = await _context.Articles
                .Where(e => e.DetailCmds.Any(d => d.Commande.Date.Year == year))
                // .GroupBy(e => e.Id)
                .Select(e => new
                {
                    categorie = e.SousCategorie.Categorie.Libelle,
                    sum = e.DetailCmds.Sum(s => s.Total)
                    // count = e.Sum(c => c.DetailCmds.Sum(s => s.PrixVente * s.QtePrise))
                })
                // .Distinct()
                .OrderByDescending(e => e.sum)
                // .Take(50)
                .ToListAsync()
                ;

            return Ok(list);
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetConst()
        {
            var list = await _context.Articles
                .Select(d => d.Constructeur)
                .Distinct()
                .ToListAsync()
                ;

            return Ok(list);
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetForExcel()
        {
            var list = await _context.Categories
                // .GroupBy(e => e.SousCategorie.Categorie.Libelle)
                .Select(e => new
                {
                    libelle = e.Libelle,
                    articles = e.SousCategories.SelectMany(s => s.Articles.Select(a => new
                    {
                        code = a.Code,
                        titreFr = a.TitreFr,
                        titreAr = a.TitreAr,
                        unite = a.Unite,
                        prixAchat = a.PrixUnitaire,
                        prixVenteMax = a.DetailCmds.Max(p => p.PrixVente),
                        stock = a.QteStock,
                        stockMin = a.StockMin,
                        constructeur = a.Constructeur,
                    }))
                })
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{year}/{idCategorie}")]
        public async Task<ActionResult<string>> GetHeightSell(int year, int idCategorie)
        {
            var y = year == 0 ? DateTime.Now.Year : year;

            //     .GroupBy(c => c.IdArticle)
            //     .Select(c => new
            //     {
            //         titreFr = c.Key,
            //         jv = c.Where(q => q.Commande.Date.Month == 0).Count(),
            //     })

            var q = _context.Articles
                .Where(e => e.DetailCmds.Any(c => c.Commande.Date.Year == y))
                .Where(e => idCategorie == 0 ? true : e.SousCategorie.IdCategorie == idCategorie)
                .OrderByDescending(c => c.DetailCmds.Count())
                .Take(50)
                .Select(c => new
                {
                    titre = c.TitreFr,
                    count = c.DetailCmds.Count(),
                    months = c.DetailCmds
                        // .OrderBy(e => e.Commande.Date.Month)
                        .Select(e => e.Commande.Date.Month)
                        ,
                    // month = c.DetailCmds.Select(e => new{
                    //     qte = e.QtePrise,
                    //     month = e.Commande.Date.Month
                    // }),
                    // fv = c.Where(q => q.Commande.Date.Month == 1).Count(),
                })
                ;
            int count = await q.CountAsync();
            var list = await q.ToListAsync();



            return Ok(new { list = list, count = count });
        }


        [HttpPost]
        public override async Task<ActionResult<Article>> Post(Article model)
        {
            var a = await _context.Articles.Where(e => e.Code == model.Code).FirstOrDefaultAsync();

            if (a != null)
            {
                return Ok(new { model = "code existe deja", code = -1 });
            }

            _context.Articles.Add(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(new { model = model, code = 1 });
        }

        // [HttpPut("{id}")]
        // public override async Task<IActionResult> Put([FromRoute] int id, [FromBody] Article model)
        // {

        //     var a = await _context.Articles.Where(e => e.Code == model.Code).FirstOrDefaultAsync();

        //     if (a != null)
        //     {
        //         return Ok(new { model =  "code existe deja", code = -1});
        //     }

        //     _context.Entry(model).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException ex)
        //     {
        //         return BadRequest(new { message = ex.Message });
        //     }

        //     return Ok(new { message =  "ok", code = 1});
        // }

        [HttpGet("{id}/{qte}")]
        public async Task<IActionResult> UpdateQte(int id, int qte)
        {
            var model = await _context.Articles.FindAsync(id);
            if (model == null)
            {
                return BadRequest("artice est null");
            }

            model.QteStock += qte;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateDateLastBuy(int id)
        {
            var model = await _context.Articles.FindAsync(id);
            if (model == null)
            {
                return BadRequest("artice est null");
            }

            model.DateDernierAchat = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(model);
        }

        [HttpGet("{idFournisseur}/{idCategorie}")]
        public async Task<ActionResult<Article>> FilterBy(int idFournisseur, int idCategorie)
        {
            var list = await _context.Articles
                .Where(r => idCategorie == 0 ? true : r.SousCategorie.IdCategorie == idCategorie)
                .Where(r => idFournisseur == 0 ? true : r.Fournitures.Any(c => c.IdFournisseur == idFournisseur))
                .OrderByDescending(o => o.Id)
                .ToListAsync()
                ;

            return Ok(list);
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<Article>> Get(int id)
        {
            return await _context.Articles
                    .Where(e => e.Id == id)
                    .Include(e => e.SousCategorie)
                    // .ThenInclude(e => e.Categorie)
                    .FirstOrDefaultAsync();
        }

        public class Search
        {
            public int StartIndex { get; set; }
            public int PageSize { get; set; }
            public string SortBy { get; set; }
            public string SortDir { get; set; }
            public string Critere { get; set; }
            public string Fournisseur { get; set; }
            public int Categorie { get; set; }
        }
    }
}