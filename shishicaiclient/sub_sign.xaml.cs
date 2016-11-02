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
    /// login1.xaml 的交互逻辑
    /// </summary>
    public partial class sub_sign : UserControl
    {
        public sub_sign()
        {
            InitializeComponent();
        }

        private void reg_Click(object sender, RoutedEventArgs e)//注册单击事件
        {
            //先判断用户名是否为空
            if (username.Text == "")//用户名

            {
                pwd.Password = "";//密码
                cf_pwd.Password = "";//确认密码
                MessageBox.Show("用户名不能为空！");
            }
                //判断密码是否为空
            else if (pwd.Password == "")//密码
            {
                pwd.Password = "";
                cf_pwd.Password = "";
                MessageBox.Show("密码不能为空！");
            }
            //判断两次输入密码是否一致
            else if (pwd.Password != cf_pwd.Password)
            {
                pwd.Password = "";
                cf_pwd.Password = "";
                MessageBox.Show("两次输入密码不一致");

            }
            else//判断结束
            {

                //用户名密码封装json
                var o = new
                {
                    opercode = "13",
                    name = username.Text,
                    password = pwd.Password,
                   
                };
                var json = JsonConvert.SerializeObject(o);//json传值
                string str = null;
                str = json.ToString();
                str = "[" + str + "]";
                PublicClass.zhucejson = str;
                var message = PublicClass.zhucejson;
                var outputBuffer = Encoding.Unicode.GetBytes(message);
                PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);

            }
        }
 
    
    
    
    
    }

}
