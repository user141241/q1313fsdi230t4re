namespace GameNet.Models.Games.TicTacToe
{
    public class TicTacToeBoard
    {
        // ATTRIBUTES

        public readonly int size = 3;
        public List<TicTacToeCell> Cells { get; set; } = new List<TicTacToeCell>();

        // CONSTRUCTORS

        public TicTacToeBoard() 
        {
            InitializeBoard();
        }

        // REQUESTS

        public TicTacToeCell? GetCellAtPosition(int x, int y) 
        {
            for (int k = 0; k < Cells.Count; k++) 
            {
                TicTacToeCell c = Cells[k];
                if (c.CoordX == x && c.CoordY == y) {
                    return c;
                }
            }
            return null;
        }

        // COMMANDS

        public void SetCellAtPosition(int x, int y, TicTacToeSymbol symbol) 
        {
            TicTacToeCell cell = GetCellAtPosition(x, y);
            if (cell != null && cell.Symbol == TicTacToeSymbol.EMPTY) 
            {
                cell.Symbol = symbol;  
            }
        }

        private void InitializeBoard()  
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TicTacToeCell ticTacToeCell = new TicTacToeCell();
                    ticTacToeCell.CoordX = i;
                    ticTacToeCell.CoordY = j;
                    Cells.Add(ticTacToeCell);
                }
            }
        }
    } 
}