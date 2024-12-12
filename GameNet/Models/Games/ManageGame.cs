using GameNet.Models.Platform;

namespace GameNet.Models.Games
{
    public abstract class ManageGame
    {
        // ATTRIBUTES
        public int MinCapacityPlayer { get; set; } = 2;
        public int MaxCapacityPlayer { get; set; } = 2;
        public Player? CurrentPlayer { get; set; } = null;
        public List<Player> Players { get; set; } = new List<Player>();
        public bool EndGame { get; set; } = false;

        // REQUESTS
        public abstract bool CheckVictory();
        public abstract List<Player> GetRankOrderByScore();
        public Player GetWinner()
        {
            return GetRankOrderByScore()[0];
        }

        // COMMANDS
        public void StopGame()
        {
            EndGame = true;
        }
        public void ProcessTurn()
        {
            if (!EndGame) {
                if (CurrentPlayer != null)
                {
                    int position = Players.IndexOf(CurrentPlayer);
                    CurrentPlayer = position + 1 == Players.Count ? Players[0] : Players[position];
                }
            }
        }
    }
}
