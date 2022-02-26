using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class PacketHandler
{
    public static void S2C_TestHandler(PacketSession session, IPacket packet)
    {

    }

    public static void S2C_ChatHandler(PacketSession session, IPacket packet)
    {
        S2C_Chat chatPacket=packet as S2C_Chat;
        ServerSession serverSession = session as ServerSession;

       // if(chatPacket.playerId ==1)
           // Console.WriteLine(chatPacket.chat);



    }


}