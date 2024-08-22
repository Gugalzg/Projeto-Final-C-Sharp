using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.Boardgame
{
    
        public abstract class Piece
        {
            public Position Position { get; set; }
            private Board board;

            protected Piece(Board board)
            {
                this.board = board;
                Position = null;
            }

            protected Board GetBoard()
            {
                return board;
            }

            public abstract bool[,] PossibleMoves();

            public bool PossibleMove(Position position)
            {
                return PossibleMoves()[position.Row, position.Column];
            }

            public bool IsThereAnyPossibleMove()
            {
                bool[,] mat = PossibleMoves();
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    for (int j = 0; j < mat.GetLength(1); j++)
                    {
                        if (mat[i, j])
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }

