using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class GameRoom : IJobQueue
    {
        List<ClientSession> _sessions = new List<ClientSession>();

      //  object _lock = new object();
        JobQueue _JobQueue=new JobQueue();
        List<ArraySegment<Byte>> _pendingList=new List<ArraySegment<Byte>>();

        public void Flush()
        {
            foreach (ClientSession s in _sessions)
                s.Send(_pendingList);

            Console.WriteLine($"Flushed {_pendingList.Count} items");
            _pendingList.Clear();
        }



        public void Broadcast(ClientSession session,string chat)
        {
            S2C_Chat packet = new S2C_Chat();
            packet.playerId = session.SessionId;
            packet.chat = chat+$"I am {packet.playerId}";
            ArraySegment<byte> segment = packet.Write();


            _pendingList.Add(segment);

           // lock(_lock)
          //  {
                //foreach (ClientSession s in _sessions)
                //    s.Send(segment);
           // }

        }
        public void Broadcast(ArraySegment<byte> segment)
        {
            _pendingList.Add(segment);

        }


        public void Enter(ClientSession session)
        {
           // lock(_lock)
          //  {
                //플레이어 추가
                _sessions.Add(session);
                session.Room = this;
            //  }

            //신입생한테 모든 플레이어 목록 전송
            S2C_PlayerList players = new S2C_PlayerList();
            foreach (ClientSession s in _sessions)
            {
                players.players.Add(new S2C_PlayerList.Player()
                {
                    isSelf = (s == session),
                    playerId = s.SessionId,
                    posX = s.PosX,
                    posY = s.PosY,
                    posZ = s.PosZ,
                });
            }
            session.Send(players.Write());
            //신입생 입장을 모두에게 알린다
            S2C_BroadcastEnterGame enter = new S2C_BroadcastEnterGame();
            enter.playerId = session.SessionId;
            enter.posX = 0;
            enter.posY = 0;
            enter.posZ = 0;
            Broadcast(enter.Write());

        }
        public void Leave(ClientSession session)
        {
          //  lock (_lock)
          //  {
                //플레이어 제고하고
                _sessions.Remove(session);
            //  }

            //모두에게 알린다
            S2C_BroadcastLeavGame leave = new S2C_BroadcastLeavGame();
            leave.playerId = session.SessionId;
            Broadcast(leave.Write());
        }
        public void Move(ClientSession session,C2S_Move packet)
        {
            //좌표 바궈주고
            session.PosX = packet.posX;
            session.PosY = packet.posY;
            session.PosX = packet.posX;

            //모두에게 알린다
            S2C_BroadcastMove move = new S2C_BroadcastMove();
            move.playerId = session.SessionId;
            move.posX = session.PosX;
            move.posY = session.PosY;
            move.posX = session.PosX;
            Broadcast(move.Write());
        }
            public void Push(Action job)
        {
            _JobQueue.Push(job);
        }
    }
}
