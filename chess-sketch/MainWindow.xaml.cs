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

namespace chess_sketch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeChessboard();
            FillInitializedChessboard();
            FillInitialPieces();

        }

        private void FillInitialPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                PlacePieceOnSquare("bp.png", 1, i);
                PlacePieceOnSquare("wp.png", 6, i);
            }
            List<string> WhiteOfficers = new List<string> { "wr.png", "wn.png", "wb.png", "wk.png", "wq.png", "wb.png", "wn.png", "wr.png" };
            List<string> BlackOfficers = new List<string> { "br.png", "bn.png", "bb.png", "bq.png", "bk.png", "bb.png", "bn.png", "br.png"  };
            for (int i = 0; i < WhiteOfficers.Count; i++)
            {
                PlacePieceOnSquare(WhiteOfficers[i], 7, i);
            }
            for (int i = 0; i < BlackOfficers.Count; i++)
            {
                PlacePieceOnSquare(BlackOfficers[i], 0, i);
            }
        }

        private void PlacePieceOnSquare(string piece, int row, int col)
        {
            // TODO:  remove old viewbox
            Viewbox vb = new Viewbox();
            Image pawnImage = new Image();
            pawnImage.Source = new BitmapImage(new Uri(piece, UriKind.RelativeOrAbsolute));
            Grid.SetRow(vb, row);
            Grid.SetColumn(vb, col);
            MainGrid.Children.Add(vb);
            vb.Child = pawnImage;

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
