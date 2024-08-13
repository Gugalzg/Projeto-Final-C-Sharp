using jogoXadrez.boardgame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.chess.pieces
{
    public class Queen : ChessPiece
    {
        public Queen(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[GetBoard().GetRows(), GetBoard().GetColumns()];
            Position p = new Position(0, 0);

            // Above
            p.SetValues(Position.Row - 1, Position.Column);
            while (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
            {
                mat[p.Row, p.Column] = true;
                p.SetValues(p.Row - 1, p.Column);
            }
            if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Below
            p.SetValues(Position.Row + 1, Position.Column);
            while (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
            {
                mat[p.Row, p.Column] = true;
                p.SetValues(p.Row + 1, p.Column);
            }
            if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Left
            p.SetValues(Position.Row, Position.Column - 1);
            while (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
            {
                mat[p.Row, p.Column] = true;
                p.SetValues(p.Row, p.Column - 1);
            }
            if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Right
            p.SetValues(Position.Row, Position.Column + 1);
            while (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
            {
                mat[p.Row, p.Column] = true;
                p.SetValues(p.Row, p.Column + 1);
            }
            if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Northwest (NW)
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

            // Northeast (NE)
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

            // Southeast (SE)
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

            // Southwest (SW)
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
