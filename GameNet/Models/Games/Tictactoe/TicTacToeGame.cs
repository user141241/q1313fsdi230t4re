using GameNet.Models.Platform;

namespace GameNet.Models.Games.TicTacToe
{
    public class TicTacToeGame : ManageGame
    {
        // ATTRIBUTES
        public TicTacToeBoard MainBoard { get; set; } = new TicTacToeBoard();
        public Dictionary<Player, TicTacToeSymbol> SymbolOfPlayers { get; set; } = [];

        public TicTacToeGame()
        {
            CurrentPlayer = null;
            Players = new List<Player>();
        }

        // REQUESTS 
        public override bool CheckVictory()
        {
            // Analyse des lignes
            int numberTokenPlaced = 0;
            for (int i = 0; i < MainBoard.size; i++) 
            {
                for (int y = 0; y < MainBoard.size; y++) 
                {
                    TicTacToeCell cell = MainBoard.GetCellAtPosition(i, y);
                    if (cell != null && cell.Symbol == SymbolOfPlayers.GetValueOrDefault(CurrentPlayer)) {
                        numberTokenPlaced++;
                    }
                }
                if (numberTokenPlaced == MainBoard.size) 
                {
                    EndGame = true;
                    return true;
                }
                numberTokenPlaced = 0;
            }

            // Analyse des colonnes 
            numberTokenPlaced = 0;
            for (int y = 0; y < MainBoard.size; y++) 
            {
                for (int i = 0; i < MainBoard.size; i++) 
                {
                    TicTacToeCell cell = MainBoard.GetCellAtPosition(i, y);
                    if (cell != null && cell.Symbol == SymbolOfPlayers.GetValueOrDefault(CurrentPlayer)) {
                        numberTokenPlaced++;
                    }
                }
                if (numberTokenPlaced == MainBoard.size) 
                {
                    EndGame = true;
                    return true;
                }
                numberTokenPlaced = 0;
            }

            // Analyse de la première diagonale
            numberTokenPlaced = 0;
            for (int i = 0; i < MainBoard.size; i++) 
            {
                TicTacToeCell cell = MainBoard.GetCellAtPosition(i, i);
                if (cell != null && cell.Symbol == SymbolOfPlayers.GetValueOrDefault(CurrentPlayer)) 
                {
                    numberTokenPlaced++;
                }
                if (numberTokenPlaced == MainBoard.size) 
                {
                    EndGame = true;
                    return true;
                }
            }

            // Analyse de la seconde diagonale
            numberTokenPlaced = 0;
            for (int i = 0; i < MainBoard.size; i++) 
            {
                TicTacToeCell cell = MainBoard.GetCellAtPosition(i, MainBoard.size - 1 - i);
                if (cell != null && cell.Symbol == SymbolOfPlayers.GetValueOrDefault(CurrentPlayer)) 
                {
                    numberTokenPlaced++;
                }
                if (numberTokenPlaced == MainBoard.size) 
                {
                    EndGame = true;
                    return true;
                }
            }
            
            // Aucun gagnant
            return false;
        }

        public override List<Player> GetRankOrderByScore()
        {
            throw new NotImplementedException();
        }

        // COMMANDS 
        public void PlaceTokenAtPosition(int i, int y) 
        {
            if (!EndGame) 
            {
                MainBoard.SetCellAtPosition(i, y, SymbolOfPlayers.GetValueOrDefault(CurrentPlayer));
                if (!CheckVictory())
                {
                    ProcessTurn();
                }
            }
        }
    }
}