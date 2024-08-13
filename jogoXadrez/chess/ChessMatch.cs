using jogoXadrez.boardgame;
using jogoXadrez.chess.pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.chess
{
    public class ChessMatch
    {
        private int turn;
        private Color currentPlayer;
        private Board board;
        private bool check;
        private bool checkMate;
        private ChessPiece enPassantVulnerable;
        private ChessPiece promoted;

        private List<Piece> piecesOnTheBoard = new List<Piece>();
        private List<Piece> capturedPieces = new List<Piece>();

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.WHITE;
            InitialSetup();
        }

        public int Turn => turn;
        public Color CurrentPlayer => currentPlayer;
        public bool Check => check;
        public bool CheckMate => checkMate;
        public ChessPiece EnPassantVulnerable => enPassantVulnerable;
        public ChessPiece Promoted => promoted;

        public ChessPiece[,] GetPieces()
        {
            ChessPiece[,] mat = new ChessPiece[board.GetRows(), board.GetColumns()];
            for (int i = 0; i < board.GetRows(); i++)
            {
                for (int j = 0; j < board.GetColumns(); j++)
                {
                    mat[i, j] = board.Piece(i, j) as ChessPiece;
                }
            }
            return mat;
        }

        public bool[,] PossibleMoves(ChessPosition sourcePosition)
        {
            Position position = sourcePosition.ToPosition();
            ValidateSourcePosition(position);
            return board.Piece(position).PossibleMoves();
        }

        public ChessPiece PerformChessMove(ChessPosition sourcePosition, ChessPosition targetPosition)
        {
            Position source = sourcePosition.ToPosition();
            Position target = targetPosition.ToPosition();
            ValidateSourcePosition(source);
            ValidateTargetPosition(source, target);
            Piece capturedPiece = MakeMove(source, target);

            if (TestCheck(currentPlayer))
            {
                UndoMove(source, target, capturedPiece);
                throw new ChessException("You can't put yourself in check");
            }

            ChessPiece movedPiece = board.Piece(target) as ChessPiece;

            // Special promotion
            promoted = null;
            if (movedPiece is Pawn)
            {
                if ((movedPiece.Color == Color.WHITE && target.Row == 0) ||
                    (movedPiece.Color == Color.BLACK && target.Row == 7))
                {
                    promoted = board.Piece(target) as ChessPiece;
                    promoted = ReplacePromotedPiece("Q");
                }
            }

            check = TestCheck(Opponent(currentPlayer));

            if (TestCheckMate(Opponent(currentPlayer)))
            {
                checkMate = true;
            }
            else
            {
                NextTurn();
            }

            // Special en passant
            if (movedPiece is Pawn && (target.Row == source.Row - 2 || target.Row == source.Row + 2))
            {
                enPassantVulnerable = movedPiece;
            }
            else
            {
                enPassantVulnerable = null;
            }

            return capturedPiece as ChessPiece;
        }

        public ChessPiece ReplacePromotedPiece(string type)
        {
            if (promoted == null)
            {
                throw new InvalidOperationException("There is no piece to be promoted");
            }
            if (!type.Equals("B") && !type.Equals("N") && !type.Equals("R") && !type.Equals("Q"))
            {
                return promoted;
            }
            Position pos = promoted.GetChessPosition().ToPosition();
            Piece p = board.RemovePiece(pos);
            piecesOnTheBoard.Remove(p);
            ChessPiece newPiece = CreateNewPiece(type, promoted.Color);
            board.PlacePiece(newPiece, pos);
            piecesOnTheBoard.Add(newPiece);

            return newPiece;
        }

        private ChessPiece CreateNewPiece(string type, Color color)
        {
            return type switch
            {
                "B" => new Bishop(board, color),
                "N" => new Knight(board, color),
                "Q" => new Queen(board, color),
                _ => new Rook(board, color)
            };
        }

        private Piece MakeMove(Position source, Position target)
        {
            ChessPiece p = board.RemovePiece(source) as ChessPiece;
            p.IncreaseMoveCount();
            Piece capturedPiece = board.RemovePiece(target);
            board.PlacePiece(p, target);

            if (capturedPiece != null)
            {
                piecesOnTheBoard.Remove(capturedPiece);
                capturedPieces.Add(capturedPiece);
            }

            // Special castling kingside rook
            if (p is King && target.Column == source.Column + 2)
            {
                Position sourceT = new Position(source.Row, source.Column + 3);
                Position targetT = new Position(source.Row, source.Column + 1);
                ChessPiece rook = board.RemovePiece(sourceT) as ChessPiece;
                board.PlacePiece(rook, targetT);
                rook.IncreaseMoveCount();
            }

            // Special castling queenside rook
            if (p is King && target.Column == source.Column - 2)
            {
                Position sourceT = new Position(source.Row, source.Column - 4);
                Position targetT = new Position(source.Row, source.Column - 1);
                ChessPiece rook = board.RemovePiece(sourceT) as ChessPiece;
                board.PlacePiece(rook, targetT);
                rook.IncreaseMoveCount();
            }

            // Special en passant
            if (p is Pawn)
            {
                if (source.Column != target.Column && capturedPiece == null)
                {
                    Position pawnPosition = p.Color == Color.WHITE
                        ? new Position(target.Row + 1, target.Column)
                        : new Position(target.Row - 1, target.Column);

                    capturedPiece = board.RemovePiece(pawnPosition);
                    capturedPieces.Add(capturedPiece);
                    piecesOnTheBoard.Remove(capturedPiece);
                }
            }

            return capturedPiece;
        }

        private void UndoMove(Position source, Position target, Piece capturedPiece)
        {
            ChessPiece p = board.RemovePiece(target) as ChessPiece;
            p.DecreaseMoveCount();
            board.PlacePiece(p, source);

            if (capturedPiece != null)
            {
                board.PlacePiece(capturedPiece, target);
                capturedPieces.Remove(capturedPiece);
                piecesOnTheBoard.Add(capturedPiece);
            }

            // Special castling kingside rook
            if (p is King && target.Column == source.Column + 2)
            {
                Position sourceT = new Position(source.Row, source.Column + 3);
                Position targetT = new Position(source.Row, source.Column + 1);
                ChessPiece rook = board.RemovePiece(targetT) as ChessPiece;
                board.PlacePiece(rook, sourceT);
                rook.IncreaseMoveCount();
            }

            // Special castling queenside rook
            if (p is King && target.Column == source.Column - 2)
            {
                Position sourceT = new Position(source.Row, source.Column - 4);
                Position targetT = new Position(source.Row, source.Column - 1);
                ChessPiece rook = board.RemovePiece(targetT) as ChessPiece;
                board.PlacePiece(rook, sourceT);
                rook.DecreaseMoveCount();
            }

            // Special en passant
            if (p is Pawn && source.Column != target.Column && capturedPiece == enPassantVulnerable)
            {
                ChessPiece pawn = board.RemovePiece(target) as ChessPiece;
                Position pawnPosition = p.Color == Color.WHITE
                    ? new Position(3, target.Column)
                    : new Position(4, target.Column);
                board.PlacePiece(pawn, pawnPosition);
            }
        }

        private void ValidateSourcePosition(Position position)
        {
            if (!board.ThereIsAPiece(position))
            {
                throw new ChessException("There is no piece on source position");
            }
            if (currentPlayer != ((ChessPiece)board.Piece(position)).Color)
            {
                throw new ChessException("The chosen piece is not yours");
            }

            if (!board.Piece(position).IsThereAnyPossibleMove())
            {
                throw new ChessException("There is no possible moves for the chosen piece");
            }
        }

        private void ValidateTargetPosition(Position source, Position target)
        {
            if (!board.Piece(source).PossibleMove(target))
            {
                throw new ChessException("The chosen piece can't move to target position");
            }
        }

        private void NextTurn()
        {
            turn++;
            currentPlayer = (currentPlayer == Color.WHITE) ? Color.BLACK : Color.WHITE;
        }

        private Color Opponent(Color color)
        {
            return (color == Color.WHITE) ? Color.BLACK : Color.WHITE;
        }

        private ChessPiece King(Color color)
        {
            var list = piecesOnTheBoard
                .Where(x => ((ChessPiece)x).Color == color)
                .ToList();
            foreach (var p in list)
            {
                if (p is King)
                {
                    return (ChessPiece)p;
                }
            }

            throw new InvalidOperationException("There is no " + color + " king on the board");
        }

        private bool TestCheck(Color color)
        {
            Position kingPosition = King(color).GetChessPosition().ToPosition();
            var opponentPieces = piecesOnTheBoard
                .Where(x => ((ChessPiece)x).Color == Opponent(color))
                .ToList();
            foreach (var p in opponentPieces)
            {
                bool[,] mat = p.PossibleMoves();
                if (mat[kingPosition.Row, kingPosition.Column])
                {
                    return true;
                }
            }
            return false;
        }

        private bool TestCheckMate(Color color)
        {
            if (!TestCheck(color))
            {
                return false;
            }
            var list = piecesOnTheBoard
                .Where(x => ((ChessPiece)x).Color == color)
                .ToList();
            foreach (var p in list)
            {
                bool[,] mat = p.PossibleMoves();
                for (int i = 0; i < board.GetRows(); i++)
                {
                    for (int j = 0; j < board.GetColumns(); j++)
                    {
                        if (mat[i, j])
                        {
                            Position source = ((ChessPiece)p).GetChessPosition().ToPosition();
                            Position target = new Position(i, j);
                            Piece capturedPiece = MakeMove(source, target);
                            bool isCheck = TestCheck(color);
                            UndoMove(source, target, capturedPiece);
                            if (!isCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void PlaceNewPiece(char column, int row, ChessPiece piece)
        {
            board.PlacePiece(piece, new ChessPosition(column, row).ToPosition());
            piecesOnTheBoard.Add(piece);
        }

        private void InitialSetup()
        {
            PlaceNewPiece('a', 1, new Rook(board, Color.WHITE));
            PlaceNewPiece('b', 1, new Knight(board, Color.WHITE));
            PlaceNewPiece('c', 1, new Bishop(board, Color.WHITE));
            PlaceNewPiece('d', 1, new Queen(board, Color.WHITE));
            PlaceNewPiece('e', 1, new King(board, Color.WHITE, this));
            PlaceNewPiece('f', 1, new Bishop(board, Color.WHITE));
            PlaceNewPiece('g', 1, new Knight(board, Color.WHITE));
            PlaceNewPiece('h', 1, new Rook(board, Color.WHITE));
            PlaceNewPiece('a', 2, new Pawn(board, Color.WHITE, this));
            PlaceNewPiece('b', 2, new Pawn(board, Color.WHITE, this));
            PlaceNewPiece('c', 2, new Pawn(board, Color.WHITE, this));
            PlaceNewPiece('d', 2, new Pawn(board, Color.WHITE, this));
            PlaceNewPiece('e', 2, new Pawn(board, Color.WHITE, this));
            PlaceNewPiece('f', 2, new Pawn(board, Color.WHITE, this));
            PlaceNewPiece('g', 2, new Pawn(board, Color.WHITE, this));
            PlaceNewPiece('h', 2, new Pawn(board, Color.WHITE, this));

            PlaceNewPiece('a', 8, new Rook(board, Color.BLACK));
            PlaceNewPiece('b', 8, new Knight(board, Color.BLACK));
            PlaceNewPiece('c', 8, new Bishop(board, Color.BLACK));
            PlaceNewPiece('d', 8, new Queen(board, Color.BLACK));
            PlaceNewPiece('e', 8, new King(board, Color.BLACK, this));
            PlaceNewPiece('f', 8, new Bishop(board, Color.BLACK));
            PlaceNewPiece('g', 8, new Knight(board, Color.BLACK));
            PlaceNewPiece('h', 8, new Rook(board, Color.BLACK));
            PlaceNewPiece('a', 7, new Pawn(board, Color.BLACK, this));
            PlaceNewPiece('b', 7, new Pawn(board, Color.BLACK, this));
            PlaceNewPiece('c', 7, new Pawn(board, Color.BLACK, this));
            PlaceNewPiece('d', 7, new Pawn(board, Color.BLACK, this));
            PlaceNewPiece('e', 7, new Pawn(board, Color.BLACK, this));
            PlaceNewPiece('f', 7, new Pawn(board, Color.BLACK, this));
            PlaceNewPiece('g', 7, new Pawn(board, Color.BLACK, this));
            PlaceNewPiece('h', 7, new Pawn(board, Color.BLACK, this));
        }
    }
}

