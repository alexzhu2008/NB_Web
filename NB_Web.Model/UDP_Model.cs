using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace NB_Web.Model
{
    public class UDP_Model
    {

        //public static Socket 
        public static string txt_Record_Log_static { get; set; }
        public static string Udp_Listen_Res { get; set; }
        public static string Udp_Close_Res { get; set; }

        //public static IPEndPoint UDP_Remote_Point { get; set; }
        public static string UDP_Remote_IP { get; set; }
        public static int UDP_Remote_Port { get; set; }
    }
}
