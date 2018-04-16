using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LockerBox
{
    enum ChargeLevelStates : byte
    {
        High = 103,
        Low = 66
    }

    enum PowerModeStates : byte
    {
        Normal = 103,
        Emergency = 66
    }

    internal class StateEventArgs : EventArgs
    {
        internal string ChargeLevelState { get; private set; }
        internal string PowerModeState { get; private set; }

        internal StateEventArgs(IEnumerable<byte> data)
        {
            PowerModeState = data.First() == (byte)PowerModeStates.Emergency ? PowerModeStates.Emergency.ToString() : PowerModeStates.Normal.ToString();
            ChargeLevelState = data.Last() == (byte)ChargeLevelStates.Low ? ChargeLevelStates.Low.ToString() : ChargeLevelStates.High.ToString();
        }
    }
}
