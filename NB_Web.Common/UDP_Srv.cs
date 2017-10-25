using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;
using System.Threading;

using System.Xml;

//using MarshalByRefObject;
using System.Runtime.InteropServices;
using System.Timers;

using System.Net.NetworkInformation;

using NB_Web.Model;

namespace NB_Web.Common
{
    public class UDP_Srv
    {
        #region Const Parameter

        const string STR_ADD = "ADD";
        const string STR_DELETE = "DELETE";
        const string STR_EDIT = "EDIT";      

        const string sLogFile = "AG_Log.txt";
        const int ERR_XML_CREATE = -31;	//-31 GenerateXMLString failed
        const int INT_TIME_CHECKSKT = 10000;    //10秒
        const int MAX_TIMEOUT = 10;         //心跳超时，10秒
        const int HEAD_LEN = 8;     //add by zy 20140714 for head package with length
        #endregion

        public string PUB_IP = "";
        public int PUB_PORT = 0;
        public static IPEndPoint PUB_ENDPOINT = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 127);

        private static byte[] result = new byte[1024];
        private static int myProt = 8888;   //端口   
        static Socket UDP_Srv_Socket;
        /// <summary>   
        /// 回调句柄  
        /// </summary> 
        public delegate void UDP_MsgBoxHandler(string sCMD);
        public UDP_MsgBoxHandler udp_msgboxHandler;

        void UDP_ListenClientConnect()
        {
            //while (true)
            {
                try
                {
                    Thread t = new Thread(ReciveMsg);//开启接收消息线程
                    t.IsBackground = true;
                    t.Start(UDP_Srv_Socket);
                }
                catch (Exception e)
                { }
            }
        }

        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }

        Thread _tRecvMsg;
        public string UDP_Listen(string IP, int port)
        {
            string sRes = "";
            bool bPortUsing = PortInUse(1111);

            if (IP == "")
                IP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault<IPAddress>(a => a.AddressFamily.ToString().Equals("InterNetwork")).ToString();


            if (UDP_Srv_Socket == null)
            {
                try  {
                    UDP_Srv_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    IPEndPoint local_endpoint = new IPEndPoint(IPAddress.Parse(IP), port);
                    UDP_Srv_Socket.Bind(local_endpoint);//绑定端口号和IP

                    _tRecvMsg = new Thread(ReciveMsg);//开启接收消息线程
                    _tRecvMsg.IsBackground = true;

                    _tRecvMsg.Start(UDP_Srv_Socket);
                }           catch(Exception e1) {                    sRes = e1.ToString();                }
            }
            return sRes;
        }

        private volatile bool _RecvThread_shouldStop;
        public void ReciveMsg(object UDP_Srv_Socket)
        {
            Socket myUDPClientSocket = (Socket)UDP_Srv_Socket;
            while (!_RecvThread_shouldStop)
            {
                //if (RecvThread_Close) break;
                try
                {
                    EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
                    byte[] buffer = new byte[1024];
                    int length = myUDPClientSocket.ReceiveFrom(buffer, ref point);//接收数据报
                    
                    string sIP = point.ToString();  

                    PUB_ENDPOINT = (IPEndPoint)point;

                    UDP_Model.UDP_Remote_Port = PUB_ENDPOINT.Port;
                    UDP_Model.UDP_Remote_IP = PUB_ENDPOINT.Address.ToString();

                    string message = Encoding.UTF8.GetString(buffer, 0, length);
                    //udp_msgboxHandler("from "+sIP+"----" + message);
                    udp_msgboxHandler(message);
                }
                catch {

                }

            }
        }

        public void Send_Udp_Msg(string Buf, string IP,int port)
        {
            IPAddress ip = IPAddress.Parse(IP);
            IPEndPoint remotePoint = new IPEndPoint(ip, port);
            UdpClient client = new UdpClient();
            client.Send(Encoding.Default.GetBytes(Buf), Encoding.Default.GetByteCount(Buf), remotePoint);//将数据发送到远程端点 
            client.Close();//关闭连接
        }

        public string Send_Udp_Msg(string Buf)
        {
            string sRes = "";
            try
            {
                IPEndPoint remotePoint = PUB_ENDPOINT;

                Log.WriteLog("prepare to send msg to:"+PUB_ENDPOINT.ToString());

                //UDP_Srv_Socket.Send(Encoding.Default.GetBytes(Buf));
                UDP_Srv_Socket.SendTo(Encoding.Default.GetBytes(Buf),PUB_ENDPOINT);

                //UdpClient client = new UdpClient();
                //client.Send(Encoding.Default.GetBytes(Buf), Encoding.Default.GetByteCount(Buf), PUB_ENDPOINT);//将数据发送到远程端点 
                //client.Close();//关闭连接
            }catch(Exception e)
            {
                sRes = e.ToString();
            }

            return sRes;
        }

        public string UDP_Close()
        {
            string sRes = "";
            try
            {
                _RecvThread_shouldStop = true;
                if (_tRecvMsg != null)
                    _tRecvMsg.Join();   // 等那个线程退出

                if (UDP_Srv_Socket == null) { return ""; }

                /// <summary>
                /// Close the socket safely.
                /// </summary>
                /// <param name="socket">The socket.</param>              

                    try
                    {
                    UDP_Srv_Socket.Shutdown(SocketShutdown.Both);
                    }
                    catch
                    {
                    }

                    try
                    {
                    UDP_Srv_Socket.Close();
                    }
                    catch
                    {
                    }
                


               // UDP_Srv_Socket.Close();
                UDP_Srv_Socket.Dispose();
                UDP_Srv_Socket = null;
            }
            catch(Exception e) {
                sRes = e.ToString();
            }
            return sRes;


        }

    }
}