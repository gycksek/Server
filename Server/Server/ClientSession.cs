using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public abstract class Packet
    {
        public ushort size;
        public ushort packetId;

        public abstract ArraySegment<byte> Write();
        public abstract void Read(ArraySegment<byte> s);

    }

    public enum PacketID
    {

        PlayerInfoReq = 1,

        Test = 2,

        //PlayerInfoReq=1,
        //PlayerInfoOk=2,
    }


    //class PlayerInfoReq//PlayerInfoReq//:Packet
    //{

    //    public byte testByte;


    //    public long playerId;


    //    public string name;


    //    public class Skill//SkillInfo
    //    {
    //        //public int id;
    //        //public short level;
    //        //public float duration;

    //        public int id;


    //        public short level;


    //        public float duration;


    //        public class Attribute//SkillInfo
    //        {
    //            //public int id;
    //            //public short level;
    //            //public float duration;

    //            public int att;


    //            public bool Write(Span<byte> s, ref ushort count)
    //            {
    //                bool success = true;
    //                //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
    //                //count += sizeof(int);
    //                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
    //                //count += sizeof(short);
    //                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
    //                //count += sizeof(float);

    //                success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.att);
    //                count += sizeof(int);

    //                return success;
    //            }
    //            public void Read(ReadOnlySpan<byte> s, ref ushort count)
    //            {

    //                /*id*/
    //                this.att = BitConverter./*ToInt32*/ToInt32(s.Slice(count, s.Length - count));
    //                count += sizeof(int/*int*/);

    //                //id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
    //                //count += sizeof(int);
    //                //level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
    //                //count += sizeof(short);
    //                //duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
    //                //count += sizeof(float);

    //            }

    //        }
    //        public List<Attribute/*SkillInfo*/> attributes/*skills */= new List<Attribute/*SkillInfo*/>();


    //        public bool Write(Span<byte> s, ref ushort count)
    //        {
    //            bool success = true;
    //            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
    //            //count += sizeof(int);
    //            //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
    //            //count += sizeof(short);
    //            //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
    //            //count += sizeof(float);

    //            success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.id);
    //            count += sizeof(int);


    //            success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.level);
    //            count += sizeof(short);


    //            success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.duration);
    //            count += sizeof(float);


    //            success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)this.attributes.Count);
    //            count += sizeof(ushort);
    //            foreach (Attribute attribute in attributes)
    //            {
    //                success &= attribute.Write(s, ref count);
    //            }

    //            return success;
    //        }
    //        public void Read(ReadOnlySpan<byte> s, ref ushort count)
    //        {

    //            /*id*/
    //            this.id = BitConverter./*ToInt32*/ToInt32(s.Slice(count, s.Length - count));
    //            count += sizeof(int/*int*/);


    //            /*id*/
    //            this.level = BitConverter./*ToInt32*/ToInt16(s.Slice(count, s.Length - count));
    //            count += sizeof(short/*int*/);


    //            /*id*/
    //            this.duration = BitConverter./*ToInt32*/ToSingle(s.Slice(count, s.Length - count));
    //            count += sizeof(float/*int*/);


    //            /*skills*/
    //            this.attributes.Clear();
    //            ushort /*skill*/attributeLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
    //            count += sizeof(ushort);

    //            for (int i = 0; i </*skill*/attributeLen; i++)
    //            {
    //                /*SkillInfo*/
    //                Attribute attribute = new /*SkillInfo*/Attribute();
    //                /*skill*/
    //                attribute.Read(s, ref count);
    //                /*skill*/
    //                attributes.Add(attribute/*skill*/);
    //            }


    //            //id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
    //            //count += sizeof(int);
    //            //level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
    //            //count += sizeof(short);
    //            //duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
    //            //count += sizeof(float);

    //        }

    //    }
    //    public List<Skill/*SkillInfo*/> skills/*skills */= new List<Skill/*SkillInfo*/>();


    //    //public long playerId;
    //    //public string name;

    //    //public struct SkillInfo
    //    //{
    //    //    public int id;
    //    //    public short level;
    //    //    public float duration;

    //    //    public bool Write(Span<byte> s , ref ushort count)
    //    //    {
    //    //        bool success = true;
    //    //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
    //    //        count += sizeof(int);
    //    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
    //    //        count += sizeof(short);
    //    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
    //    //        count += sizeof(float);

    //    //        return success;
    //    //    }
    //    //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
    //    //    {
    //    //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
    //    //        count += sizeof(int);
    //    //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
    //    //        count += sizeof(short);
    //    //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
    //    //        count += sizeof(float);

    //    //    }

    //    //}

    //    //public List<SkillInfo> skills = new List<SkillInfo>();

    //    //public PlayerInfoReq()
    //    //{
    //    //    this.packetId = (ushort)PacketID.PlayerInfoReq;
    //    //}
    //    public /*override*/ void Read(ArraySegment<byte> segment)
    //    {
    //        ushort count = 0;

    //        ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

    //        //ushort size = BitConverter.ToUInt16(s.Array, s.Offset);
    //        count += sizeof(ushort);
    //        // ushort id = BitConverter.ToUInt16(s.Array, s.Offset + count);
    //        count += sizeof(ushort);

    //        // this.playerId = BitConverter.ToInt64(s.Array, s.Offset + count);

    //        //this.playerId = BitConverter.ToInt64(new ReadOnlySpan<byte>(s.Array, s.Offset + count, s.Count - count));
    //        //this.playerId = BitConverter.ToInt64(s.Slice(count,s.Length-count));

    //        //count += sizeof(long);
    //        ////string
    //        //ushort nameLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
    //        //count += sizeof(ushort);
    //        //this.name=Encoding.Unicode.GetString(s.Slice(count, nameLen));
    //        //count += nameLen;

    //        ////skill list
    //        //skills.Clear();
    //        //ushort skillLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
    //        //count += sizeof(ushort);

    //        //for(int i=0;i<skillLen;i++)
    //        //{
    //        //    SkillInfo skill = new SkillInfo();
    //        //    skill.Read(s, ref count);
    //        //    skills.Add(skill);
    //        //}


    //        this.testByte = (byte)segment.Array[segment.Offset + count];
    //        count += sizeof(byte);



    //        /*id*/
    //        this.playerId = BitConverter./*ToInt32*/ToInt64(s.Slice(count, s.Length - count));
    //        count += sizeof(long/*int*/);


    //        ushort nameLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
    //        count += sizeof(ushort);
    //        this.name = Encoding.Unicode.GetString(s.Slice(count, nameLen));
    //        count += nameLen;


    //        /*skills*/
    //        this.skills.Clear();
    //        ushort /*skill*/skillLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
    //        count += sizeof(ushort);

    //        for (int i = 0; i </*skill*/skillLen; i++)
    //        {
    //            /*SkillInfo*/
    //            Skill skill = new /*SkillInfo*/Skill();
    //            /*skill*/
    //            skill.Read(s, ref count);
    //            /*skill*/
    //            skills.Add(skill/*skill*/);
    //        }


    //    }

    //    public /*override*/ ArraySegment<byte> Write()
    //    {
    //        ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
    //        ushort count = 0;
    //        bool success = true;

    //        Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

    //        //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
    //        //count += sizeof(ushort);
    //        //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
    //        //count += sizeof(ushort);
    //        //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
    //        //count += sizeof(long);
    //        //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

    //        count += sizeof(ushort);
    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), /*this.packetId*/(ushort)PacketID.PlayerInfoReq/*PlayerInfoReq*/);
    //        count += sizeof(ushort);
    //        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
    //        //count += sizeof(long);
    //        ////success &= BitConverter.TryWriteBytes(s, count);

    //        ////string len[2]
    //        ////byte[]
    //        //// ushort nameLen=(ushort)Encoding.Unicode.GetByteCount(this.name);
    //        //// success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
    //        //// count += sizeof(ushort);
    //        //// Array.Copy(Encoding.Unicode.GetBytes(this.name), 0, segment.Array, count, nameLen);
    //        //// count += nameLen;

    //        //ushort nameLen =(ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count+sizeof(ushort));
    //        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
    //        //count += sizeof(ushort);
    //        //count += nameLen;

    //        ////skill list
    //        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)skills.Count);
    //        //count += sizeof(ushort);
    //        //foreach(SkillInfo skill in skills)
    //        //{
    //        //    success &= skill.Write(s, ref count);
    //        //}


    //        segment.Array[segment.Offset + count] = (byte)this.testByte;
    //        count += sizeof(byte);


    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
    //        count += sizeof(long);


    //        ushort nameLen = (ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count + sizeof(ushort));
    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
    //        count += sizeof(ushort);
    //        count += nameLen;



    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)this.skills.Count);
    //        count += sizeof(ushort);
    //        foreach (Skill skill in skills)
    //        {
    //            success &= skill.Write(s, ref count);
    //        }


    //        success &= BitConverter.TryWriteBytes(s, count);

    //        if (success == false)
    //            return null;

    //        return SendBufferHelper.Close(count);

    //    }
    //}




    //class Test//PlayerInfoReq//:Packet
    //{

    //    public int testInt;


    //    //public long playerId;
    //    //public string name;

    //    //public struct SkillInfo
    //    //{
    //    //    public int id;
    //    //    public short level;
    //    //    public float duration;

    //    //    public bool Write(Span<byte> s , ref ushort count)
    //    //    {
    //    //        bool success = true;
    //    //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
    //    //        count += sizeof(int);
    //    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
    //    //        count += sizeof(short);
    //    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
    //    //        count += sizeof(float);

    //    //        return success;
    //    //    }
    //    //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
    //    //    {
    //    //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
    //    //        count += sizeof(int);
    //    //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
    //    //        count += sizeof(short);
    //    //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
    //    //        count += sizeof(float);

    //    //    }

    //    //}

    //    //public List<SkillInfo> skills = new List<SkillInfo>();

    //    //public PlayerInfoReq()
    //    //{
    //    //    this.packetId = (ushort)PacketID.PlayerInfoReq;
    //    //}
    //    public /*override*/ void Read(ArraySegment<byte> segment)
    //    {
    //        ushort count = 0;

    //        ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

    //        //ushort size = BitConverter.ToUInt16(s.Array, s.Offset);
    //        count += sizeof(ushort);
    //        // ushort id = BitConverter.ToUInt16(s.Array, s.Offset + count);
    //        count += sizeof(ushort);

    //        // this.playerId = BitConverter.ToInt64(s.Array, s.Offset + count);

    //        //this.playerId = BitConverter.ToInt64(new ReadOnlySpan<byte>(s.Array, s.Offset + count, s.Count - count));
    //        //this.playerId = BitConverter.ToInt64(s.Slice(count,s.Length-count));

    //        //count += sizeof(long);
    //        ////string
    //        //ushort nameLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
    //        //count += sizeof(ushort);
    //        //this.name=Encoding.Unicode.GetString(s.Slice(count, nameLen));
    //        //count += nameLen;

    //        ////skill list
    //        //skills.Clear();
    //        //ushort skillLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
    //        //count += sizeof(ushort);

    //        //for(int i=0;i<skillLen;i++)
    //        //{
    //        //    SkillInfo skill = new SkillInfo();
    //        //    skill.Read(s, ref count);
    //        //    skills.Add(skill);
    //        //}


    //        /*id*/
    //        this.testInt = BitConverter./*ToInt32*/ToInt32(s.Slice(count, s.Length - count));
    //        count += sizeof(int/*int*/);

    //    }

    //    public /*override*/ ArraySegment<byte> Write()
    //    {
    //        ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
    //        ushort count = 0;
    //        bool success = true;

    //        Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

    //        //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
    //        //count += sizeof(ushort);
    //        //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
    //        //count += sizeof(ushort);
    //        //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
    //        //count += sizeof(long);
    //        //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

    //        count += sizeof(ushort);
    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), /*this.packetId*/(ushort)PacketID.Test/*PlayerInfoReq*/);
    //        count += sizeof(ushort);
    //        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
    //        //count += sizeof(long);
    //        ////success &= BitConverter.TryWriteBytes(s, count);

    //        ////string len[2]
    //        ////byte[]
    //        //// ushort nameLen=(ushort)Encoding.Unicode.GetByteCount(this.name);
    //        //// success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
    //        //// count += sizeof(ushort);
    //        //// Array.Copy(Encoding.Unicode.GetBytes(this.name), 0, segment.Array, count, nameLen);
    //        //// count += nameLen;

    //        //ushort nameLen =(ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count+sizeof(ushort));
    //        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
    //        //count += sizeof(ushort);
    //        //count += nameLen;

    //        ////skill list
    //        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)skills.Count);
    //        //count += sizeof(ushort);
    //        //foreach(SkillInfo skill in skills)
    //        //{
    //        //    success &= skill.Write(s, ref count);
    //        //}


    //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.testInt);
    //        count += sizeof(int);


    //        success &= BitConverter.TryWriteBytes(s, count);

    //        if (success == false)
    //            return null;

    //        return SendBufferHelper.Close(count);

    //    }
    //}






    //class PlayerInfoOk : Packet
    //{
    //    public int hp;
    //    public int attack;
    //}
    //public enum PacketID
    //{
    //    PlayerInfoReq = 1,
    //    PlayerInfoOk = 2,
    //}
    class ClientSession : /*Session*/PacketSession
    {
        public int SessionId { get; set; }
        public GameRoom Room { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected: {endPoint}");

            Program.Room.Push(() => Program.Room.Enter(this));
           // Program.Room.Enter(this);

            // Knight knight = new Knight() { attack = 10, hp = 100 };
            // Packet packet = new Packet() { size = 100, packetId = 10 };

            //byte[] sendBuff = new byte[1024];


            //byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to Server");

            //  ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
            //byte[] buffer = BitConverter.GetBytes(knight.hp);
            //byte[] buffer2 = BitConverter.GetBytes(knight.attack);
            //   byte[] buffer = BitConverter.GetBytes(packet.size);
            //  byte[] buffer2 = BitConverter.GetBytes(packet.packetId);
            // Array.Copy(buffer, 0, sendBuff, 0, buffer.Length);
            //Array.Copy(buffer2, 0, sendBuff, buffer.Length, buffer2.Length);
            //  Array.Copy(buffer, 0, openSegment.Array, openSegment.Offset, buffer.Length);
            //  Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset+buffer.Length, buffer2.Length);
            //  ArraySegment<byte> sendBuff=SendBufferHelper.Close(buffer.Length + buffer2.Length);


            // Send(sendBuff);

            //Thread.Sleep(5000);
            //Disconnect();

        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected: {endPoint}");
            SessionManager.Instance.Remove(this);

            if(Room != null)
            {
                GameRoom room = Room;
                room.Push(() => room.Leave(this));
               // Room.Leave(this);
                Room = null;
            }
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);

            //ushort count = 0;

            //ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            //count += 2;
            //ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
            //count += 2;

            //switch((PacketID)id)
            //{
            //    case PacketID.PlayerInfoReq:
            //        {
            //            //long playerId = BitConverter.ToInt64(buffer.Array, buffer.Offset + count);
            //            //count += 8;
            //            PlayerInfoReq p = new PlayerInfoReq();
            //            p.Read(buffer);
            //            //Console.WriteLine($"PlayerInfoReq: {p.playerId} {p.name}");


            //            //foreach(PlayerInfoReq.Skill skill in p.skills)
            //            //{
            //            //    Console.WriteLine($"Skill ({skill.id} {skill.level} {skill.duration})");
            //            //}
            //        }
            //        break;
            //}

           // Console.WriteLine($"RecvPacketId: {id},Size : {size}");
        }

        //public override int OnRecv(ArraySegment<byte> buffer)
        //{
        //    string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count); ;
        //    Console.WriteLine($"[From Client] : {recvData}");

        //    return buffer.Count;
        //}

        public override void OnSend(int numOfBytes)
        {
           // Console.WriteLine($"Trandsferred bytes: {numOfBytes}");
        }
    }
}
