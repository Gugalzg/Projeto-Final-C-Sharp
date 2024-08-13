using jogoXadrez.boardgame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.chess.pieces
{
    public class Pawn : ChessPiece
    {
        private ChessMatch chessMatch;

        public Pawn(Board board, Color color, ChessMatch chessMatch) : base(board, color)
        {
            this.chessMatch = chessMatch;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[GetBoard().GetRows(), GetBoard().GetColumns()];
            Position p = new Position(0, 0);

            if (Color == Color.WHITE)
            {
                // Move one step forward
                p.SetValues(Position.Row - 1, Position.Column);
                if (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
                {
                    mat[p.Row, p.Column] = true;
                }

                // Move two steps forward
                p.SetValues(Position.Row - 2, Position.Column);
                Position p2 = new Position(Position.Row - 1, Position.Column);
                if (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p) &&
                    GetBoard().PositionExists(p2) && !GetBoard().ThereIsAPiece(p2) &&
                   MoveCount == 0)
                {
                    mat[p.Row, p.Column] = true;
                }

                // Capture diagonally left
                p.SetValues(Position.Row - 1, Position.Column - 1);
                if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
                {
                    mat[p.Row, p.Column] = true;
                }

                // Capture diagonally right
                p.SetValues(Position.Row - 1, Position.Column + 1);
                if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
                {
                    mat[p.Row, p.Column] = true;
                }

                // Special move en passant white
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (GetBoard().PositionExists(left) && IsThereOpponentPiece(left) &&
                        GetBoard().Piece(left) == chessMatch.EnPassantVulnerable)
                    {
                        mat[left.Row - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (GetBoard().PositionExists(right) && IsThereOpponentPiece(right) &&
                        GetBoard().Piece(right) == chessMatch.EnPassantVulnerable)
                    {
                        mat[right.Row - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                // Move one step forward
                p.SetValues(Position.Row + 1, Position.Column);
                if (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p))
                {
                    mat[p.Row, p.Column] = true;
                }

                // Move two steps forward
                p.SetValues(Position.Row + 2, Position.Column);
                Position p2 = new Position(Position.Row + 1, Position.Column);
                if (GetBoard().PositionExists(p) && !GetBoard().ThereIsAPiece(p) &&
                    GetBoard().PositionExists(p2) && !GetBoard().ThereIsAPiece(p2) &&
                    MoveCount == 0)
                {
                    mat[p.Row, p.Column] = true;
                }

                // Capture diagonally left
                p.SetValues(Position.Row + 1, Position.Column - 1);
                if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
                {
                    mat[p.Row, p.Column] = true;
                }

                // Capture diagonally right
                p.SetValues(Position.Row + 1, Position.Column + 1);
                if (GetBoard().PositionExists(p) && IsThereOpponentPiece(p))
                {
                    mat[p.Row, p.Column] = true;
                }

                // Special move en passant black
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (GetBoard().PositionExists(left) && IsThereOpponentPiece(left) &&
                        GetBoard().Piece(left) == chessMatch.EnPassantVulnerable)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (GetBoard().PositionExists(right) && IsThereOpponentPiece(right) &&
                        GetBoard().Piece(right) == chessMatch.EnPassantVulnerable)
                    {
                        mat[right.Row + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
