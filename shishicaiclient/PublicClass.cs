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
        public static string zhucejson;
        public static string loginjson;
        public static List<JToken> Code_json = new List<JToken>();
        public static string localIP = "?";

        }
        
}
