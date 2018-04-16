using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LockerBox
{
    public partial class MainForm : Form
    {
        private static readonly string CONNECT = "Connect";
        private static readonly string DISCONNECT = "Disconnect";
        private Transport _transport;
        private bool _transportIsAlive;

        public MainForm()
        {
            _transportIsAlive = false;

            InitializeComponent();

            _transport = new Transport();
            _transport.Received += received;
            _transport.Connected += connectionStatus;
            _transport.Failed += connectionStatus;
            _transport.StateChanged += stateChanged;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _transport.Received -= received;
            _transport.Connected -= connectionStatus;
            _transport.Failed -= connectionStatus;
            _transport.StateChanged -= stateChanged;
            _transport.Disconnect();

            base.OnClosing(e);
        }

        private void connectButtonClick(object sender, EventArgs e)
        {
            _transportIsAlive = !_transportIsAlive;
            this.Invoke(new Action(() => connectButton.Text = !_transportIsAlive ? CONNECT : DISCONNECT));
            if (_transportIsAlive)
            {
                _transport.Connect();
            }
            else
            {
                _transport.Disconnect();
            }
        }

        private void connectionStatus(object sender, LinkEventArgs e)
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


        private void received(object sender, ReceiveEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            if (e.SourcePackage.Data != null)
            {
                foreach (byte item in e.SourcePackage.Data)
                {
                    builder.Append(item);
                }
            }
            else
            {
                builder.Append(Convert.ToString(e.SourcePackage.Cmd));
            }
            addMessage("{0}", builder);
        }

        private void stateChanged(object sender, StateEventArgs e)
        {
            Invoke(new Action(() => powerLabel.Text = string.Format("Power mode: {0}", e.PowerModeState)));
            Invoke(new Action(() => chargeLabel.Text = string.Format("Charge level: {0}", e.ChargeLevelState)));
        }

    }
}
