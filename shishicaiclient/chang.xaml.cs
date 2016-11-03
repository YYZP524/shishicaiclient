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
using Newtonsoft.Json;
using System.Net.Sockets;

namespace shishicaiclient
{
    /// <summary>
    /// changpwd.xaml 的交互逻辑
    /// </summary>
    public partial class chang : UserControl
    {
        public chang()
        {
            InitializeComponent();
        }
        string str = "";
        private void reg_Click(object sender, RoutedEventArgs e)
        {
            var o = new
            {
                opercode = "7",
                username = PublicClass.username,
                oldpassword = oldpwd.Password,
                newpassword = newpwd.Password    
            }; 
            var json = JsonConvert.SerializeObject(o);
            str = json.ToString();
            PublicClass.loginjson = str;
            var message = PublicClass.loginjson;
            var outputBuffer = Encoding.Unicode.GetBytes(message);
            PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
        }
    }
}
