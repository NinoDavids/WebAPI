using Microsoft.AspNetCore.Identity;

namespace APIweek6.Models;

public class User
{        
    public enum Gender { Male, Female, Expired, Locked }
    public class ApplicationUser : IdentityUser
    {
        public Gender Gender { get; set; }
    }
}