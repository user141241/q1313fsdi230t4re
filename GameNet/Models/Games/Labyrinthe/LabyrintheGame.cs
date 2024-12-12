namespace GameNet.Models.Games.Labyrinthe;

public class LabyrintheGame
{
    private Tile[,] Tiles { get; set; }
    private readonly Random _rand = new Random();
    public readonly Tile EndTile;
    public List<Player> Players { get; set; } = [];
    public readonly Tile StartTile;
    
    public LabyrintheGame()
    {
        BuildLabyrinthe(10, 10);
        StartTile = Tiles[5, 0];
        EndTile = Tiles[5, 9];
        Players.Add(new Player("Player1", StartTile));
        Players.Add(new Player("Player2", StartTile));
        
    }

    public void MovePlayer(Player player, DirectionType direction)
    {

        if (player.CurrentTile.NeighborTiles.TryGetValue(direction, out Tile? value))
        {
            player.CurrentTile = value;
        }
        else
        {
            Console.WriteLine("Déplacement impossible, y'a un mur là !");
        }
    }

    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }

    public void BuildLabyrinthe(int width, int height)
    {
        Tiles = new Tile[height, width];
        var cellIds = new int[height, width];
        var walls = new List<(int x1, int y1, DirectionType dir1, int x2, int y2, DirectionType dir2)>();

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Tiles[y, x] = new Tile();
                cellIds[y, x] = y * 10 + x;

                
                if (x < width - 1)
                    walls.Add((x, y, DirectionType.Right, x + 1, y, DirectionType.Left));
                if (y < height - 1)
                    walls.Add((x, y, DirectionType.Bottom, x, y + 1, DirectionType.Top));
            }
        }

       
        walls = walls.OrderBy(_ => _rand.Next()).ToList(); // Melange

        foreach (var (x1, y1, dir1, x2, y2, dir2) in walls)
        {
            var id1 = cellIds[y1, x1];
            var id2 = cellIds[y2, x2];

            if (id1 != id2)
            {
                Tiles[y1, x1].NeighborTiles[dir1] = Tiles[y2, x2];
                Tiles[y2, x2].NeighborTiles[dir2] = Tiles[y1, x1];
                
                var oldId = id2;
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (cellIds[y, x] == oldId)
                            cellIds[y, x] = id1;
                    }
                }
            }
        }
    }

    public Tile[,] GetTiles()
    {
        return Tiles;
    }
}