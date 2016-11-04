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
    /// LiweiTest.xaml 的交互逻辑
    /// </summary>
    public partial class LiweiTest : UserControl
    {
        public LiweiTest()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //rectlab rect = new rectlab();
            //rect.Width = 30;
            //rect.Height = 30;
            //rect.create_rect(2, "龙");
            //maincanvas.Children.Add(rect);

            elllab ell = new elllab();
            ell.Width = 30;
            ell.Height = 30;
            ell.Margin = new Thickness(0, 0, 0, 0);
            ell.create_ell(0, "");
            if (maincanvas.Children.Count == 0)
            {
                maincanvas.Children.Add(ell);
            }
            
        }
    }
}
