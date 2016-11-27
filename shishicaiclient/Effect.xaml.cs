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
    /// Effect.xaml 的交互逻辑
    /// </summary>
    public partial class Effect : UserControl
    {
        public Effect()
        {
            InitializeComponent();
        }

        public void create_effect(string content, int colortype)
        {
            Label labx = new Label();//新建”labx“lable
            labx.Content = "龙";
            labx.HorizontalContentAlignment = HorizontalAlignment.Center;
            labx.Margin = new Thickness(500, 200, 0, 0);
            labx.FontSize = 12;
            labx.Foreground = Brushes.OrangeRed;




            maincanvas.Children.Add(labx);
          
           



        }

        private void anmi_Click(object sender, RoutedEventArgs e)
        {


            DoubleAnimation backanmi = new DoubleAnimation();
            backanmi.To = 200;
            backanmi.Duration = TimeSpan.FromSeconds(0.5);
            back.BeginAnimation(Label.FontSizeProperty, backanmi);

            DoubleAnimation backop = new DoubleAnimation();
            backop.To = 0;
            backop.Duration = TimeSpan.FromSeconds(0.5);
            back.BeginAnimation(Label.FontSizeProperty, backop);




            DoubleAnimation fontanmi = new DoubleAnimation();
            fontanmi.To = 100;
            fontanmi.Duration = TimeSpan.FromSeconds(1.5);
            back.BeginAnimation(Label.FontSizeProperty, fontanmi);



            DoubleAnimation fontop = new DoubleAnimation();
            fontop.To = 100;
            fontop.Duration = TimeSpan.FromSeconds(1.5);
            font.BeginAnimation(Label.FontSizeProperty, fontop);








        }





    }



}

