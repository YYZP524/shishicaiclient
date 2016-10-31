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
    /// LeftEll.xaml 的交互逻辑
    /// </summary>
    public partial class LeftEll : UserControl
    {
        public LeftEll()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void create_lab(string content, int colortype)
        {

            Ellipse ell = new Ellipse();

            ell.Width = 22;

            ell.Height = 22;

            Label lab = new Label();

            lab.Content = content;

            if (colortype == 0)
            {
                ell.Fill = Brushes.OrangeRed;
                lab.Foreground = Brushes.White;
                lab.Margin = new Thickness(2, -2, 0, 0);
            }

            else
            {

                ell.Fill = Brushes.LightGray;
                lab.Margin = new Thickness(0, -2, 0, 0);

            }

            maincanvas.Children.Add(ell);

            maincanvas.Children.Add(lab);



        }
    }
}
