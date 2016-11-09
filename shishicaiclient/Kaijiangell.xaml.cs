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
    /// Kaijiangell.xaml 的交互逻辑
    /// </summary>
    public partial class Kaijiangell : UserControl
    {
        public Kaijiangell()
        {
            InitializeComponent();
        }


        public void create_kaijiang(string content, int colortype)
        {

            Ellipse kaiell = new Ellipse();

            kaiell.Width = 38;

            kaiell.Height = 38;

            Label kailab = new Label();
           
            kailab.Content = content;

            if (colortype == 0)
            {
                kaiell.Fill = Brushes.OrangeRed;
                kailab.Foreground = Brushes.White;
                kailab.Margin = new Thickness(2, -2, 0, 0);
            }

            else
            {

                kaiell.Fill = Brushes.OrangeRed;
                kailab.Margin = new Thickness(0, -2, 0, 0);

            }

            maincanvas.Children.Add(kaiell);

            maincanvas.Children.Add(kailab);



        }
            
    }
}
