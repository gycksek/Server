
        using ServerCore;
        using System.Net;
        using System.Text;
        using System;
        using System.Collections.Generic;

         public enum PacketID
         {
            
        C2S_PlayerInfoReq=1,

        S2C_Test=2,

        C2S_Chat=3,

        S2C_Chat=4,

        S2C_BroadcastEnterGame=5,

        C2S_LeaveGame=6,

        S2C_BroadcastLeavGame=7,

        S2C_PlayerList=8,

        C2S_Move=9,

        S2C_BroadcastMove=10,

             //PlayerInfoReq=1,
             //PlayerInfoOk=2,
         }

        public interface IPacket
        {
            ushort Protocol { get; }
            void Read(ArraySegment<byte> segment);
            ArraySegment<byte> Write();
        }

        

    public class C2S_PlayerInfoReq : IPacket//PlayerInfoReq//:Packet
    {
            
	        public byte testByte;
	
	    
	        public long playerId;
	
	    
	        public string name;
	
	    
	         public class Skill//SkillInfo
	        {
	            //public int id;
	            //public short level;
	            //public float duration;
	                
		        public int id;
		
		    
		        public short level;
		
		    
		        public float duration;
		
		    
		         public class Attribute//SkillInfo
		        {
		            //public int id;
		            //public short level;
		            //public float duration;
		                
			        public int att;
			
		
		            //public bool Write(Span<byte> s , ref ushort count)
		            public bool Write(ArraySegment<byte> segment , ref ushort count)
		            {
		                bool success = true;
		                //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
		                //count += sizeof(int);
		                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
		                //count += sizeof(short);
		                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
		                //count += sizeof(float);
		                
			         Array.Copy(BitConverter.GetBytes(this.att),0,segment.Array,segment.Offset+count,sizeof(int));
			
			        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.att);
			        count += sizeof(int);
			
		                return success;
		            }
		           //public void Read(ReadOnlySpan<byte> s,ref ushort count)
		            public void Read(ArraySegment<byte> segment,ref ushort count)
		            {
		                
			        /*id*/this.att = BitConverter./*ToInt32*/ToInt32(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
			        count += sizeof(int/*int*/);
			
		                //id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		                //count += sizeof(int);
		                //level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
		                //count += sizeof(short);
		                //duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		                //count += sizeof(float);
		
		            }
		
		        }
		        public List<Attribute/*SkillInfo*/>  attributes/*skills */= new List<Attribute/*SkillInfo*/>();
		
	
	            //public bool Write(Span<byte> s , ref ushort count)
	            public bool Write(ArraySegment<byte> segment , ref ushort count)
	            {
	                bool success = true;
	                //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
	                //count += sizeof(int);
	                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
	                //count += sizeof(short);
	                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
	                //count += sizeof(float);
	                
		         Array.Copy(BitConverter.GetBytes(this.id),0,segment.Array,segment.Offset+count,sizeof(int));
		
		        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.id);
		        count += sizeof(int);
		
		
		         Array.Copy(BitConverter.GetBytes(this.level),0,segment.Array,segment.Offset+count,sizeof(short));
		
		        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.level);
		        count += sizeof(short);
		
		
		         Array.Copy(BitConverter.GetBytes(this.duration),0,segment.Array,segment.Offset+count,sizeof(float));
		
		        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.duration);
		        count += sizeof(float);
		
		
		
		         Array.Copy(BitConverter.GetBytes((ushort)this.attributes.Count),0,segment.Array,segment.Offset+count,sizeof(ushort));
		
		       // success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)this.attributes.Count);
		        count += sizeof(ushort);
		        foreach(Attribute attribute in attributes)
		        {
		           // success &= attribute.Write(s, ref count);
		            attribute.Write(segment, ref count);
		        }
		
	                return success;
	            }
	           //public void Read(ReadOnlySpan<byte> s,ref ushort count)
	            public void Read(ArraySegment<byte> segment,ref ushort count)
	            {
	                
		        /*id*/this.id = BitConverter./*ToInt32*/ToInt32(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
		        count += sizeof(int/*int*/);
		
		
		        /*id*/this.level = BitConverter./*ToInt32*/ToInt16(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
		        count += sizeof(short/*int*/);
		
		
		        /*id*/this.duration = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
		        count += sizeof(float/*int*/);
		
		
		         /*skills*/this.attributes.Clear();
		         ushort /*skill*/attributeLen = BitConverter.ToUInt16(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
		         count += sizeof(ushort);
		       
		         for(int i=0;i</*skill*/attributeLen;i++)
		         {
		             /*SkillInfo*/ Attribute attribute = new /*SkillInfo*/Attribute();
		             /*skill*/attribute.Read(segment, ref count);
		             /*skill*/attributes.Add(attribute/*skill*/);
		         }
		
		
	                //id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
	                //count += sizeof(int);
	                //level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
	                //count += sizeof(short);
	                //duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
	                //count += sizeof(float);
	
	            }
	
	        }
	        public List<Skill/*SkillInfo*/>  skills/*skills */= new List<Skill/*SkillInfo*/>();
	

    public ushort Protocol { get { return (ushort)PacketID.C2S_PlayerInfoReq; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
	        this.testByte=(byte)segment.Array[segment.Offset + count];
	        count += sizeof(byte);
	
	
	
	        /*id*/this.playerId = BitConverter./*ToInt32*/ToInt64(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(long/*int*/);
	
	
	        ushort nameLen = BitConverter.ToUInt16(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(ushort);
	        this.name=Encoding.Unicode.GetString(/*s.Slice(count*/segment.Array,segment.Offset+count, nameLen);
	        count += nameLen;
	
	
	         /*skills*/this.skills.Clear();
	         ushort /*skill*/skillLen = BitConverter.ToUInt16(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	         count += sizeof(ushort);
	       
	         for(int i=0;i</*skill*/skillLen;i++)
	         {
	             /*SkillInfo*/ Skill skill = new /*SkillInfo*/Skill();
	             /*skill*/skill.Read(segment, ref count);
	             /*skill*/skills.Add(skill/*skill*/);
	         }
	
	
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.C2S_PlayerInfoReq), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.C2S_PlayerInfoReq/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.C2S_PlayerInfoReq/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

            
	        segment.Array[segment.Offset + count]=(byte)this.testByte;
	        count += sizeof(byte);
	
	
	         Array.Copy(BitConverter.GetBytes(this.playerId),0,segment.Array,segment.Offset+count,sizeof(long));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
	        count += sizeof(long);
	
	
	        ushort nameLen =(ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count+sizeof(ushort));
	        Array.Copy(BitConverter.GetBytes(nameLen),0,segment.Array,segment.Offset+count,sizeof(ushort));        
	
	       // success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
	        count += sizeof(ushort);
	        count += nameLen;
	
	
	
	
	         Array.Copy(BitConverter.GetBytes((ushort)this.skills.Count),0,segment.Array,segment.Offset+count,sizeof(ushort));
	
	       // success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)this.skills.Count);
	        count += sizeof(ushort);
	        foreach(Skill skill in skills)
	        {
	           // success &= skill.Write(s, ref count);
	            skill.Write(segment, ref count);
	        }
	    
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }




    public class S2C_Test : IPacket//PlayerInfoReq//:Packet
    {
            
	        public int testInt;
	

    public ushort Protocol { get { return (ushort)PacketID.S2C_Test; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
	        /*id*/this.testInt = BitConverter./*ToInt32*/ToInt32(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(int/*int*/);
	
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.S2C_Test), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_Test/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_Test/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

            
	         Array.Copy(BitConverter.GetBytes(this.testInt),0,segment.Array,segment.Offset+count,sizeof(int));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.testInt);
	        count += sizeof(int);
	    
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }




    public class C2S_Chat : IPacket//PlayerInfoReq//:Packet
    {
            
	        public string chat;
	

    public ushort Protocol { get { return (ushort)PacketID.C2S_Chat; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
	        ushort chatLen = BitConverter.ToUInt16(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(ushort);
	        this.chat=Encoding.Unicode.GetString(/*s.Slice(count*/segment.Array,segment.Offset+count, chatLen);
	        count += chatLen;
	
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.C2S_Chat), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.C2S_Chat/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.C2S_Chat/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

            
	        ushort chatLen =(ushort)Encoding.Unicode.GetBytes(this.chat, 0, this.chat.Length, segment.Array, segment.Offset + count+sizeof(ushort));
	        Array.Copy(BitConverter.GetBytes(chatLen),0,segment.Array,segment.Offset+count,sizeof(ushort));        
	
	       // success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), chatLen);
	        count += sizeof(ushort);
	        count += chatLen;
	
	    
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }




    public class S2C_Chat : IPacket//PlayerInfoReq//:Packet
    {
            
	        public int playerId;
	
	    
	        public string chat;
	

    public ushort Protocol { get { return (ushort)PacketID.S2C_Chat; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
	        /*id*/this.playerId = BitConverter./*ToInt32*/ToInt32(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(int/*int*/);
	
	
	        ushort chatLen = BitConverter.ToUInt16(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(ushort);
	        this.chat=Encoding.Unicode.GetString(/*s.Slice(count*/segment.Array,segment.Offset+count, chatLen);
	        count += chatLen;
	
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.S2C_Chat), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_Chat/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_Chat/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

            
	         Array.Copy(BitConverter.GetBytes(this.playerId),0,segment.Array,segment.Offset+count,sizeof(int));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
	        count += sizeof(int);
	
	
	        ushort chatLen =(ushort)Encoding.Unicode.GetBytes(this.chat, 0, this.chat.Length, segment.Array, segment.Offset + count+sizeof(ushort));
	        Array.Copy(BitConverter.GetBytes(chatLen),0,segment.Array,segment.Offset+count,sizeof(ushort));        
	
	       // success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), chatLen);
	        count += sizeof(ushort);
	        count += chatLen;
	
	    
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }




    public class S2C_BroadcastEnterGame : IPacket//PlayerInfoReq//:Packet
    {
            
	        public int playerId;
	
	    
	        public float posX;
	
	    
	        public float posY;
	
	    
	        public float posZ;
	

    public ushort Protocol { get { return (ushort)PacketID.S2C_BroadcastEnterGame; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
	        /*id*/this.playerId = BitConverter./*ToInt32*/ToInt32(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(int/*int*/);
	
	
	        /*id*/this.posX = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(float/*int*/);
	
	
	        /*id*/this.posY = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(float/*int*/);
	
	
	        /*id*/this.posZ = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(float/*int*/);
	
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.S2C_BroadcastEnterGame), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_BroadcastEnterGame/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_BroadcastEnterGame/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

            
	         Array.Copy(BitConverter.GetBytes(this.playerId),0,segment.Array,segment.Offset+count,sizeof(int));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
	        count += sizeof(int);
	
	
	         Array.Copy(BitConverter.GetBytes(this.posX),0,segment.Array,segment.Offset+count,sizeof(float));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posX);
	        count += sizeof(float);
	
	
	         Array.Copy(BitConverter.GetBytes(this.posY),0,segment.Array,segment.Offset+count,sizeof(float));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posY);
	        count += sizeof(float);
	
	
	         Array.Copy(BitConverter.GetBytes(this.posZ),0,segment.Array,segment.Offset+count,sizeof(float));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posZ);
	        count += sizeof(float);
	    
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }




    public class C2S_LeaveGame : IPacket//PlayerInfoReq//:Packet
    {
        

    public ushort Protocol { get { return (ushort)PacketID.C2S_LeaveGame; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.C2S_LeaveGame), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.C2S_LeaveGame/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.C2S_LeaveGame/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

                
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }




    public class S2C_BroadcastLeavGame : IPacket//PlayerInfoReq//:Packet
    {
            
	        public int playerId;
	

    public ushort Protocol { get { return (ushort)PacketID.S2C_BroadcastLeavGame; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
	        /*id*/this.playerId = BitConverter./*ToInt32*/ToInt32(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(int/*int*/);
	
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.S2C_BroadcastLeavGame), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_BroadcastLeavGame/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_BroadcastLeavGame/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

            
	         Array.Copy(BitConverter.GetBytes(this.playerId),0,segment.Array,segment.Offset+count,sizeof(int));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
	        count += sizeof(int);
	    
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }




    public class S2C_PlayerList : IPacket//PlayerInfoReq//:Packet
    {
            
	         public class Player//SkillInfo
	        {
	            //public int id;
	            //public short level;
	            //public float duration;
	                
		        public bool isSelf;
		
		    
		        public int playerId;
		
		    
		        public float posX;
		
		    
		        public float posY;
		
		    
		        public float posZ;
		
	
	            //public bool Write(Span<byte> s , ref ushort count)
	            public bool Write(ArraySegment<byte> segment , ref ushort count)
	            {
	                bool success = true;
	                //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
	                //count += sizeof(int);
	                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
	                //count += sizeof(short);
	                //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
	                //count += sizeof(float);
	                
		         Array.Copy(BitConverter.GetBytes(this.isSelf),0,segment.Array,segment.Offset+count,sizeof(bool));
		
		        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.isSelf);
		        count += sizeof(bool);
		
		
		         Array.Copy(BitConverter.GetBytes(this.playerId),0,segment.Array,segment.Offset+count,sizeof(int));
		
		        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
		        count += sizeof(int);
		
		
		         Array.Copy(BitConverter.GetBytes(this.posX),0,segment.Array,segment.Offset+count,sizeof(float));
		
		        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posX);
		        count += sizeof(float);
		
		
		         Array.Copy(BitConverter.GetBytes(this.posY),0,segment.Array,segment.Offset+count,sizeof(float));
		
		        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posY);
		        count += sizeof(float);
		
		
		         Array.Copy(BitConverter.GetBytes(this.posZ),0,segment.Array,segment.Offset+count,sizeof(float));
		
		        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posZ);
		        count += sizeof(float);
		
	                return success;
	            }
	           //public void Read(ReadOnlySpan<byte> s,ref ushort count)
	            public void Read(ArraySegment<byte> segment,ref ushort count)
	            {
	                
		        /*id*/this.isSelf = BitConverter./*ToInt32*/ToBoolean(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
		        count += sizeof(bool/*int*/);
		
		
		        /*id*/this.playerId = BitConverter./*ToInt32*/ToInt32(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
		        count += sizeof(int/*int*/);
		
		
		        /*id*/this.posX = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
		        count += sizeof(float/*int*/);
		
		
		        /*id*/this.posY = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
		        count += sizeof(float/*int*/);
		
		
		        /*id*/this.posZ = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
		        count += sizeof(float/*int*/);
		
	                //id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
	                //count += sizeof(int);
	                //level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
	                //count += sizeof(short);
	                //duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
	                //count += sizeof(float);
	
	            }
	
	        }
	        public List<Player/*SkillInfo*/>  players/*skills */= new List<Player/*SkillInfo*/>();
	

    public ushort Protocol { get { return (ushort)PacketID.S2C_PlayerList; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
	         /*skills*/this.players.Clear();
	         ushort /*skill*/playerLen = BitConverter.ToUInt16(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	         count += sizeof(ushort);
	       
	         for(int i=0;i</*skill*/playerLen;i++)
	         {
	             /*SkillInfo*/ Player player = new /*SkillInfo*/Player();
	             /*skill*/player.Read(segment, ref count);
	             /*skill*/players.Add(player/*skill*/);
	         }
	
	
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.S2C_PlayerList), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_PlayerList/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_PlayerList/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

            
	
	         Array.Copy(BitConverter.GetBytes((ushort)this.players.Count),0,segment.Array,segment.Offset+count,sizeof(ushort));
	
	       // success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)this.players.Count);
	        count += sizeof(ushort);
	        foreach(Player player in players)
	        {
	           // success &= player.Write(s, ref count);
	            player.Write(segment, ref count);
	        }
	    
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }




    public class C2S_Move : IPacket//PlayerInfoReq//:Packet
    {
            
	        public float posX;
	
	    
	        public float posY;
	
	    
	        public float posZ;
	

    public ushort Protocol { get { return (ushort)PacketID.C2S_Move; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
	        /*id*/this.posX = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(float/*int*/);
	
	
	        /*id*/this.posY = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(float/*int*/);
	
	
	        /*id*/this.posZ = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(float/*int*/);
	
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.C2S_Move), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.C2S_Move/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.C2S_Move/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

            
	         Array.Copy(BitConverter.GetBytes(this.posX),0,segment.Array,segment.Offset+count,sizeof(float));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posX);
	        count += sizeof(float);
	
	
	         Array.Copy(BitConverter.GetBytes(this.posY),0,segment.Array,segment.Offset+count,sizeof(float));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posY);
	        count += sizeof(float);
	
	
	         Array.Copy(BitConverter.GetBytes(this.posZ),0,segment.Array,segment.Offset+count,sizeof(float));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posZ);
	        count += sizeof(float);
	    
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }




    public class S2C_BroadcastMove : IPacket//PlayerInfoReq//:Packet
    {
            
	        public int playerId;
	
	    
	        public float posX;
	
	    
	        public float posY;
	
	    
	        public float posZ;
	

    public ushort Protocol { get { return (ushort)PacketID.S2C_BroadcastMove; } }


        //public long playerId;
        //public string name;

        //public struct SkillInfo
        //{
        //    public int id;
        //    public short level;
        //    public float duration;

        //    public bool Write(Span<byte> s , ref ushort count)
        //    {
        //        bool success = true;
        //        success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count),id);
        //        count += sizeof(int);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), level);
        //        count += sizeof(short);
        //        success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), duration);
        //        count += sizeof(float);

        //        return success;
        //    }
        //    public void Read(ReadOnlySpan<byte> s,ref ushort count)
        //    {
        //        id = BitConverter.ToInt32(s.Slice(count, s.Length - count));
        //        count += sizeof(int);
        //        level = BitConverter.ToInt16(s.Slice(count, s.Length - count));
        //        count += sizeof(short);
        //        duration = BitConverter.ToSingle(s.Slice(count, s.Length - count));
        //        count += sizeof(float);

        //    }

        //}

        //public List<SkillInfo> skills = new List<SkillInfo>();

        //public PlayerInfoReq()
        //{
        //    this.packetId = (ushort)PacketID.PlayerInfoReq;
        //}
        public /*override*/ void Read(ArraySegment<byte> segment)
        {
            ushort count = 0;

            //ReadOnlySpan<byte> s =new ReadOnlySpan<byte>(segment.Array, segment.Offset + count, segment.Count - count);

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
            //{
            //    SkillInfo skill = new SkillInfo();
            //    skill.Read(s, ref count);
            //    skills.Add(skill);
            //}

            
	        /*id*/this.playerId = BitConverter./*ToInt32*/ToInt32(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(int/*int*/);
	
	
	        /*id*/this.posX = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(float/*int*/);
	
	
	        /*id*/this.posY = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(float/*int*/);
	
	
	        /*id*/this.posZ = BitConverter./*ToInt32*/ToSingle(/*s.Slice(count, s.Length - count)*/segment.Array,segment.Offset+count);
	        count += sizeof(float/*int*/);
	
        }

        public /*override*/ ArraySegment<byte> Write()
        {
            ArraySegment<byte> /*openSegment*/segment = SendBufferHelper.Open(4096);
            ushort count = 0;
           // bool success = true;

            //Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);

            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), packet.size);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.packetId);
            //count += sizeof(ushort);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + count, s.Count - count), this.playerId);
            //count += sizeof(long);
            //success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), count);

            count += sizeof(ushort);
            Array.Copy(BitConverter.GetBytes((ushort)PacketID.S2C_BroadcastMove), 0, segment.Array, segment.Offset + count, sizeof(ushort));

            //success &= BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_BroadcastMove/*PlayerInfoReq*/);
           // BitConverter.TryWriteBytes(s.Slice(count,s.Length-count), /*this.packetId*/(ushort)PacketID.S2C_BroadcastMove/*PlayerInfoReq*/);
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
            //{
            //    success &= skill.Write(s, ref count);
            //}

            
	         Array.Copy(BitConverter.GetBytes(this.playerId),0,segment.Array,segment.Offset+count,sizeof(int));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
	        count += sizeof(int);
	
	
	         Array.Copy(BitConverter.GetBytes(this.posX),0,segment.Array,segment.Offset+count,sizeof(float));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posX);
	        count += sizeof(float);
	
	
	         Array.Copy(BitConverter.GetBytes(this.posY),0,segment.Array,segment.Offset+count,sizeof(float));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posY);
	        count += sizeof(float);
	
	
	         Array.Copy(BitConverter.GetBytes(this.posZ),0,segment.Array,segment.Offset+count,sizeof(float));
	
	        //success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.posZ);
	        count += sizeof(float);
	    
            
            //success &= BitConverter.TryWriteBytes(s, count);
             Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset , sizeof(ushort));

            //BitConverter.TryWriteBytes(s, count);
            //if (success == false)
            //    return null;

             return SendBufferHelper.Close(count);

        }
    }



