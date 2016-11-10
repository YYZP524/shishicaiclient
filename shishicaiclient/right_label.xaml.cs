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
    /// right_label.xaml 的交互逻辑
    /// </summary>
    public partial class right_label : UserControl
    {
        public right_label()
        {
            InitializeComponent();
        }
        public void creat_label(string comment)
        {
            Rectangle label = new Rectangle();
            label.Width = 80;
            label.Height = 25;
            label.Margin = new Thickness(1, 1, 0, 0);
            Label lab = new Label();
            lab.Content = comment;
            lab.FontSize = 16;
            lab.Foreground = Brushes.White;
            lab.Margin = new Thickness(0, -3, 0, 0);
            maincanvas.Children.Add(label);
            maincanvas.Children.Add(lab);
        }
    }
}
