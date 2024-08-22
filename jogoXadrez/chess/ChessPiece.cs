using jogoXadrez.Boardgame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.Chess
{
    public abstract class ChessPiece : Piece
    {
        private Color color;
        private int moveCount;

        public ChessPiece(Board board, Color color) : base(board)
        {
            this.color = color;
        }

        public Color Color
        {
            get { return color; }
        }

        public int MoveCount
        {
            get { return moveCount; }
        }

        public void IncreaseMoveCount()
        {
            moveCount++;
        }

        public void DecreaseMoveCount()
        {
            moveCount--;
        }

        public ChessPosition GetChessPosition()
        {
            return ChessPosition.FromPosition(Position);
        }

        protected bool IsThereOpponentPiece(Position position)
        {
            ChessPiece p = (ChessPiece)GetBoard().Piece(position);
            return p != null && p.Color != color;
        }
    }
}

