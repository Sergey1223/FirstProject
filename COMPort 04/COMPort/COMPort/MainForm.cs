using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace COMPort
{
    internal partial class MainForm : Form
    {
        private Transport _transport;
        private static readonly string START = "Start";
        private static readonly string STOP = "Stop";
        private bool _transportIsAlive;

        internal MainForm()
        {
            _transportIsAlive = false;

            InitializeComponent();

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            if (portNames.Length != 0)
            {
                portComboBox.Items.AddRange(portNames);
                portComboBox.SelectedIndex = 0;
            }
            
            _transport = new Transport();
            _transport.Read += portRead;
            _transport.Connected += portStatus;
            _transport.Failed += portStatus;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _transport.Read -= portRead;
            _transport.Connected -= portStatus;
            _transport.Failed -= portStatus;
            _transport.Stop();

            base.OnClosing(e);
        }

        private void buttonClick(object sender, EventArgs e)
        {
            _transportIsAlive = !_transportIsAlive;
            this.Invoke(new Action(() => startStopButton.Text = _transportIsAlive ? STOP : START));
            portComboBox.Enabled = !_transportIsAlive;
            if (_transportIsAlive)
            {
                _transport.Start(portComboBox.SelectedItem as string);
            }
            else
            {
                _transport.Stop();
            }
        }

        private void portRead(object sender, CardEventArgs e)
        {
            addMessage("Type of card: {0}", e.SourcePackage.CardType);
            addMessage("Service data: {0}", e.SourcePackage.ServiceData);
            addMessage("Facility: {0}", 
                (e.SourcePackage.Facility.HasValue) ? e.SourcePackage.Facility.Value.ToString() : "N/D");
            addMessage("Number: {0}", e.SourcePackage.Number);
            addMessage("Hex number: {0}", 
                e.SourcePackage.HNumber.HasValue ? Convert.ToString(e.SourcePackage.HNumber.Value, 16) : "N/D");
        }

        private void portStatus(object sender, LinkEventArgs e)
        {
            addMessage(e.Message);
        }

        private void addMessage(string format, params object[] messages)
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.AppendFormat(format, messages);
            sBuilder.AppendLine();

            listBox.Invoke(new Action(() => listBox.Items.Add(sBuilder.ToString())));
        }

        private void portComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            startStopButton.Enabled = true;
        }

        private void cleanToolStripMenuItemClick(object sender, EventArgs e)
        {
            listBox.Items.Clear();
        }


    }
}
