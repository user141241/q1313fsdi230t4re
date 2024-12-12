namespace GameNet.Models.Games.Labyrinthe;

public class LabyrintheViewModel
{
    public required List<Player> Players { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public Tile EndTile { get; set; }

    public required Tile[,] Tiles { get; set; }

    public Tile GetTileAt(int y, int x)
    {
        return Tiles[y, x];
    }

    public (int X, int Y) GetTilePosition(Tile tile)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Tiles[y, x] == tile)
                {
                    return (x, y);
                }
            }
        }
        throw new Exception("Tile not found in the maze.");
    }

}
