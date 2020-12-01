using System;
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
    public class CommandesController : SuperController<Commande>
    {
        public CommandesController(MyDbContext context) : base(context) { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{id}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, int id)
        {
            // int i = typeof(T).FullName.LastIndexOf('.');
            // string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            var list = await _context.Commandes
                .Where(e => id == 0 ? true : e.IdClient == id)
                .OrderByName<Commande>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                .Include(e => e.Client)
                .ToListAsync()
                ;
            int count = await _context.Commandes.CountAsync();

            return Ok(new { list = list, count = count });
        }


        // [HttpGet("{year}")]
        // public async Task<ActionResult<string>> GetHeightSell(int year)
        // {
        //     var y = year == 0 ? 2019 : year;
        //     var list = await _context.Commandes
        //         .Where(e => e.Date.Year == y)
        //         .Select(e => new
        //         {
        //             article = e.
        //             janvier = e.DetailCmds.Where(d => d.Commande.Date.Month == 0).Select(d => d.IdArticle).Count()
        //         })
        //         .ToListAsync()
        //         ;



        //     return Ok(list);
        // }

        [HttpPost]
        public async Task<IActionResult> GetAllByDate(MyData data)
        {
            var toDay = DateTime.Now;
            // var d = DateTime.Compare(data.D1.Date, DateTime.Now.Date);

            var q = await _context.Commandes
                .Where(e => 
                // (e.Date.Year == toDay.Year && e.Date.Month == toDay.Month/* && e.Date.Day == toDay.Day*/) 
                // ||
                DateTime.Compare(e.Date.Date, data.D1.Date) >= 0 && DateTime.Compare(e.Date.Date, data.D2.Date) <= 0
                )
                .Where(e => data.IdClient == 0 ? true : e.IdClient == data.IdClient)
                .OrderByName<Commande>(data.SortBy, data.SortDir == "desc")
                .Include(e => e.DetailCmds)
                .Include(e => e.Client)
                .ToListAsync()
                ;


            int count = q.Count();

            decimal credit =  q.Sum(e => e.Credit);
            decimal avance = q.Sum(e => e.Avance);

            var list = q
                .Skip(data.StartIndex)
                .Take(data.PageSize)
                
                .ToList()
                ;

            return Ok(new { list = list, count = count, credit, avance });
        }

        [HttpGet("{value}")]
        public async Task<ActionResult<string>> AutoCompleteClient(string value)
        {
            var list = await _context.Commandes
                .Select(d => d.NomClient)
                .Distinct()
                .Where(e => e.Contains(value))
                .Take(10)
                .ToListAsync()
                ;

            return Ok(list);
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> ChiffreParMois(int year)
        {
            var list = await _context.Commandes
                .Where(e => e.Date.Year == year)
                .GroupBy(e => e.Date.Month)
                .Select(e => new
                {
                    mois = e.Key,
                    chiffre = e.Sum(s => s.Total)
                })
                .ToListAsync()
                ;

            return Ok(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetCreditClients()
        {
            return Ok(await _context.Commandes.SumAsync(e => e.Credit));
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> GetChiffreParAnnee(int year)
        {
            return Ok(await _context.Commandes.Where(e => e.Date.Year == year).SumAsync(e => e.Total));
        }

        [HttpGet]
        public async Task<IActionResult> ChiffreParAnnee()
        {
            var list = await _context.Commandes
                .GroupBy(e => e.Date.Year)
                .Select(e => new
                {
                    year = e.Key,
                    chiffre = e.Sum(s => s.Total)
                })
                .ToListAsync()
                ;

            return Ok(list);
        }

        // [HttpGet("{year}")]
        // public async Task<IActionResult> ChiffreParCategorie(int year)
        // {
        //     var list = await _context.Commandes
        //         .Where(e => e.Date.Year == year)
        //         .GroupBy(e => e.Date)
        //         .Select(e => new
        //         {
        //             mois = e.Key,
        //             chiffre = e.Sum(s => s.Total)
        //         })
        //         .ToListAsync()
        //         ;

        //     return Ok(list);
        // }

        [HttpGet("{id}")]
        public override async Task<ActionResult<Commande>> Get(int id)
        {
            var model = await _context.Commandes
                .Where(e => e.Id == id)
                .Include(e => e.DetailCmds)
                .ThenInclude(e => e.Article)
                .FirstOrDefaultAsync()
                ;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreditByClient(int id)
        {
            decimal sum = await _context.Commandes
                            .Where(e => id == 0 ? true : e.IdClient == id)
                            .SumAsync(e => e.Credit)
                            ;

            return Ok(sum);
        }

    }

    public class MyData
    {
        public int StartIndex { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortDir { get; set; }
        public DateTime D1 { get; set; }
        public DateTime D2 { get; set; }
        public int IdClient { get; set; }
    }
}