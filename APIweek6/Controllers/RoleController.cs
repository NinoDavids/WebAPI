using APIweek6.Data;
using APIweek6.Models;
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
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<User> userManager)
        {
            roleManager = roleMgr;
            _userManager = userManager;
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
        {
            if (roleManager.Roles == null)
            {
                return NotFound();
            }
            return await roleManager.Roles.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles(string name)
        {
            if (!ModelState.IsValid)
            {
                return Problem("ModelState is invalid!");
            }
            IdentityResult resultaat = await roleManager.CreateAsync(new IdentityRole(name));
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        [HttpDelete("DeleteWithID/{id}")]
        public async Task<IActionResult> DeleteRolesByID(string id)
        {
            if (roleManager.Roles == null)
            {
                return NotFound();
            }
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            IdentityResult resultaat = await roleManager.DeleteAsync(role);
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        [HttpDelete("DeleteWithName/{name}")]
        public async Task<IActionResult> DeleteRolesByName(string name)
        {
            if (roleManager.Roles == null)
            {
                return NotFound();
            }
            IdentityRole role = await roleManager.FindByNameAsync(name);
            if (role == null)
            {
                return NotFound();
            }
            IdentityResult resultaat = await roleManager.DeleteAsync(role);
            return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        }

        [HttpPost("AddRole/{username}/{roleName}")]
        public async Task<IActionResult> AddRoleToUser(string username, string roleName)
        { 
            if (!ModelState.IsValid) 
            {
                return Problem("ModelState is invalid!");
            }
            User user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("No user found with that name!");
            }
            IdentityRole role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound("No role found with that name!");
            }
            await _userManager.AddToRoleAsync(user, role.Name);
            return StatusCode(201);
         }
        //
        // [HttpPost]
        // public async Task<IActionResult> RemoveRoleToUser(string name)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return Problem("ModelState is invalid!");
        //     }
        //     IdentityResult resultaat = await roleManager.CreateAsync(new IdentityRole(name));
        //     return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
        // }

        //public ViewResult Index() => View(roleManager.Roles);

    }
}
