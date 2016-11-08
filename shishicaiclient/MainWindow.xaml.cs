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

        string timecount;
        string expect;
        string last;

        //存储json
        public class jsonclass
        {
            public string expect { get; set; }
            public string opencode { get; set; }
        }

        private void create_lab( string createtype)//createtype longhu daxiao daxiaoxulie danshuang danshuangxulie
        {
            Dispatcher.Invoke(new Action(delegate
            {
                Longhu longhu = MainWindow.FindChild<Longhu>(Application.Current.MainWindow, "longhu");
                if (longhu != null && (createtype == "longhu" || createtype == "all"))
                {
                    for (int i = 0; i < PublicClass.Code_json.Count; i++)
                    {
                        string[] opencodes = PublicClass.Code_json[i]["opencode"].ToString().Split(',');
                        rectlab rect = new rectlab();
                        rect.Width = 30;
                        rect.Height = 30;
                        if (int.Parse(opencodes[1]) > int.Parse(opencodes[4]))
                        {
                            rect.create_rect(0, "龙");
                        }
                        else if (int.Parse(opencodes[1]) == int.Parse(opencodes[4]))
                        {
                            rect.create_rect(2, "和");
                        }
                        else 
                        {
                            rect.create_rect(1, "虎");
                        }
                        rect.Margin = new Thickness(i / 6 * 30, i % 6 * 30, 0, 0);
                        longhu.maincanvas.Children.Add(rect);
                    }
                }

                Longhu daxiao = MainWindow.FindChild<Longhu>(Application.Current.MainWindow, "daxiao");
                if (daxiao != null && (createtype == "daxiao" || createtype == "all"))
                {
                    for (int i = 0; i < PublicClass.Code_json.Count; i++)
                    {
                        string[] opencodes = PublicClass.Code_json[i]["opencode"].ToString().Split(',');
                        rectlab rect = new rectlab();
                        rect.Width = 30;
                        rect.Height = 30;

                        int daxiaocount = int.Parse(opencodes[1]) + int.Parse(opencodes[2]) + int.Parse(opencodes[3]) + int.Parse(opencodes[4]);
                        if (daxiaocount > 17)
                        {
                            rect.create_rect(0, "大");
                        }
                        else
                        {
                            rect.create_rect(1, "小");
                        }
                        rect.Margin = new Thickness(i / 6 * 30, i % 6 * 30, 0, 0);
                        daxiao.maincanvas.Children.Add(rect);
                    }
                }

                Longhu danshuang = MainWindow.FindChild<Longhu>(Application.Current.MainWindow, "danshuang");
                if (danshuang != null && (createtype == "danshuang" || createtype == "all"))
                {
                    for (int i = 0; i < PublicClass.Code_json.Count; i++)
                    {
                        string[] opencodes = PublicClass.Code_json[i]["opencode"].ToString().Split(',');
                        rectlab rect = new rectlab();
                        rect.Width = 30;
                        rect.Height = 30;

                        int daxiaocount = int.Parse(opencodes[1]) + int.Parse(opencodes[2]) + int.Parse(opencodes[3]) + int.Parse(opencodes[4]);
                        if (daxiaocount %2==0)
                        {
                            rect.create_rect(1, "双");
                        }
                        else
                        {
                            rect.create_rect(0, "单");
                        }
                        rect.Margin = new Thickness(i / 6 * 30, i % 6 * 30, 0, 0);
                        danshuang.maincanvas.Children.Add(rect);
                    }
                }

                Daxiaoxulie daxiaoxulie = MainWindow.FindChild<Daxiaoxulie>(Application.Current.MainWindow, "daxiaoxulie");
                if (daxiaoxulie != null && (createtype == "daxiaoxulie" || createtype == "all"))
                {
                    Point next_d = new Point(0, 0);
                    Point next_s = new Point(0, 0);
                    Point cur = new Point(0, 0);
                    int[,] fill_lianxu = new int[121, 7];
                    for (int i = 0; i < PublicClass.Code_json.Count; i++)
                    {
                        string opencode = PublicClass.Code_json[i]["opencode"].ToString();
                        string[] sopencode = opencode.Split(',');
                        int mod = int.Parse(sopencode[1]) + int.Parse(sopencode[2]) + int.Parse(sopencode[3]) + int.Parse(sopencode[4]);
                        //mod = mod % 2;
                        if (mod > 17)
                        {
                            mod = 0;
                        }
                        else
                        {
                            mod = 1;
                        }
                        if (i == 0)
                        {
                            if (mod == 0)
                            {
                                fill_lianxu[0, 0] = 20 + mod;
                                next_d = new Point(1, 0);
                                next_s = new Point(0, 1);
                            }
                            else
                            {
                                fill_lianxu[0, 0] = 10 + mod;
                                next_d = new Point(0, 1);
                                next_s = new Point(1, 0);
                            }
                        }
                        else if (mod == 0)
                        {
                            fill_lianxu[(int)next_s.X, (int)next_s.Y] = 20 + mod;
                            next_d = new Point(next_s.X + 1, 0);
                            if (fill_lianxu[(int)next_s.X, (int)next_s.Y + 1] == 0)
                            {
                                if (next_s.Y + 1 == 6 || fill_lianxu[(int)next_s.X, (int)next_s.Y + 1] == 1)
                                {
                                    next_s = new Point(next_s.X + 1, next_s.Y);
                                    for (int b = (int)next_d.X; b > 0; b--)
                                    {
                                        if (fill_lianxu[b, 0] == 0)
                                        {
                                            next_d = new Point(b, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    next_s = new Point(next_s.X, next_s.Y + 1);
                                }
                            }
                            else
                            {
                                next_s = new Point(next_s.X, next_s.Y + 1);
                            }
                        }
                        else
                        {
                            fill_lianxu[(int)next_d.X, (int)next_d.Y] = 10 + mod;
                            next_s = new Point(next_d.X + 1, 0);
                            if (fill_lianxu[(int)next_d.X, (int)next_d.Y + 1] == 0)
                            {
                                if (next_d.Y + 1 == 6 || fill_lianxu[(int)next_d.X, (int)next_d.Y + 1] == 2)
                                {
                                    next_d = new Point(next_d.X + 1, next_d.Y);
                                    for (int b = (int)next_s.X; b > 0; b--)
                                    {
                                        if (fill_lianxu[b, 0] == 0)
                                        {
                                            next_s = new Point(b, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    next_d = new Point(next_d.X, next_d.Y + 1);
                                }
                            }
                            else
                            {
                                next_d = new Point(next_d.X, next_d.Y + 1);
                            }
                        }
                    }

                    for (int i = 0; i < 120; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (fill_lianxu[i, j] !=0)
                            {
                                int type = 1;
                                if (fill_lianxu[i, j] == 20)
                                {
                                    type = 0;
                                }
                                elllab ell = new elllab();
                                ell.Width = 30;
                                ell.Height = 30;
                                ell.create_ell(type, "");
                                ell.Margin = new Thickness(i * 30, j * 30, 0, 0);
                                daxiaoxulie.maincanvas.Children.Add(ell);
                            }
                        }
                    }
                }



                Daxiaoxulie danshuangxulie = MainWindow.FindChild<Daxiaoxulie>(Application.Current.MainWindow, "danshuangxulie");
                if (danshuangxulie != null && (createtype == "danshuangxulie" || createtype == "all"))
                {
                    Point next_d = new Point(0, 0);
                    Point next_s = new Point(0, 0);
                    Point cur = new Point(0, 0);
                    int[,] fill_lianxu = new int[121, 7];
                    for (int i = 0; i < PublicClass.Code_json.Count; i++)
                    {
                        string opencode = PublicClass.Code_json[i]["opencode"].ToString();
                        string[] sopencode = opencode.Split(',');
                        int mod = int.Parse(sopencode[1]) + int.Parse(sopencode[2]) + int.Parse(sopencode[3]) + int.Parse(sopencode[4]);
                        mod = mod % 2;

                        if (i == 0)
                        {
                            if (mod == 0)
                            {
                                fill_lianxu[0, 0] = 20 + mod;
                                next_d = new Point(1, 0);
                                next_s = new Point(0, 1);
                            }
                            else
                            {
                                fill_lianxu[0, 0] = 10 + mod;
                                next_d = new Point(0, 1);
                                next_s = new Point(1, 0);
                            }
                        }
                        else if (mod == 0)
                        {
                            fill_lianxu[(int)next_s.X, (int)next_s.Y] = 20 + mod;
                            next_d = new Point(next_s.X + 1, 0);
                            if (fill_lianxu[(int)next_s.X, (int)next_s.Y + 1] == 0)
                            {
                                if (next_s.Y + 1 == 6 || fill_lianxu[(int)next_s.X, (int)next_s.Y + 1] == 1)
                                {
                                    next_s = new Point(next_s.X + 1, next_s.Y);
                                    for (int b = (int)next_d.X; b > 0; b--)
                                    {
                                        if (fill_lianxu[b, 0] == 0)
                                        {
                                            next_d = new Point(b, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    next_s = new Point(next_s.X, next_s.Y + 1);
                                }
                            }
                            else
                            {
                                next_s = new Point(next_s.X, next_s.Y + 1);
                            }
                        }
                        else
                        {
                            fill_lianxu[(int)next_d.X, (int)next_d.Y] = 10 + mod;
                            next_s = new Point(next_d.X + 1, 0);
                            if (fill_lianxu[(int)next_d.X, (int)next_d.Y + 1] == 0)
                            {
                                if (next_d.Y + 1 == 6 || fill_lianxu[(int)next_d.X, (int)next_d.Y + 1] == 2)
                                {
                                    next_d = new Point(next_d.X + 1, next_d.Y);
                                    for (int b = (int)next_s.X; b > 0; b--)
                                    {
                                        if (fill_lianxu[b, 0] == 0)
                                        {
                                            next_s = new Point(b, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    next_d = new Point(next_d.X, next_d.Y + 1);
                                }
                            }
                            else
                            {
                                next_d = new Point(next_d.X, next_d.Y + 1);
                            }
                        }
                    }

                    for (int i = 0; i < 120; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (fill_lianxu[i, j] != 0)
                            {
                                int type = 0;
                                if (fill_lianxu[i, j] == 20)
                                {
                                    type = 1;
                                }
                                elllab ell = new elllab();
                                ell.Width = 30;
                                ell.Height = 30;
                                ell.create_ell(type, "");
                                ell.Margin = new Thickness(i * 30, j * 30, 0, 0);
                                danshuangxulie.maincanvas.Children.Add(ell);
                            }
                        }
                    }
                }


            }));

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            //获取内网IP
            IPAddress ipaddr = Dns.Resolve(Dns.GetHostName()).AddressList[0];
            PublicClass.localIP = ipaddr.ToString();


            longhu_stack.Width = chat_stack.ActualWidth / 3d - 2;
            daxiao_stack.Width = chat_stack.ActualWidth / 3d - 2;

            danshuang_stack.Width = chat_stack.ActualWidth / 3d - 2;
            //龙虎划线
            Longhu longhu = new Longhu();//龙虎
            longhu.Name = "longhu";
            longhu.Width = 600;
            longhu.Height = 200;
            longhu.create_line();
            longhu_scroll.Content = longhu;

            Longhu daxiao = new Longhu();//大小
            daxiao.Name = "daxiao";
            daxiao.Width = 600;
            daxiao.Height = 200;
            daxiao.create_line();
            daxiao_scroll.Content = daxiao;

            //序列划线（大小序列）
            Daxiaoxulie daxiaoxulie = new Daxiaoxulie();
            daxiaoxulie.Name = "daxiaoxulie";
            daxiaoxulie.Width = 3600;
            daxiaoxulie.Height = 200;
            daxiaoxulie.create_line();
            daxiaoxulie_scroll.Content = daxiaoxulie;

            Longhu danshuang = new Longhu();//单双
            danshuang.Name = "danshuang";
            danshuang.Width = 600;
            danshuang.Height = 200;
            danshuang.create_line();
            danshuang_scroll.Content = danshuang;

            //序列划线（单双序列）
            Daxiaoxulie danshuangxulie = new Daxiaoxulie();
            danshuangxulie.Name = "danshuangxulie";
            danshuangxulie.Width = 3600;
            danshuangxulie.Height = 200;
            danshuangxulie.create_line();
            danshuangxulie_scroll.Content = danshuangxulie;





           

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

<<<<<<< HEAD


        //调用页面显示中奖号码及结果
=======
        //左边历史开奖记录显示
>>>>>>> origin/master
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
             string[] sinopen = opencode.Split(',');  //把字符串的各个字符分开
             //开奖号码显示
             foreach (var sin in sinopen)
             {
                 LeftEll ell = new LeftEll();
                 ell.create_lab(sin, 0);
                 ell.Width = 24;
                 ell.Height = 24;
                 stack.Children.Add(ell);
             }

            
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


             //判断单双结果
             int last = number4;
<<<<<<< HEAD
             if (last % 2 == 0)//奇偶
=======
             int sum = number1 + int.Parse(sinopen[2]) + int.Parse(sinopen[3]) + number4;
             if (sum % 2 == 0)
>>>>>>> origin/master
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("双", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
             else if (sum % 2 !=0)
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("单", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }


             //判断大小结果
             
              if (sum < 18)
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("小", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
             else if(sum >18)
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("大", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
             
             left_opencode.Items.Add(stack);  //显示在页面的left_opencode的listbox里
         }));
        }





        private void lastopencode()
        {
       
            // select * from pulic.json where expect = '212012' 
            var lastexpect = from c in PublicClass.Code_json where c["expect"].ToString() == expect select c;   //查询Code_json的最后一条数据
            if (lastexpect.Count() > 0)
            {
                last = lastexpect.First()["opencode"].ToString();
            }
           
            StackPanel stack = new StackPanel(); //实例化
            stack.Orientation = Orientation.Horizontal;  //stackpanel横向调节
            Label lab = new Label();
            lab.Content = expect; //显示期数号
            lab.Foreground = Brushes.Gray;  //  lab字体颜色
            stack.Children.Add(lab);  //把lab放在stack里
            string[] la = last.Split(',');
            for (int aa = 0; aa < la.Count(); aa++)
            {
                Label number = new Label();
                stack.Children.Add(number);
                elllab bigell = new elllab();
                string num = la[aa];
                bigell.create_ell(0, num);
                bigell.Width = 30;
                bigell.Height = 30;
                stack.Children.Add(bigell);
            } 
            
            expectlast.Children.Add(stack);

        }


        //接收服务器消息
        public void ReceiveMessage(IAsyncResult ar)
        {
            
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                //读取出来消息内容
                var message = Encoding.Unicode.GetString(buffer, 0, length);
<<<<<<< HEAD
                //MessageBox.Show(message);
                string[] messages = message.Split('+');
                histroyopen = messages[0];
                JToken jsonstr = JToken.Parse(histroyopen);

             
                //if(jsonstr["opercode"].ToString()=="10")   //操作数为10是历史记录
                oper = jsonstr["opercode"].ToString();

                if (oper == "16")
=======
                string[] messages = message.Split('+');
                int mess_count = messages.Count();
                if(mess_count>1)
>>>>>>> origin/master
                {
                    mess_count--;
                }
                for (int a = 0; a < mess_count; a++)
                {
                    histroyopen = messages[a];
                    JToken jsonstr = JToken.Parse(histroyopen);
                    string oper = jsonstr["opercode"].ToString();

                    //服务端返回密码修改信息
                    if (oper == "1")
                    {
                                    Dispatcher.Invoke(new Action(delegate
            {
                        timecount = jsonstr["countdown"].ToString();
                        expect = jsonstr["expect"].ToString();
                        countdown.Content = timecount;
                        nextexpect.Content = jsonstr["nextissuse"].ToString();
                        lastopencode();
            }));
                    }


                    if (oper == "4")
                    {


                    }
                    else if (oper == "8")
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


                   //服务端返回当天历史开奖记录
                    else if (oper == "10")   //操作数为10是历史记录
                    {
                        JArray jsonstrs = JArray.Parse(jsonstr["data"].ToString());

                        for (int i = 0; i < jsonstrs.Count; i++)
                        {
                            PublicClass.Code_json.Add(jsonstrs[i]);
                            string ccc = jsonstrs[i]["opencode"].ToString();
                            show_leftopenjiang(jsonstrs[i]["opencode"].ToString(), jsonstrs[i]["expect"].ToString());

                        }
                    }


                    //服务端返回注册信息
                    else if (oper == "15")
                    {
                        if (jsonstr["status"].ToString() == "100")
                        {
                            MessageBoxResult dr = MessageBox.Show("注册成功", "提示", MessageBoxButton.OK);
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
<<<<<<< HEAD
                    create_lab("all");
                }
=======
>>>>>>> origin/master

                    //服务端返回登录信息
                    else if (oper == "16")
                    {
                        if (jsonstr["status"].ToString() == "100")
                        {

                            MessageBoxResult dr = MessageBox.Show("登录成功", "提示", MessageBoxButton.OK);
                            if (dr == MessageBoxResult.OK)
                            {
                                Dispatcher.Invoke(new Action(delegate
                                {
                                    user.Content = PublicClass.username;
                                    PublicClass.balance = jsonstr["amount"].ToString();
                                    amount.Content = "账户余额：" + PublicClass.balance.ToString();
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



                    //服务端返回下线通知
                    else if (oper == "17")
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
                }
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）

                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        //用户注册窗口弹窗
        private void sign_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
           
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
        //登录窗口弹窗
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

        //密码修改窗口弹窗
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

        //注销登录向服务端发送申请数据
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

<<<<<<< HEAD
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
        //滚动条效果：龙虎
        private void longhu_scroll_MouseEnter(object sender, MouseEventArgs e)//鼠标放上出现（MouseEnter）
=======


        private void longhu_scroll_MouseEnter(object sender, MouseEventArgs e)
>>>>>>> origin/master
        {
            longhu_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void longhu_scroll_MouseLeave(object sender, MouseEventArgs e)
        {
            longhu_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;//鼠标移开消失（MouseLeave）
        }


        //滚动条：大小
        private void daxiao_scroll_MouseEnter(object sender, MouseEventArgs e)
        {
            daxiao_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void daxiao_scroll_MouseLeave(object sender, MouseEventArgs e)
        {
            daxiao_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }


        //滚动条：大小序列
        private void daxiaoxulie_scroll_MouseEnter(object sender, MouseEventArgs e)
        {
            daxiaoxulie_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void daxiaoxulie_scroll_MouseLeave(object sender, MouseEventArgs e)
        {
            daxiaoxulie_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

        }



        //滚动条：单双
        private void danshuang_scroll_MouseEnter(object sender, MouseEventArgs e)
        {
            danshuang_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void danshuang_scroll_MouseLeave(object sender, MouseEventArgs e)
        {
            danshuang_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        //投注龙
        private void jialong_Click(object sender, RoutedEventArgs e)
        {
          
                resultlong.Content = (Convert.ToDouble(resultlong.Content) + Convert.ToDouble(100)).ToString();
            
        }

        private void jianlong_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultlong.Content).ToString()) > 0)
            {
                resultlong.Content = (Convert.ToDouble(resultlong.Content) - Convert.ToDouble(100)).ToString();
            }
            else
            {
                resultlong.Content = "0";
            }
        }
        //投注虎
        private void jiahu_Click(object sender, RoutedEventArgs e)
        {
           
                resulthu.Content = (Convert.ToDouble(resulthu.Content) + Convert.ToDouble(100)).ToString();
           
        }

        private void jianhu_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resulthu.Content).ToString()) > 0)
            {
                resulthu.Content = (Convert.ToDouble(resulthu.Content) - Convert.ToDouble(100)).ToString();
            }
            else
            {
                resulthu.Content = "0";
            }
               
        }
        //投注和
        private void jiahe_Click(object sender, RoutedEventArgs e)
        {

            resulthe.Content = (Convert.ToDouble(resulthe.Content) + Convert.ToDouble(100)).ToString();

        }

        private void jianhe_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resulthe.Content).ToString()) > 0)
            {
                resulthe.Content = (Convert.ToDouble(resulthe.Content) - Convert.ToDouble(100)).ToString();
            }
            else
            {
                resulthe.Content = "0";
            }
        }

        //投注单
        private void jiadan_Click(object sender, RoutedEventArgs e)
        {

            resultdan.Content = (Convert.ToDouble(resultdan.Content) + Convert.ToDouble(100)).ToString();

        }

        private void jiandan_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultdan.Content).ToString()) > 0)
            {
                resultdan.Content = (Convert.ToDouble(resultdan.Content) - Convert.ToDouble(100)).ToString();
            }
            else
            {
                resultdan.Content = "0";
            }
            

        }

        //投注双
        private void jiashuang_Click(object sender, RoutedEventArgs e)
        {

            resultshuang.Content = (Convert.ToDouble(resultshuang.Content) + Convert.ToDouble(100)).ToString();

        }

        private void jianshuang_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultshuang.Content).ToString()) > 0)
            {
                resultshuang.Content = (Convert.ToDouble(resultshuang.Content) - Convert.ToDouble(100)).ToString();
            }
            else
            {
                resultshuang.Content = "0";
            }
        }

        //投注大
        private void jiada_Click(object sender, RoutedEventArgs e)
        {
            resultda.Content = (Convert.ToDouble(resultda.Content) + Convert.ToDouble(100)).ToString();
        }

        private void jianda_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultda.Content).ToString()) > 0)
            {
                resultda.Content = (Convert.ToDouble(resultda.Content) - Convert.ToDouble(100)).ToString();
            }
            else
            {
                resultda.Content = "0";
            }
        }


        //投注小
        private void jiaxiao_Click(object sender, RoutedEventArgs e)
        {
            resultxiao.Content = (Convert.ToDouble(resultxiao.Content) + Convert.ToDouble(100)).ToString();
        }

        private void jianxiao_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultxiao.Content).ToString()) > 0)
            {
                resultxiao.Content = (Convert.ToDouble(resultxiao.Content) + Convert.ToDouble(100)).ToString();
            }
            else
            {
                resultxiao.Content = "0";
            }
        }

        class touzhu_head
        {
            public string opercode;
            public string username;
            public List<touzhu_data> data = new List<touzhu_data>();
        }
        class touzhu_data
        {
            public string type;
            public string amount;
        }


        //提交投注结果
        private void sure_Click(object sender, RoutedEventArgs e)
        {
            touzhu_head touzhu = new touzhu_head();
            if (PublicClass.username != "/")
            {
                
                touzhu.opercode = "2";
                touzhu.username = PublicClass.username;
               
                if (resultlong.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "long";
                    data.amount = resultlong.Content.ToString();
                    touzhu.data.Add(data);
                    
                }
                if (resulthu.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "hu";
                    data.amount = resulthu.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resulthe.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "he";
                    data.amount = resulthe.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resultdan.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "dan";
                    data.amount = resultdan.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resultshuang.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "shuang";
                    data.amount = resultshuang.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resultda.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "da";
                    data.amount = resultda.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resultxiao.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "xiao";
                    data.amount = resultxiao.Content.ToString();
                    touzhu.data.Add(data);
                }

                   JToken touzhuJSON = JsonConvert.SerializeObject(touzhu);
                    PublicClass.zhucejson = touzhuJSON.ToString();
                    var message = PublicClass.zhucejson;
                    var outputBuffer = Encoding.Unicode.GetBytes(message);
                    PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
   
            }
        }
        //查询历史投注消息
        private void selecthistroylonghu_Click(object sender, RoutedEventArgs e)
        {
            if ((startimelonghu.SelectedDate).ToString() != "" && (stoptimelonghu.SelectedDate).ToString() != "")
            {
                var o = new
                {
                    opercode = "11",
                    username = PublicClass.username,
                    type = "longhu",
                    begindate = (startimelonghu.SelectedDate).ToString(),
                    enddate = (stoptimelonghu.SelectedDate).ToString()
                };
                var json = JsonConvert.SerializeObject(o);
            }

        }

       

      
      

       



        //滚动条：单双序列
        private void danshuangxulie_scroll_MouseEnter(object sender, MouseEventArgs e)
        {
            danshuangxulie_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void danshuangxulie_scroll_MouseLeave(object sender, MouseEventArgs e)
        {
            danshuangxulie_scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }







        private void daxiao_tab_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                create_lab("daxiaoxulie");
            }
            catch { };
        }

        private void danshuang_tab_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                create_lab("danshuangxulie");
            }
            catch { };
        }



    }
}
