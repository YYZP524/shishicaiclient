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
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using C1.WPF;

 
        

    
namespace shishicaiclient 
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    
        static byte[] buffer = new byte[1024*1024];

        string histroyopen = "";

       public class jsonclass
        {
            public  string expect { get; set; }
            public  string opencode { get; set; }
        }
     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //C1Window win = new C1Window();
            //LiweiTest test = new LiweiTest();
            //win.Content = test;
            //win.Show();

            //连接到指定服务器的指定端口
            PublicClass.socket.Connect("192.168.1.109", 4530);
            if (!PublicClass.socket.Connected)
            {
                MessageBox.Show("connect to the server");
            }
            else
            {
                MessageBox.Show("welcome");
                PublicClass.socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), PublicClass.socket);
            }


        }
        //接收消息
        public  void ReceiveMessage(IAsyncResult ar)
        {
    
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                //读取出来消息内容
                var message = Encoding.Unicode.GetString(buffer, 0, length);
                histroyopen = message;
                //提取服务端传输的json
                JToken jsonstr = JToken.Parse(histroyopen);
                JArray jsonstrs = JArray.Parse(jsonstr["data"].ToString());
                //将json按条提取
                for (int i = 0; i < jsonstrs.Count; i++)
                {
                    PublicClass.Code_json.Add(jsonstrs[i]);
                    string hisexpect = PublicClass.Code_json[i]["expect"].ToString();
                    string hisopencode = PublicClass.Code_json[i]["opencode"].ToString();
                    
                   
                }
                
           
                //显示消息
                MessageBox.Show(message);
               
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
      //发送用户注册信息
        private void sign_Click(object sender, RoutedEventArgs e)
        {
            Window1 neww = new Window1();
            neww.Show();
        }
        //发送用户登录信息
        private void login_Click(object sender, RoutedEventArgs e)
        {
            login login = new login();
            login.Show();
        }
        
        

       
    }
}
