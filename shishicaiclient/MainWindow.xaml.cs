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
        string longmount;
        string humount;
        string hemount;
        string danmount;
        string shuangmount;
        string damount;
        string xiaomount;
        string expect;    //开奖期数
        string last;  //上一期开奖
        string next;

        JToken jsonstr;
        List<JToken> Codejson = new List<JToken>();


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
                Longhu longhu = MainWindow.FindChild<Longhu>(Application.Current.MainWindow, "longhu");//龙虎开奖历史
                if (longhu != null && (createtype == "longhu" || createtype == "all"))
                {
                    for (int i = 0; i < longhu.maincanvas.Children.Count; i++)
                    {
                        rectlab del = longhu.maincanvas.Children[i] as rectlab;
                        if (del != null)
                        {
                            longhu.maincanvas.Children.Remove(del);
                            i--;
                        }
                    }

                        for (int i = 0; i < PublicClass.Code_json.Count; i++)
                        {
                            string[] opencodes = PublicClass.Code_json[i]["opencode"].ToString().Split(',');
                            rectlab rect = new rectlab();
                            rect.Width = 30;
                            rect.Height = 30;
                            if (int.Parse(opencodes[1]) > int.Parse(opencodes[4]))//第二位大于第五位为龙
                            {
                                rect.create_rect(0, "龙");
                            }
                            else if (int.Parse(opencodes[1]) == int.Parse(opencodes[4]))//第二位等于第五位
                            {
                                rect.create_rect(2, "和");
                            }
                            else
                            {
                                rect.create_rect(1, "虎");
                            }
                            rect.Margin = new Thickness(i / 6 * 30, i % 6 * 30, 0, 0);//“龙”“虎”“和”显示位置与规律
                            longhu.maincanvas.Children.Add(rect);
                        }
                }

                Longhu daxiao = MainWindow.FindChild<Longhu>(Application.Current.MainWindow, "daxiao");//大小
                if (daxiao != null && (createtype == "daxiao" || createtype == "all"))//判断大小不能为空
                {
                    for (int i = 0; i < daxiao.maincanvas.Children.Count; i++)
                    {
                        rectlab del = daxiao.maincanvas.Children[i] as rectlab;
                        if (del != null)
                        {
                            daxiao.maincanvas.Children.Remove(del);
                            i--;
                        }
                    }
                    for (int i = 0; i < PublicClass.Code_json.Count; i++)//条件
                    {
                        string[] opencodes = PublicClass.Code_json[i]["opencode"].ToString().Split(',');
                        rectlab rect = new rectlab();//rect实例化与属性
                        rect.Width = 30;
                        rect.Height = 30;

                        int daxiaocount = int.Parse(opencodes[1]) + int.Parse(opencodes[2]) + int.Parse(opencodes[3]) + int.Parse(opencodes[4]);//大小判断（第二位加第三位加到第五位的和来判断大小）
                        if (daxiaocount > 17)//大于17为大
                        {
                            rect.create_rect(0, "大");
                        }
                        else
                        {
                            rect.create_rect(1, "小");
                        }
                        rect.Margin = new Thickness(i / 6 * 30, i % 6 * 30, 0, 0);//“大”“小”位置显示与位置
                        daxiao.maincanvas.Children.Add(rect);
                    }
                }

                Longhu danshuang = MainWindow.FindChild<Longhu>(Application.Current.MainWindow, "danshuang");//单双
                if (danshuang != null && (createtype == "danshuang" || createtype == "all"))
                {
                    for (int i = 0; i < danshuang.maincanvas.Children.Count; i++)
                    {
                        rectlab del = danshuang.maincanvas.Children[i] as rectlab;
                        if (del != null)
                        {
                            danshuang.maincanvas.Children.Remove(del);
                            i--;
                        }
                    }
                    for (int i = 0; i < PublicClass.Code_json.Count; i++)
                    {
                        string[] opencodes = PublicClass.Code_json[i]["opencode"].ToString().Split(',');
                        rectlab rect = new rectlab();
                        rect.Width = 30;
                        rect.Height = 30;

                        int daxiaocount = int.Parse(opencodes[1]) + int.Parse(opencodes[2]) + int.Parse(opencodes[3]) + int.Parse(opencodes[4]);//单双判断（奇偶）
                        if (daxiaocount %2==0)//单双判断依据：第二位加到底五位的和
                        {
                            rect.create_rect(1, "双");
                        }
                        else
                        {
                            rect.create_rect(0, "单");
                        }
                        rect.Margin = new Thickness(i / 6 * 30, i % 6 * 30, 0, 0);//单双位置
                        danshuang.maincanvas.Children.Add(rect);
                    }
                }

                Daxiaoxulie daxiaoxulie = MainWindow.FindChild<Daxiaoxulie>(Application.Current.MainWindow, "daxiaoxulie");//大小序列
                if (daxiaoxulie != null && (createtype == "daxiaoxulie" || createtype == "all"))
                {
                    for (int i = 0; i < daxiaoxulie.maincanvas.Children.Count; i++)
                    {
                        elllab del = daxiaoxulie.maincanvas.Children[i] as elllab;
                        if (del != null)
                        {
                            daxiaoxulie.maincanvas.Children.Remove(del);
                            i--;
                        }
                    }
                    Point next_d = new Point(0, 0);//next_d:X轴
                    Point next_s = new Point(0, 0);//next_s:Y轴
                    Point cur = new Point(0, 0);
                    int[,] fill_lianxu = new int[121, 7];
                    for (int i = 0; i < PublicClass.Code_json.Count; i++)
                    {
                        string opencode = PublicClass.Code_json[i]["opencode"].ToString();
                        string[] sopencode = opencode.Split(',');
                        int mod = int.Parse(sopencode[1]) + int.Parse(sopencode[2]) + int.Parse(sopencode[3]) + int.Parse(sopencode[4]);//大小序列判断
                        //mod = mod % 2;
                        if (mod > 17)//18.............
                        {
                            mod = 0;//大
                        }
                        else
                        {
                            mod = 1;//小
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

                    for (int i = 0; i < 120; i++)//单双序列
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
                    for (int i = 0; i < danshuangxulie.maincanvas.Children.Count; i++)
                    {
                        elllab del = danshuangxulie.maincanvas.Children[i] as elllab;
                        if (del != null)
                        {
                            danshuangxulie.maincanvas.Children.Remove(del);
                            i--;
                        }
                    }
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

                    for (int i = 0; i < 120; i++)//大小序列
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
            PublicClass.socket.Connect("192.168.1.103", 4530);
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


        private void show_righthistroy(string expect, string bettingtype, string amount, string systemtype, string income, string rate, string created_at, string type)
        {
             Dispatcher.Invoke(new Action(delegate
         {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Height = 25;
            Label rightexpect = new Label();
            rightexpect.Width = 100;
            rightexpect.Content = expect; //开奖期数
            rightexpect.Foreground = Brushes.Gray;
            stack.Children.Add(rightexpect);
            if (type == "longhu")
            {
                if (systemtype == "0")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "  龙";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }
                else if (systemtype == "1")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "  虎";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }
                else if(systemtype == "2")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "  和";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }
                else if (systemtype == "-1")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "开奖中";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }
                if (bettingtype == "0")
                {
                    Label righttouzhu = new Label();
                    righttouzhu.Width = 80;
                    righttouzhu.Content = "  龙"; //投注类型
                    stack.Children.Add(righttouzhu);
                }
                else if (bettingtype == "1")
                {
                    Label righttouzhu = new Label();
                    righttouzhu.Width = 80;
                    righttouzhu.Content = "  虎"; //投注类型
                    stack.Children.Add(righttouzhu);
                }
                else if (bettingtype == "2")
                {
                    Label righttouzgu = new Label();
                   righttouzgu.Width = 80;
                    righttouzgu.Content = "  和"; //投注类型
                    stack.Children.Add(righttouzgu);
                }
                
            }

            else if(type == "danshuang")
            {
                if (systemtype == "0")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "  单";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }
                else if(systemtype == "1")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "  双";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }
                 else if (systemtype == "-1")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "开奖中";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }

                if (bettingtype == "0")
                {
                    Label righttouzhu = new Label();
                    righttouzhu.Width = 80;
                    righttouzhu.Content = "  单"; //投注类型
                    stack.Children.Add(righttouzhu);
                }
                else
                {
                    Label righttouzhu = new Label();
                    righttouzhu.Width = 80;
                    righttouzhu.Content = "  双"; //投注类型
                    stack.Children.Add(righttouzhu);
                }

            }

            else if(type == "daxiao")
            {
                if (systemtype == "0")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "  大";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }
                else if(systemtype == "1")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "  小";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }
                else if (systemtype == "-1")
                {
                    Label rightopentype = new Label();
                    rightopentype.Width = 80;
                    rightopentype.Content = "开奖中";//系统开奖类型
                    stack.Children.Add(rightopentype);
                }

                if (bettingtype == "0")
                {
                    Label righttouzhu = new Label();
                    righttouzhu.Width = 80;
                    righttouzhu.Content = "  大"; //投注类型
                    stack.Children.Add(righttouzhu);
                }
                else
                {
                    Label righttouzhu = new Label();
                    righttouzhu.Width = 80;
                    righttouzhu.Content = "  小"; //投注类型
                    stack.Children.Add(righttouzhu);
                }
            }
            Label righttime = new Label();
            righttime.Width = 80;
            righttime.Content = created_at;//投注时间
            stack.Children.Add(righttime);
            Label rightamount = new Label();
            rightamount.Width = 80;
            rightamount.Content = amount; //投注金额
            stack.Children.Add(rightamount);
            Label rightrate = new Label();
            rightrate.Width = 80;
            rightrate.Content = rate;   //赔率
            stack.Children.Add(rightrate);
            Label rightincome = new Label();
            rightincome.Width = 80;
            rightincome.Content = income;  //输赢金额
            stack.Children.Add(rightincome );
            if (right_tabcontrol.SelectedIndex == 0)
            {
                right_today.Items.Add(stack); //显示在页面的left_opencode的listbox里
            }
            else if (right_tabcontrol.SelectedIndex == 1)
            {
                if (type == "longhu")
                {
                    right_longhu.Items.Add(stack );
                }
                else if (type == "danshuang")
                {
                    right_danshuang.Items.Add(stack);
                }
                else if (type == "daxiao")
                {
                    right_daxiao.Items.Add(stack);
                }
            }
         }));
            
            
            
        }

        //调用页面显示当日开奖号码及结果 opercode=10调用方法

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

             int sum = number1 + int.Parse(sinopen[2]) + int.Parse(sinopen[3]) + number4;
             if (sum % 2 == 0)

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
             else 
             {
                 LeftEll ellreturn = new LeftEll();
                 ellreturn.create_lab("大", 1);
                 ellreturn.Width = 24;
                 ellreturn.Height = 24;
                 stack.Children.Add(ellreturn);
             }
             if(left_tabcontrol.SelectedIndex==0 )
             {
                
            left_opencode.Items.Add(stack);  //显示在页面的left_opencode的listbox里
             }
             else if (left_tabcontrol.SelectedIndex == 1)
             {
                
                 yesopen_opencode.Items.Add(stack);
             }
             else if (left_tabcontrol.SelectedIndex == 2)
             {
                
                 toyesopen_opencode.Items.Add(stack);
             }
             else if (left_tabcontrol.SelectedIndex == 3)
             {
                 //yesopen_opencode.Items.Clear();
                 //toyesopen_opencode.Items.Clear();
                 //histroy_opencode.Items.Clear();
                 histroy_opencode.Items.Add(stack);
             }
              }));
        }
   




        //显示开奖上一期的结果，opercode = 1调用方法
        private void lastopencode()
        {
       
            // select * from pulic.json where expect = '212012' 
            //var lastexpect = from c in PublicClass.Code_json where c["expect"].ToString() == expect.ToString() select c;   //查询Code_json的最后一条数据

            //if (lastexpect.Count() > 0)
            //{
            //    last = lastexpect.First()["opencode"].ToString();

         
                StackPanel stack = new StackPanel(); //实例化
                stack.Orientation = Orientation.Horizontal;  //stackpanel横向调节
                Label lab = new Label();
                lab.Height = 38;
                lab.FontSize = 26;
                lab.Content = PublicClass.Code_json.Last()["expect"].ToString(); //显示期数号
                lab.Foreground = Brushes.Gray;  //  lab字体颜色
                stack.Children.Add(lab);  //把lab放在stack里
                string[] la = PublicClass.Code_json.Last()["opencode"].ToString().Split(',');
                for (int aa = 0; aa < la.Count(); aa++)
                {
                    Label number = new Label();
                    stack.Children.Add(number);
                    Kaijiangell bigell = new Kaijiangell();
                    string num = la[aa];
                    bigell.create_kaijiang(num, 0);
                    bigell.Width = 40;
                    bigell.Height = 40;
                    stack.Children.Add(bigell);
                }

                expectlast.Children.Add(stack);
            //}

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

                string[] messages = message.Split('+');
                //int mess_count = messages.Count();
                //if(mess_count>1)

                //{
                //    mess_count--;
                //}
                for (int a = 0; a < messages.Count() - 1; a++)
                {
                    histroyopen = messages[a];
                    jsonstr = JToken.Parse(histroyopen);
                    string oper = jsonstr["opercode"].ToString();

                    //服务端返回倒计时
                    if (oper == "1")
                    {
                                                Dispatcher.Invoke(new Action(delegate
                        {
                            timecount = jsonstr["countdown"].ToString();
                            expect = jsonstr["expect"].ToString();
                            countdown.Content = timecount;
                            nextexpect.Content = jsonstr["nextissuse"].ToString();
                            next = jsonstr["nextissuse"].ToString();
                            if (nextexpect.Content.ToString() != hidden_nextexpect.Text)
                            {

                                hidden_nextexpect.Text = nextexpect.Content.ToString();
                            }
                            //lastopencode();
                        }));

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


                   //服务端返回历史开奖记录
                    else if (oper == "10")   
                    {
                      
                        update_code_json();
                        create_analyze_chat();

                    }


                    else if (oper == "12") //服务端回应投注历史
                    {
                        string aaaaaaaa = message;
                        JArray jsonstrs = JArray.Parse(jsonstr["data"].ToString());
                        PublicClass.touzhu_json.Clear();
                        for (int i = 0; i < jsonstrs.Count; i++)
                        {
                            PublicClass.touzhu_json.Add(jsonstrs[i]);
                            show_righthistroy(jsonstrs[i]["expect"].ToString(), jsonstrs[i]["bettingtype"].ToString(), jsonstrs[i]["amount"].ToString(), jsonstrs[i]["systemtype"].ToString(), jsonstrs[i]["income"].ToString(), jsonstrs[i]["rate"].ToString(), jsonstrs[i]["created_at"].ToString(), jsonstrs[i]["type"].ToString());
                        
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
                                    PublicClass.userbase =  jsonstr["base"].ToString();
                                    PublicClass.longhufending=  jsonstr["longhucapping"].ToString();
                                    PublicClass.danshuangfending= jsonstr["danshuangcapping"].ToString();
                                    PublicClass.daxiaofending= jsonstr["daxiaocapping"].ToString();
                                    PublicClass.hefending = jsonstr["hecapping"].ToString();
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

                    else if(oper == "26")
                    {
                        if (jsonstr["status"].ToString() == "100")
                        {
                            MessageBox.Show("投注成功！");
                          
                                                    Dispatcher.Invoke(new Action(delegate         //线程加载
                        {
                            resultlong.Content = "0";
                            resulthu.Content = "0";
                            resulthe.Content = "0";
                            resultdan.Content = "0";
                            resultshuang.Content = "0";
                            resultda.Content = "0";
                            resultxiao.Content = "0";
                            if (longmount != null)
                            {
                                longhures.Content = " 龙 ： " + longmount + "      ";
                            }
                            if (humount != null)
                            {
                                longhures.Content = longhures.Content + " 虎 : " + humount + "      ";
                            }
                            if (hemount != null)
                            {
                                longhures.Content = longhures.Content + " 和 : " + hemount + "      ";
                            }
                            if (danmount != null)
                            {
                                danshaungres.Content = " 单 ： " + danmount + "      ";
                            }
                            if (shuangmount != null)
                            {
                                danshaungres.Content = danshaungres.Content + " 双 ： " + shuangmount + "      ";
                            }
                            if (damount != null)
                            {
                                daxiaores.Content = " 大 ： " + damount + "      ";
                            }
                            if (xiaomount != null)
                            {
                                daxiaores.Content = daxiaores.Content + " 小 ： " + xiaomount + "      ";
                            }
                        }));
                        }
                    }

                    else if (oper == "28")
                    {
                        Dispatcher.Invoke(new Action(delegate         //线程加载
                        {
                        expectlast.Children.Clear();
                      
                   
                        string expect1 = jsonstr["lastexpect"].ToString();
                        string opencode1 = jsonstr["lastopencode"].ToString();
                        show_leftopenjiang(opencode1,expect1);
                        StackPanel stack = new StackPanel(); //实例化
                        stack.Orientation = Orientation.Horizontal;  //stackpanel横向调节
                        Label lab = new Label();
                        lab.Height = 38;
                        lab.FontSize = 26;
                        lab.Content = expect1; //显示期数号
                        lab.Foreground = Brushes.Gray;  //  lab字体颜色
                        stack.Children.Add(lab);  //把lab放在stack里
                        string[] la = opencode1.Split(',');
                        for (int aa = 0; aa < la.Count(); aa++)
                        {
                            Label number = new Label();
                            stack.Children.Add(number);
                            Kaijiangell bigell = new Kaijiangell();
                            string num = la[aa];
                            bigell.create_kaijiang(num, 0);
                            bigell.Width = 40;
                            bigell.Height = 40;
                            stack.Children.Add(bigell);
                        }

                        expectlast.Children.Add(stack);
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
        //点击用户名显示用户信息
        private void user_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((user.Content).ToString() == PublicClass.username)
            {
                User usermessage = new User();
                C1Window um = new C1Window();
                um.Width = 450;
                um.Height = 290;
                um.ShowMaximizeButton = false;
                um.ShowMinimizeButton = false;
                um.IsResizable = false;
                um.Margin = new Thickness((SystemParameters.WorkArea.Width - um.Width) / 2, (SystemParameters.WorkArea.Height - um.Height) / 2, 0, 0);
                um.Content = usermessage;
                um.Show();
            }
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


        //滚动条效果：龙虎
        private void longhu_scroll_MouseEnter(object sender, MouseEventArgs e)//鼠标放上出现（MouseEnter）

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
                resultlong.Content = (Convert.ToDouble(resultlong.Content) + Convert.ToDouble(PublicClass.userbase)).ToString();
                cal_user_balance(-float.Parse(PublicClass.userbase)); 
                if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
                {
                    allfalse();//调用判断投注大小与余额关联：  private void allfalse
                }
                else
                {
                    alltrue();// 调用判断投注大小与余额关联：  private void alltrue()

                }
        }

        private void jianlong_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultlong.Content).ToString()) > 0)
            {
                resultlong.Content = (Convert.ToDouble(resultlong.Content) - Convert.ToDouble(PublicClass.userbase)).ToString();

                cal_user_balance(float.Parse(PublicClass.userbase));
                if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
                {
                    allfalse();//调用判断投注大小与余额关联：  private void allfalse()
                }
                else
                {


                    alltrue();// 调用判断投注大小与余额关联：  private void alltrue()
                }
                

            }
            else
            {
                resultlong.Content = "0";
               
            }
        }
        //投注虎
        private void jiahu_Click(object sender, RoutedEventArgs e)
        {
          
                resulthu.Content = (Convert.ToDouble(resulthu.Content) + Convert.ToDouble(PublicClass.userbase)).ToString();
                cal_user_balance(-float.Parse(PublicClass.userbase));
                if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
                {
                    allfalse();//调用判断投注大小与余额关联：  private void allfalse()



                }
                else
                {
                    alltrue();// 调用判断投注大小与余额关联：  private void alltrue()
                }
        }

        private void jianhu_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resulthu.Content).ToString()) > 0)
            {
                resulthu.Content = (Convert.ToDouble(resulthu.Content) - Convert.ToDouble(PublicClass.userbase)).ToString();

                cal_user_balance(float.Parse(PublicClass.userbase));


                if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
                {
                    allfalse();//调用判断投注大小与余额关联：  private void allfalse()
                }

                else
                {

                    alltrue();// 调用判断投注大小与余额关联：  private void alltrue() 
                }
            }
            else
            {
                resulthu.Content = "0";
            }
               
        }
        //投注和
        private void jiahe_Click(object sender, RoutedEventArgs e)
        {

            resulthe.Content = (Convert.ToDouble(resulthe.Content) + Convert.ToDouble(PublicClass.userbase)).ToString();
            cal_user_balance(-float.Parse(PublicClass.userbase));
            if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
            {
                allfalse();//调用判断投注大小与余额关联：  private void allfalse()



            }
            else
            {
                alltrue();// 调用判断投注大小与余额关联：  private void alltrue()
            }

        }

        private void jianhe_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resulthe.Content).ToString()) > 0)
            {
                resulthe.Content = (Convert.ToDouble(resulthe.Content) - Convert.ToDouble(PublicClass.userbase)).ToString();
                cal_user_balance(float.Parse(PublicClass.userbase));
                if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
                {
                    allfalse();//调用判断投注大小与余额关联：  private void allfalse()
                }
                else
                {

                    alltrue();// 调用判断投注大小与余额关联：  private void alltrue()
                }

            }
            else
            {
                resulthe.Content = "0";
            }
        }

        //投注单
        private void jiadan_Click(object sender, RoutedEventArgs e)
        {

            resultdan.Content = (Convert.ToDouble(resultdan.Content) + Convert.ToDouble(PublicClass.userbase)).ToString();
            cal_user_balance(-float.Parse(PublicClass.userbase));
            if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
            {
                allfalse();//调用判断投注大小与余额关联：  private void allfalse()
            }
            else
            {
                alltrue();// 调用判断投注大小与余额关联：  private void alltrue()
            }
        }

        private void jiandan_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultdan.Content).ToString()) > 0)
            {
                resultdan.Content = (Convert.ToDouble(resultdan.Content) - Convert.ToDouble(PublicClass.userbase)).ToString();
                cal_user_balance(float.Parse(PublicClass.userbase));

                if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
                {
                    allfalse();//调用判断投注大小与余额关联：  private void allfalse()
                }
                else
                {
                    alltrue();// 调用判断投注大小与余额关联：  private void alltrue()
                }
            }
            else
            {
                resultdan.Content = "0";
            }
            

        }

        //投注双
        private void jiashuang_Click(object sender, RoutedEventArgs e)
        {

            resultshuang.Content = (Convert.ToDouble(resultshuang.Content) + Convert.ToDouble(PublicClass.userbase)).ToString();
            cal_user_balance(-float.Parse(PublicClass.userbase));
            if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
            {
                allfalse();//调用判断投注大小与余额关联：  private void allfalse()

            }
            else
            {
                alltrue();// 调用判断投注大小与余额关联：  private void alltrue()
            }
        }

        private void jianshuang_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultshuang.Content).ToString()) > 0)
            {
                resultshuang.Content = (Convert.ToDouble(resultshuang.Content) - Convert.ToDouble(PublicClass.userbase)).ToString();
                cal_user_balance(float.Parse(PublicClass.userbase));


                if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
                {
                    allfalse();//调用判断投注大小与余额关联：  private void allfalse()


                }
                else
                {

                    alltrue();// 调用判断投注大小与余额关联：  private void alltrue()

                }
            }
            else
            {
                resultshuang.Content = "0";
            }
        }

        //投注大
        private void jiada_Click(object sender, RoutedEventArgs e)
        {
            resultda.Content = (Convert.ToDouble(resultda.Content) + Convert.ToDouble(PublicClass.userbase)).ToString();
            cal_user_balance(-float.Parse(PublicClass.userbase));
            if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
            {
                allfalse();//调用判断投注大小与余额关联：  private void allfalse()


            }
            else
            {
                alltrue();// 调用判断投注大小与余额关联：  private void alltrue()
            }
        }

        private void jianda_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultda.Content).ToString()) > 0)
            {
                resultda.Content = (Convert.ToDouble(resultda.Content) - Convert.ToDouble(PublicClass.userbase)).ToString();
                cal_user_balance(float.Parse(PublicClass.userbase));


                if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
                {
                    allfalse();//调用判断投注大小与余额关联：  private void allfalse()
                }

                else
                {
                    alltrue();// 调用判断投注大小与余额关联：  private void alltrue()

                }
            }
            else
            {
                resultda.Content = "0";
            }
        }


        //投注小
        private void jiaxiao_Click(object sender, RoutedEventArgs e)
        {
            resultxiao.Content = (Convert.ToDouble(resultxiao.Content) + Convert.ToDouble(PublicClass.userbase)).ToString();
            cal_user_balance(-float.Parse(PublicClass.userbase));
            if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
            {
                allfalse();//调用判断投注大小与余额关联：  private void allfalse()
               


            }
            else
            {
                alltrue();// 调用判断投注大小与余额关联：  private void alltrue()
            }
        }

        private void jianxiao_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((resultxiao.Content).ToString()) > 0)
            {
                resultxiao.Content = (Convert.ToDouble(resultxiao.Content) + Convert.ToDouble(PublicClass.userbase)).ToString();
                cal_user_balance(float.Parse(PublicClass.userbase));

                if (float.Parse(amount.Content.ToString().Substring(5)) < 100f)
                {
                    allfalse();//调用判断投注大小与余额关联：  private void allfalse()
                }
                else
                {
                    alltrue();// 调用判断投注大小与余额关联：  private void alltrue()

                }
            }
            else
            {
                resultxiao.Content = "0";
            }
        }

        //判断投注大小与余额关联
        private void alltrue()//（余额>=100)，“+”能点
        {
            jiaxiao.IsEnabled = true;

            jialong.IsEnabled = true;
            jiahu.IsEnabled = true;
            jiahe.IsEnabled = true;
            jiadan.IsEnabled = true;
            jiashuang.IsEnabled = true;
            jiada.IsEnabled = true;
        }


        //判断投注大小与余额关联
        private void allfalse()//(余额<100),“-”不能点
        {
            jiaxiao.IsEnabled = false;

            jialong.IsEnabled = false;
            jiahu.IsEnabled = false;
            jiahe.IsEnabled = false;
            jiadan.IsEnabled = false;
            jiashuang.IsEnabled = false;
            jiada.IsEnabled = false;
        }

        class touzhu_head
        {
            public string opercode;
            public string username;
            public string clientIP;
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
            string str2 = "0:01:59";
            if(DateTime.Parse(timecount).Minute > DateTime.Parse(str2).Minute)
            {
            touzhu_head touzhu = new touzhu_head();
            if (PublicClass.username != "/")
            {

                touzhu.opercode = "2";
                touzhu.username = PublicClass.username;
                touzhu.clientIP = PublicClass.localIP;

                if (resultlong.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "long";
                    data.amount = resultlong.Content.ToString();
                    longmount = resultlong.Content.ToString();
                    touzhu.data.Add(data);

                }
                if (resulthu.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "hu";
                    data.amount = resulthu.Content.ToString();
                    humount = resulthu.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resulthe.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "he";
                    data.amount = resulthe.Content.ToString();
                    hemount = resulthe.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resultdan.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "dan";
                    data.amount = resultdan.Content.ToString();
                    danmount = resultdan.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resultshuang.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "shuang";
                    data.amount = resultshuang.Content.ToString();
                    shuangmount = resultshuang.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resultda.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "da";
                    data.amount = resultda.Content.ToString();
                    damount = resultda.Content.ToString();
                    touzhu.data.Add(data);
                }
                if (resultxiao.Content.ToString() != "0")
                {
                    touzhu_data data = new touzhu_data();
                    data.type = "xiao";
                    data.amount = resultxiao.Content.ToString();
                    xiaomount = resultxiao.Content.ToString();
                    touzhu.data.Add(data);
                }

                JToken touzhuJSON = JsonConvert.SerializeObject(touzhu);
                PublicClass.zhucejson = touzhuJSON.ToString();
                var message = PublicClass.zhucejson;
                var outputBuffer = Encoding.Unicode.GetBytes(message);
                PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                sure.IsEnabled = false;
            }
            else
            {
                resultlong.Content = "0";
                resulthu.Content = "0";
                resulthe.Content = "0";
                resultdan.Content = "0";
                resultshuang.Content = "0";
                resultda.Content = "0";
                resultxiao.Content = "0";
                MessageBox.Show("请先登陆！");

            }
            }

        
            else
            {
                MessageBox.Show("投注时间已过，请等待下次投注");
            }

        }

        
        //查询历史投注消息
        private void selecthistroy_Click(object sender, RoutedEventArgs e)
        {
            if ((startime.SelectedDate).ToString() != "" && (endtime.SelectedDate).ToString() != "")
            {
              
                var o = new
                {
                    opercode = "11",
                    username = PublicClass.username,
                    begindate = startime.SelectedDate.Value.ToString("yyyy/MM/dd") ,
                    enddate = endtime.SelectedDate.Value.ToString("yyyy/MM/dd"),
                    clientIP = PublicClass.localIP
                }; 
                var json = JsonConvert.SerializeObject(o);
                var outputBuffer = Encoding.Unicode.GetBytes(json);
                PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                right_longhu.Items.Clear();
                right_danshuang.Items.Clear();
                right_daxiao.Items.Clear();
            }
            else
            {
                MessageBox.Show("请选择日期");
            }
        }


        //查询某天开奖情况
        private void opendate_Click(object sender, RoutedEventArgs e)
        {
            if ((selectopendate.SelectedDate).ToString() != "")
            {
                var o = new
                {
                    opercode = "9",
                    date = selectopendate.SelectedDate.Value.ToString("yyyy/MM/dd"),
                    clientIP = PublicClass.localIP
                };
                var json = JsonConvert.SerializeObject(o);
                var outputBuffer = Encoding.Unicode.GetBytes(json);
                PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                histroy_opencode.Items.Clear();
            }
            else
            {
                MessageBox.Show("请选择日期");
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
        //刷新纪录
        private void hidden_nextexpect_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            var o = new
            {
                opercode = "27",  //查询刚刚开奖的期数号吗
                clientIP = PublicClass.localIP
            };
            var json = JsonConvert.SerializeObject(o);
            var outputBuffer = Encoding.Unicode.GetBytes(json);
            PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);

            var o1 = new
            {
                opercode = "29",
                username = PublicClass.username,
                clientIP = PublicClass.localIP
            };
            var json1 = JsonConvert.SerializeObject(o1);
            var outputBuffer1 = Encoding.Unicode.GetBytes(json1);
            PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
           
           
            if (left_tabcontrol.SelectedIndex == 0)
            {
                create_lab("all");
            }
            
        }
        //接收开奖记录的json方法
        private void update_code_json()
        {
            JArray jsonstrs = JArray.Parse(jsonstr["data"].ToString());
            PublicClass.Code_json.Clear();
          
            for (int i = 0; i < jsonstrs.Count; i++)
            {
                PublicClass.Code_json.Add(jsonstrs[i]);
                
                show_leftopenjiang(PublicClass.Code_json[i]["opencode"].ToString(),PublicClass.Code_json[i]["expect"].ToString());
            }
            
            create_lab("all");
            
        }

        //开奖今天、昨天、前天显示的改变
        private void left_tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (left_tabcontrol.IsLoaded)
            {

                try
                {
                    
                    if (left_tabcontrol.SelectedIndex == 0)
                    {
                        var o = new
                        {
                            opercode = "22",  //今日
                            clientIP = PublicClass.localIP
                        };
                        var json = JsonConvert.SerializeObject(o);
                        var outputBuffer = Encoding.Unicode.GetBytes(json);
                        
                        left_opencode.Items.Clear();
                        PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                        //left_opencode.UpdateLayout();
                        //System.Windows.Forms.Application.DoEvents();
                        //create_analyze_chat();
                    }

                    else if (left_tabcontrol.SelectedIndex == 1)
                    {
                        var o = new
                        {
                            opercode = "23",  //昨天
                            clientIP = PublicClass.localIP
                        };
                        var json = JsonConvert.SerializeObject(o);
                        var outputBuffer = Encoding.Unicode.GetBytes(json);
                        PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                        yesopen_opencode.Items.Clear();
                    }

                    else if (left_tabcontrol.SelectedIndex == 2)
                    {
                        var o = new
                        {
                            opercode = "24",   //前天
                            clientIP = PublicClass.localIP
                        };
                        var json = JsonConvert.SerializeObject(o);
                        var outputBuffer = Encoding.Unicode.GetBytes(json);
                        PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                        toyesopen_opencode.Items.Clear();

                    }
                }
                catch { }
 
            }
            
        }


        //投注历史界面切换
        private void right_tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (right_tabcontrol.IsLoaded)
            {
                try
                {
                    if (right_tabcontrol.SelectedIndex == 0)
                    {
                        var o = new
                        {
                            opercode = "25",
                            username = PublicClass.username,
                            clientIP = PublicClass.localIP
                        };
                        var json = JsonConvert.SerializeObject(o);
                        var outputBuffer = Encoding.Unicode.GetBytes(json);
                        PublicClass.socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
                        right_today.Items.Clear();
                    }
                }
                catch { }
            }
        }




        //开奖结果分析进度条效果

        private void create_analyze_chat()
        {
            int dragon_element = 0;
            int hu_element = 0;
            int he_element = 0;

            int da_element = 0;
            int xiao_element = 0;

            int dan_element = 0;
            int shuang_element = 0;
            
            foreach (var myelement in PublicClass.Code_json)//判断名称
            {
                string[] opencodes = myelement["opencode"].ToString().Split(',');
                if (int.Parse(opencodes[0]) > int.Parse(opencodes[4]))//龙
                {
                    dragon_element++;
                }
                else if (int.Parse(opencodes[0]) < int.Parse(opencodes[4]))//虎
                {
                    hu_element++;
                }
                else
                {
                    he_element++;//和
                }

                int daxiao_sum = int.Parse(opencodes[0].ToString()) + int.Parse(opencodes[1].ToString()) + int.Parse(opencodes[2].ToString()) + int.Parse(opencodes[3].ToString()) + int.Parse(opencodes[4].ToString());
                if (daxiao_sum < 23)//和小于23为小
                {
                    xiao_element++;
                }
                else
                {
                    da_element++;
                }

                if (daxiao_sum % 2 == 0)//单双
                {
                    shuang_element++;
                }
                else
                {
                    dan_element++;
                }


            }
            //清空进度条重复
            Dispatcher.Invoke(new Action(delegate
                {


                    for (int i = 0; i < analyze_panel.Children.Count; i++)
                    {
                        Progress del = analyze_panel.Children[i] as Progress;
                        if (del != null)
                        {
                            analyze_panel.Children.Remove(del);
                            i--;
                        }
                    }


            //进度条分析结果显示
            Progress dragon = new Progress();
            dragon.Margin = new Thickness(0, 20, 0, 0);
            dragon.Width = 390;
            dragon.Height = 30;

            dragon.create_progress(0, dragon_element, PublicClass.Code_json.Count, "龙");
            analyze_panel.Children.Add(dragon);

            Progress hu = new Progress();
            hu.Width = 390;
            hu.Height = 30;
            hu.create_progress(1, hu_element, PublicClass.Code_json.Count, "虎");
            analyze_panel.Children.Add(hu);


            Progress he = new Progress();
            he.Width = 390;
            he.Height = 30;
            he.create_progress(2, he_element, PublicClass.Code_json.Count, "和");
            analyze_panel.Children.Add(he);


            Progress da = new Progress();
            da.Margin = new Thickness(0, 10, 0, 0);
            da.Width = 390;
            da.Height = 30;
            da.create_progress(0, da_element, PublicClass.Code_json.Count, "大");
            analyze_panel.Children.Add(da);

            Progress xiao = new Progress();
            xiao.Width = 390;
            xiao.Height = 30;
            xiao.create_progress(1, xiao_element, PublicClass.Code_json.Count, "小");
            analyze_panel.Children.Add(xiao);

            Progress dan = new Progress();
            dan.Margin = new Thickness(0, 10, 0, 0);
            dan.Width = 390;
            dan.Height = 30;
            dan.create_progress(0, dan_element, PublicClass.Code_json.Count, "单");
            analyze_panel.Children.Add(dan);

            Progress shuang = new Progress();
            shuang.Width = 390;
            shuang.Height = 30;
            shuang.create_progress(1, shuang_element, PublicClass.Code_json.Count, "双");
            analyze_panel.Children.Add(shuang);

                }));

        }


      

        private void cal_user_balance(float before_touzhu, float tijiao_touzhu)
        {

        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }


        private void left_opencode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void yesopen_opencode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void toyesopen_opencode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void cal_user_balance(float balance)
        {
          
            amount.Content = "账户余额：" + (float.Parse(amount.Content.ToString().Substring(5)) + balance);
           


            

        }

        private void histroy_opencode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void right_today_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void right_longhu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void right_danshuang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void right_daxiao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

       


    



      
    }
}
