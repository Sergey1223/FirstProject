using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LockerBox
{
    internal static class StringExtension
    {

        internal static byte[] ToBytes(this string raw)
        {
            List<byte> result = new List<byte>(Encoding.ASCII.GetBytes("LKL\n"));
            foreach (string item in raw.Split(new char[] { ' ' }))
            {
                result.AddRange(Encoding.ASCII.GetBytes(item));
            }
            return result.ToArray();
        }
    }
}
