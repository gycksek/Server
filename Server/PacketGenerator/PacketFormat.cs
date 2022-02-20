using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketGenerator
{
    internal class PacketFormat
    {
        //{0} 패킷 등록
        public static string managerFormat =
@"
     using ServerCore;

    internal class PacketManager
    {{
        static PacketManager _instance;

        public static  PacketManager Instance
        {{
            get
            {{
                if (_instance == null)
                    _instance = new PacketManager();

                return _instance;
            }}
        }}

        Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();
        Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();
        public void Register()
        {{
            {0}

        }}

        public void OnRecvPacket(PacketSession session,ArraySegment<byte> buffer)
        {{
            ushort count = 0;

            ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            count += 2;
            ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
            count += 2;

            Action<PacketSession, ArraySegment<byte>> action = null;
            if (_onRecv.TryGetValue(id, out action))
                action.Invoke(session, buffer);

        }}

        void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket,new()
        {{
            T p = new T();
            p.Read(buffer);
            Action<PacketSession, IPacket> action = null;
            if (_handler.TryGetValue(p.Protocol, out action))
                action.Invoke(session, p);
        }}
    }}

";
        //{0} 패킷 이름
        public static string managerRegisterFormat =
@"
        _onRecv.Add((ushort)PacketID.{0}, MakePacket<{0}>);
        _handler.Add((ushort)PacketID.{0}, PacketHandler.{0}Handler);
";

        //{0} 패킷 이름/번호 목록
        //{1} 패킷 번호
        public static string fileFormat =
@"
        using ServerCore;
        using System.Net;
        using System.Text;

         public enum PacketID
         {{
            {0}
             //PlayerInfoReq=1,
             //PlayerInfoOk=2,
         }}

        interface IPacket
        {{
            ushort Protocol {{ get; }}
            void Read(ArraySegment<byte> segment);
            ArraySegment<byte> Write();
        }}

        {1}
";

        //{0} 패킷 이름
        //{1} 패킷 번호
        public static string PacketEnumFormat =
@"
        {0}={1},
";


        //{0} 패킷이름
        //{1} 멤버 변수들
        //{2} 멤버 변수 Read
        //{2} 멤버 변수 Write

        public static string packetFormat =
@"

class {0} : IPacket//PlayerInfoReq//:Packet
    {{
        {1}

    public ushort Protocol {{ get {{ return (ushort)PacketID.{0}; }} }}


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {{
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }}
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {{
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }}

        //}}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {{
            ushort count = 0;

            ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

            //ushort size = BitConverter.ToUInt16(s.Array, s.Offset);
            count += sizeof(ushort);
            // ushort id = BitConverter.ToUInt16(s.Array, s.Offset + count);
            count += sizeof(ushort);

            // this.playerId = BitConverter.ToInt64(s.Array, s.Offset + count);

            //this.playerId = BitConverter.ToInt64(new ReadOnlySpan<byte>(s.Array, s.Offset + count, s.Count - count));
            //this.playerId = BitConverter.ToInt64(s.Slice(count,s.Length-count));

            //count += sizeof(long);
            ////string
            //ushort nameLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
            //count += sizeof(ushort);
            //this.name=Encoding.Unicode.GetString(s.Slice(count, nameLen));
            //count += nameLen;

            ////skill list
            //skills.Clear();
            //ushort skillLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
            //count += sizeof(ushort);
          
            //for(int i=0;i<skillLen;i++)
            //{{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}}

            {2}
        }}

        public /*override*/ ArraySegment<byte> Write()
        {{
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
            bool success = true;

            Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.{0}/*PlayerInfoReq*/);
            count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
            //count += sizeof(long);
            ////success &= BitConverter.TryWriteBytes(s, count);

            ////string len[2]
            ////byte[]
            //// ushort nameLen=(ushort)Encoding.Unicode.GetByteCount(this.name);
            //// success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
            //// count += sizeof(ushort);
            //// Array.Copy(Encoding.Unicode.GetBytes(this.name), 0, segment.Array, count, nameLen);
            //// count += nameLen;
            
            //ushort nameLen =(ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count+sizeof(ushort));
            //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
            //count += sizeof(ushort);
            //count += nameLen;

            ////skill list
            //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)skills.Count);
            //count += sizeof(ushort);
            //foreach(SkillInfo skill in skills)
            //{{
            //    success &= skill.Write(s, ref count);
            //}}

            {3}    
            
            success &= BitConverter.TryWriteBytes(s, count);

            if (success == false)
                return null;

             return SendBufferHelper.Close(count);

        }}
    }}


