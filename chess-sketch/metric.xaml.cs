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
using System.Windows.Shapes;

namespace chess_sketch
{
    /// <summary>
    /// Interaction logic for metric.xaml
    /// </summary>
    public partial class metric : Window
    {
        public metric()
        {
            InitializeComponent();
            Viewbox v = new Viewbox();
            Image i = new Image();
            i.Source =  new BitmapImage(new Uri("bp.png", UriKind.RelativeOrAbsolute));
            
            Grid.SetRow(v, 1);
            Grid.SetColumn(v, 1);
            metricgrid.Children.Add(v);
            v.Child = i;
        }
    }
}
