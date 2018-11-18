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
            //for (int i = 0; i < 10; i++)
            //{
            //    ColumnDefinition c1 = new ColumnDefinition();
            //    c1.Width = GridLength.Auto;
            //    MainGrid.ColumnDefinitions.Add(c1);

            //    RowDefinition rd = new RowDefinition();
            //    rd.Height = GridLength.Auto;
            //    MainGrid.RowDefinitions.Add(new RowDefinition());
            //}


            //for (int i=0; i < 8; i++)
            //{
            //    // var cell = MainGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
            //    var cell = MainGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == 0 && Grid.GetColumn(e) == i);

            //}
        }

        private void InitializeChessboard()
        {
            GridLengthConverter myGridLengthConverter = new GridLengthConverter();
            GridLength side = (GridLength)myGridLengthConverter.ConvertFromString("2*");
            for (int i = 0; i < 9; i++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                MainGrid.ColumnDefinitions[i].Width = side;
                MainGrid.RowDefinitions.Add(new RowDefinition());
                MainGrid.RowDefinitions[i].Height = side;
            }

        }

        private void FillInitializedChessboard()
        {
            //int squareSize = Math.Max(
            //        (double)(Window.WidthProperty) / 8,
            //        (double)( Window.HeightProperty*0.125) );
            int squareSize = 50;
            var a = Window.WidthProperty;
            Rectangle[,] square = new Rectangle[9, 9];
            for (int row = 0; row < 9; row++)
                for (int col = 0; col < 9; col++)
                {
                    square[row, col] = new Rectangle();
                    square[row, col].Height = squareSize;
                    square[row, col].Width = squareSize;
                    Grid.SetColumn(square[row, col], col);
                    Grid.SetRow(square[row, col], row);
                    if ((row + col) % 2 == 0)
                    {
                        square[row, col].Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                    }
                    else
                    {
                        square[row, col].Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    }
                    MainGrid.Children.Add(square[row, col]);
                }
        }
    }
}
