namespace LockerBox
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
            this.connectButton = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.powerLabel = new System.Windows.Forms.Label();
            this.chargeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 25);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButtonClick);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(12, 54);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(295, 121);
            this.listBox.TabIndex = 3;
            // 
            // powerLabel
            // 
            this.powerLabel.AutoSize = true;
            this.powerLabel.Location = new System.Drawing.Point(171, 13);
            this.powerLabel.Name = "powerLabel";
            this.powerLabel.Size = new System.Drawing.Size(35, 13);
            this.powerLabel.TabIndex = 4;
            this.powerLabel.Text = "State:";
            // 
            // chargeLabel
            // 
            this.chargeLabel.AutoSize = true;
            this.chargeLabel.Location = new System.Drawing.Point(171, 30);
            this.chargeLabel.Name = "chargeLabel";
            this.chargeLabel.Size = new System.Drawing.Size(35, 13);
            this.chargeLabel.TabIndex = 5;
            this.chargeLabel.Text = "State:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 261);
            this.Controls.Add(this.chargeLabel);
            this.Controls.Add(this.powerLabel);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.connectButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Label powerLabel;
        private System.Windows.Forms.Label chargeLabel;
    }
}

