using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace LockerBox
{
    internal class Transport
    {
        private static readonly IPAddress IPADDRESS = IPAddress.Parse("127.0.0.1");
        private const int TIMEOUT = 5000;
        private const int PORT = 7776;

        internal event EventHandler<ReceiveEventArgs> Received;
        internal event EventHandler<LinkEventArgs> Failed;
        internal event EventHandler<LinkEventArgs> Connected;
        internal event EventHandler<StateEventArgs> StateChanged;

        private Socket _socket;
        private Thread _thread;
        private bool _isALive;
        private ushort _state;

        internal Transport()
        {
            _socket = new Socket(IPADDRESS.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _socket.ReceiveTimeout = TIMEOUT;
            _thread = null;
            _isALive = false;
            _state = 0;
        }

        private void run()
        {
            while (_isALive)
            {
                if (!connection())
                {
                    break;
                }

                bool result = session();

                if (result)
                {
                    break;
                }
            }
        }

        internal void Connect()
        {
            Disconnect();

            _isALive = true;
            _thread = new Thread(run);
            _thread.Start();
        }

        internal void Disconnect()
        {
            _isALive = false;

            if (_thread != null)
            {
                _thread.Join(1000);
                _thread = null;
            }
        }

        private bool trySendAndReceive(Package raw, out Package result)
        {
            try
            {
                _socket.Send(raw.ToBytes());
                if (tryReceive(out result))
                {
                    return true;
                }
                return false;
            }
            catch (SocketException)
            {
                result = null;
                return false;
            }
        }

        private bool connection()
        {
            bool connected = false;
            while (_isALive)
            {
                try
                {
                    _socket.Connect(IPADDRESS, PORT);
                    Package package;
                    if (trySendAndReceive(new Package(0, Convert.ToByte('v'), null),  out package))
                    {
                        Connected.Raise(this, new LinkEventArgs(String.Format("The connection is established ({0}). Version: {1}", PORT, string.Join(".", package.Data.Skip(4).Take(4)))));
                        //Received.Raise(this, new ReceiveEventArgs(package));
                        return true;
                    }
                    return false;
                }
                catch (SocketException)
                {
                    Thread.Sleep(1000);
                    if (!connected)
                    {
                        connected = true;
                        Connected.Raise(this, new LinkEventArgs(String.Format("The connection is failed ({0})", PORT)));
                        return true;
                    }
                }
            }
            return false;
        }

        private bool session()
        {
            Package result;
            Package stateResult;
            while (_isALive)
            {
                int start = Environment.TickCount;
                while(Environment.TickCount - start < 1500)
                {
                    if (_socket.Available != 0)
                    {
                        if (tryReceive(out result))
                        {
                            Received.Raise(this, new ReceiveEventArgs(result));
                        }
                    }
                    Thread.Sleep(100);
                }
                if (trySendAndReceive(new Package(3, Convert.ToByte('h'), null), out stateResult))
                {
                    if(BitConverter.ToInt16(stateResult.Data.ToArray(), 0) != _state)
                    {
                        StateChanged.Raise(this, new StateEventArgs(stateResult.Data));
                        _state = (ushort)BitConverter.ToInt16(stateResult.Data.ToArray(), 0);
                    }
                }
            }
            return true;
        }

        private bool tryReceive(out Package result)
        {
            try
            {
                int start = Environment.TickCount;
                int available;
                byte[] buffer;
                while (Environment.TickCount - start < 5000)
                {
                    available = _socket.Available;
                    if(available != 0)
                    {
                        buffer = new byte[available];
                        _socket.Receive(buffer);
                        if (Package.TryParse(buffer, out result))
                        {
                            return true;
                        }
                        return false;
                    }
                }
                result = null;
                return false;
            }
            catch (SocketException)
            {
                result = null;
                return false;
            }
        }
    }
}
