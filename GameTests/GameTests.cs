using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace gsChessLib
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GeneratePawns()
        {
            List<Game.Piece> pieces = new List<Game.Piece>();

            // generate eight white pawns
            for (int i = 0; i < 8; i++)
            {
                Game.Piece p = new Game.Piece();
                p.color = "w";
                p.x = (i + 1).ToString()[0]; // note this won't work for bigger boards
                p.y = '2';
                p.moved = false;
                pieces.Add(p);
            }

            Assert.IsTrue(pieces.Count == 8);
            Assert.IsTrue(pieces[0].color == "w");
            Assert.IsTrue(pieces[0].x == '1');
            Assert.IsTrue(pieces[0].y == '2');
            Assert.IsTrue(pieces[0].moved == false);
        }

        [TestMethod]
        public void GeneratePawnsOnABoard()
        {
            Game.Board b = new Game.Board();
            b.Pieces = new List<Game.Piece>();

            // generate eight white pawns
            for (int i = 0; i < 8; i++)
            {
                Game.Piece p = new Game.Piece();
                p.color = "w";
                p.x = (i + 1).ToString()[0]; // note this won't work for bigger boards
                p.y = '2';
                p.moved = false;
                b.Pieces.Add(p);
            }
            Assert.IsTrue(b.Pieces.Count == 8);
        }

        [TestMethod]
        public void Test_8x8_BoardString()
        {
            Game.Board b = new Game.Board();
            Assert.IsTrue(Game.Board.IsValidBoardString_8x8("RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr"));
            // Assert.IsTrue(b.IsValidBoardString_8x8("RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr"));  // cannot access the method this way - why?

            //Assert.IsFalse(Game.Board.IsValidBoardString_8x8("RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr"));

            // missing char in first row
            Assert.IsFalse(Game.Board.IsValidBoardString_8x8("RNBKQNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr"));
        }

        [TestMethod]
        public void GetInitialWhitePieces()
        {
            Game.Board b = new Game.Board();
            b.BoardString = b.Initialize8x8Board();
            Assert.IsTrue(b.BoardString == "RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr");

        }

        [TestMethod]
        public void TestBoardStringToPieces()
        {
            Game.Board b = new Game.Board();
            b.BoardString = b.Initialize8x8Board();
            b.BoardStringToPieces();
            Assert.IsTrue(b.Pieces != null);
            Assert.IsTrue( b.Pieces.Count == 32);
        }

        [TestMethod]
        public void Test_GetPiecesOnSquare_InitialBoard()
        {
            Game.Board b = new Game.Board();
            b.BoardString = b.Initialize8x8Board();
            b.BoardStringToPieces();
            Assert.IsTrue(b.Pieces != null);
            Assert.IsTrue(b.Pieces.Count == 32);

            // iterate over the board

            // first row
            string FirstRow = "RNBKQBNR";
            for (int i = 1; i < 8; i++)
            {
                Game.Piece p = Game.GetPieceOnSquare(b, i.ToString()[0], '1');
                Assert.IsTrue(p != null);
                Assert.IsTrue(p.color == "w");
                Assert.IsTrue(p.type == FirstRow[i - 1].ToString());
            }

            // second row
            // (all pawns)
            for (int i = 1; i < 8; i++)
            {
                Game.Piece p = Game.GetPieceOnSquare(b, i.ToString()[0], '2');
                Assert.IsTrue(p != null);
                Assert.IsTrue(p.color == "w");
                Assert.IsTrue(p.type == 'P'.ToString());
            }

            // third - sixth rows have no pieces on start
            for (int j = 3; j < 7; j++)
            {
                for (int i = 1; i < 8; i++)
                {
                    Game.Piece p = Game.GetPieceOnSquare(b, i.ToString()[0], j.ToString()[0]);
                    Assert.IsTrue(p == null);
                }
            }
            
            // seventh row
            for (int i = 1; i < 8; i++)
            {
                Game.Piece p = Game.GetPieceOnSquare(b, i.ToString()[0], '7');
                Assert.IsTrue(p != null);
                Assert.IsTrue(p.color == "b");
                Assert.IsTrue(p.type == 'p'.ToString());
            }

            // eighth row
            string EighthRow = "rnbkqbnr";
            for (int i = 1; i < 8; i++)
            {
                Game.Piece p = Game.GetPieceOnSquare(b, i.ToString()[0], '8');
                Assert.IsTrue(p != null);
                Assert.IsTrue(p.color == "b");
                Assert.IsTrue(p.type == EighthRow[i-1].ToString());
            }

        }
    }
}
