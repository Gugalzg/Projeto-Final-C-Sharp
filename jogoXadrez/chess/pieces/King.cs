using jogoXadrez.boardgame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.chess.pieces
{
    public class King : ChessPiece
    {
        private ChessMatch chessMatch;

        public King(Board board, Color color, ChessMatch chessMatch) : base(board, color)
        {
            this.chessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position position)
        {
            ChessPiece p = (ChessPiece)GetBoard().Piece(position);
            return p == null || p.Color != Color;
        }

        private bool TestRookCastling(Position position)
        {
            ChessPiece p = (ChessPiece)GetBoard().Piece(position);
            return p != null && p is Rook && p.Color == Color && p.MoveCount == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[GetBoard().GetRows(), GetBoard().GetColumns()];
            Position p = new Position(0, 0);

            // Above
            p.SetValues(Position.Row - 1, Position.Column);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Below
            p.SetValues(Position.Row + 1, Position.Column);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Left
            p.SetValues(Position.Row, Position.Column - 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Right
            p.SetValues(Position.Row, Position.Column + 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // NW
            p.SetValues(Position.Row - 1, Position.Column - 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // NE
            p.SetValues(Position.Row - 1, Position.Column + 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // SW
            p.SetValues(Position.Row + 1, Position.Column - 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // SE
            p.SetValues(Position.Row + 1, Position.Column + 1);
            if (GetBoard().PositionExists(p) && CanMove(p))
            {
                mat[p.Row, p.Column] = true;
            }

            // Special move: Castling
            if (MoveCount == 0 && !chessMatch.Check)
            {
                // Kingside castling
                Position postT1 = new Position(Position.Row, Position.Column + 3);
                if (TestRookCastling(postT1))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (GetBoard().Piece(p1) == null && GetBoard().Piece(p2) == null)
                    {
                        mat[Position.Row, Position.Column + 2] = true;
                    }
                }

                // Queenside castling
                Position postT2 = new Position(Position.Row, Position.Column - 4);
                if (TestRookCastling(postT2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (GetBoard().Piece(p1) == null && GetBoard().Piece(p2) == null && GetBoard().Piece(p3) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
