using System.IdentityModel.Tokens.Jwt;
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

    public AccountController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    [Route("registreer")]
    public async Task<ActionResult<IEnumerable<User>>> Registreer([FromBody] GebruikerMetWachwoord gebruikerMetWachtwoord)
    {
        if ((int)gebruikerMetWachtwoord.Gender >= 4 || (int)gebruikerMetWachtwoord.Gender < 0)
        {
            return Problem(detail:"Gender isn't a valid value!");
        }

        var resultaat = await _userManager.CreateAsync(gebruikerMetWachtwoord, gebruikerMetWachtwoord.Password);
        return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserWithID(String id)
    {
        if (_userManager == null)
        {
            return NotFound();
        }
        User user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetUserWithName(String username)
    {
        if (_userManager == null)
        {
            return NotFound();
        }
        User user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound();
        }
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
        var _user = await _userManager.FindByNameAsync(gebruikerLogin.UserName);
        if (_user != null)
            if (await _userManager.CheckPasswordAsync(_user, gebruikerLogin.Password))
            {
                var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh89awe98f89uawef9j8aw89hefawef"));

                var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName) };
                var roles = await _userManager.GetRolesAsync(_user);
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));
                var tokenOptions = new JwtSecurityToken
                (
                    issuer: "https://localhost:7277",
                    audience: "https://localhost:7277",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signingCredentials
                );
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });
            }

        return Unauthorized();
    }
}
