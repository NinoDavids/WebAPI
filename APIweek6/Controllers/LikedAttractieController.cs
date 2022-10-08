using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIweek6.Data;
using APIweek6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace APIweek6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikedAttractieController : ControllerBase
    {
        private readonly PretparkContext _context;
        private readonly UserManager<User> _userManager;

        public LikedAttractieController(PretparkContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        
        // GET: api/LikedAttractie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikedAttractie>>> GetLikedAttractie()
        {
            return await _context.LikedAttractie.ToListAsync();
        }

        // GET: api/LikedAttractie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LikedAttractie>> GetLikedAttractie(int id)
        {
            var likedAttractie = await _context.LikedAttractie.FindAsync(id);

            if (likedAttractie == null)
            {
                return NotFound();
            }

            return likedAttractie;
        }

        // PUT: api/LikedAttractie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLikedAttractie(int id, LikedAttractie likedAttractie)
        {
            if (id != likedAttractie.AttractieId)
            {
                return BadRequest();
            }

            _context.Entry(likedAttractie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikedAttractieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LikedAttractie>> PostLikedAttractie([FromQuery] int id)
        {
            Attractie attractie = await _context.Attractie.FindAsync(id);
            User user = await _userManager.GetUserAsync(this.User);

            if (attractie == null || user == null)
            {
                return NotFound();
            }
            List<LikedAttractie> likedAttracties = await _context.LikedAttractie.ToListAsync();

            for (int i = 0; i < likedAttracties.Count; i++)
            {
                if (likedAttracties[i].Attractie == attractie && likedAttracties[i].User == user)
                {
                    return Problem("Attraction was already liked by: " + user.UserName + "!");
                }
            }
            
            LikedAttractie likedAttractie = new LikedAttractie(attractie, user);

            _context.LikedAttractie.Add(likedAttractie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLikedAttractie", new { id = likedAttractie.AttractieId }, likedAttractie);
        }

        // DELETE: api/LikedAttractie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLikedAttractie(int id)
        {
            var likedAttractie = await _context.LikedAttractie.FindAsync(id);
            if (likedAttractie == null)
            {
                return NotFound();
            }

            _context.LikedAttractie.Remove(likedAttractie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikedAttractieExists(int id)
        {
            return (_context.LikedAttractie?.Any(e => e.AttractieId == id)).GetValueOrDefault();
        }

    }
    

}
