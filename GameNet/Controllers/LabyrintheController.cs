using GameNet.Models.Games.Labyrinthe;
using Microsoft.AspNetCore.Mvc;

namespace GameNet.Controllers;

public class LabyrintheController : Controller
{
    private static LabyrintheGame game = new LabyrintheGame();

    public IActionResult Index()
    {
        Console.WriteLine("OK GUYSSSSSS");
        var model = new LabyrintheViewModel
        {
            Width = 10,
            Height = 10,
            Tiles = game.GetTiles(),
            Players = game.Players,
            EndTile = game.EndTile
        };

        return View(model);
    }

    [HttpPost]
    [Route("Labyrinthe/MovePlayer/{username}/{direction}")]
    public IActionResult MovePlayer([FromRoute] string username, [FromRoute] string direction)
    {
        Console.WriteLine(username);
        Console.WriteLine("cool");
        var player = game.Players.FirstOrDefault(p => p.Username == username);
        
        if (player != null && Enum.TryParse<DirectionType>(direction, true, out var dir))
        {
            Console.WriteLine(player.Username);
            game.MovePlayer(player, dir);
            Console.WriteLine(dir);
        }
        
        return RedirectToAction("Index");
    }



}