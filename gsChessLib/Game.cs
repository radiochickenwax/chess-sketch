using System;
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
            public string AlgebraicCoordinate { get; set; }
            public string color; // 'w' or 'b' for now
            public string type; // "knight", "pawn", ...
            public Boolean moved; // pawns can move two spaces if they have not moved yet
            // history
        }

        public class Move
        {
            public Point StartPoint { get; set; }
            public Point EndPoint { get; set; }
            public String StartPointAlgebraic { get; set; }
            public String EndPointAlgebraic { get; set; }

        }

        public static string PointToAlgebraic(Point p)
        {
            // TODO: try/catch cases:  p.X/p.Y > 8?
            String alph = "abcdefgh";
            String row, column;
            // TODO: String rank, file;
            column = alph[(int)(p.X - 1)].ToString(); 
            //rank = row;
            row = p.Y.ToString();
            return column + row;
        }

        public static Point AlgebraicStringToPoint(string s)
        {
            // TODO: try/catch cases:  p.X/p.Y > 8?
            Point p = new Point();
            char c = s[0];
            p.X = "abcdefgh".IndexOf(c) + 1;
            p.Y = s[1] - '0'; // convert char to int
            return p;
        }


        public class Board
        {
            //List<Point> points;
            public List<Piece> Pieces { get; set; }
            public string BoardString { get; set; } // starting pos could be: 'RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr'
            public string MoveList_HorizonalNotation { get; set; } // Horizontally: " 1. e4 e5 2. Nf3 Nc6 3. Bb5 a6 "

            public Board()
            {

            }

            public Board(string s)
            {
                Initialize8x8Board(s);
            }

            public string Initialize8x8Board()
            {
                BoardString = "RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr";
                BoardStringToPieces();
                return BoardString;
            }

            public void Initialize8x8Board(string s)
            {
                BoardString = s;
                BoardStringToPieces();
                //return BoardString;
            }

            //public void Initialize8x8Board() // can't overload like this
            //{
            //    BoardString = "RNBKQBNR\nPPPPPPPP\n........\n........\n........\n........\npppppppp\nrnbkqbnr";
            //    //return BoardString;
            //}

            public static Boolean IsValidBoardString_8x8(string boardString)
            {
                Boolean flag = true; // innocent until proven guilty
                                     // 1. the string should have 64 spaces
                                     // 2. the string should only have upper or lower 'rnbqkp.'

                Boolean flag1 = (boardString.Length == 71);
                flag = flag1 & flag;

                // verify there are 8 rows
                Boolean flag2 = boardString.Count(x => x == '\n') == 7;
                flag = flag2 & flag;

                // verify each row has 8 chars
                string[] rows = boardString.Split('\n');
                Boolean flag3 = rows.Count() == 8;
                flag = flag3 & flag;

                return flag;
            }

            public void BoardStringToPieces()
            {
                List<Piece> newPieces = new List<Piece>();
                // TODO: verify BoardString is not null or empty
                // Update the list of Pieces given the boardstring

                // need to traverse the string backwards - or reorganize the data so it doesn't need to be backwards
                string[] rows = BoardString.Split('\n');
                for (int i = 0; i < rows.Length; i++)
                {
                    string row = rows[i];
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (row[j] != '.')
                        {
                            Piece p = new Piece();
                            // set position
                            p.x = (j + 1).ToString()[0];
                            p.y = (i + 1).ToString()[0];
                            p.AlgebraicCoordinate = PointToAlgebraic(new Point(j+1,i+1));
                            // set color
                            if (char.IsUpper(row[j].ToString()[0]))
                                p.color = "w";
                            else
                                p.color = "b";  // TODO: extend this beyond upper/lower
                            // set type
                            p.type = row[j].ToString()[0].ToString();  // TODO:  rethink this - probably want more than the first character at some point
                            newPieces.Add(p);
                        }

                    }
                }
                Pieces = newPieces;
            }

            // get a list of possible moves per piece given the board
            public static List<string> GetPossibleMoves(Board b)
            {
                List<string> ValidMoves = new List<string>();
                return ValidMoves;
            }

            public static List<Piece> WhitePieces()
            {
                List<Piece> Pieces = new List<Piece>();
                return Pieces;
            }

            public static List<Piece> BlackPieces()
            {
                List<Piece> Pieces = new List<Piece>();
                return Pieces;

            }

        }

        // TODO:  look into the following:
        // https://stackoverflow.com/questions/7532882/is-there-any-graph-data-structure-implemented-for-c-sharp
        // https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx#datastructures20_5_topic3
        // https://archive.codeplex.com/?p=quickgraph
        // Single depth list of moves for now - see the links above
        public static List<Point> ValidMoves(Board b, Piece p)
        {
            List<Point> Points = new List<Point>();
            // TODO: use this instead: List<Move> Moves = new List<Move>();
            // Pawn
            List<Point> PawnMoves = ValidPawnMoves(b,p);
            Points.AddRange(PawnMoves);
            // Rook
            List<Point> RookMoves = ValidRookMoves(b);
            Points.AddRange(RookMoves);
            // Knight
            List<Point> KnightMoves = ValidKnightMoves(b);
            Points.AddRange(KnightMoves);
            // Bishop
            List<Point> BishopMoves = ValidBishopMoves(b);
            Points.AddRange(BishopMoves);
            // Queen
            List<Point> QueenMoves = ValidQueenMoves(b);
            Points.AddRange(QueenMoves);
            // King
            List<Point> KingMoves = ValidKingMoves(b);
            Points.AddRange(KingMoves);
            return Points;
        }

        // 2018-11-15 - cobbling this from https://github.com/radiochickenwax/radiochickenwax-chess/blob/master/gpyChess.py
        public static Piece GetPieceOnSquare(Board b, char x, char y)
        {
            // searching for piece on b,x,y
            Piece thisPiece = null;
            foreach (Piece p in b.Pieces)
            {
                // # print(piece.type+' tpcolor:'+str(piece.color)+' tpx:'+str(piece.x)+' tpy:'+str(piece.y))
                if (p.x == x && p.y == y)
                    thisPiece = p;
            }
            return thisPiece;

        }

        public static Piece GetPieceOnSquare(Board b, string algebraicCoordinate)
        {
            List<Piece> pieces =  b.Pieces.Where(piece => piece.AlgebraicCoordinate == algebraicCoordinate).ToList();
            return (pieces == null || pieces.Count < 1) ? null : pieces[0];
        }


        // get the piece that is n spaces forward from the supplied piece
        public static Piece CheckForward(Board b, Piece p, int n)
        {
            // TODO: Define "forward" by piece color
            //int y = Convert.ToInt32(p.y);
            int y = p.y - '0';  // hack hack hack - this converts the char to an int
            int yn = y + n;
            Piece ReturnPiece = GetPieceOnSquare(b, p.x, yn.ToString()[0]); // TODO: verify this doesn't overflow the board
            return ReturnPiece;
        }


        public static List<Move> ValidPawnMoves(Board b, string algebraicCoordinate)
        {
            return null;
        }

            /// <summary>
            /// Validate b and p upstream
            /// </summary>
            /// <param name="b"></param>
            /// <param name="p"></param>
            /// <returns></returns>
            public static List<Point> ValidPawnMoves(Board b, Piece p)
        {
            // evaluate as if p.type == "pawn" regardless of whether it is or isn't
            List<Point> ValidPoints = new List<Point>();
            // GetPieceOnSquare(b, p.x.ToString()[0], p.y.ToString()[0]); // this would be redundant
            // forward moves
            // -------------
            // 1. can move forward one space if nothing is blocking
            // check forward space - this is +1 if white or -1 if black on a 8x8 game 
            // TODO: Get Piece on Point
            // TODO: Get Piece by algebraic notation
            
            Point pt = new Point();

            if ( CheckForward(b,p,1) == null)
            {
                double tmp;
                if (Double.TryParse(p.x.ToString(), out tmp))
                    pt.X = tmp;
                if (Double.TryParse(p.y.ToString(), out tmp))
                    pt.Y = tmp+1;
                ValidPoints.Add(pt);

                if (p.moved == false)
                    if (CheckForward(b, p, 2) == null)
                    {
                        if (Double.TryParse(p.x.ToString(), out tmp))
                            pt.X = tmp;
                        if (Double.TryParse(p.y.ToString(), out tmp))
                            pt.Y = tmp + 2;
                        ValidPoints.Add(pt);
                    }
            }

            //if (p.color == 'w' && p.y + 1)

            // 2. can move forward two spaces if nothing is blocking and piece has not moved yet

            // diagonal moves
            // ---------------
            // 1. can move forward left if an opposing color is on that square
            // 2. can move forward right if an opposing color is on that square
            return ValidPoints;
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
