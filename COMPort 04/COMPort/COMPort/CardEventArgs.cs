using System;

namespace COMPort
{
    internal class CardEventArgs : EventArgs
    {
        internal Package SourcePackage { get; private set; }

        internal CardEventArgs(Package package)
        {
            SourcePackage = package;
        }
    }
}
