// Copyright Joy Developing.

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Lib.Net
{
    /// <summary>
    /// <c>UDP</c> listener.
    /// </summary>
    public class UDPListener
    {
        /// <summary>
        /// Port.
        /// </summary>
        private int Port = 0;

        /// <summary>
        /// End point.
        /// </summary>
        private IPEndPoint EndPoint = null;

        /// <summary>
        /// <c>UDP</c> client.
        /// </summary>
        private UdpClient UDPClient = null;

        /// <summary>
        /// Receive bytes handler.
        /// </summary>
        /// <param name="bytes">массив байтов</param>
        public delegate void OnReceiveBytesHandler(byte[] bytes);

        /// <summary>
        /// Receive bytes event.
        /// </summary>
        public event OnReceiveBytesHandler OnReceiveBytes = null;

        /// <summary>
        /// Receive string handler.
        /// </summary>
        /// <param name="str">строка</param>
        public delegate void OnReceiveStringHandler(string str);

        /// <summary>
        /// Receive string event.
        /// </summary>
        public event OnReceiveStringHandler OnReceiveString = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="port">port</param>
        public UDPListener(int port)
        {
            Port = port;
            EndPoint = new IPEndPoint(IPAddress.Any, Port);
            UDPClient = new UdpClient(EndPoint);
            BeginReceive();
        }

        /// <summary>
        /// Begin receive.
        /// </summary>
        private void BeginReceive()
        {
            UDPClient.BeginReceive(new AsyncCallback(ReceiveCallback), UDPClient);
        }

        /// <summary>
        /// Callback for receive.
        /// </summary>
        /// <param name="ar">asynchronous call result</param>
        void ReceiveCallback(IAsyncResult ar)
        {
            RaiseEvents(UDPClient.EndReceive(ar, ref EndPoint));
            BeginReceive();
        }

        /// <summary>
        /// Raise events.
        /// </summary>
        /// <param name="bytes">bytes array</param>
        private void RaiseEvents(byte[] bytes)
        {
            if (bytes == null)
            {
                return;
            }

            if (OnReceiveBytes != null)
            {
                OnReceiveBytes(bytes);
            }

            if (OnReceiveString != null)
            {
                OnReceiveString(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
            }
        }
    }
}
