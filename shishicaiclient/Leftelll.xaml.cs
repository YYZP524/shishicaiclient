using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace shishicaiclient
{
    /// <summary>
    /// Leftelll.xaml 的交互逻辑
    /// </summary>
    public partial class Leftelll : UserControl
    {
        public Leftelll()
        {
            InitializeComponent();
        }

        public void create_lab(string content, int colortype)
        {
            Ellipse ell = new Ellipse();
            ell.Width = 28;
            ell.Height = 28;
            Label lab = new Label();
            lab.Content = content;
            if (colortype == 0)
            {
                ell.Fill = Brushes.OrangeRed;
                lab.Foreground = Brushes.White;
            }
            else
            {
                ell.Fill = Brushes.LightGray;
            }
            maincanvas.Children.Add(ell);
            maincanvas.Children.Add(lab);

        }


    }
}
