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
    /// rectlab.xaml 的交互逻辑
    /// </summary>
    public partial class rectlab : UserControl
    {
        public rectlab()
        {
            InitializeComponent();
        }
        public void create_rect(int colortype,string content)//创建方框 colortype 0 红 1蓝 2绿
        {
            Rectangle rect = new Rectangle();
            rect.Width = 26;
            rect.Height = 26;
            if (colortype == 0)
            {
                rect.Fill = Brushes.OrangeRed;
            }
            else if (colortype == 1)
            {
                rect.Fill = Brushes.DodgerBlue;
            }
            else
            {
                rect.Fill = Brushes.LimeGreen;
            }




            Label lab = new Label();
            lab.Content = content;
            lab.FontSize = 16;
            lab.Foreground = Brushes.White;
            lab.Margin = new Thickness(0, -3, 0, 0);
            maincanvas.Children.Add(rect);
            maincanvas.Children.Add(lab);
          
        }
        

        
    }
}
