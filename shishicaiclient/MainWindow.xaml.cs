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
using Newtonsoft.Json;






namespace shishicaiclient
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //查找控件的FindChild方法
        public static T FindChild<T>(DependencyObject parent, string childName)//查找控件
           where T : DependencyObject
        {
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // 如果子控件不是需查找的控件类型 
                T childType = child as T;
                if (childType == null)
                {
                    // 在下一级控件中递归查找 
                    foundChild = FindChild<T>(child, childName);
                    // 找到控件就可以中断递归操作  
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // 如果控件名称符合参数条件 
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // 查找到了控件 
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }


        static byte[] buffer = new byte[1024 * 1024];

        string histroyopen = "";

        //存储json
        public class jsonclass
        {
            public string expect { get; set; }
            public string opencode { get; set; }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            //获取内网IP
            IPAddress ipaddr = Dns.Resolve(Dns.GetHostName()).AddressList[0];
            PublicClass.localIP = ipaddr.ToString();
            longhu_stack.Width = chat_stack.ActualWidth / 3d - 2;
            daxiao_stack.Width = chat_stack.ActualWidth / 3d - 2;
            danshuang_stack.Width = chat_stack.ActualWidth / 3d - 2;
            Longhu longhu = new Longhu();
            longhu.Width = 600;
            longhu.Height = 200;
            longhu.create_line();
            longhu_scroll.Content = longhu;

            Daxiao daxiao = new Daxiao();
            daxiao.Width = 600;
            daxiao.Height = 200;
            daxiao.create_line();
            daxiao_scroll.Content = daxiao;

            Danshuang danshuang = new Danshuang();
            danshuang.Width = 600;
            danshuang.Height = 200;
            danshuang.create_line();
            danshuang_scroll.Content = danshuang;
           

            //连接到指定服务器的指定端口
            PublicClass.socket.Connect("192.168.1.112", 4530);
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

        //调用页面显示中奖号码及结果
        private void show_leftopenjiang(string opencode, string expect)
        {
            Dispatcher.Invoke(new Action(delegate
         {
             StackPanel stack = new StackPanel(); //实例化
             stack.Orientation = Orientation.Horizontal;  //stackpanel横向调节
             Label lab = new Label();
             lab.Content = expect; //显示期数号
             lab.Foreground = Brushes.Gray;  //  lab字体颜色
             stack.Children.Add(lab);  //把lab放在stack里
             string[] sinopen = opencode.Split(',');
             //开奖号码显示
             foreach (var sin in sinopen)
             {
                 LeftEll ell = new LeftEll();
                 ell.create_lab(sin, 0);
                 ell.Width = 24;
                 ell.Height = 24;
                 stack.Children.Add(ell);
             }

             //string [] kk = opencode.Split(',');
             int number1 = int.Parse(sinopen[1]); //第二个数字
             int number4 = int.Parse(sinopen[4]); //最后一个数字

             //开奖结果显示
             //判断龙虎和结果
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


             //判断大小
             int sum = number1 + int.Parse(sinopen[2]) + int.Parse(sinopen[3]) + number4;
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

        //接收服务器消息
        public void ReceiveMessage(IAsyncResult ar)
        {
            string oper = "";
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                //读取出来消息内容
                var message = Encoding.Unicode.GetString(buffer, 0, length);
                //MessageBox.Show(message);
                histroyopen = message;
                JToken jsonstr = JToken.Parse(histroyopen);

             
                //if(jsonstr["opercode"].ToString()=="10")   //操作数为10是历史记录
                oper = jsonstr["opercode"].ToString();

                if (oper == "16")
                {
                    if (jsonstr["status"].ToString() == "100")
                    {
                         
                         MessageBoxResult dr=  MessageBox.Show("登录成功","提示",MessageBoxButton.OK);
                         if (dr == MessageBoxResult.OK)

                         {
                            Dispatcher.Invoke(new Action(delegate
         {
                             user.Content = PublicClass.username ;
                             PublicClass.balance = jsonstr["amount"].ToString();
                            amount.Content= "账户余额：" + PublicClass.balance.ToString();
                             if (user.Visibility == Visibility.Hidden || amount.Visibility == Visibility.Hidden)
                             {
                                 user.Visibility = Visibility.Visible;
                                 amount.Visibility = Visibility.Visible;
                             }
                             login.Visibility = System.Windows.Visibility.Collapsed;
                             changepwd.Visibility = System.Windows.Visibility.Visible;
                             //关闭登录窗口
                             C1.WPF.C1Window close1 = MainWindow.FindChild<C1.WPF.C1Window>(
                                 Application.Current.MainWindow, "closelogin");
                             if (close1 != null)
                             {
                                 close1.Close();
                             }
                             
         }));
                         }
        
                    }
                    else if (jsonstr["status"].ToString() == "200")
                    {
                        MessageBox.Show("用户名或密码错误");
                    }
                    else
                    {
                        MessageBox.Show("用户禁用");
                    }
                }

                if (oper == "15")
                {
                    if (jsonstr["status"].ToString() == "100")
                    {
                        MessageBoxResult dr =  MessageBox.Show("注册成功","提示",MessageBoxButton.OK);
                        if (dr == MessageBoxResult.OK)
                        {
                             Dispatcher.Invoke(new Action(delegate         //线程加载
         {

                            //关闭注册窗口
                            C1.WPF.C1Window close2 = MainWindow.FindChild<C1.WPF.C1Window>(
                              Application.Current.MainWindow, "closesign");
                            if (close2 != null)
                            {
                                close2.Close();
                            }
         }));
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("用户名重名");
                    }
                }


                if (oper == "17")
                {
                    MessageBox.Show("用户下线");
                     Dispatcher.Invoke(new Action(delegate         //线程加载
         {
                    login.Visibility = System.Windows.Visibility.Visible;
                    user.Visibility = System.Windows.Visibility.Hidden;
                    amount.Visibility = System.Windows.Visibility.Hidden;
                    PublicClass.username = "";
                    PublicClass.balance = "";
                    if (changepwd.Visibility == Visibility.Visible)
                    {
                        changepwd.Visibility = Visibility.Hidden;
                    }
         }));
                }



                if (oper == "10")   //操作数为10是历史记录
                {
                    JArray jsonstrs = JArray.Parse(jsonstr["data"].ToString());

                    for (int i = 0; i < jsonstrs.Count; i++)
                    {
                        PublicClass.Code_json.Add(jsonstrs[i]);
                        string ccc = jsonstrs[i]["opencode"].ToString();
                        show_leftopenjiang(jsonstrs[i]["opencode"].ToString(), jsonstrs[i]["expect"].ToString());

                    }
                }




                if (oper == "8")
                {
                    if (jsonstr["status"].ToString() == "1")
                    {
                        MessageBox.Show("密码修改成功");
                        Dispatcher.Invoke(new Action(delegate         //线程加载
                        {

                            //关闭注册窗口
                            C1.WPF.C1Window close3 = MainWindow.FindChild<C1.WPF.C1Window>(
                              Application.Current.MainWindow, "change");
                            if (close3 != null)
                            {
                                close3.Close();
                            }
                        }));
                    }
                    else if (jsonstr["status"].ToString() == "0")
                    {
                        MessageBox.Show("旧密码输入错误");
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

        //发送消息
        private void sign_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            //用户注册窗口
            sub_sign login = new sub_sign();//用户注册窗口login
            C1Window win = new C1Window();//C1:win宽高
            win.Name = "closesign";
            win.Width = 300;
            win.Height = 300;
            win.ShowMaximizeButton = false;//最大化隐藏
            win.ShowMinimizeButton = false;//最小化
            win.IsResizable = false;
            win.Margin = new Thickness((SystemParameters.WorkArea.Width - win.Width) / 2, (SystemParameters.WorkArea.Height - win.Height) / 2, 0, 0);//用户注册窗口居中：（屏幕宽度-窗口宽度）除以2，高度一样的方法
            win.Content = login;//内容
            win.Show();
            
        }
        //登录
        private void login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            login sign = new login();
            C1Window lo = new C1Window();
            lo.Header = "用户登录";
            lo.Width = 450;
            lo.Height = 300;
            lo.ShowMaximizeButton = false;
            lo.ShowMinimizeButton = false;
            lo.IsResizable = false;
            lo.Margin = new Thickness((SystemParameters.WorkArea.Width - lo.Width) / 2, (SystemParameters.WorkArea.Height - lo.Height) / 2, 0, 0);
            lo.Content = sign;
            lo.Show();
            lo.Name = "closelogin";

        }
        //注销登录
        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string str = "";
            var o = new
            {
                opercode = "18",
                name = PublicClass.username,
                clientIP = PublicClass.localIP,
            };
            var json = JsonConvert.SerializeObject(o);
            str = json.ToString();
            PublicClass.loginjson = str;
            var message = PublicClass.loginjson;
            var outputBuffer = Encoding.Unicode.GetBytes(message);
            PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
        }

        private void changepwd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chang change = new chang();
            C1Window cg = new C1Window();
            cg.Width = 300;
            cg.Height = 300;
            cg.ShowMaximizeButton = false;
            cg.ShowMinimizeButton = false;
            cg.IsResizable = false;
            cg.Margin = new Thickness((SystemParameters.WorkArea.Width - cg.Width) / 2, (SystemParameters.WorkArea.Height - cg.Height) / 2, 0, 0);
            cg.Content = change;
            cg.Show();
            cg.Name = "change";
        }

        private void longhu_scroll_MouseEnter(object sender, MouseEventArgs e)
        {
            longhu_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void longhu_scroll_MouseLeave(object sender, MouseEventArgs e)
        {
            longhu_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        }



        private void daxiao_scroll_MouseEnter(object sender, MouseEventArgs e)
        {
            daxiao_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void daxiao_scroll_MouseLeave(object sender, MouseEventArgs e)
        {
            daxiao_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        }


        private void danshuang_scroll_MouseEnter(object sender, MouseEventArgs e)
        {
            danshuang_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void danshuang_scroll_MouseLeave(object sender, MouseEventArgs e)
        {
            danshuang_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        }





    }
}
