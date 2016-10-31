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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

       public string str = null;
       
              private void zhuce_Click(object sender, RoutedEventArgs e)
        {
            if (password.Password == passwordchar.Password)//判断两次输入是否一致
            {

                //用户名密码封装json
                var o = new
                {
                    opercode = "13",
                    name = name.Text,
                    password = password.Password,
                };
                var json = JsonConvert.SerializeObject(o);
                str = json.ToString();
                str = "[" + str + "]";
                PublicClass.zhucejson = str;
                var message = PublicClass.zhucejson;
                var outputBuffer = Encoding.Unicode.GetBytes(message);
              PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
  
            }

            else
            {
                passwordchar.Password = "";
                password.Password = "";
                MessageBox.Show("两次输入密码不一致");
            }
        }
        
    }
}
