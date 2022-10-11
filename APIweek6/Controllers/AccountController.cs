using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using API.Models;
using APIweek6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;

namespace APIweek6.Controllers;

public class GebruikerMetWachwoord : User
{
    public Gender Gender { get; init; }
    public string? Password { get; init; }
}

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [Route("registreer")]
    public async Task<ActionResult<IEnumerable<User>>> Registreer([FromBody] GebruikerMetWachwoord gebruikerMetWachtwoord)
    {
        if ((int)gebruikerMetWachtwoord.Gender >= 4 || (int)gebruikerMetWachtwoord.Gender < 0) return Problem(detail:"Gender isn't a valid value!");

        var resultaat = await _userManager.CreateAsync(gebruikerMetWachtwoord, gebruikerMetWachtwoord.Password);
        return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
    }
    [Authorize(Roles = "Medewerker")]
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserWithID(String id)
    {
        User user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();
        return user;
    }
    [Authorize(Roles = "Medewerker")]
    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetUserWithName(String username)
    {
        User user = await _userManager.FindByNameAsync(username);
        if (user == null) return NotFound();
        return user;
    }

    public class GebruikerLogin
    {
        public string? UserName { get; init; }
        public string? Password { get; init; }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] GebruikerLogin gebruikerLogin)
    {
        User user = await _userManager.FindByNameAsync(gebruikerLogin.UserName);
        if (user != null)
        {
            await _signInManager.SignInAsync(user, true);
            if (_userManager.GetRolesAsync(user) != null)
            {
                await _userManager.AddToRoleAsync(user, "Gast");
            }

            return Ok();
        }

        return Unauthorized();
    }
}
