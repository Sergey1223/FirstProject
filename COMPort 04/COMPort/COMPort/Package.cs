using System.Globalization;
using System.Text.RegularExpressions;

namespace COMPort
{
    internal class Package
    {
        private static Regex _packageParcerRegEx = new Regex(@"(?<cType>HID)\[(?<sData>[0-9A-F]+)\] (?<num>\d{1,5})|(?<cType>Em-Marine)\[(?<sData>[0-9A-F]+)\] (?<fac>\d{1,3}),(?<num>\d{1,5})");

        internal byte? Facility { get; private set; }
        internal ushort Number { get; private set; }
        internal uint? HNumber { get; private set; }
        internal string CardType { get; private set; }
        internal uint ServiceData { get; private set; }

        private Package(byte? facility, ushort number, uint? hnumber, string cardType, uint serviceData)
        {
            Facility = facility;
            Number  = number;
            HNumber = hnumber;
            CardType = cardType;
            ServiceData = serviceData;
        }

        internal static bool TryParse(string raw, out Package result)
        {
            result = null;
            Match match = _packageParcerRegEx.Match(raw);
            
            if (!match.Success)
            {
                return false;
            }

            ushort number;
            uint serviceData;
            if (!ushort.TryParse(match.Groups["num"].Value, out number) ||
                !uint.TryParse(match.Groups["sData"].Value, NumberStyles.HexNumber, null, out serviceData))
            {
                return false;
            }

            byte facility;
            if (match.Groups["cType"].Value == "HID")
            {
                result = new Package(null, number, null, match.Groups["cType"].Value, serviceData);
                return true;
            }
            else if (match.Groups["cType"].Value == "Em-Marine" &&
                byte.TryParse(match.Groups["fac"].Value, out facility))
            {
                result = new Package(facility, number, (uint)(facility << 16) | number, match.Groups["cType"].Value, serviceData);
                return true;
            }

            return false;
        }
    }
}
