// Author: Alexey Rybakov

using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Lib.Net
{
    /// <summary>
    /// <c>UDP</c> functionality.
    /// </summary>
    public static class UDP
    {
        /// <summary>
        /// Create end point.
        /// </summary> 
        /// <param name="addr">address</param>
        /// <param name="port">port</param>
        /// <returns>end point</returns>
        public static IPEndPoint EndPoint(string addr, int port)
        {
            IPAddress ip = IPAddress.Parse(addr);

            return new IPEndPoint(ip, port);
        }

        /// <summary>
        /// Send bytes array with <c>UDP</c>.
        /// </summary>
        /// <param name="bytes">bytes array</param>
        /// <param name="end_point">end point</param>
        public static void SendTo(byte[] bytes, IPEndPoint end_point)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            s.SendTo(bytes, end_point);
            s.Close();
        }

        /// <summary>
        /// Send bytes array with <c>UDP</c>.
        /// </summary>
        /// <param name="bytes">bytes array</param>
        /// <param name="addr">address</param>
        /// <param name="port">port</param>
        public static void SendTo(byte[] bytes, string addr, int port)
        {
            SendTo(bytes, EndPoint(addr, port));
        }

        /// <summary>
        /// Send string with <c>UDP</c>.
        /// </summary>
        /// <param name="str">string</param>
        /// <param name="end_point">end point</param>
        public static void SendTo(string str, IPEndPoint end_point)
        {
            SendTo(Encoding.ASCII.GetBytes(str), end_point);
        }

        /// <summary>
        /// Send string with <c>UDP</c>.
        /// </summary>
        /// <param name="str">string</param>
        /// <param name="addr">address</param>
        /// <param name="port">port</param>
        public static void SendTo(string str, string addr, int port)
        {
            SendTo(Encoding.ASCII.GetBytes(str), EndPoint(addr, port));
        }
    }
}
