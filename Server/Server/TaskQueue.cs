using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface ITask
    {
        void Execute();
    }
    class BroadCastTask : ITask
    {
        GameRoom _room;
        ClientSession _session;
        string _chat;

        BroadCastTask(GameRoom room,ClientSession session,string chat)
        {
            _room = room;
           _session = session;
            _chat = chat;  
        }
        public void Execute()
        {
           _room.Broadcast(_session, _chat);
        }
    }
    class TaskQueue//잡큐대신 이렇게 클래스 만들어서 쓸수 잇지만 람다로 쓰는게 더 편해서 안쓴다
    {
        Queue<ITask> _queue=new Queue<ITask>();
    }
}
