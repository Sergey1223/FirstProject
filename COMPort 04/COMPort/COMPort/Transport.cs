using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace COMPort
{
    internal class Transport
    {
        private SerialPort _port;
        private Thread _thread;
        private bool _isALive;

        internal event EventHandler<CardEventArgs> Read;
        internal event EventHandler<LinkEventArgs> Failed;
        internal event EventHandler<LinkEventArgs> Connected;

        internal Transport()
        {
            _port = new SerialPort();
            _thread = null;
            _isALive = false;
        }

        private void run()      //"Em-Marine[1A00] 198,40242\r"  // "Em-Marine[1A00] 198,40242\r\nNo card\r\n" // "HID[00100217] 15503\r\nNo card\r\n"
        {
            while (_isALive)
            {
                if (!connection())
                {
                    break;
                }

                bool result = session();

                if (_port.IsOpen)
                {
                    _port.Close();
                }

                if (result)
                {
                    break;
                }
            }
        }

        private bool tryRead(List<byte> result)
        {
            if (_port.BytesToRead == 0)
            {
                return false;
            }

            byte[] buffer = new byte[_port.BytesToRead];
            _port.Read(buffer, 0, buffer.Length);
            result.AddRange(buffer);
            return true;
        }

        internal void Start(string portName)
        {
            Stop();

            _port.PortName = portName;
            _isALive = true;
            _thread = new Thread(run);
            _thread.Start();
        }

        internal void Stop()
        {
            _isALive = false;

            if(_thread != null)
            {
                _thread.Join(1000);
                _thread = null;
            }
        }

        private bool connection()
        {
            bool connected = false;
            while(_isALive)
            {
                try
                {
                    _port.Open();
                    Connected.Raise(this, new LinkEventArgs(String.Format("{0} opened", _port.PortName)));
                    return true;
                }
                catch (IOException)
                {
                    Thread.Sleep(1000);
                    if(!connected)
                    {
                        connected = true;
                        Connected.Raise(this, new LinkEventArgs(String.Format("{0} failed", _port.PortName)));
                        return true;
                    }
                }
            }
            return false;
        }

        private bool session()
        {
            List<byte> buffer = new List<byte>();
            while (_isALive)
            {
                try
                {
                    if (_port.BytesToRead != 0)
                    {
                        if (tryRead(buffer))
                        {
                            if (buffer.IndexOf((byte)'\n') != -1)
                            {
                                System.Diagnostics.Trace.WriteLine(Encoding.ASCII.GetString(buffer.ToArray()));
                                Package package;
                                if (Package.TryParse(Encoding.ASCII.GetString(buffer.ToArray()), out package))
                                {
                                    Read.Raise<CardEventArgs>(this, new CardEventArgs(package));
                                }
                                buffer.RemoveRange(0, buffer.IndexOf((byte)'\n') + 1);
                            }
                        }

                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Failed.Raise(this, new LinkEventArgs(String.Format("Hardware error. The device is faulty or not connected ({0})", _port.PortName)));
                    return false;
                }
                catch (Exception e)
                {
                    Failed.Raise(this, new LinkEventArgs(e.Message));
                    return false;
                }
            }
            return true;
        }
    }
}
