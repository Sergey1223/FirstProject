using System;

namespace LockerBox
{
    internal class ReceiveEventArgs : EventArgs
    {
        internal Package SourcePackage { get; private set; }

        internal ReceiveEventArgs(Package package)
        {
            SourcePackage = package;
        }
    }
}
