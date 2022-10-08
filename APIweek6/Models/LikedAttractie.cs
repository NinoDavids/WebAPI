using System.ComponentModel.DataAnnotations;
using API.Models;
using APIweek6.Controllers;
using APIweek6.Data;
using Microsoft.AspNetCore.Identity;

namespace APIweek6.Models;

public class LikedAttractie
{
    public int AttractieId { get; set; }
    public Attractie Attractie { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }

    private LikedAttractie(int attractieId, string userId)
    {
        this.AttractieId = attractieId;
        this.UserId = userId;
    }
    
    public LikedAttractie(Attractie attractie, User user) : this (attractie.Id, user.Id)
    {
        this.Attractie = attractie;
        this.User = user;
    }
}