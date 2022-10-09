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
        [Authorize(Roles = "Medewerker")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikedAttractie>>> GetLikedAttractie()
        {
            return await _context.LikedAttractie.ToListAsync();
        }

        [Authorize(Roles = "Medewerker")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLikedAttractie(int id, LikedAttractie likedAttractie)
        {
            if (id != likedAttractie.AttractieId) return BadRequest();

            _context.Entry(likedAttractie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikedAttractieExists(id)) return NotFound();
                
                else throw;
            }

            return NoContent();
        }
        
        [Authorize(Roles = "Medewerker")]
        [HttpGet("getUsernames/{attractieName}")]
        public async Task<ActionResult<IEnumerable<string>>> GetPeopleWhoLikedAttractie(string attractieName)
        {
            var attractieList = await _context.Attractie.Where(x => x.name == attractieName).ToListAsync();
            
            if (attractieList.Count == 0 || attractieList.Count > 1) return NotFound();

            Attractie attractie = attractieList[0];

            var likedAttractieList = await _context.LikedAttractie.Where(x => x.AttractieId == attractie.Id).ToListAsync();
            
            List<string> usernames = new List<string>();

            foreach (var likedAttractie in likedAttractieList)
            {
                User user = await _userManager.FindByIdAsync(likedAttractie.UserId);
                usernames.Add(user.UserName);
            }

            return usernames;
        }
        [Authorize(Roles = "Medewerker")]
        [HttpGet("getAmountOfLikes/{attractieName}")]
        public async Task<ActionResult<string>> GetAmountOfLikesOnAttractie(string attractieName)
        {
            var attractieList = await _context.Attractie.Where(x => x.name == attractieName).ToListAsync();
            
            if (attractieList.Count == 0 || attractieList.Count > 1) return NotFound();

            Attractie attractie = attractieList[0];

            List<LikedAttractie> likedAttracties = await _context.LikedAttractie.Where(x => x.AttractieId == attractie.Id).ToListAsync();

            if (likedAttracties.Count == 1) return attractie.name + " has: " + likedAttracties.Count + " like";
                
            return attractie.name + " has: " + likedAttracties.Count + " likes";
        }
        [Authorize(Roles = "Medewerker")]
        [HttpGet("getLikes/{attractieName}")]
        public async Task<ActionResult<IEnumerable<LikedAttractie>>> GetLikesOnAttractie(string attractieName)
        {
            var attractieList = await _context.Attractie.Where(x => x.name == attractieName).ToListAsync();
            
            if (attractieList.Count == 0 || attractieList.Count > 1) return NotFound();

            Attractie attractie = attractieList[0];

            return await _context.LikedAttractie.Where(x => x.AttractieId == attractie.Id).ToListAsync();
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LikedAttractie>> PostLikedAttractie([FromQuery] int attractieId)
        {
            Attractie attractie = await _context.Attractie.FindAsync(attractieId);
            User user = await _userManager.GetUserAsync(this.User);

            if (attractie == null || user == null) return NotFound();
            
            List<LikedAttractie> likedAttracties = await _context.LikedAttractie.ToListAsync();

            for (int i = 0; i < likedAttracties.Count; i++)
            {
                if (likedAttracties[i].Attractie == attractie && likedAttracties[i].User == user) return Problem("Attraction was already liked by: " + user.UserName + "!");
            }
            
            LikedAttractie likedAttractie = new LikedAttractie(attractie, user);

            _context.LikedAttractie.Add(likedAttractie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLikedAttractie", new { id = likedAttractie.AttractieId }, likedAttractie);
        }

        // DELETE: api/LikedAttractie/5
        [Authorize(Roles = "Gast,Medewerker")]
        [HttpDelete]
        public async Task<IActionResult> DeleteLikedAttractie([FromQuery] int attractieId)
        {
            Attractie attractie = await _context.Attractie.FindAsync(attractieId);
            User user = await _userManager.GetUserAsync(this.User);

            if (attractie == null || user == null) return NotFound();
            
            List<LikedAttractie> likedAttracties = await _context.LikedAttractie.ToListAsync();

            for (int i = 0; i < likedAttracties.Count; i++)
            {
                if (likedAttracties[i].Attractie == attractie && likedAttracties[i].User == user) return Problem("Attraction was already liked by: " + user.UserName + "!");
            }
            
            LikedAttractie likedAttractie = new LikedAttractie(attractie, user);

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
