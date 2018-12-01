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
                            p.AlgebraicCoordinate = PointToAlgebraic(new Point(j,i));
                            // set color
                            if (char.IsUpper(row[j].ToString()[0]))
                                p.color = "w";
                            else
                                p.color = "b";  // TODO: extend this beyond upper/lower
                            // set type
                            p.type = row[j].ToString()[0].ToString();  // TODO:  rethink this - probably want more than the first character at some point
                            newPieces.Add(p);  // TODO: rethink the upper/lower scenario above as now the UI is built on it
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


        public static string PointToAlgebraic(Point p)
        {
            // TODO: try/catch cases:  p.X/p.Y > 8?
            String alph = "abcdefgh";
            String row, column;
            // TODO: String rank, file;
            column = alph[(int)(p.X)].ToString();
            //rank = row;
            row = (p.Y + 1).ToString();
            return column + row;
        }

        public static Point AlgebraicStringToPoint(string s)
        {
            // TODO: try/catch cases:  p.X/p.Y > 8?
            Point p = new Point();
            char c = s[0];
            p.X = "abcdefgh".IndexOf(c);
            p.Y = s[1] - '0' - 1; // convert char to int
            return p;
        }


        // TODO:  look into the following:
        // https://stackoverflow.com/questions/7532882/is-there-any-graph-data-structure-implemented-for-c-sharp
        // https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx#datastructures20_5_topic3
        // https://archive.codeplex.com/?p=quickgraph
        // https://github.com/nikaburu/wpf-chess
        // https://programming-pages.com/2012/01/15/wpfs-grid-layout-in-xaml-and-c/
        // Single depth list of moves for now - see the links above
        public static List<Point> ValidMoves(Board b, Piece p)
        {
            List<Point> Points = new List<Point>();
            // TODO: use this instead: List<Move> Moves = new List<Move>();
            // Pawn
            if (p.type.ToLower() == "p")
            {
                List<Point> PawnMoves = ValidPawnMoves(b, p);
                Points.AddRange(PawnMoves);
            }
            
            // Rook
            if (p.type.ToLower() == "r")
            {
                List<Point> RookMoves = ValidRookMoves(b,p);                
                Points.AddRange(RookMoves);
            }
            
            // Knight
            if (p.type.ToLower() == "n")
            {
                List<Point> KnightMoves = ValidKnightMoves(b);
                Points.AddRange(KnightMoves);
            }
            
            // Bishop
            if (p.type.ToLower() == "b")
            {
                List<Point> BishopMoves = ValidBishopMoves(b,p);
                Points.AddRange(BishopMoves);
            }
            
            // Queen
            if (p.type.ToLower() == "q")
            {
                List<Point> QueenMoves = ValidQueenMoves(b,p);
                Points.AddRange(QueenMoves);
            }

            // King
            if (p.type.ToLower() == "k")
            {
                List<Point> KingMoves = ValidKingMoves(b);
                Points.AddRange(KingMoves);
            }
            
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
        public static Piece CheckForward(Board b, Piece p, int n, string direction)
        {
            Piece ReturnPiece = null;
            int x = p.x - '0';
            int y = p.y - '0';  // hack hack hack - this converts the char to an int

            int yn = -1;
            int xn = -1;

            if (direction == "n")
            {
                xn = x;
                yn = y + n;
            }
                
            if (direction == "e")
            {
                xn = x - n;
                yn = y;
            }
                
            if (direction == "s")
            {
                xn = x;
                yn = y - n;
            }
                
            if (direction == "w")
            {
                xn = x + n;
                yn = y;
            }
            
            if (xn > 0)
                if (xn < 9)
                    if (yn > 0)
                        if (yn < 9)
                            ReturnPiece = GetPieceOnSquare(b, xn.ToString()[0], yn.ToString()[0]); // TODO: verify this doesn't overflow the board
            return ReturnPiece;
        }

        // get the piece that is n spaces diagonal from the supplied piece
        public static Piece CheckDiagonal(Board b, Piece p, int n, string direction)
        {
            Piece ReturnPiece = null;
            int x = p.x - '0';  // convert the char to an int
            int y = p.y - '0';  // convert the char to an int
            int yn = -1;
            int xn = -1;
            if (direction == "ne")
            {
                xn = (x - n);
                yn = (y + n);
            }
            if (direction == "nw")
            {
                xn = (x + n);
                yn = (y + n);
            }
            if (direction == "se")
            {
                xn = (x - n);
                yn = (y - n);
            }
            if (direction == "sw")
            {
                xn = (x + n);
                yn = (y - n);
            }

            if (xn > 0) // verify this doesn't overflow the board
                if (yn > 0)
                    if (xn < 9)
                        if (yn < 9)
                            ReturnPiece = GetPieceOnSquare(b, xn.ToString()[0], yn.ToString()[0]); 
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
            double tmp;
            // GetPieceOnSquare(b, p.x.ToString()[0], p.y.ToString()[0]); // this would be redundant
            // forward moves
            // -------------
            // 1. can move forward one space if nothing is blocking
            // check forward space - this is +1 if white or -1 if black on a 8x8 game 
            // TODO: Get Piece on Point
            // TODO: Get Piece by algebraic notation

            Point pt = new Point();

            Piece firstPiece = (p.color == "b") ? CheckForward(b, p, 1, "s") : CheckForward(b, p, 1, "n");
            if ( firstPiece == null)
            {
                if (Double.TryParse(p.x.ToString(), out tmp))
                    pt.X = tmp;
                if (Double.TryParse(p.y.ToString(), out tmp))
                {
                    pt.Y = tmp + 1;
                    if (p.color == "b")
                        pt.Y = tmp - 1;
                }       
                ValidPoints.Add(pt);

                if (p.moved == false)
                {
                    Piece secondPiece = (p.color == "b") ? CheckForward(b, p, 2, "s") : CheckForward(b, p, 2, "n");
                    if (secondPiece == null)
                    {
                        if (Double.TryParse(p.x.ToString(), out tmp))
                            pt.X = tmp;
                        if (Double.TryParse(p.y.ToString(), out tmp))
                            pt.Y = tmp + 2;
                        if (p.color == "b")
                            pt.Y = tmp - 2;
                        ValidPoints.Add(pt);
                    }

                }
            }

            //if (p.color == 'w' && p.y + 1)

            // 2. can move forward two spaces if nothing is blocking and piece has not moved yet

            // diagonal moves
            // ---------------
            // 1. can move forward left if an opposing color is on that square
            // 2. can move forward right if an opposing color is on that square
            string forward_right = (p.color == "b") ? "sw" : "ne";
            string forward_left = (p.color == "b") ? "se" : "nw";

            Piece forward_right_piece = CheckDiagonal(b, p, 1, forward_right);
            Piece forward_left_piece = CheckDiagonal(b, p, 1, forward_left);

            if ( forward_right_piece != null)
            {
                if (forward_right_piece.color != p.color)
                {
                    if (Double.TryParse(forward_right_piece.x.ToString(), out tmp))
                        pt.X = tmp;
                    if (Double.TryParse(forward_right_piece.y.ToString(), out tmp))
                        pt.Y = tmp;
                    ValidPoints.Add(pt);
                }
            }
            if (forward_left_piece != null)
            {
                if (forward_left_piece.color != p.color)
                {
                    if (Double.TryParse(forward_left_piece.x.ToString(), out tmp))
                        pt.X = tmp;
                    if (Double.TryParse(forward_left_piece.y.ToString(), out tmp))
                        pt.Y = tmp;
                    ValidPoints.Add(pt);
                }
            }

            return ValidPoints;
        }

        public static List<Point> GetEmptyDiagonalPieces(Board b, Piece p, string direction)
        {
            List<Point> ReturnPoints = new List<Point>();
            Piece TestPiece = null;
            int i = 1;
            bool EndOfBoard = false;
            int x = p.x - '0';   // convert from char to int
            int y = p.y - '0';
            while (TestPiece == null && !EndOfBoard)
            {
                TestPiece = CheckDiagonal(b, p, i, direction);
                if (TestPiece == null)
                {
                    if (direction == "ne")
                    {
                        if (y + i > 8 || x - i < 1)
                            EndOfBoard = true;
                        else
                        {
                            Point EmptySquare = new Point { X = (double)(x - i), Y = (double)(y + i) };
                            ReturnPoints.Add(EmptySquare);
                        }
                    }
                    if (direction == "nw")
                    {
                        if (y + i > 8 || x + i > 8)
                            EndOfBoard = true;
                        else
                        {
                            Point EmptySquare = new Point { X = (double)(x + i), Y = (double)(y + i) };
                            ReturnPoints.Add(EmptySquare);
                        }
                    }
                    if (direction == "sw")
                    {
                        if (y - i < 1 || x + i > 8)
                            EndOfBoard = true;
                        else
                        {
                            Point EmptySquare = new Point { X = (double)(x + i), Y = (double)(y - i) };
                            ReturnPoints.Add(EmptySquare);
                        }
                    }
                    if (direction == "se")
                    {
                        if (y - i < 1 || x - i < 1)
                            EndOfBoard = true;
                        else
                        {
                            Point EmptySquare = new Point { X = (double)(x - i), Y = (double)(y - i) };
                            ReturnPoints.Add(EmptySquare);
                        }
                    }
                    i++;
                }
                else if (TestPiece.color != p.color)
                {
                    ReturnPoints.Add(new Point { X = (double)(TestPiece.x - '0'), Y = (double)(TestPiece.y - '0') });
                    EndOfBoard = true;
                    break;
                }

            }
            return ReturnPoints;
        }

        public static List<Point> GetEmptyForwardPieces(Board b, Piece p, string direction)
        {
            List<Point> ReturnPoints = new List<Point>();
            Piece TestPiece = null;
            int i = 1;
            bool EndOfBoard = false;
            int x = p.x - '0';   // convert from char to int
            int y = p.y - '0';
            while (TestPiece == null && !EndOfBoard)
            {
                TestPiece = CheckForward(b, p, i, direction);
                if (TestPiece == null)
                {
                    if (direction == "n")
                    {
                        if (y + i > 8)
                            EndOfBoard = true;
                        else
                        {
                            Point EmptySquare = new Point { X = (double)(x), Y = (double)(y + i) };
                            ReturnPoints.Add(EmptySquare);
                        }
                    }

                    else if (direction == "s")
                    {
                        if (y - i < 1)
                            EndOfBoard = true;
                        else
                        {
                            Point EmptySquare = new Point { X = (double)(x), Y = (double)(y - i) };
                            ReturnPoints.Add(EmptySquare);
                        }
                    }

                    else if (direction == "e")
                    {
                        if (x - i < 1)
                            EndOfBoard = true;
                        else
                        {
                            Point EmptySquare = new Point { X = (double)(x - i), Y = (double)(y) };
                            ReturnPoints.Add(EmptySquare);
                        }
                    }

                    else if (direction == "w")
                    {
                        if (x + i > 8)
                            EndOfBoard = true;
                        else
                        {
                            Point EmptySquare = new Point { X = (double)(x + i), Y = (double)(y) };
                            ReturnPoints.Add(EmptySquare);
                        }
                    }

                    else
                        EndOfBoard = true;
                    i++;
                }
                else if (TestPiece.color != p.color)
                {
                    ReturnPoints.Add(new Point { X = (double)(TestPiece.x - '0'), Y = (double)(TestPiece.y - '0') });
                    EndOfBoard = true;
                    break;
                }

            }
            return ReturnPoints;
        }

        public static List<Point> ValidRookMoves(Board b, Piece p)
        {
            List<Point> ValidMoves = new List<Point>();
            // check north
            List<Point> NorthPoints = GetEmptyForwardPieces(b,p,"n");
            ValidMoves.AddRange(NorthPoints);
            // check south
            List<Point> SouthPoints = GetEmptyForwardPieces(b, p, "s");
            ValidMoves.AddRange(SouthPoints);
            // check east
            List<Point> EastPoints = GetEmptyForwardPieces(b, p, "e");
            ValidMoves.AddRange(EastPoints);
            // check west
            List<Point> WestPoints = GetEmptyForwardPieces(b, p, "w");
            ValidMoves.AddRange(WestPoints);
            return ValidMoves;
        }

        public static List<Point> ValidBishopMoves(Board b, Piece p)
        {
            List<Point> ValidMoves = new List<Point>();

            List<Point> NorthEastPoints = GetEmptyDiagonalPieces(b,p,"ne");
            ValidMoves.AddRange(NorthEastPoints);

            List<Point> NorthWestPoints = GetEmptyDiagonalPieces(b, p, "nw");
            ValidMoves.AddRange(NorthWestPoints);

            List<Point> SouthWestPoints = GetEmptyDiagonalPieces(b, p, "sw");
            ValidMoves.AddRange(SouthWestPoints);

            List<Point> SouthEastPoints = GetEmptyDiagonalPieces(b, p, "se");
            ValidMoves.AddRange(SouthEastPoints);

            return ValidMoves;
        }

        public static List<Point> ValidKnightMoves(Board b)
        {
            List<Point> ValidMoves = new List<Point>();
            return ValidMoves;
        }

        public static List<Point> ValidQueenMoves(Board b, Piece p)
        {
            List<Point> ValidMoves = new List<Point>();
            List<Point> BishopStylePoints = ValidBishopMoves(b, p);
            ValidMoves.AddRange(BishopStylePoints);
            return ValidMoves;
        }

        public static List<Point> ValidKingMoves(Board b)
        {
            List<Point> ValidMoves = new List<Point>();
            return ValidMoves;
        }


    }


}
