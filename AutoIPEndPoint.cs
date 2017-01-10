using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Synless.Model
{
    class AutoIPEndPoint
    {
        protected static Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        protected static IPEndPoint[] endPoints;
        protected string debug = "-";

        private static IPEndPoint endPoint;
        private static UdpClient Client;
        private static string answer;
        private static string received;
        private static int UDP_Port;
        private bool once = true;
        private int shift = 0;

        private static bool connected = false;
       
        public AutoSocket(int _port = 8787, string querry = "ping", string _answer = "pong", int maxNumberOfDevices = 50)
        {

            answer = _answer;
            endPoints = new IPEndPoint[maxNumberOfDevices];

            byte[] ping = Encoding.ASCII.GetBytes(querry);
            //LISTENNING TO ALL INCOMING DATA ON PORT UDP 8787
            Client = new UdpClient(UDP_Port);
            Client.BeginReceive(new AsyncCallback(recv), null);
            try
            {
                for (byte n = 0; n < 255; n++)
                {
                    //SCANNING 192.168.0.X
                    endPoint = new IPEndPoint(new IPAddress(new byte[4] { 192, 168, 0, n }), UDP_Port);
                    //SENDING THE QUERRY
                    try { sock.SendTo(ping, endPoint); }
                    catch { }
                    //WAITING FOR THE ANSWER
                    System.Threading.Thread.Sleep(5);
                    if (connected)
                    {
                        //PARSE THE END OF THE DATA, THAT WILL BE ITS INDEX IN THE ENDPOINTS ARRAY
                        // ex : answer="pong15" -> endPoints[0]= ...
                        // ex : answer="pong18" -> endPoints[3]= ...
                        //SO THIS IS SORTED BY WHAT IS PROGRAMMED TO ANSWER WITHING THE µC
                        //THE BEST IS TO SEND BACK PONG10 THEN PONG11 THEN PONG12 TO HAVE THE BOARDS WELL SORTED
                        if (once)
                        {
                            once = false;
                            shift = n;
                        }
                        int index = int.Parse(received.Remove(0, answer.Length));
                        endPoints[index - n] = endPoint;
                        break;
                    }

                    //SAME 192.168.1.X
                    endPoint = new IPEndPoint(new IPAddress(new byte[4] { 192, 168, 1, n }), UDP_Port);
                    try { sock.SendTo(ping, endPoint); }
                    catch { }
                    System.Threading.Thread.Sleep(5);
                    if (connected)
                    {
                        if (once)
                        {
                            once = false;
                            shift = n;
                        }
                        int index = int.Parse(received.Remove(0, answer.Length));
                        endPoints[index - n] = endPoint;
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void recv(IAsyncResult res)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, UDP_Port);
            received = Encoding.UTF8.GetString(Client.EndReceive(res, ref endPoint));
            Client.BeginReceive(new AsyncCallback(recv), null);
            if (received.Contains(answer))
            {
                connected = true;
            }
        }
    }
}
