namespace GameNet.Models.Games.TicTacToe
{
    public class TicTacToeCell
    {
        public int CoordX { get; set; } = 0;
        public int CoordY { get; set; } = 0;
        public TicTacToeSymbol Symbol { get; set; } = TicTacToeSymbol.EMPTY;
        public TicTacToeCell() { }
    }
}