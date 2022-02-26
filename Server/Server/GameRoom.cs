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



        public void Enter(ClientSession session)
        {
           // lock(_lock)
          //  {
                _sessions.Add(session);
                session.Room = this;
          //  }

        }
        public void Leave(ClientSession session)
        {
          //  lock (_lock)
          //  {
                _sessions.Remove(session);
          //  }
        }

        public void Push(Action job)
        {
            _JobQueue.Push(job);
        }
    }
}
