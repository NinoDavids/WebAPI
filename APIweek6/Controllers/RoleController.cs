using APIweek6.Data;
using APIweek6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace APIweek6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<User> userManager)
        {
            _roleManager = roleMgr;
            _userManager = userManager;
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        [Authorize(Roles = "Medewerker")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
        {
            if (_roleManager.Roles == null) return NotFound();
            return await _roleManager.Roles.ToListAsync();
        }

        [Authorize(Roles = "Medewerker")]
        [HttpPost]
        public async Task<IActionResult> CreateRoles(string name)
        {
            if (!ModelState.IsValid) return Problem("ModelState is invalid!");

            IdentityResult resultaat = await _roleManager.CreateAsync(new IdentityRole(name));
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        [Authorize(Roles = "Medewerker")]
        [HttpDelete("DeleteWithID/{id}")]
        public async Task<IActionResult> DeleteRolesById(string id)
        {
            if (_roleManager.Roles == null) return NotFound();

            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            IdentityResult resultaat = await _roleManager.DeleteAsync(role);
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        [Authorize(Roles = "Medewerker")]
        [HttpDelete("DeleteWithName/{name}")]
        public async Task<IActionResult> DeleteRolesByName(string name)
        {
            if (_roleManager.Roles == null) return NotFound();

            IdentityRole role = await _roleManager.FindByNameAsync(name);
            if (role == null) return NotFound();

            IdentityResult resultaat = await _roleManager.DeleteAsync(role);
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        [Authorize(Roles = "Gast,Medewerker")]
        [HttpPost("AddRole/{username}/{roleName}")]
        public async Task<IActionResult> AddRoleToUser(string username, string roleName)
        {
            if (!ModelState.IsValid) return Problem("ModelState is invalid!");

            User user = await _userManager.FindByNameAsync(username);
            IdentityRole role = await _roleManager.FindByNameAsync(roleName);
            if (user == null) return NotFound("No user or role found with that name!");
            
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded) return StatusCode(201);
            return Problem();
         }

        [Authorize(Roles = "Medewerker")]
        [HttpDelete("RemoveRole/{username}/{roleName}")]
        public async Task<IActionResult> RemoveRoleFromUser(string username, string roleName)
        {
            if (!ModelState.IsValid)
            {
                return Problem("ModelState is invalid!");
            }

            User user = await _userManager.FindByNameAsync(username);
            IdentityRole role = await _roleManager.FindByNameAsync(roleName);
            if (user == null || role == null) return NotFound("No user or role found with that name!");

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded) return StatusCode(201);
            return Problem("User doesn't have the: " + role + " role!");
        }
    }
}
