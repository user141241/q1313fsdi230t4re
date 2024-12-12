namespace GameNet.Models.Games.Labyrinthe;

public class Tile
{
    public Dictionary<DirectionType, Tile> NeighborTiles { get; set; } = new Dictionary<DirectionType, Tile>();

    public int GetImageId()
    {
        return NeighborTiles.Keys.Aggregate(0, (cpt, key) => cpt | (int)key);
    }
}
