using System;
using System.Collections.Generic;
using System.Windows;
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

        //[TestMethod]
        //public void GetInitialWhitePieces()
        //{
        //    Game.Board b = new Game.Board();
        //    b.BoardString = b.Initialize8x8Board();
        //    Assert.IsTrue(b.BoardString == "RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr");

        //}

        [TestMethod]
        public void TestBoardStringToPieces()
        {
            Game.Board b = new Game.Board();
            b.BoardString = b.Initialize8x8Board();
            b.BoardStringToPieces();
            Assert.IsTrue(b.Pieces != null);
            Assert.IsTrue(b.Pieces.Count == 32);
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
                Assert.IsTrue(p.type == EighthRow[i - 1].ToString());
            }

        }

        [TestMethod]
        public void Test_GetAlgebraicPiecesOnSquare_InitialBoard()
        {
            Game.Board b = new Game.Board("RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr");
            Assert.IsTrue(b.Pieces != null);
            Assert.IsTrue(b.Pieces.Count == 32);


            Game.Piece p = Game.GetPieceOnSquare(b, "a1");
            Assert.IsFalse(p == null);
            Assert.IsTrue(p.AlgebraicCoordinate == "a1");
            Assert.IsTrue(p.type == "R");  // This is a little counter-intuitive
            Assert.IsTrue(p.color == "w");

        }

        [TestMethod]
        public void TestCheckForward_negative()
        {
            Game.Board b = new Game.Board();
            // b.BoardString = b.Initialize8x8Board();
            b.BoardString = "RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr";
            b.BoardStringToPieces();


            Game.Piece p = Game.GetPieceOnSquare(b, '1', '2');
            Assert.IsTrue(p != null);
            Assert.IsTrue(p.type == "P");

            Game.Piece p1 = Game.CheckForward(b, p, -2);
            Assert.IsTrue(p1 == null);
        }

        [TestMethod]
        public void TestCheckForward_positive()
        {
            Game.Board b = new Game.Board();
            // b.BoardString = b.Initialize8x8Board();
            b.BoardString = "RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr";
            b.BoardStringToPieces();
            // ==============
            // negative case
            // ==============
            b.BoardString = "RNBKQBNR\nQPPPPPPP\np.......\n........\n........\n........\npppppppp\nrnbkqbnr";
            b.BoardStringToPieces();
            Game.Piece p = Game.GetPieceOnSquare(b, '1', '2');
            Assert.IsTrue(p != null);
            Assert.IsTrue(p.type == "Q");

            Game.Piece p1 = Game.GetPieceOnSquare(b, '1', '3');
            Assert.IsTrue(p1.type == "p");
            p1 = Game.CheckForward(b, p, 1);
            Assert.IsTrue(p1 != null);
        }

        [TestMethod]
        public void TestValidPawnMovesForward_init()
        {
            Game.Board b = new Game.Board();
            b.BoardString = b.Initialize8x8Board();
            b.BoardStringToPieces();

            // initial board returns a valid set of two moves
            Game.Piece p = Game.GetPieceOnSquare(b, '1', '2');
            Assert.IsTrue(p != null);

            List<Point> pts2 = Game.ValidMoves(b, p);
            Assert.IsTrue(pts2.Count > 0);
            Assert.IsTrue(pts2.Count == 2);
            Assert.IsTrue(pts2[0].X == 1);   // first move coordinates
            Assert.IsTrue(pts2[0].Y == 3);
            Assert.IsTrue(pts2[1].X == 1);   // second move coordinates
            Assert.IsTrue(pts2[1].Y == 4);

        }

        [TestMethod]
        public void TestValidPawnMovesForward_blocking()
        {
            // condition where there is a piece blocking
            Game.Board b = new Game.Board("RNBKQBNR\nPPPPPPPP\n........\np.......\n........\n........\n.ppppppp\nrnbkqbnr");
            Assert.IsTrue(b.Pieces.Count == 32);
            // initial board returns a valid set of two moves -> this one should only return one
            Game.Piece p = Game.GetPieceOnSquare(b, '1', '2');
            Assert.IsTrue(p != null);
            List<Point> pts = Game.ValidMoves(b, p);
            Assert.IsTrue(pts.Count == 1);

            //b.BoardStringToPieces();
        }

        [TestMethod]
        public void TestPointToAlgebraic()
        {
            String alph = "abcdefgh";

            // first row: verify (1,1)-(8,1)  ==>  a1-h1
            for (int i = 1; i <= alph.Length; i++)
            {
                Point p = new Point(i, 1);
                string testString = Game.PointToAlgebraic(p);
                Assert.IsTrue(testString == alph[i-1].ToString() + "1");
                // test integration with AlgebraicStringToPoint()
                Assert.IsTrue(p == Game.AlgebraicStringToPoint(testString));
            }
            // second row: verify (1,2)-(8,2)  ==>  a2-h2
            for (int i = 1; i <= alph.Length; i++)
            {
                Point p = new Point(i, 2);
                string testString = Game.PointToAlgebraic(p);
                Assert.IsTrue(testString == alph[i-1].ToString() + "2");
                // test integration with AlgebraicStringToPoint()
                Assert.IsTrue(p == Game.AlgebraicStringToPoint(testString));
            }
            // ...  not really necessary to continue, but you can follow on from above if you want
        }

        [TestMethod]
        public void TestAlgebraicStringToPoint()
        {
            string alph = "abcdefgh";
            string InputString = "a1";
            Point p = Game.AlgebraicStringToPoint(InputString);
            Assert.IsTrue(p.X == 1);
            Assert.IsTrue(p.Y == 1);
            string OutputString = Game.PointToAlgebraic(p);
            Assert.IsTrue(InputString == OutputString); // test integration with PointToAlgebraic()

            InputString = "h8";
            p = Game.AlgebraicStringToPoint(InputString);
            Assert.IsTrue(p.X == 8);
            Assert.IsTrue(p.Y == 8);

            

        }
    }
}
