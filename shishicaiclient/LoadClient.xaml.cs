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
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace shishicaiclient
{
    /// <summary>
    /// LoadClient.xaml 的交互逻辑
    /// </summary>
    public partial class LoadClient : Window
    {
        public LoadClient()
        {
            InitializeComponent();
        }


        public struct config
        {
            public string ip;
        }


        private void client_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            client.Focus();
            //控制输入只能是数字
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
           

            client.MaxLength = 13; 
            
                if (client.Text.ToString().Length == 3 || client.Text.ToString().Length == 7 || client.Text.ToString().Length == 9)
                {
                    client.Text = client.Text + ".";
                    client.SelectionStart = client.Text.Length;
                }

                
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           
            StreamWriter writer = new StreamWriter("App.config");
            config ccc = new config();
            ccc.ip = client.Text;
            XmlSerializer serialier = new XmlSerializer(typeof(config));
            serialier.Serialize(writer, ccc);
            writer.Close();
            MainWindow app = new MainWindow();
            Application.Current.MainWindow = app;
            this.Close();
            app.Show();
            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string strPath = "";
            XmlDocument config = new XmlDocument();
            config.Load("App.config");

            foreach (XmlNode node in config.SelectNodes("/config"))
            {
                strPath = node.SelectSingleNode("ip").InnerText;
            }
            if (strPath != "")
            {
                MainWindow app = new MainWindow();
                Application.Current.MainWindow = app;
                this.Close();
                app.Show();
            }
            
        }

       
    }
}
