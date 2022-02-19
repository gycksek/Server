// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    //class GameSession : Session
    //{
    //    public override void OnConnected(EndPoint endPoint)
    //    {
    //        Console.WriteLine($"OnConnected: {endPoint}");


    //        byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server");

    //        Send(sendBuff);

    //        Thread.Sleep(1000);
    //       Disconnect();

    //    }

    //    public override void OnDisconnected(EndPoint endPoint)
    //    {
    //        Console.WriteLine($"OnDisconnected: {endPoint}");
    //    }

    //    public override void OnRecv(ArraySegment<byte> buffer)
    //    {
    //        string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count); ;
    //        Console.WriteLine($"[From Client] : {recvData}");
    //    }

    //    public override void OnSend(int numOfBytes)
    //    {
    //        Console.WriteLine($"Trandsferred bytes: {numOfBytes}");
    //    }
    //}

    //class Program
    //{
    //    static Listener _listener = new Listener();

    //   // static void OnAcceptHandler(Socket clientSocket)
    //  //  {

    //       // try
    //       // {
    //            //GameSession session = new GameSession();
    //           // session.Start(clientSocket);
    //            //byte[] recvBuff = new byte[1024];
    //            //int recvBytes = clientSocket.Receive(recvBuff);
    //            //string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
    //            //Console.WriteLine($"[From Client] : {recvData}");

    //            //byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server");
    //            //clientSocket.Send(sendBuff);

    //            //clientSocket.Shutdown(SocketShutdown.Both);
    //            //clientSocket.Close();
    //            //session.Send(sendBuff);

    //            //Thread.Sleep(1000);
    //            //session.Disconnect();
    //    //    }
    //       // catch(Exception e)
    //     //   {
    //            //Console.WriteLine(e.ToString());
    //    //    }

    //    //}

    //    static void Main(string[] args)
    //    {
    //        string host = Dns.GetHostName();
    //        IPHostEntry ipHost = Dns.GetHostEntry(host);
    //        IPAddress ipAddr = ipHost.AddressList[0];
    //        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);


    //        //Socket listenSocket = new Socket(endPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);

    //        try
    //        {
    //            //listenSocket.Bind(endPoint);
    //            //listenSocket.Listen(10);
    //            // _listener.Init(endPoint,OnAcceptHandler);
    //            _listener.Init(endPoint,() =>{return new GameSession();});
    //            Console.WriteLine("Listening....");
    //            while (true)
    //            {
    //                //Console.WriteLine("Listening....");
    //                //Socket clientSocket = _listener.Accept();

    //                //byte[] recvBuff = new byte[1024];
    //                //int recvBytes = clientSocket.Receive(recvBuff);
    //                //string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
    //                //Console.WriteLine($"[From Client] : {recvData}");

    //                //byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server");
    //                //clientSocket.Send(sendBuff);

    //                //clientSocket.Shutdown(SocketShutdown.Both);
    //                //clientSocket.Close();
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e.ToString());
    //        }


    //    }
    //}
}
