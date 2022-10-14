using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using APIweek6.Data;
using APIweek6.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIweek6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractieController : ControllerBase
    {
        private readonly PretparkContext _context;
        private readonly int pageSize = 10;


        public AttractieController(PretparkContext context)
        {
            _context = context;
        }

        // GET: api/Attractie
        [Authorize(Roles = "Gast,Medewerker")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attractie>>> GetAttractie()
        {
            return await _context.Attractie.ToListAsync();
        }

        [Authorize(Roles = "Gast,Medewerker")]
        [HttpGet("page/{page}/{ascdec?}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAttractieByPage(int page, bool? ascdec)
        {
            List<Attractie> attractieListFull = new List<Attractie>();
            if (ascdec == null || ascdec == false)
            {
                attractieListFull = await _context.Attractie.OrderBy(a=> a.Id).ToListAsync();
            }
            else
            {
                attractieListFull = await _context.Attractie.OrderByDescending(a=> a.Id).ToListAsync();
            }

            if (attractieListFull.Count == 0) return NotFound("there are no attractions!");

            List<string> attractieListToSend = new List<string>();

            int startCount = (page * pageSize) - pageSize;
            for (int i = startCount; (i < startCount + pageSize); i++)
            {
                if (i > (attractieListFull.Count -1)) break;
                attractieListToSend.Add("Id: " + attractieListFull[i].Id + " Name: " + attractieListFull[i].name + " spooky: " + attractieListFull[i].spooky + " buildyear: " + attractieListFull[i].buildYeaar);
            }

            if (attractieListToSend.Count == 0)
            {
                var results = Enumerable.Repeat(pageSize, page/pageSize).ToList();
                if(page % pageSize != 0) results.Add(page % pageSize);
                return Problem("There are no attractions on this page: page " + page + ", there are only " + attractieListFull.Count + " attractions in total (" + results.Count + " pages of attractions!)");
            }

            return attractieListToSend;
        }

        [Authorize(Roles = "Gast,Medewerker")]
        [HttpGet("filter/{filter}/{minValue}/{maxValue}/{ascdec?}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAttractieByFilter(string filter, int minValue, int maxValue ,bool? ascdec)
        {
            List<Attractie> attractieListFull = await _context.Attractie.ToListAsync();

            if (attractieListFull.Count == 0) return NotFound("there are no attractions!");
            List<Attractie> attractieListFiltered = new List<Attractie>();

            if (ascdec != null || ascdec == false)
            {
                switch (filter.ToLower())
                {
                    case "spooky": case "engheid":
                        attractieListFiltered = attractieListFull.OrderBy(a=>a.spooky).Where(a => a.spooky >= minValue && a.spooky <= maxValue).ToList();
                        break;
                    case "buildyear": case "bouwjaar": case "year": case "jaar":
                        attractieListFiltered = attractieListFull.OrderBy(a=>a.buildYeaar).Where(a => a.buildYeaar.Year >= minValue && a.buildYeaar.Year <= maxValue).ToList();
                        break;
                    default:
                        return Problem("your sort parameter isn't a valid value! (valid values: spooky (engheid), buildyear (bouwjaar, year, jaar))");
                }
            }
            else
            {
                switch (filter.ToLower())
                {
                    case "spooky": case "engheid":
                        attractieListFiltered = attractieListFull.OrderByDescending(a=>a.spooky).Where(a => a.spooky >= minValue && a.spooky <= maxValue).ToList();
                        break;
                    case "buildyear": case "bouwjaar": case "year": case "jaar":
                        attractieListFiltered = attractieListFull.OrderByDescending(a=>a.buildYeaar).Where(a => a.buildYeaar.Year >= minValue && a.buildYeaar.Year <= maxValue).ToList();
                        break;
                    default:
                        return Problem("your sort parameter isn't a valid value! (valid values: spooky (engheid), buildyear (bouwjaar, year, jaar))");
                }
            }

            if (attractieListFiltered.Count == 0) return Problem("No attractions found with this filter!");

            List<string> attractieList = new List<string>();
            foreach (Attractie attractie in attractieListFiltered)
            {
                attractieList.Add("Id: " + attractie.Id + " Name: " + attractie.name + " spooky: " + attractie.spooky + " buildyear: " + attractie.buildYeaar);
            }

            return attractieList;
        }

        [Authorize(Roles = "Gast,Medewerker")]
        [HttpGet("sort/{sort}/{ascdec?}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAttractieBySort(string sort, bool? ascdec)
        {
            List<Attractie> attractieListFull = await _context.Attractie.ToListAsync();

            if (attractieListFull.Count == 0) return NotFound("there are no attractions!");
            List<Attractie> attractieListSorted = new List<Attractie>();

            if (ascdec != null || ascdec == false)
            {
                switch (sort.ToLower())
                {
                    case "name": case "naam":
                        attractieListSorted = attractieListFull.OrderBy(a=>a.name).ToList();
                        break;
                    case "spooky": case "engheid":
                        attractieListSorted = attractieListFull.OrderBy(a=>a.spooky).ToList();
                        break;
                    case "buildyear": case "bouwjaar": case "year": case "jaar":
                        attractieListSorted = attractieListFull.OrderByDescending(a=>a.buildYeaar).ToList();
                        break;
                    case "id": case "identity": case "number": case "nummer":
                        attractieListSorted = attractieListFull.OrderBy(a=>a.Id).ToList();
                        break;
                    default:
                        return Problem("your sort parameter isn't a valid value! (valid values: name (naam), spooky (engheid), buildyear (bouwjaar, year, jaar), id (identity, number, nummer))");
                }
            }
            else
            {
                switch (sort.ToLower())
                {
                    case "name": case "naam":
                        attractieListSorted = attractieListFull.OrderByDescending(a=>a.name).ToList();
                        break;
                    case "spooky": case "engheid":
                        attractieListSorted = attractieListFull.OrderByDescending(a=>a.spooky).ToList();
                        break;
                    case "buildyear": case "bouwjaar": case "year": case "jaar":
                        attractieListSorted = attractieListFull.OrderByDescending(a=>a.buildYeaar).ToList();
                        break;
                    case "id": case "identity": case "number": case "nummer":
                        attractieListSorted = attractieListFull.OrderByDescending(a=>a.Id).ToList();
                        break;
                    default:
                        return Problem("your sort parameter isn't a valid value! (valid values: name (naam), spooky (engheid), buildyear (bouwjaar, year, jaar), id (identity, number, nummer))");
                }
            }

            List<string> attractieList = new List<string>();
            foreach (Attractie attractie in attractieListSorted)
            {
                attractieList.Add("Id: " + attractie.Id + " Name: " + attractie.name + " spooky: " + attractie.spooky + " buildyear: " + attractie.buildYeaar);
            }

            return attractieList;
        }

        // GET: api/Attractie/5
        [Authorize(Roles = "Gast,Medewerker")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Attractie>> GetAttractie(int id)
        {
            var attractie = await _context.Attractie.FindAsync(id);

            if (attractie == null) return NotFound();

            return attractie;
        }

        // PUT: api/Attractie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Medewerker")]
        [HttpPut]
        public async Task<IActionResult> PutAttractie(string naam, string? nieuwenaam, int? nieuwespooky)
        {
            var attractie = _context.Attractie.Single(x => x.name.ToLower() == naam.ToLower());
            if (attractie.Equals(null)) return Problem();

            if (nieuwenaam == null) nieuwenaam = attractie.name;
            if (nieuwespooky == null) nieuwespooky = attractie.spooky;

            try
            {
                attractie.name = nieuwenaam;
                attractie.spooky = nieuwespooky.Value;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
            }
            return Ok();
        }

        // POST: api/Attractie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Medewerker")]
        [HttpPost]
        public async Task<ActionResult<Attractie>> PostAttractie(Attractie attractie)
        {
            var attractieList = await _context.Attractie.Where(x => x.name == attractie.name).ToListAsync();

            if (attractieList.Count != 0) return Problem("Attraction already exists with than name!");

            _context.Attractie.Add(attractie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAttractie", new { id = attractie.Id }, attractie);
        }

        // DELETE: api/Attractie/5
        [Authorize(Roles = "Medewerker")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttractie(int id)
        {
            var attractie = await _context.Attractie.FindAsync(id);
            if (attractie == null) return NotFound();

            _context.Attractie.Remove(attractie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Gast,Medewerker")]
        [HttpGet("{id}/Name")]
        public async Task<ActionResult<String>> GetAttractieName(int id)
        {
            var attractie = await _context.Attractie.FindAsync(id);

            if (attractie == null) return NotFound();

            return attractie.name;
        }

        [Authorize(Roles = "Gast,Medewerker")]
        [HttpGet("{id}/Spooky")]
        public async Task<ActionResult<int>> GetAttractieSpooky(int id)
        {
            var attractie = await _context.Attractie.FindAsync(id);

            if (attractie == null) return NotFound();

            return attractie.spooky;
        }

        [Authorize(Roles = "Gast,Medewerker")]
        [HttpGet("{id}/BuildYear")]
        public async Task<ActionResult<DateTime>> GetAttractieDate(int id)
        {
            var attractie = await _context.Attractie.FindAsync(id);

            if (attractie == null) return NotFound();

            return attractie.buildYeaar;
        }

        private bool AttractieExists(int id)
        {
            return (_context.Attractie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
