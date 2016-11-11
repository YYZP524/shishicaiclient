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

                Rectangle rectborder = new Rectangle();//边框
                rectborder.Width = 300;
                rectborder.Height = 20;
                rectborder.StrokeThickness = 1;//边框
                rectborder.SnapsToDevicePixels = true;//像素对齐


                Rectangle rect = new Rectangle();
                rect.Width = float.Parse(element.ToString()) / float.Parse(element_count.ToString()) * 300d;//内部元素宽度
                rect.Height = 20;
                rect.SnapsToDevicePixels = true;//像素对齐

                if (colortype == 0)//判断颜色(0 红色 1 蓝色 其他为绿色）
                {
                    rectborder.Stroke = Brushes.OrangeRed;//红色
                    rect.Fill = Brushes.OrangeRed;//内部填充色红色
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

                rect.Margin = new Thickness(45, 5, 0, 0);//位置
                rectborder.Margin = new Thickness(45, 5, 0, 0);
                maincanvas.Children.Add(rectborder);
                maincanvas.Children.Add(rect);
                typelab.Content = content+" "+element;//名称如“龙”“虎'
                presslab.Content = (int)(float.Parse(element.ToString()) / float.Parse(element_count.ToString()) * 100f) + "%";//进度条计算（如一期开出的“龙”占这期总数的百分比）



                DoubleAnimation wid = new DoubleAnimation();//进度条动画
                wid.From = 0;
                wid.To = float.Parse(element.ToString()) / float.Parse(element_count.ToString()) * 300f;
                wid.Duration = TimeSpan.FromSeconds(1);//进度条动画时间1秒
                rect.BeginAnimation(Rectangle.WidthProperty, wid);



        }

    }
}
