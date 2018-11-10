﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gsChessLib
{
    
    public class Game
    {
        public class Piece
        {
            public char x { get; set; }
            public char y { get; set; }
            public char color; // 'w' or 'b' for now
            public Boolean moved; // pawns can move two spaces if they have not moved yet
            // history
        }

        public class Board
        {
            //List<Point> points;
            public List<Piece> Pieces { get; set; }
            public string BoardString { get; set; } // starting pos could be: 'RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr'
            public string MoveList_HorizonalNotation { get; set; } // Horizontally: " 1. e4 e5 2. Nf3 Nc6 3. Bb5 a6 "
        }

        // get a list of possible moves per piece given the board
        public static List<string> GetPossibleMoves(Board b)
        {
            List<string> ValidMoves = new List<string>();
            return ValidMoves;
        }

        public static List<Point> ValidMoves(Board b)
        {
            List<Point> Moves = new List<Point>();
            // Pawn
            List<Point> PawnMoves = ValidPawnMoves(p);
            // Rook
            List<Point> RookMoves = ValidRookMoves(p);
            // Knight
            List<Point> KnightMoves = ValidKnightMoves(p);
            // Bishop
            List<Point> BishopMoves = ValidBishopMoves(p);
            // Queen
            List<Point> QueenMoves = ValidQueenMoves(p);
            // King
            List<Point> KingMoves = ValidKingMoves(p);
            return Moves;
        }

        public static Boolean ValidBoardString_2x2(string boardString)
        {
            Boolean flag = true; // innocent until proven guilty
            // 1. the string should have 64 spaces
            // 2. the string should only have upper or lower 'rnbqkp.'
            
            return flag;
        }

        public static List<Point> ValidPawnMoves(Board b)
        {
            List<Point> ValidMoves = new List<Point>();
            // forward moves
            // -------------
            // 1. can move forward one space if nothing is blocking
            // 2. can move forward two spaces if nothing is blocking and piece has not moved yet
            // diagonal moves
            // ---------------
            // 1. can move forward left if an opposing color is on that square
            // 2. can move forward right if an opposing color is on that square
            return ValidMoves;
        }

        public static List<Point> ValidRookMoves(Board b)
        {
            List<Point> ValidMoves = new List<Point>();
            return ValidMoves;
        }

        public static List<Point> ValidKnightMoves(Board b)
        {
            List<Point> ValidMoves = new List<Point>();
            return ValidMoves;
        }

        public static List<Point> ValidBishopMoves(Board b)
        {
            List<Point> ValidMoves = new List<Point>();
            return ValidMoves;
        }

        public static List<Point> ValidQueenMoves(Board b)
        {
            List<Point> ValidMoves = new List<Point>();
            return ValidMoves;
        }

        public static List<Point> ValidKingMoves(Board b)
        {
            List<Point> ValidMoves = new List<Point>();
            return ValidMoves;
        }


    }


}
