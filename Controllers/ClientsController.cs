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
    public class ClientsController : SuperController<Client>
    {
        public ClientsController(MyDbContext context) : base(context) { }

         [HttpGet("{value}")]
        public async Task<ActionResult<string>> AutoCompleteClient(string value)
        {
            var list = await _context.Clients
                .Where(e => e.Nom.Contains(value))
                .Take(10)
                .ToListAsync()
                ;

            return Ok(list);
        }
    }
}