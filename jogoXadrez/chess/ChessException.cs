using jogoXadrez.boardgame.boardgame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.chess
{
    public class ChessException : BoardException
    {
        private static readonly long SerialVersionUid = 1L;

        public ChessException(string msg) : base(msg)
        {
        }
    }
}
