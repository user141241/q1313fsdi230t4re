namespace GameNet.Models.Games.Labyrinthe;

public class Player
{
    public string Username { get; set; }
    public Tile CurrentTile { get; set; }

    public Player(string username, Tile startingTile)
    {
        Username = username;
        CurrentTile = startingTile;
    }
}