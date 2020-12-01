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
    public class DevissController : SuperController<Devis>
    {
        public DevissController(MyDbContext context) : base(context) { }

        
         [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoDevis(int id)
        {
            // var list = await _context.Achats
            //                 .Where(e => e.Id == id)
            //                 .Include(e => e.Fournitures)
            //                 .ThenInclude(e => e.Article)
            //                 .FirstOrDefaultAsync()
            //                 ;

            return Ok(await _context.Deviss
                            .Where(e => e.Id == id)
                            .Include(e => e.DevisActicles)
                            .ThenInclude(e => e.Article)
                            .FirstOrDefaultAsync()
                            );
        }


    }

   
}