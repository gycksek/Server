using Server;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    internal class PacketHandler
    {
        public static void C2S_PlayerInfoReqHandler(PacketSession session,IPacket packet)
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

        if(clientSession.Room==null)
        {
            return;
        }

        clientSession.Room.Broadcast(clientSession, p.chat);

    }



}

