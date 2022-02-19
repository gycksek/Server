// See https://aka.ms/new-console-template for more information
using ServerCore;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DummyClient
{
    class Packet
    {
        public ushort size;
        public ushort packetId;
    }

    class GameSession : Session
{
    public override void OnConnected(EndPoint endPoint)
    {
        Console.WriteLine($"OnConnected: {endPoint}");

            Packet packet = new Packet() { size = 4, packetId = 7 };

            for (int i = 0; i < 5; ++i)
            {
                //byte[] sendBuff = Encoding.UTF8.GetBytes($"Hello World!{i}");
                // Send(sendBuff);

                ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);;
                byte[] buffer = BitConverter.GetBytes(packet.size);
                byte[] buffer2 = BitConverter.GetBytes(packet.packetId);
                Array.Copy(buffer, 0, openSegment.Array, openSegment.Offset, buffer.Length);
                Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer.Length, buffer2.Length);
                ArraySegment<byte> sendBuff = SendBufferHelper.Close(packet.size);

                Send(sendBuff);
            }
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Console.WriteLine($"OnDisconnected: {endPoint}");
    }

    public override int OnRecv(ArraySegment<byte> buffer)
    {
        string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count); ;
        Console.WriteLine($"[From Server] : {recvData}");

        return buffer.Count;
    }

    public override void OnSend(int numOfBytes)
    {
        Console.WriteLine($"Trandsferred bytes: {numOfBytes}");
    }
}

    class program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            Connector connector = new Connector();
            connector.Connect(endPoint, () => { return new GameSession(); });

            while (true)
            {
               // Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    //socket.Connect(endPoint);
                    //if (socket.RemoteEndPoint != null)
                    {
                        //Console.WriteLine($"Connected to {socket.RemoteEndPoint.ToString()}");
                    }

                    //for (int i = 0; i < 5; ++i)
                    //{
                    //    byte[] sendBuff = Encoding.UTF8.GetBytes($"Hello World!{i}");
                    //    int sendBytes = socket.Send(sendBuff);
                    //}

                    //byte[] sendBuff = Encoding.UTF8.GetBytes("Hello World!");
                    //int sendBytes = socket.Send(sendBuff);

                    //byte[] recvBuff = new byte[1024];
                    //int recvBytes = socket.Receive(recvBuff);
                    //string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                    //Console.WriteLine($"[From Server] {recvData}");

                   // socket.Shutdown(SocketShutdown.Both);
                   // socket.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Thread.Sleep(1000);
            }
        }
    }
}