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
    public partial class Effectanimation : UserControl
    {
        public Effectanimation()
        {
            InitializeComponent();
        }

      

        public void animationl(string opencode)
        {
            string[] aa = opencode.Split(',');
           

            if(int.Parse(aa[0])>int.Parse(aa[4]))
            {
                back.Content="龙";
                font.Content="龙";
                back.Foreground = Brushes.OrangeRed;
                font.Foreground = Brushes.OrangeRed;
            }
            else if (int.Parse(aa[0]) < int.Parse(aa[4]))
            {

                back.Content = "虎";
                font.Content = "虎";
                back.Foreground = Brushes.Blue;
                font.Foreground = Brushes.Blue;


            }
            else
            {
                back.Content = "和";
                font.Content = "和";
                back.Foreground = Brushes.Green;
                font.Foreground = Brushes.Green;
            }

            DoubleAnimation backanmi = new DoubleAnimation();
            backanmi.To = 200;
            backanmi.Duration = TimeSpan.FromSeconds(0.6);
            back.BeginAnimation(Label.FontSizeProperty, backanmi);

            DoubleAnimation backop = new DoubleAnimation();
            backop.To = 0;
            backop.Duration = TimeSpan.FromSeconds(0.6);
            back.BeginAnimation(Label.OpacityProperty, backop);




            DoubleAnimation fontanmi = new DoubleAnimation();
            fontanmi.To = 100;
            fontanmi.Duration = TimeSpan.FromSeconds(1.5);
            font.BeginAnimation(Label.FontSizeProperty, fontanmi);



            DoubleAnimation fontop = new DoubleAnimation();
            fontop.To = 0;
            fontop.Duration = TimeSpan.FromSeconds(1.5);
            font.BeginAnimation(Label.OpacityProperty, fontop);

        }
        public void animationd(string opencode)
        {
            string[] aa = opencode.Split(',');
           
            int daxiaocount = int.Parse(aa[1]) + int.Parse(aa[2]) + int.Parse(aa[3]) + int.Parse(aa[4]) + int.Parse(aa[0]);
            if (daxiaocount > 23)
            {
                back.Content = "大";
                font.Content = "大";
                back.Foreground = Brushes.OrangeRed;
                font.Foreground = Brushes.OrangeRed;
            }
            else
            {
                back.Content = "小";
                font.Content = "小";
                back.Foreground = Brushes.Green;
                font.Foreground = Brushes.Green;
            }
            DoubleAnimation backanmi = new DoubleAnimation();
            backanmi.To = 200;
            backanmi.Duration = TimeSpan.FromSeconds(0.3);
            back.BeginAnimation(Label.FontSizeProperty, backanmi);

            DoubleAnimation backop = new DoubleAnimation();
            backop.To = 0;
            backop.Duration = TimeSpan.FromSeconds(0.3);
            back.BeginAnimation(Label.OpacityProperty, backop);




            DoubleAnimation fontanmi = new DoubleAnimation();
            fontanmi.To = 100;
            fontanmi.Duration = TimeSpan.FromSeconds(1);
            font.BeginAnimation(Label.FontSizeProperty, fontanmi);



            DoubleAnimation fontop = new DoubleAnimation();
            fontop.To = 0;
            fontop.Duration = TimeSpan.FromSeconds(1);
            font.BeginAnimation(Label.OpacityProperty, fontop);
                }



        
        public void animations(string opencode)
        {
            string[] aa = opencode.Split(',');
            int danshaungcount = int.Parse(aa[1]) + int.Parse(aa[2]) + int.Parse(aa[3]) + int.Parse(aa[4]) + int.Parse(aa[0]);
            if (danshaungcount % 2 == 0)
            {
                back.Content = "单";
                font.Content = "单";
                back.Foreground = Brushes.OrangeRed;
                font.Foreground = Brushes.OrangeRed;
            }


            else
            {

                back.Content = "双";
                font.Content = "双";
                back.Foreground = Brushes.Green;
                font.Foreground = Brushes.Green;
            }

      

            DoubleAnimation backanmi = new DoubleAnimation();
            backanmi.To = 200;
            backanmi.Duration = TimeSpan.FromSeconds(0.3);
            back.BeginAnimation(Label.FontSizeProperty, backanmi);

            DoubleAnimation backop = new DoubleAnimation();
            backop.To = 0;
            backop.Duration = TimeSpan.FromSeconds(0.3);
            back.BeginAnimation(Label.OpacityProperty, backop);




            DoubleAnimation fontanmi = new DoubleAnimation();
            fontanmi.To = 100;
            fontanmi.Duration = TimeSpan.FromSeconds(1);
            font.BeginAnimation(Label.FontSizeProperty, fontanmi);



            DoubleAnimation fontop = new DoubleAnimation();
            fontop.To = 0;
            fontop.Duration = TimeSpan.FromSeconds(1);
            font.BeginAnimation(Label.OpacityProperty, fontop);
        }







        }

     




    }





