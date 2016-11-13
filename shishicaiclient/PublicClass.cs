using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace shishicaiclient
{
    class PublicClass
    {
        //创建一个Socket
       public static  Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static string zhucejson;  //注册信息json
        public static string loginjson;  //登录信息json
        public static List<JToken> Code_json = new List<JToken>();
        public static List<JToken> touzhu_json = new List<JToken>();
       
        public static string localIP = "?";    //内网IP
        public static string username = "/";    //登录成功的用户名
        public static string balance = "0";    //账户余额


        }
        
}
