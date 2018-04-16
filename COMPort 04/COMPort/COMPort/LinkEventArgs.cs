using System;

namespace COMPort
{
    internal class LinkEventArgs : EventArgs
    {
        internal string Message { get; private set; }

        internal LinkEventArgs(string message)
        {
            Message = message;
        }
    }
}
