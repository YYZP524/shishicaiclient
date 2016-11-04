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
    /// longhu.xaml 的交互逻辑
    /// </summary>
    public partial class Longhu : UserControl
    {
        public Longhu()
        {
            InitializeComponent();
        }

        public void create_line()
        {
            for (int i = 0; i < 7; i++)
            {
                Line line = new Line();
                line.X1 = 0;
                line.Y1 = i * 30 + 1;
                line.X2 = 600;
                line.Y2 = i * 30 + 1;
                line.StrokeThickness = 1;
                line.Stroke = Brushes.DodgerBlue;
                line.SnapsToDevicePixels = true;
                line.Opacity = 0.2;
                maincanvas.Children.Add(line);
            }
            for (int i = 0; i < 21; i++)
            {
                Line line = new Line();
                line.X1 = i * 30;
                line.Y1 = 0;
                line.X2 = i * 30;
                line.Y2 = 180;
                line.StrokeThickness = 1;
                line.Stroke = Brushes.DodgerBlue;
                line.SnapsToDevicePixels = true;
                line.Opacity = 0.2;
                maincanvas.Children.Add(line);
            }
        }

    }
}
