using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LockerBox
{
    internal class Employee
    {
        internal byte[] Rfid { get; private set ; }
        internal byte[] Name { get; private set; }
        internal byte[] Info { get; private set; }
        internal Label[] Labels { get; private set; }
        internal byte[] Worktime { get; private set; }


        internal Employee(ulong rfid, string name, string worktime, string info, params Label[] labels)
        {
            Rfid = new byte[5];
            Array.Copy(BitConverter.GetBytes(rfid), 0, Rfid, 0, 5);
            Name = new byte[64];
            Array.Copy(Encoding.ASCII.GetBytes(name), 0, Name, 0, name.Length);
            Worktime = Encoding.ASCII.GetBytes(worktime);
            Info = new byte[128];
            Array.Copy(Encoding.ASCII.GetBytes(info), 0, Info, 0, info.Length);
            Labels = labels;
        }
    }
}
