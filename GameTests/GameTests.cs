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
                p.color = 'w';
                p.x = (i + 1).ToString()[0]; // note this won't work for bigger boards
                p.y = '2';
                p.moved = false;
                pieces.Add(p);
            }

            Assert.IsTrue( pieces.Count == 8);
            Assert.IsTrue(pieces[0].color == 'w');
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
                p.color = 'w';
                p.x = (i + 1).ToString()[0]; // note this won't work for bigger boards
                p.y = '2';
                p.moved = false;
                b.Pieces.Add(p);
            }
            Assert.IsTrue(b.Pieces.Count == 8);
        }
    }
}
