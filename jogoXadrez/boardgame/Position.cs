using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.Boardgame
{
        public class Position
        {
            public int Row { get; set; }
            public int Column { get; set; }

            public Position(int row, int column)
            {
                Row = row;
                Column = column;
            }

            public void SetValues(int row, int column)
            {
                Row = row;
                Column = column;
            }

            public override string ToString()
            {
                return $"{Row}, {Column}";
            }
        }
    }
