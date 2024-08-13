using System;
using System.Collections.Generic;
using jogoXadrez.application;
using jogoXadrez.chess;

namespace jogoXadrez.application
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessMatch chessMatch = new ChessMatch();
            List<ChessPiece> captured = new List<ChessPiece>();

            while (!chessMatch.CheckMate)
            {
                try
                {
                    UI.ClearScreen();
                    UI.PrintMatch(chessMatch, captured);
                    Console.WriteLine();
                    Console.WriteLine("Source: ");
                    ChessPosition source = UI.ReadChessPosition();

                    bool[,] possibleMoves = chessMatch.PossibleMoves(source);
                    UI.ClearScreen();
                    UI.PrintHeader();
                    UI.PrintBoard(chessMatch.GetPieces(), possibleMoves);

                    Console.WriteLine();
                    Console.WriteLine("Target: ");
                    ChessPosition target = UI.ReadChessPosition();

                    ChessPiece capturedPiece = chessMatch.PerformChessMove(source, target);

                    if (capturedPiece != null)
                    {
                        captured.Add(capturedPiece);
                    }

                    if (chessMatch.Promoted != null)
                    {
                        Console.Write("Enter piece for promotion (B/N/R/Q): ");
                        string type = Console.ReadLine().ToUpper();
                        while (!type.Equals("B") && !type.Equals("N") && !type.Equals("R") && !type.Equals("Q"))
                        {
                            Console.Write("Invalid value! Enter piece for promotion (B/N/R/Q): ");
                            type = Console.ReadLine().ToUpper();
                        }
                        chessMatch.ReplacePromotedPiece(type);
                    }

                }
                catch (ChessException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }

                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

            UI.ClearScreen();
            UI.PrintHeader();
            UI.PrintMatch(chessMatch, captured);
        }
    }
}
