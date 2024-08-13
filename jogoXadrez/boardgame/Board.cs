using jogoXadrez.boardgame.boardgame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace jogoXadrez.boardgame
{
    public class Board
    {
        private int rows;
        private int columns;
        private Piece[,] pieces;

        public Board(int rows, int columns)
        {
            if (rows < 1 || columns < 1)
            {
                throw new BoardException("Error creating board: there must be at least 1 row and 1 column");
            }
            this.rows = rows;
            this.columns = columns;
            pieces = new Piece[rows, columns];
        }

        public int GetRows()
        {
            return rows;
        }

        public int GetColumns()
        {
            return columns;
        }

        public Piece Piece(int row, int column)
        {
            if (!PositionExists(row, column))
            {
                throw new BoardException("Position not on the board");
            }
            return pieces[row, column];
        }

        public Piece Piece(Position position)
        {
            if (!PositionExists(position))
            {
                throw new BoardException("Position not on the board");
            }
            return pieces[position.Row, position.Column];
        }

        public void PlacePiece(Piece piece, Position position)
        {
            if (ThereIsAPiece(position))
            {
                throw new BoardException("There is already a piece on position " + position);
            }
            pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (!PositionExists(position))
            {
                throw new BoardException("Position not on the board");
            }
            if (Piece(position) == null)
            {
                return null;
            }
            Piece aux = Piece(position);
            aux.Position = null;
            pieces[position.Row, position.Column] = null;
            return aux;
        }

        public bool PositionExists(int row, int column)
        {
            return row >= 0 && row < rows && column >= 0 && column < columns;
        }

        public bool PositionExists(Position position)
        {
            return PositionExists(position.Row, position.Column);
        }

        public bool ThereIsAPiece(Position position)
        {
            if (!PositionExists(position))
            {
                throw new BoardException("Position not on the board");
            }
            return Piece(position) != null;
        }
    }
}

