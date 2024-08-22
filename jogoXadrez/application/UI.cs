using System;
using System.Collections.Generic;
using System.Linq;
using jogoXadrez.Chess;

namespace jogoXadrez.Application
{
    static class UI
    {
        // Cores do texto
        private const string ANSI_RESET = "\u001B[0m";
        private const string ANSI_BLACK = "\u001B[30m";
        private const string ANSI_RED = "\u001B[31m";
        private const string ANSI_GREEN = "\u001B[32m";
        private const string ANSI_YELLOW = "\u001B[33m";
        private const string ANSI_BLUE = "\u001B[34m";
        private const string ANSI_PURPLE = "\u001B[35m";
        private const string ANSI_CYAN = "\u001B[36m";
        private const string ANSI_WHITE = "\u001B[37m";

        // Cores do fundo
        private const string ANSI_BLACK_BACKGROUND = "\u001B[40m";
        private const string ANSI_RED_BACKGROUND = "\u001B[41m";
        private const string ANSI_GREEN_BACKGROUND = "\u001B[42m";
        private const string ANSI_YELLOW_BACKGROUND = "\u001B[43m";
        private const string ANSI_BLUE_BACKGROUND = "\u001B[44m";
        private const string ANSI_PURPLE_BACKGROUND = "\u001B[45m";
        private const string ANSI_CYAN_BACKGROUND = "\u001B[46m";
        private const string ANSI_WHITE_BACKGROUND = "\u001B[47m";

        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static ChessPosition ReadChessPosition()
        {
            try
            {
                string s = Console.ReadLine();
                char column = s[0];
                int row = int.Parse(s.Substring(1));
                return new ChessPosition(column, row);
            }
            catch (Exception)
            {
                throw new FormatException("Error reading ChessPosition. Valid values are from a1 to h8");
            }
        }

        public static void PrintBoard(ChessPiece[,] pieces)
        {
            PrintBoardBorder();
            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                Console.Write((8 - i) + " |");
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    PrintPiece(pieces[i, j], false);
                }
                Console.WriteLine("|");
            }
            PrintBoardBorder();
            Console.WriteLine("   a b c d e f g h");
        }

        public static void PrintMatch(ChessMatch chessMatch, List<ChessPiece> captured)
        {
            PrintHeader();
            PrintBoard(chessMatch.GetPieces());
            Console.WriteLine();
            PrintCapturedPieces(captured);
            Console.WriteLine();
            Console.WriteLine("Turn : " + chessMatch.Turn);
            if (!chessMatch.CheckMate)
            {
                Console.WriteLine("Waiting player: " + chessMatch.CurrentPlayer);
                if (chessMatch.Check)
                {
                    Console.WriteLine(" !! CHECK !!");
                }
            }
            else
            {
                Console.WriteLine("!!!! CHECKMATE !!!");
                Console.WriteLine("Winner: " + chessMatch.CurrentPlayer);
            }
        }

        public static void PrintBoard(ChessPiece[,] pieces, bool[,] possibleMoves)
        {
            PrintBoardBorder();
            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                Console.Write((8 - i) + " |");
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    PrintPiece(pieces[i, j], possibleMoves[i, j]);
                }
                Console.WriteLine("|");
            }
            PrintBoardBorder();
            Console.WriteLine("   a b c d e f g h");
        }

        private static void PrintPiece(ChessPiece piece, bool background)
        {
            if (background)
            {
                Console.Write(ANSI_BLUE_BACKGROUND);
            }
            if (piece == null)
            {
                Console.Write("-" + ANSI_RESET);
            }
            else
            {
                if (piece.Color == Color.WHITE)
                {
                    Console.Write(ANSI_WHITE + piece + ANSI_RESET);
                }
                else
                {
                    Console.Write(ANSI_YELLOW + piece + ANSI_RESET);
                }
            }
            Console.Write(" ");
        }

        private static void PrintCapturedPieces(List<ChessPiece> captured)
        {
            var white = captured.Where(x => x.Color == Color.WHITE).ToList();
            var black = captured.Where(x => x.Color == Color.BLACK).ToList();
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            Console.Write(ANSI_WHITE);
            Console.WriteLine($"[{string.Join(", ", white)}]");
            Console.Write(ANSI_RESET);
            Console.Write("Black: ");
            Console.Write(ANSI_YELLOW);
            Console.WriteLine($"[{string.Join(", ", black)}]");
            Console.Write(ANSI_RESET);
        }

        public static void PrintBoardBorder()
        {
            Console.WriteLine("  +----------------+");
        }

        public static void PrintHeader()
        {
            Console.WriteLine(ANSI_CYAN + "============================         R -> Torre "
                + "|| N -> Cavalo");
            Console.WriteLine("|       CHESS GAME         |         B -> Bispo || Q -> Rainha");
            Console.WriteLine("============================         K -> Rei   || P -> Peao" + ANSI_RESET);
        }
    }
}
