using API.Models;
using Microsoft.AspNetCore.Identity;

namespace APIweek6.Models;

public class User
{        
    public enum Gender { Male, Female, Other, Hidden }
    public class ApplicationUser : IdentityUser
    {
        public Gender Gender { get; set; }
        
    }
}