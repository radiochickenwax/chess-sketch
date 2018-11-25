using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using gsChessLib;

namespace chess_sketch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game.Board Board { get; set; }

        Dictionary<string, string> PiecesToPngDict = new Dictionary<string, string> {
            { "R", "wr.png" },
            { "N", "wn.png" },
            { "B", "wb.png" },
            { "K", "wk.png" },
            { "Q", "wq.png" },
            { "P", "wp.png" },
            { "r", "br.png" },
            { "n", "bn.png" },
            { "b", "bb.png" },
            { "k", "bk.png" },
            { "q", "bq.png" },
            { "p", "bp.png" }
        };

        public MainWindow()
        {
            InitializeComponent();
            InitializeChessboard();


            // these are UI only -- they need to link to the Game.cs
            FillInitializedChessboard();
            // FillInitialPieces();

            Board = new Game.Board("RNBKQBNR\nPPPPPPPP\n........\np.......\n........\n........\n.ppppppp\nrnbkqbnr");
            ViewBoardString();

        }

        private void ViewBoardString()
        {
            Board.BoardStringToPieces();
            foreach (Game.Piece p in Board.Pieces)
            {
                string PngPieceName = PiecesToPngDict[p.type];
                int x = p.y - '0' - 1;  // convert char to int
                int y = p.x - '0' - 1;  // convert char to int
                PlacePieceOnSquare(PngPieceName, x, y);
            }

        }

        private void GetPieceFromPngName() { }


        // No longer needed
        //private void FillInitialPieces()
        //{
        //    for (int i = 0; i < 8; i++)
        //    {
        //        PlacePieceOnSquare("bp.png", 1, i);
        //        PlacePieceOnSquare("wp.png", 6, i);
        //    }
        //    List<string> WhiteOfficers = new List<string> { "wr.png", "wn.png", "wb.png", "wk.png", "wq.png", "wb.png", "wn.png", "wr.png" };
        //    List<string> BlackOfficers = new List<string> { "br.png", "bn.png", "bb.png", "bq.png", "bk.png", "bb.png", "bn.png", "br.png"  };
        //    for (int i = 0; i < WhiteOfficers.Count; i++)
        //    {
        //        PlacePieceOnSquare(WhiteOfficers[i], 7, i);
        //    }
        //    for (int i = 0; i < BlackOfficers.Count; i++)
        //    {
        //        PlacePieceOnSquare(BlackOfficers[i], 0, i);
        //    }
            
        //}

        private void PlacePieceOnSquare(string piece, int row, int col)
        {
            // remove old viewbox -> this should be it's own method?
            Viewbox v = GetViewBoxOnGrid(row, col);
            if (v != null)
            {
                v.Child = null;
                MainGrid.Children.Remove(v);
            }
            
            // add new viewbox and image
            Viewbox vb = new Viewbox();
            vb.Name = "vb_" + row.ToString() + "_" + col.ToString();

            Image pawnImage = new Image();
            pawnImage.Name = "im_" + row.ToString() + "_" + col.ToString();

            vb.MouseDown += SquareClicked;
            pawnImage.Source = new BitmapImage(new Uri(piece, UriKind.RelativeOrAbsolute));
            Grid.SetRow(vb, row);
            Grid.SetColumn(vb, col);
            MainGrid.Children.Add(vb);
            vb.Child = pawnImage;
        }

        private Viewbox GetViewBoxOnGrid(int row, int col)
        {
            Viewbox v = null;
            var element = MainGrid.Children
                .Cast<UIElement>()
                .First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);
            if (element != null)
                if (element.GetType().Name == "Viewbox")
                    v = (Viewbox)element;
            return v;
        }

        private void SquareClicked(object sender, MouseEventArgs e)
        {
            if (sender.GetType().Name == "Viewbox")
            {
                Viewbox v = (Viewbox)sender;
                string name = v.Name;
                // MessageBox.Show("hello " + name);
                SidePanelTextBox.Text += "\n" + name + " clicked.";
                SidePanelTextBox.ScrollToEnd();
            }

            if (sender.GetType().Name == "Image")
            {
                Image i = (Image)sender;
                string name = i.Name;
                //MessageBox.Show("hello " + name);
                SidePanelTextBox.Text += "\n" + name + " clicked.";
                SidePanelTextBox.ScrollToEnd();
            }
            
        }

       
        private void InitializeChessboard()
        {

            for (int i = 0; i < 8; i++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }

        }

        private void FillInitializedChessboard()
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    Border b = new Border();

                    Label square = new Label();
                    square.Content = row.ToString() + "," + col.ToString();

                    Grid.SetColumn(b, col);
                    Grid.SetRow(b, row);
                    if ((row + col) % 2 == 0)
                    {
                        b.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                        square.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    }
                    else
                    {
                        b.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                        square.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                    }
                    MainGrid.Children.Add(b);
                    b.Child = square;
                }
        }

    }
}
