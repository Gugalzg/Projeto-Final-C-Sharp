using jogoXadrez.boardgame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.chess.pieces
{
    public class Knight : ChessPiece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "N";
        }

        private bool CanMove(Position position)
        {
            ChessPiece p = (ChessPiece)GetBoard().Piece(position);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[GetBoard().GetRows(), GetBoard().GetColumns()];
            Position p = new Position(0, 0);

            // Above
            p.SetValues(Position.Row - 1, Position.Column - 2);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Below
            p.SetValues(Position.Row - 2, Position.Column - 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Left
            p.SetValues(Position.Row - 2, Position.Column + 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Right
            p.SetValues(Position.Row - 1, Position.Column + 2);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // NW
            p.SetValues(Position.Row + 1, Position.Column + 2);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // NE
            p.SetValues(Position.Row + 2, Position.Column + 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // SW
            p.SetValues(Position.Row + 2, Position.Column - 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // SE
            p.SetValues(Position.Row + 1, Position.Column - 2);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            return mat;
        }
    }
}
