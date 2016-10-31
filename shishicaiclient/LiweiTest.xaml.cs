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
            StackPanel mystack = new StackPanel();
            mystack.Orientation = Orientation.Horizontal;
            Leftelll newleft = new Leftelll();
            newleft.create_lab("1", 0);
            newleft.Width = 30;
            newleft.Height = 30;
            mystack.Children.Add(newleft);
        }
    }
}
