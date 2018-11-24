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

// 2018-11-18
// to really understand this -> https://stackoverflow.com/questions/15681352/transitioning-from-windows-forms-to-wpf/15684569#15684569
// https://rachel53461.wordpress.com/2011/05/08/simplemvvmexample/
// TODO: make a new project for that - link to it here
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
            DataContext = new ChessBoard();
        }
    }

    public class ChessBoard
    {
        public List<ChessSquare> Squares { get; private set; }

        public Command<ChessSquare> SquareClickCommand { get; private set; }

        public ChessBoard()
        {
            Squares = new List<ChessSquare>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Squares.Add(new ChessSquare() { Row = i, Column = j });
                }
            }

            SquareClickCommand = new Command<ChessSquare>(OnSquareClick);
        }

        private void OnSquareClick(ChessSquare square)
        {
            MessageBox.Show("You clicked on Row: " + square.Row + " - Column: " + square.Column);
            // TODO:  show available moves (light them or something)
            // you need to get this from the Game.cs and you need the board state to do it
        }
    }
    public class ChessSquare
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public bool IsBlack { get { return (Row + Column) % 2 == 1; } }
    }

    public class Command<T> : ICommand
    {
        public Action<T> Action { get; set; }

        public void Execute(object parameter)
        {
            if (Action != null && parameter is T)
                Action((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;

        public Command(Action<T> action)
        {
            Action = action;
        }
    }
}
