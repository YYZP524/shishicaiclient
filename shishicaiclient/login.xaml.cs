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
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace shishicaiclient
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class login : UserControl
    {
        public login()
        {
            InitializeComponent();
        }
 

        //向服务端发送登录申请数据
        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
           
            var o = new
            {
                opercode = "14",
                name = name.Text,
                password = password.Password,
                clientIP = PublicClass.localIP,
            };
            PublicClass.username = name.Text;
            var json = JsonConvert.SerializeObject(o);
            string str = json.ToString();
            PublicClass.loginjson = str;
            var message = PublicClass.loginjson;
            var outputBuffer = Encoding.Unicode.GetBytes(message);
            PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
        }
    }
}
