using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogoXadrez.boardgame
{
    using System;

    namespace boardgame
    {
        [Serializable]
        public class BoardException : Exception
        {
            private const long SerialVersionUid = 1L;

            public BoardException(string message)
                : base(message)
            {
            }

            // Para suportar a serialização
            protected BoardException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
                : base(info, context)
            {
            }
        }
    }

}
