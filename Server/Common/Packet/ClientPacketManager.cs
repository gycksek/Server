
     using ServerCore;

    internal class PacketManager
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


        Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();
        Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();
        public void Register()
        {
            
        _onRecv.Add((ushort)PacketID.S2C_Test, MakePacket<S2C_Test>);
        _handler.Add((ushort)PacketID.S2C_Test, PacketHandler.S2C_TestHandler);

        _onRecv.Add((ushort)PacketID.S2C_Chat, MakePacket<S2C_Chat>);
        _handler.Add((ushort)PacketID.S2C_Chat, PacketHandler.S2C_ChatHandler);


        }

        public void OnRecvPacket(PacketSession session,ArraySegment<byte> buffer)
        {
            ushort count = 0;

            ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            count += 2;
            ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
            count += 2;

            Action<PacketSession, ArraySegment<byte>> action = null;
            if (_onRecv.TryGetValue(id, out action))
                action.Invoke(session, buffer);

        }

        void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket,new()
        {
            T p = new T();
            p.Read(buffer);
            Action<PacketSession, IPacket> action = null;
            if (_handler.TryGetValue(p.Protocol, out action))
                action.Invoke(session, p);
        }
    }

