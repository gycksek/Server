using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
    //public abstract class Packet
    //{
    //    public ushort size;
    //    public ushort packetId;

    //    public abstract ArraySegment<byte> Write();
    //    public abstract void  Read(ArraySegment<byte> s);

    //}


    //class PlayerInfoOk : Packet
    //{
    //    public int hp;
    //    public int attack;
    //}
    //public enum PacketID
    //{ 
    //    PlayerInfoReq=1,
    //    PlayerInfoOk=2,
    //}

    class ServerSession : Session
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected: {endPoint}");

            // Packet packet = new Packet() { size = 4, packetId = 7 };
            C2S_PlayerInfoReq packet = new C2S_PlayerInfoReq() { /*size = 4,*/ /*packetId = (ushort)PacketID.PlayerInfoReq,*/playerId=1001 ,name ="ABCD"};
            var skill = new C2S_PlayerInfoReq.Skill() { id = 101, level = 1, duration = 3.0f };
            skill.attributes.Add(new C2S_PlayerInfoReq.Skill.Attribute() { att = 77 });
            packet.skills.Add(skill);
            packet.skills.Add(new C2S_PlayerInfoReq.Skill() { id = 201, level = 2, duration = 4.0f });
            packet.skills.Add(new C2S_PlayerInfoReq.Skill() { id = 301, level = 3, duration = 5.0f });
            packet.skills.Add(new C2S_PlayerInfoReq.Skill() { id = 401, level = 4, duration = 6.0f });

            // for (int i = 0; i < 5; ++i)
            {
                //byte[] sendBuff = Encoding.UTF8.GetBytes($"Hello World!{i}");
                // Send(sendBuff);

                //ArraySegment<byte> openSegment = SendBufferHelper.Open(4096); ;
                //byte[] buffer = BitConverter.GetBytes(packet.size);
                //byte[] buffer2 = BitConverter.GetBytes(packet.packetId);
                //Array.Copy(buffer, 0, openSegment.Array, openSegment.Offset, buffer.Length);
                //Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer.Length, buffer2.Length);
                //ArraySegment<byte> sendBuff = SendBufferHelper.Close(packet.size);

                // ArraySegment<byte> /*openSegment*/s = SendBufferHelper.Open(4096);
                // ushort count = 0;
                // bool success = true;

                //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
                //  count += 2;
                //  success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset+count, s.Count-count), packet.packetId);
                //  count += 2;
                //  success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset+count, s.Count-count), packet.playerId);
                //  count += 8;
                //  success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

                //byte[] size = BitConverter.GetBytes(packet.size);//2
                //byte[] packetId = BitConverter.GetBytes(packet.packetId);//2
                //byte[] playerId = BitConverter.GetBytes(packet.playerId);//8

                //Array.Copy(size, 0, openSegment.Array, openSegment.Offset, size.Length);
                //Array.Copy(packetId, 0, openSegment.Array, openSegment.Offset + packetId.Length, packetId.Length);
                //Array.Copy(playerId, 0, openSegment.Array, openSegment.Offset + packetId.Length, packetId.Length);
                // ArraySegment<byte> sendBuff = SendBufferHelper.Close(packet.size);

                //ushort count = 0;
                //Array.Copy(size, 0, s.Array, s.Offset+count, 2);
                //count += 2;
                //Array.Copy(packetId, 0, s.Array, s.Offset + count, 2);
                //count += 2;
                //Array.Copy(playerId, 0, s.Array, s.Offset + count, 8);
                //count += 8;

                // ArraySegment<byte> sendBuff = SendBufferHelper.Close(count);

                ArraySegment<byte>  s =packet.Write();

                //if(success)
                // Send(sendBuff);

                if(s!=null)
                 Send(s);
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
}
