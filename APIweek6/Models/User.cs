using API.Models;
using Microsoft.AspNetCore.Identity;

namespace APIweek6.Models;
public enum Gender
{ Hidden = 0, Male = 1, Female = 2, Other = 3}
public class User : IdentityUser
{
    public Gender Gender { get; set; }
}
