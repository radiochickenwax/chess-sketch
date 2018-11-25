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
using System.Windows.Media.Effects;
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
        public Game.Board Board { get; set; }
        public string BoardString { get; set; }

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
            DataContext = this;
            InitializeChessboard();

            FillInitializedChessboard(); // this is UI only 

            BoardString = "RNBKQBNR\nPPPPPPPP\n........\np.......\n........\n........\n.ppppppp\nrnbkqbnr";
            Board = new Game.Board(BoardString);
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

        private string GetPieceFromPngName(string value)
        {
            return PiecesToPngDict.FirstOrDefault(x => x.Value == value).Key;
        }

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

        private Border GetBorderOnGrid(int row, int col)
        {
            Border v = null;
            var element = MainGrid.Children
                .Cast<UIElement>()
                .First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);
            if (element != null)
                if (element.GetType().Name == "Border")
                    v = (Border)element;
            return v;
        }

        private void SquareClicked(object sender, MouseEventArgs e)
        {
            if (sender.GetType().Name == "Viewbox")
            {
                Viewbox v = (Viewbox)sender;
                string name = v.Name;
                SidePanelTextBox.Text += "\n" + name + " clicked.";
                SidePanelTextBox.ScrollToEnd();
                // get coords from name
                string[] vals = name.Split('_');
                if (vals.Count() == 3)
                {
                    string x_str = vals[1];
                    string y_str = vals[2];
                    int x = x_str[0] - '0';
                    int y = y_str[0] - '0';
                    SidePanelTextBox.Text += String.Format("X: {0} Y: {0}", x, y );
                    // Need to register this square as "clicked" somewhere upstream
                    // Viewbox v2 = GetViewBoxOnGrid(x,y);
                    LightUpBorderOnGrid(x, y);
                }
                
                // get image name from grid?
                
            }
        }

        private void LightUpBorderOnGrid(int row, int col)
        {
            Border b = GetBorderOnGrid(row, col);
            
            if (b != null)
            {
                //var blur = new BlurEffect();
                //var glow = new OuterGlowBitmapEffect();
                //var shade = new DropShadowEffect { Color=Colors.Red, BlurRadius=20, Opacity=1, ShadowDepth=100};
                //blur.Radius = 20;
                ////blur.KernelType = color
                //// b.Effect = blur;
                //// b.Effect = glow;
                //b.Effect = shade;
                Border light = new Border { Background = Brushes.Red, Opacity = 0.6, Name = "lb_" + row.ToString() + "_" + col.ToString() };
                Grid.SetRow(light, row);
                Grid.SetColumn(light, col);
                MainGrid.Children.Add(light);
            }
        }

        private void UnLightUpBorderOnGrid(int row, int col)
        {
            Border b = GetBorderOnGrid(row, col);

            if (b != null)
            {
                b.Effect = null;
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
