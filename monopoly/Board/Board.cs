using System.Collections.Generic;

namespace monopoly
{
    public interface IBoard
    {
        void AddSquare(Square square);
        Square GetSquare(int position);
        int GetSquaresCount();
    }

    public class Board : IBoard
    {
        private List<Square> squares;

        public Board()
        {
            squares = new List<Square>();
        }

        public void AddSquare(Square square)
        {
            squares.Add(square);
        }

        public Square GetSquare(int position)
        {
            foreach (Square square in squares)
            {
                if (square.GetPosition() == position)
                {
                    return square;
                }
            }
            return null;
        }
        public int GetSquaresCount()
        {
            return squares.Count;
        }
    }
}
