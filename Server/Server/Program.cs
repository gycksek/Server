using System.Net;
using System.Net.Sockets;
using System.Text;
using ServerCore;

namespace Server
{
    //class Knight
    //{
    //    public int hp;
    //    public int attack;
    //}
    //class Packet
    //{
    //    public ushort size;
    //    public ushort packetId;
    //}

    //class GameSession : /*Session*/PacketSession
    //{
    //    public override void OnConnected(EndPoint endPoint)
    //    {
    //        Console.WriteLine($"OnConnected: {endPoint}");

    //       // Knight knight = new Knight() { attack = 10, hp = 100 };
    //       // Packet packet = new Packet() { size = 100, packetId = 10 };

    //        //byte[] sendBuff = new byte[1024];


    //        //byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server");

    //      //  ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
    //        //byte[] buffer = BitConverter.GetBytes(knight.hp);
    //        //byte[] buffer2 = BitConverter.GetBytes(knight.attack);
    //      //   byte[] buffer = BitConverter.GetBytes(packet.size);
    //      //  byte[] buffer2 = BitConverter.GetBytes(packet.packetId);
    //        // Array.Copy(buffer, 0, sendBuff, 0, buffer.Length);
    //        //Array.Copy(buffer2, 0, sendBuff, buffer.Length, buffer2.Length);
    //      //  Array.Copy(buffer, 0, openSegment.Array, openSegment.Offset, buffer.Length);
    //      //  Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset+buffer.Length, buffer2.Length);
    //      //  ArraySegment<byte> sendBuff=SendBufferHelper.Close(buffer.Length + buffer2.Length);


    //       // Send(sendBuff);

    //        Thread.Sleep(5000);
    //        Disconnect();

    //    }

    //    public override void OnDisconnected(EndPoint endPoint)
    //    {
    //        Console.WriteLine($"OnDisconnected: {endPoint}");
    //    }

    //    public override void OnRecvPacket(ArraySegment<byte> buffer)
    //    {
    //        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
    //        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset+2);
    //        Console.WriteLine($"RecvPacketId: {id},Size : {size}");
    //    }

    //    //public override int OnRecv(ArraySegment<byte> buffer)
    //    //{
    //    //    string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count); ;
    //    //    Console.WriteLine($"[From Client] : {recvData}");

    //    //    return buffer.Count;
    //    //}

    //    public override void OnSend(int numOfBytes)
    //    {
    //        Console.WriteLine($"Trandsferred bytes: {numOfBytes}");
    //    }
    //}

    class Program
    {
        static Listener _listener = new Listener();
        public static GameRoom Room = new GameRoom();

        static void FlushRoom()
        {
            Room.Push(() => Room.Flush());
            JobTimer.Instance.Push(FlushRoom, 250);
        }


        static void Main(string[] args)
        {
           // PacketManager.Instance.Register();

            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            try
            {

               // _listener.Init(endPoint, () => { return new ClientSession(); });
                _listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
                Console.WriteLine("Listening....");


                // FlushRoom();

                JobTimer.Instance.Push(FlushRoom);

                while (true)
                {
                    JobTimer.Instance.Flush();

                    //Room.Push(()=>Room.Flush());
                    //Thread.Sleep(250);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }
    }
}