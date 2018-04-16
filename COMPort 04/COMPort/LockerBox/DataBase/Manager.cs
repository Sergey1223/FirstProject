using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LockerBox
{

    internal class Manager
    {
        private Employee[] _employees;
        private Label[] _labels;

        internal Manager(int num)
        {
            _labels = new Label[3];
            _labels[0] = new Label(LabelsType.lab_1.ToString(), "Em-Marine", "Метка один");
            _labels[0] = new Label(LabelsType.lab_1.ToString(), "HID", "Метка два");
            _labels[0] = new Label(LabelsType.lab_1.ToString(), "HM", "Метка три");
            _employees = new Employee[3];
            _employees[0] = new Employee(14563, "Иванов Иван Иванович", "F", "Охранник", _labels[0], _labels[2]);
            _employees[0] = new Employee(1241454, "Петров Петр Петрович", "08:20-18:45", "Водитель", _labels[0], _labels[1], _labels[2]);
            _employees[0] = new Employee(3242667, "Федоров Федор Федорович", "16:20-18:45", "Уборщик", _labels[1]);
        }

        internal byte[] GetRfids()
        {
            List<byte> temp = new List<byte>();
            foreach (Employee item in _employees)
            {
                temp.AddRange(item.Rfid);
            }
            return temp.ToArray();
        }

        internal byte[] GetInfo(ulong rfid)
        {
            List<byte> temp = new List<byte>();
            foreach (Employee item in _employees)
            {
                if(rfid == BitConverter.ToUInt64(item.Rfid, 0))
                {
                    temp.AddRange(item.Name);
                    temp.AddRange(item.Info);
                    return temp.ToArray();
                }
            }
            return null;
        }

        internal byte[] GetLabels(ulong rfid)
        {
            foreach (Employee item in _employees)
            {
                if (rfid == BitConverter.ToUInt64(item.Rfid, 0))
                {
                    List<byte> result = new List<byte>();
                    foreach (Label label in item.Labels)
                    {
                        result.AddRange(label.Type);
                    }
                    return result.ToArray();
                }
            }
            return null;
        }

        internal byte[] GetWorktime(ulong rfid)
        {
            foreach (Employee item in _employees)
            {
                if (rfid == BitConverter.ToUInt64(item.Rfid, 0))
                {
                    return item.Worktime;
                }
            }
            return null;
        }

        internal byte[] GetLabelList()
        {
            List<byte> result = new List<byte>();
            foreach(Label label in _labels)
            {
                result.AddRange(label.Type);
            }
            return result.ToArray();
        }

        internal byte[] GetLabelInfo(byte[] type)
        {
            List<byte> result = new List<byte>();
            foreach (Label label in _labels)
            {
                result.AddRange(label.Type);
            }
            return result.ToArray();
        }
    }
}
