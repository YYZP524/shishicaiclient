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
using System.Net;

 
        

    
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
            IPAddress ipAddr = Dns.Resolve(Dns.GetHostName()).AddressList[0];//获得当前IP地址
           PublicClass.localIP = ipAddr.ToString();
            //C1Window win = new C1Window();
            //LiweiTest test = new LiweiTest();
            //win.Content = test;
            //win.Show();

            //连接到指定服务器的指定端口
            PublicClass.socket.Connect("192.168.1.106", 4530);
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

        //用页面
        private void show_leftopenjiang(string opencode, string expect)
        {
               Dispatcher.Invoke(new Action(delegate
            {
            StackPanel stack = new StackPanel(); //实例化
            stack.Orientation = Orientation.Horizontal;  //stackpanel横向调节
            Label lab = new Label();
            lab.Content = expect;
            lab.Foreground = Brushes.Gray;  //  lab字体颜色
            stack.Children.Add(lab);  //把lab放在stack里
            string[] sinopen = opencode.Split(',');   
            foreach (var sin in sinopen)
            {
                LeftEll ell = new LeftEll();
                ell.create_lab(sin, 0);
                ell.Width = 24;
                ell.Height = 24;
                stack.Children.Add(ell);
            }

            string [] kk = opencode.Split(',');
             int number1 = int.Parse(kk[1]); //第二个数字
             int number4 = int.Parse(kk[4]); //最后一个数字
          
            //判断龙虎和
             if (number1 > number4)
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("龙", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
             else if (number1 < number4)
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("虎", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
             else
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("和", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
           

                //判断单双
             int last = number4;
             if (last % 2 == 0)
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("双", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
             else
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("单", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }


             int sum = number1 + int.Parse(kk[2]) + int.Parse(kk[3]) + number4;
             if (sum < 18)
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("小", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
             else
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("大", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
             
            
   
            left_opencode.Items.Add(stack);
            }));
        }

        //接收初始历史记录消息
        public  void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                //读取出来消息内容
                var message = Encoding.Unicode.GetString(buffer, 0, length);
                MessageBox.Show(message);
                histroyopen = message;
                JToken jsonstr = JToken.Parse(histroyopen);
             
                if(jsonstr["opercode"].ToString()=="10")   //操作数为10是历史记录
                {
                JArray jsonstrs = JArray.Parse(jsonstr["data"].ToString());
              
                for (int i = 0; i < jsonstrs.Count; i++)
                {
                    PublicClass.Code_json.Add(jsonstrs[i]);
                   string ccc = jsonstrs[i]["opencode"].ToString();
                    show_leftopenjiang(jsonstrs[i]["opencode"].ToString(), jsonstrs[i]["expect"].ToString());
                   
                }
                }

               
              
                    

                
           
                //显示消息
                //MessageBox.Show(message);

               
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
            //Window1 neww = new Window1();
            //neww.Show();
            //用户注册窗口
            sub_sign login = new sub_sign();//用户注册窗口login
            C1Window win = new C1Window();//C1:win宽高
            win.Width = 300;
            win.Height = 300;
            win.ShowMaximizeButton = false;//最大化隐藏
            win.ShowMinimizeButton = false;//最小化
            win.IsResizable = false;
            win.Margin = new Thickness((SystemParameters.WorkArea.Width - win.Width) / 2, (SystemParameters.WorkArea.Height - win.Height) / 2, 0, 0);//用户注册窗口居中：（屏幕宽度-窗口宽度）除以2，高度一样的方法

            win.Content = login;//内容
            win.Show();
            //SystemParameters.PrimaryScreenWidth;//屏幕宽度
            //SystemParameters.PrimaryScreenHeight;//屏幕高度
        }
        //发送用户登录信息
        private void login_Click(object sender, RoutedEventArgs e)
        {




            login login = new login();

         
            login.Show();
        }
        
        

       
    }
}
