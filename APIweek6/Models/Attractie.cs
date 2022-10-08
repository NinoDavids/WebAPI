using System.ComponentModel.DataAnnotations;
using APIweek6.Models;

namespace API.Models;

public class Attractie
{
    [Key]
    public int Id { get; set; }
    public string name { get; set; }
    public int spooky { get; set; }
    public DateTime buildYeaar { get; set; }
    public ICollection<LikedAttractie>? LikedAttracties { get; set; }
}
