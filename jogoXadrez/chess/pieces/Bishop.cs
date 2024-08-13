using jogoXadrez.boardgame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.chess.pieces
{
    public class Bishop : ChessPiece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[GetBoard().GetRows(), GetBoard().GetColumns()];
            Position p = new Position(0, 0);

            // NW
            p.SetValues(Position.Row - 1, Position.Column - 1);
            while (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
            {
                mat[p.Row, p.Column] = true;
                p.SetValues(p.Row - 1, p.Column - 1);
            }
            if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // NE
            p.SetValues(Position.Row - 1, Position.Column + 1);
            while (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
            {
                mat[p.Row, p.Column] = true;
                p.SetValues(p.Row - 1, p.Column + 1);
            }
            if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // SE
            p.SetValues(Position.Row + 1, Position.Column + 1);
            while (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
            {
                mat[p.Row, p.Column] = true;
                p.SetValues(p.Row + 1, p.Column + 1);
            }
            if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // SW
            p.SetValues(Position.Row + 1, Position.Column - 1);
            while (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
            {
                mat[p.Row, p.Column] = true;
                p.SetValues(p.Row + 1, p.Column - 1);
            }
            if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
            {
                mat[p.Row, p.Column] = true;
            }

            return mat;
        }
    }
}
