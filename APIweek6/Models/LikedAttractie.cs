using API.Models;

namespace APIweek6.Models;

public class LikedAttractie
{
    public string UserId { get; set; }
    public User User { get; set; }
    public int AttractieId { get; set; }
    public Attractie Attractie { get; set; }
}