";
        //{0} 변수 형식
        //{1} 변수이름
        public static string memberFormat =
@"    
        public {0} {1};
";
        //{0} 리스트 이름 [대문자]
        //{1} 리스트 이름 [소문자]
        //{2} 멤버 변수들
        //{3} 멤버 변수 Read
        //{4} 멤버 변수 Write
        public static string memberListFormat =
@"    
         public class {0}//SkillInfo
        {{
            //public int id;
            //public short level;
            //public float duration;
            {2}

            public bool Write(Span<byte> s , ref ushort count)
            {{
                bool success = true;
                //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
                //count += sizeof(int);
                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
                //count += sizeof(short);
                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
                //count += sizeof(float);
                {4}
                return success;
            }}
            public void Read(ReadOnlySpan<byte> s,ref ushort count)
            {{
                {3}
                //id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
                //count += sizeof(int);
                //level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
                //count += sizeof(short);
                //duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
                //count += sizeof(float);

            }}

        }}
        public List<{0}/*SkillInfo*/>  {1}s/*skills */= new List<{0}/*SkillInfo*/>();
";



        //{0} 변수이름
        //{1} To~변수형식
        //{2} 변수형식
        public static string readFormat =
@"
        /*id*/this.{0} = BitConverter./*ToInt32*/{1}(s.Slice(count, s.Length - count));
        count += sizeof({2}/*int*/);
";

        //{0} 변수이름
        //{1}변수형식
        public static string readByteFormat =
@"
        this.{0}=({1})segment.Array[segment.Offset + count];
        count += sizeof({1});

";

        //{0} 변수이름
        public static string readStringFormat =
@"
        ushort {0}Len = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
        count += sizeof(ushort);
        this.{0}=Encoding.Unicode.GetString(s.Slice(count, {0}Len));
        count += {0}Len;
";
        //{0} 리스트 이름 [대문자]
        //{1} 리스트 이름 [소문자]
        public static string readListFormat =
@"
         /*skills*/this.{1}s.Clear();
         ushort /*skill*/{1}Len = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
         count += sizeof(ushort);
       
         for(int i=0;i</*skill*/{1}Len;i++)
         {{
             /*SkillInfo*/ {0} {1} = new /*SkillInfo*/{0}();
             /*skill*/{1}.Read(s, ref count);
             /*skill*/{1}s.Add({1}/*skill*/);
         }}

";



        //{0} 변수이름
        //{1} 변수형식
        public static string writeFormat =
@"
        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.{0});
        count += sizeof({1});
";
        //{0} 변수이름
        public static string writeStringFormat =
@"
        ushort {0}Len =(ushort)Encoding.Unicode.GetBytes(this.{0}, 0, this.{0}.Length, segment.Array, segment.Offset + count+sizeof(ushort));
        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), {0}Len);
        count += sizeof(ushort);
        count += {0}Len;

";
        //{0} 리스트 이름 [대문자]
        //{1} 리스트 이름 [소문자]
        public static string writeListFormat =
@"
        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)this.{1}s.Count);
        count += sizeof(ushort);
        foreach({0} {1} in {1}s)
        {{
            success &= {1}.Write(s, ref count);
        }}
";

        //{0} 변수이름
        //{1}변수형식
        public static string writeByteFormat =
@"
        segment.Array[segment.Offset + count]=(byte)this.{0};
        count += sizeof({1});
";
    }
}
