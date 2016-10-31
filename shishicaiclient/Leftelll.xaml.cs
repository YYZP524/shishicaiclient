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

        public void create_lab(string content, int colortype)//创建圆标签，width30,height30
        {
            Ellipse ell = new Ellipse();//定义圆:ell 
            //圆的宽高
            ell.Width = 28;
            ell.Height = 28;
          
            //圆内数字:lab
            Label lab = new Label();//定义lab
            lab.Content = content;//lab内容
            //颜色判断
            if (colortype == 0)
            {
                ell.Fill = Brushes.OrangeRed;//0为红色
                lab.Foreground = Brushes.White;//数字为白色
            }
            else
            {
                ell.Fill = Brushes.LightGray;//1为亮灰色
            }

            maincanvas.Children.Add(ell);//把ell(圆)放在定义的maincanvas上
            maincanvas.Children.Add(lab);//把lab（数字）放在圆上

        }


    }
}
