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
    /// LiweiTest.xaml 的交互逻辑
    /// </summary>
    public partial class LiweiTest : UserControl
    {
        public LiweiTest()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            for (int i = 0; i < 5; i++)
            {
                LeftEll ell = new LeftEll();
                ell.Width = 24;
                ell.Height = 24;
                ell.create_lab(i.ToString(), 0);
                stack.Children.Add(ell);
            }
            LeftEll ell2 = new LeftEll();
            ell2.Width = 24;
            ell2.Height = 24;
            ell2.create_lab("龙", 1);
            stack.Children.Add(ell2);
            listbox.Items.Add(stack);
        }
    }
}
