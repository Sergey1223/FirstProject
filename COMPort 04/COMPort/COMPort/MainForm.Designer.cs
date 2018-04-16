namespace COMPort
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.startStopButton = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.listBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.listBoxContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // startStopButton
            // 
            this.startStopButton.Enabled = false;
            this.startStopButton.Location = new System.Drawing.Point(12, 9);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(75, 23);
            this.startStopButton.TabIndex = 1;
            this.startStopButton.Text = "Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // listBox
            // 
            this.listBox.ContextMenuStrip = this.listBoxContextMenuStrip;
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(12, 38);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(357, 212);
            this.listBox.TabIndex = 2;
            // 
            // listBoxContextMenuStrip
            // 
            this.listBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.listBoxContextMenuStrip.Name = "listBoxContextMenuStrip";
            this.listBoxContextMenuStrip.Size = new System.Drawing.Size(153, 48);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.cleanToolStripMenuItemClick);
            // 
            // portComboBox
            // 
            this.portComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(248, 11);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(121, 21);
            this.portComboBox.TabIndex = 3;
            this.portComboBox.SelectedIndexChanged += new System.EventHandler(this.portComboBoxSelectedIndexChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(184, 14);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(61, 13);
            this.portLabel.TabIndex = 4;
            this.portLabel.Text = "Select port:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 262);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.portComboBox);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.startStopButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.listBoxContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.ContextMenuStrip listBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    }
}

