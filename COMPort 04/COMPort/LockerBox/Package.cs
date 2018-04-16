using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace LockerBox
{
    internal class Package
    {
        // 00 00 00 00 17 07 20 AA
        private static ReadOnlyCollection<byte> HEAD = new ReadOnlyCollection<byte>(Encoding.ASCII.GetBytes("LKL\n"));

        //private static Regex _packageParcerRegEx = new Regex(@"(?<head>) (?<addr>) (?<cmd>)");

        internal byte Address { get; private set; }
        internal byte Cmd { get; private set; }
        private List<byte> _data;
        internal IEnumerable<byte> Data { get { return _data;} }

        internal Package(byte addr, byte cmd, byte[] data)
        {
            Address = addr;
            Cmd = cmd;
            _data = new List<byte>();
            if (data != null)
            {
                _data.AddRange(data);
            }
        }

        internal static bool TryParse(byte[] raw, out Package result)
        {
            try
            {
                ushort length = (ushort)((raw[6] << 8) | raw[7]);
                //ushort length = (ushort)BitConverter.ToInt16(raw, 6);

                byte[] data = null;
                if (length != 0)
                {
                    data = new byte[length];
                    Array.Copy(raw, 8, data, 0, length);
                }
                result = new Package(raw[4], raw[5], data);
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                result = null;
                return false;
            }
        }

        internal byte[] ToBytes()
        {
            List<byte> result = new List<byte>(HEAD);
            result.Add(Address);
            result.Add(Cmd);
            result.Add(Data != null ? (byte)(_data.Count & 0xFF) : (byte)0);
            result.Add(Data != null ? (byte)(_data.Count >> 8) : (byte) 0);
            if (Data != null)
            {
                result.AddRange(Data.ToArray());
            }
            return result.ToArray();
        }
    }
}
