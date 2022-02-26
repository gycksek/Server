using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    struct JobTImerElem : IComparable<JobTImerElem>
    {
        public int execTick;
        public Action action;


        public int CompareTo(JobTImerElem other)
        {
           return other.execTick -execTick;
        }
    }

    internal class JobTimer
    {
        PriorityQueue<JobTImerElem> _pq = new PriorityQueue<JobTImerElem>();
        object _lock = new object();

        public static JobTimer Instance{get;}=new JobTimer();

        public void Push(Action action, int tickAfter=0)
        {
            JobTImerElem job;
            job.execTick=System.Environment.TickCount+ tickAfter;
            job.action=action;

            lock(_lock)
            {
                _pq.Push(job);
            }
        }
        public void Flush()
        {
            while (true)
            {
                int now = System.Environment.TickCount;
                JobTImerElem job;

                lock (_lock)
                {
                    if (_pq.Count == 0)
                        break;

                    job = _pq.Peek();
                    if (job.execTick > now)
                        break;

                    _pq.Pop();

                }
                job.action.Invoke();
            }
        }
    }
}
