using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIweek6.Controllers;

public class GebruikerMetWachwoord : IdentityUser
{
    public string? Password { get; init; }
}

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [Route("registreer")]
    public async Task<ActionResult<IEnumerable<Attractie>>> Registreer([FromBody] GebruikerMetWachwoord gebruikerMetWachwoord)
    {
        var resultaat = await _userManager.CreateAsync(gebruikerMetWachwoord, gebruikerMetWachwoord.Password);
        return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
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
        {
            await _signInManager.SignInAsync(_user, true);
            return Ok();
        }

        return Unauthorized();
    }
}
