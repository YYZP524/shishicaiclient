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
    /// elllab.xaml 的交互逻辑
    /// </summary>
    public partial class elllab : UserControl
    {
        public elllab()
        {
            InitializeComponent();
        }

        public void create_ell(int colortype, string content)
        {
            Ellipse ell = new Ellipse();
            ell.Width = 28;
            ell.Height = 28;
            ell.StrokeThickness = 2;
            ell.Name="ell";
            ell.MouseEnter += new MouseEventHandler(UserControl_MouseEnter);
            ell.MouseLeave += new MouseEventHandler(UserControl_MouseLeave);
            Ellipse anmiell = new Ellipse();
            anmiell.Width = 28;
            anmiell.Height = 28;
            anmiell.StrokeThickness = 0;
            anmiell.Opacity = 0;
            anmiell.Name = "anmiell";
            anmiell.MouseEnter += new MouseEventHandler(UserControl_MouseEnter);
            anmiell.MouseLeave += new MouseEventHandler(UserControl_MouseLeave);
            Label lab = new Label();
            lab.Content = content;
            lab.FontSize = 16;
            lab.Margin = new Thickness(1, -3, 0, 0);
            lab.MouseEnter += new MouseEventHandler(UserControl_MouseEnter);
            lab.MouseLeave += new MouseEventHandler(UserControl_MouseLeave);
            if (colortype == 0)
            {
                ell.Stroke = Brushes.OrangeRed;
                lab.Foreground = Brushes.OrangeRed;
                anmiell.Stroke = Brushes.OrangeRed;
            }
            else
            {
                ell.Stroke = Brushes.DodgerBlue;
                lab.Foreground = Brushes.DodgerBlue;
                anmiell.Stroke = Brushes.DodgerBlue;
            }


            
            
            maincanvas.Children.Add(ell);
            maincanvas.Children.Add(anmiell);
            maincanvas.Children.Add(lab);
          
        }


        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Ellipse anmiell = new Ellipse();
            foreach (var ch in maincanvas.Children)
            {
                anmiell = ch as Ellipse;
                if (anmiell != null)
                {
                    if (anmiell.Name == "anmiell")
                    {
                        break;
                    }
                }
            }
            DoubleAnimation stroke = new DoubleAnimation();
            stroke.To = 14;
            stroke.Duration = TimeSpan.FromSeconds(0.5);
            anmiell.BeginAnimation(Ellipse.StrokeThicknessProperty, stroke);

            DoubleAnimation anmiop = new DoubleAnimation();
            anmiop.To = 0.4;
            anmiop.Duration = TimeSpan.FromSeconds(0.5);
            anmiell.BeginAnimation(Ellipse.OpacityProperty, anmiop);

            Ellipse ell = new Ellipse();
            foreach (var ch in maincanvas.Children)
            {
                ell = ch as Ellipse;
                if (ell != null)
                {
                    if (ell.Name == "ell")
                    {
                        break;
                    }
                }
            }
            DoubleAnimation ellop = new DoubleAnimation();
            ellop.To = 0;
            ellop.Duration = TimeSpan.FromSeconds(0.5);
            ell.BeginAnimation(Ellipse.OpacityProperty, ellop);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Ellipse anmiell = new Ellipse();
            foreach (var ch in maincanvas.Children)
            {
                anmiell = ch as Ellipse;
                if (anmiell != null)
                {
                    if (anmiell.Name == "anmiell")
                    {
                        break;
                    }
                }
            }
            DoubleAnimation stroke = new DoubleAnimation();
            stroke.To = 0;
            stroke.Duration = TimeSpan.FromSeconds(0.5);
            anmiell.BeginAnimation(Ellipse.StrokeThicknessProperty, stroke);

            DoubleAnimation anmiop = new DoubleAnimation();
            anmiop.To = 0;
            anmiop.Duration = TimeSpan.FromSeconds(0.5);
            anmiell.BeginAnimation(Ellipse.OpacityProperty, anmiop);

            Ellipse ell = new Ellipse();
            foreach (var ch in maincanvas.Children)
            {
                ell = ch as Ellipse;
                if (ell != null)
                {
                    if (ell.Name == "ell")
                    {
                        break;
                    }
                }
            }
            DoubleAnimation ellop = new DoubleAnimation();
            ellop.To = 1;
            ellop.Duration = TimeSpan.FromSeconds(0.5);
            ell.BeginAnimation(Ellipse.OpacityProperty, ellop);
        }

    }
}
