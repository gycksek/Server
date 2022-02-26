﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    public abstract class PacketSession: Session
    {
        public static readonly int HeaderSize = 2;
        public sealed override int OnRecv(ArraySegment<byte> buffer)
        {
            int processLen = 0;
            int packetCount = 0;

            while(true)
            {
                //최소한 헤더는 파싱할수있는지 확인
                if (buffer.Count < HeaderSize)
                    break;

                //패킷이 완전체로 도착했는지 확인
                ushort dataSize = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
                if (buffer.Count < dataSize)
                    break;

                //여기까지 왔으면 패킷 조립 가능
                OnRecvPacket(new ArraySegment<byte>(buffer.Array,buffer.Offset,dataSize));
                packetCount++;
                processLen += dataSize;
                buffer = new ArraySegment<byte>(buffer.Array, buffer.Offset + dataSize,buffer.Count-dataSize);
            }

            if(packetCount>1)
                Console.WriteLine($"패킷 모아보내기{packetCount}");

            return processLen;
        }
        public abstract void OnRecvPacket(ArraySegment<byte> buffer);

    }

    public abstract class Session
    {
        Socket _socket;
        int _disconnect = 0;
        RecvBuffer _recvBuffer = new RecvBuffer(65535);

        object _lock = new object();
        //Queue<byte[]> _sendQueue = new Queue<byte[]>();
        Queue<ArraySegment<byte>> _sendQueue = new Queue<ArraySegment<byte>>();
        //bool _pending = false;
        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();

        SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();
        SocketAsyncEventArgs _recvArgs = new SocketAsyncEventArgs();

        public abstract void OnConnected(EndPoint endPoint);
        public abstract int OnRecv(ArraySegment<byte> buffer);
        public abstract void OnSend(int numOfBytes);
        public abstract void OnDisconnected(EndPoint endPoint);


        void Clear()
        {
            lock(_lock)
            {
                _sendQueue.Clear();
                _pendingList.Clear();
            }
        }

        public void Start(Socket socket)
        {
            _socket = socket;
            // SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
            _recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);

           // _recvArgs.SetBuffer(new byte[1024],0,1024);

            _sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);

            RegisterRecv(/*_recvArgs*/);
        }
        public void Send(/*byte[] sendBuff*/ArraySegment<byte>  sendBuff)
        {
            lock(_lock)
            {
                _sendQueue.Enqueue(sendBuff);
                //if (_pending == false)
               if (_pendingList.Count == 0)
                   RegisterSend();
                //_socket.Send(sendBuff);
                //SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
                //sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);
                //_sendArgs.SetBuffer(sendBuff, 0, sendBuff.Length);

                //RegisterSend(/*_sendArgs*/);
            }

        }
        public void Send(/*byte[] sendBuff*/List<ArraySegment<byte>> sendBuffList)
        {
            if (sendBuffList.Count == 0)
                return;

            lock (_lock)
            {
                foreach (ArraySegment<byte> sendBuff in sendBuffList)
                    _sendQueue.Enqueue(sendBuff);

                //_sendQueue.Enqueue(sendBuff);
                //if (_pending == false)
                if (_pendingList.Count == 0)
                    RegisterSend();
                //_socket.Send(sendBuff);
                //SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
                //sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);
                //_sendArgs.SetBuffer(sendBuff, 0, sendBuff.Length);

                //RegisterSend(/*_sendArgs*/);
            }

        }
        public void Disconnect()
        {
            if (Interlocked.Exchange(ref _disconnect, 1) == 1)
                return;

            OnDisconnected(_socket.RemoteEndPoint);

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            Clear();
        }
        #region 네트워크 통신
        void RegisterSend(/*SocketAsyncEventArgs args*/)
        {
            if (_disconnect == 1)
                return;

            //_pending = true;
            // List<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
            _pendingList.Clear();
            while(_sendQueue.Count>0)
            {
               // byte[] buff = _sendQueue.Dequeue();
                ArraySegment<byte> buff = _sendQueue.Dequeue();
                //_pendingList.Add(new ArraySegment<byte>(buff, 0, buff.Length));
                _pendingList.Add(buff);
            }

            _sendArgs.BufferList = _pendingList;
           // byte[] buff = _sendQueue.Dequeue();
            //_sendArgs.SetBuffer(buff, 0, buff.Length);

            try
            {
                bool pending = _socket.SendAsync(_sendArgs);
                if (pending == false)
                    OnSendCompleted(null, _sendArgs);
            }
            catch(Exception e)
            {
                Console.WriteLine($"RegisterSend Failed{e}");
            }

        }
        void OnSendCompleted(object sender,SocketAsyncEventArgs args)
        {

             lock(_lock)
            {
                if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
                {
                    try
                    {
                        _sendArgs.BufferList = null;
                        _pendingList.Clear();

                        OnSend(_sendArgs.BytesTransferred);

                       // Console.WriteLine($"Trandsferred bytes: {_sendArgs.BytesTransferred}");
                        if(_sendQueue.Count>0)
                        {
                            RegisterSend();
                        }
                        //else
                        // _pending = false;


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"OnSendCompleted Fail{e}");
                    }

                }
                else
                {
                    Disconnect();
                }
            }
    
        }

        void RegisterRecv(/*SocketAsyncEventArgs args*/)
        {
            if (_disconnect == 1)
                return;

            _recvBuffer.Clean();
            ArraySegment<byte> segment = _recvBuffer.Writegment;
            _recvArgs.SetBuffer(segment.Array, segment.Offset, segment.Count);

            try
            {
                bool pending = _socket.ReceiveAsync(_recvArgs);
                if (pending == false)
                    OnRecvCompleted(null, _recvArgs);
            }
            catch(Exception e)
            {
                Console.WriteLine($"RegisterRecv Failed {e}");
            }


        }
        void OnRecvCompleted(object sender,SocketAsyncEventArgs args)
        {
            if(args.BytesTransferred>0 && args.SocketError==SocketError.Success)
            {
                try
                {
                    //write 커서 이동
                    if(_recvBuffer.OnWrite(args.BytesTransferred)==false)
                    {
                        Disconnect();
                        return;
                    }


                   // OnRecv(new ArraySegment<byte>(args.Buffer, args.Offset, args.BytesTransferred));
                   //컨텐츠쪽으로 데이터를 넘겨주고 얼마나 처리했는지 받는다
                    int processLen=OnRecv(_recvBuffer.ReadSegment);
                    if(processLen<0 || _recvBuffer.DataSize<processLen)
                    { Disconnect();return; }   
                    
                    //Read커서 이동
                    if(_recvBuffer.OnRead(processLen)==false)
                    {
                        Disconnect();
                        return;
                    }

                    //string recvData = Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred); ;
                    //Console.WriteLine($"[From Client] : {recvData}");

                    RegisterRecv(/*args*/);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"OnRecvCompleted Fail{e}");
                }

         
            }
            else
            {
                Disconnect();
            }
        }
        #endregion
    }
}
