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
using System.Windows.Media.Animation;

namespace shishicaiclient
{
    /// <summary>
    /// Progress.xaml 的交互逻辑
    /// </summary>
    public partial class Progress : UserControl
    {
        public Progress()
        {
            InitializeComponent();
        }

        public void create_progress(int colortype,int element,int element_count,string content)//colortype 0 红 1蓝 2绿 element 占比个数 element_count 总数 content 内容
        {

                Rectangle rectborder = new Rectangle();
                rectborder.Width = 300;
                rectborder.Height = 20;
                rectborder.StrokeThickness = 1;
                rectborder.SnapsToDevicePixels = true;


                Rectangle rect = new Rectangle();
                rect.Width = float.Parse(element.ToString()) / float.Parse(element_count.ToString()) * 300d;
                rect.Height = 20;
                rect.SnapsToDevicePixels = true;

                if (colortype == 0)
                {
                    rectborder.Stroke = Brushes.OrangeRed;
                    rect.Fill = Brushes.OrangeRed;
                }
                else if (colortype == 1)
                {
                    rectborder.Stroke = Brushes.DodgerBlue;
                    rect.Fill = Brushes.DodgerBlue;
                }
                else
                {
                    rectborder.Stroke = Brushes.LimeGreen;
                    rect.Fill = Brushes.LimeGreen;
                }

                rect.Margin = new Thickness(45, 5, 0, 0);
                rectborder.Margin = new Thickness(45, 5, 0, 0);
                maincanvas.Children.Add(rectborder);
                maincanvas.Children.Add(rect);
                typelab.Content = content+" "+element;
                presslab.Content = (int)(float.Parse(element.ToString()) / float.Parse(element_count.ToString()) * 100f) + "%";



                DoubleAnimation wid = new DoubleAnimation();
                wid.From = 0;
                wid.To = float.Parse(element.ToString()) / float.Parse(element_count.ToString()) * 300f;
                wid.Duration = TimeSpan.FromSeconds(1);
                rect.BeginAnimation(Rectangle.WidthProperty, wid);



        }

    }
}
