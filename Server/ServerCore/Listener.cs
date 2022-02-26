using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    public  class Listener
    {
        Socket? _listenSocket;
        Action<Socket>? _onAcceptHandler;
        Func<Session> _sessionFactory;

        public void Init(IPEndPoint endPoint, /*Action<Socket> OnAcceptHandler*/Func<Session> sessionFactory,int register=10,int backlog=100)
        {
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //_onAcceptHandler = OnAcceptHandler;
            _sessionFactory += sessionFactory;

            _listenSocket.Bind(endPoint);
            _listenSocket.Listen(backlog);

            for (int i = 0; i < register; i++)//문지기를  늘려준다
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
                RegisterAccept(args);
            }
        }

        void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;

           bool pending= _listenSocket.AcceptAsync(args);
            if (pending == false)
                OnAcceptCompleted(null,args);
        }
        void OnAcceptCompleted(object sender,SocketAsyncEventArgs args)
        {
            if(args.SocketError==SocketError.Success)
            {
                //GameSession session = new GameSession();
                Session session = _sessionFactory.Invoke();
                session.Start(args.AcceptSocket);
                session.OnConnected(args.AcceptSocket.RemoteEndPoint);
                //_onAcceptHandler.Invoke(args.AcceptSocket);
            }
            else
                Console.WriteLine(args.SocketError.ToString());

            RegisterAccept(args);
        }

        //public Socket Accept()
        //{
        //    //_listenSocket.AcceptAsync();

        //    return _listenSocket.Accept();
        //}
    }
}
