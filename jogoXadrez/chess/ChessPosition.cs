using jogoXadrez.Boardgame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.Chess
{
    public class ChessPosition
    {
        private char column;
        private int row;

        public ChessPosition(char column, int row)
        {
            if (column < 'a' || column > 'h' || row < 1 || row > 8)
            {
                throw new ChessException("Error instantiating ChessPosition. Valid values are from a1 to h8");
            }
            this.column = column;
            this.row = row;
        }

        public char Column
        {
            get { return column; }
        }

        public int Row
        {
            get { return row; }
        }

        public Position ToPosition()
        {
            return new Position(8 - row, column - 'a');
        }

        public static ChessPosition FromPosition(Position position)
        {
            return new ChessPosition((char)('a' + position.Column), 8 - position.Row);
        }

        public override string ToString()
        {
            return "" + column + row;
        }
    }
}
