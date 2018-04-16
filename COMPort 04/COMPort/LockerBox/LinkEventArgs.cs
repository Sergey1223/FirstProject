using System;

namespace LockerBox
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
