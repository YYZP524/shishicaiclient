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
        public void create_lab(string content, int colortype)//创建圆ell 设置宽高
        {

            Ellipse ell = new Ellipse();//新建“ell”圆

            ell.Width = 22;

            ell.Height = 22;

            Label lab = new Label();//新建”lab“lable

            lab.Content = content;//lab内容

            if (colortype == 0)//判断
            {
                ell.Fill = Brushes.OrangeRed;//为0时圆ell为橘红色
                lab.Foreground = Brushes.White;//为0时lab数字为白色
                lab.Margin = new Thickness(2, -2, 0, 0);//数字位置
            }

            else
            {

                ell.Fill = Brushes.LightGray;//为1时圆ell为亮灰色，lab内数字默认为黑色就不写出来
                lab.Margin = new Thickness(0, -2, 0, 0);//位置

            }

            maincanvas.Children.Add(ell);//把画好的圆放在 maincanvas上

            maincanvas.Children.Add(lab);//把定义好的lab放在圆上



        }
    }
}
