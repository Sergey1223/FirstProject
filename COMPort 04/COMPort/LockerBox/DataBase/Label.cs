using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LockerBox
{
    enum LabelsType
    {
        lab_1,
        lab_2,
        lab_3
    }

    internal class Label
    {
        internal byte[] Type { get; private set; }
        internal byte[] Name { get; private set; }
        internal byte[] Info { get; private set; }

        internal Label(string type, string name, string info)
        {
            Type = Encoding.ASCII.GetBytes(type);
            Name = new byte[64];
            Array.Copy(Encoding.ASCII.GetBytes(name).Reverse().ToArray(), 0, Name, 0, name.Length);
            Info = new byte[128];
            Array.Copy(Encoding.ASCII.GetBytes(info).Reverse().ToArray(), 0, Info, 0, info.Length);
        }
    }
}
