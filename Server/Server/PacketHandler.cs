using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class PacketHandler
{
    public static void C2S_PlayerInfoReqHandler(PacketSession session, IPacket packet)
    {
        C2S_PlayerInfoReq p = packet as C2S_PlayerInfoReq;

        Console.WriteLine($"PlayerInfoReq: {p.playerId} {p.name}");


        foreach (C2S_PlayerInfoReq.Skill skill in p.skills)
        {
            Console.WriteLine($"Skill ({skill.id} {skill.level} {skill.duration})");
        }
    }


    public static void C2S_ChatHandler(PacketSession session, IPacket packet)
    {
        C2S_Chat p = packet as C2S_Chat;
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
        {
            return;
        }

        GameRoom room = clientSession.Room;
        room.Push(() => room.Broadcast(clientSession, p.chat)); //주문서에 넣는 행동

        // clientSession.Room.Push(() => clientSession.Room.Broadcast(clientSession, p.chat)); //주문서에 넣는 행동

        // clientSession.Room.Broadcast(clientSession, p.chat);
    }
    public static void C2S_LeaveGameHandler(PacketSession session, IPacket packet)
    {
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
        {
            return;
        }

        GameRoom room = clientSession.Room;
        room.Push(() => room.Leave(clientSession));

    }
    public static void C2S_MoveHandler(PacketSession session, IPacket packet)
    {
        C2S_Move movePacket= packet as C2S_Move;
        ClientSession clientSession = session as ClientSession;

        if (clientSession.Room == null)
        {
            return;
        }

        //Console.WriteLine($"{movePacket.posX},{movePacket.posY},{movePacket.posZ}");

        GameRoom room = clientSession.Room;
        room.Push(() => room.Move(clientSession,movePacket));

    }

    public static void C2S_Chat_Proto_Handler(PacketSession session, IMessage packet)
    {
        S2C_Chat_Proto chatPacket = packet as S2C_Chat_Proto;
        ClientSession serverSession = session as ClientSession;
        Console.WriteLine(chatPacket.Context);
    }
}

