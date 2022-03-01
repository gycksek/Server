
     using ServerCore;
    using System;
    using System.Collections.Generic;

    public class PacketManager
    {
       static PacketManager _instance=new PacketManager();

        public static  PacketManager Instance
        {
            get
            {
                //if (_instance == null)
                //    _instance = new PacketManager();

                return _instance;
            }
        }

     PacketManager()
    {
        Register();
    }

        Dictionary<ushort, Func<PacketSession, ArraySegment<byte>,IPacket>> _makeFunc = new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>,IPacket>>();

        //Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();
        Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();
        public void Register()
        {
            
        _makeFunc.Add((ushort)PacketID.S2C_Test, MakePacket<S2C_Test>);
        _handler.Add((ushort)PacketID.S2C_Test, PacketHandler.S2C_TestHandler);

        _makeFunc.Add((ushort)PacketID.S2C_Chat, MakePacket<S2C_Chat>);
        _handler.Add((ushort)PacketID.S2C_Chat, PacketHandler.S2C_ChatHandler);

        _makeFunc.Add((ushort)PacketID.S2C_BroadcastEnterGame, MakePacket<S2C_BroadcastEnterGame>);
        _handler.Add((ushort)PacketID.S2C_BroadcastEnterGame, PacketHandler.S2C_BroadcastEnterGameHandler);

        _makeFunc.Add((ushort)PacketID.S2C_BroadcastLeavGame, MakePacket<S2C_BroadcastLeavGame>);
        _handler.Add((ushort)PacketID.S2C_BroadcastLeavGame, PacketHandler.S2C_BroadcastLeavGameHandler);

        _makeFunc.Add((ushort)PacketID.S2C_PlayerList, MakePacket<S2C_PlayerList>);
        _handler.Add((ushort)PacketID.S2C_PlayerList, PacketHandler.S2C_PlayerListHandler);

        _makeFunc.Add((ushort)PacketID.S2C_BroadcastMove, MakePacket<S2C_BroadcastMove>);
        _handler.Add((ushort)PacketID.S2C_BroadcastMove, PacketHandler.S2C_BroadcastMoveHandler);


        }

        public void OnRecvPacket(PacketSession session,ArraySegment<byte> buffer,Action<PacketSession,IPacket> _onRecvCallback=null)
        {
            ushort count = 0;

            ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            count += 2;
            ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
            count += 2;

            //Action<PacketSession, ArraySegment<byte>> action = null;
            //if (_onRecv.TryGetValue(id, out action))
            //    action.Invoke(session, buffer);
            Func<PacketSession, ArraySegment<byte>, IPacket> func = null;
            if (_makeFunc.TryGetValue(id, out func))
            {
                 IPacket packet= func.Invoke(session, buffer);

            if (_onRecvCallback != null)
                _onRecvCallback.Invoke(session, packet);
            else
                HandlePacket(session, packet);
            }

        }

        T MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket,new()
        {
            T p = new T();
            p.Read(buffer);
            //Action<PacketSession, IPacket> action = null;
            //if (_handler.TryGetValue(p.Protocol, out action))
            //    action.Invoke(session, p);
             return p;
        }

        public void HandlePacket(PacketSession session,IPacket packet)
         {
            Action<PacketSession, IPacket> action = null;
            if (_handler.TryGetValue(packet.Protocol, out action))
            action.Invoke(session, packet);
         }


    }

