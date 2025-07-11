// MainForm.Designer.cs
namespace GameServerHelper
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.TextBox txtUpdateScript;
        private System.Windows.Forms.NumericUpDown numUpdateHour;
        private System.Windows.Forms.NumericUpDown numUpdateMinute;
        private System.Windows.Forms.Label lblUpdateTime;
        private System.Windows.Forms.CheckBox chkUpdateBeforeStart;
        private System.Windows.Forms.CheckBox chkUpdateOnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRestartUpdate;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Label lblExePath;
        private System.Windows.Forms.Label lblArguments;
        private System.Windows.Forms.Label lblUpdateScript;
        private System.Windows.Forms.Label lblStatus;

        private void InitializeComponent()
        {
            txtExePath = new TextBox();
            txtArguments = new TextBox();
            txtUpdateScript = new TextBox();
            numUpdateHour = new NumericUpDown();
            numUpdateMinute = new NumericUpDown();
            lblUpdateTime = new Label();
            chkUpdateBeforeStart = new CheckBox();
            chkUpdateOnStop = new CheckBox();
            btnStart = new Button();
            btnStop = new Button();
            btnRestartUpdate = new Button();
            btnSaveSettings = new Button();
            lblExePath = new Label();
            lblArguments = new Label();
            lblUpdateScript = new Label();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)numUpdateHour).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUpdateMinute).BeginInit();
            SuspendLayout();
            // 
            // txtExePath
            // 
            txtExePath.Location = new Point(130, 12);
            txtExePath.Name = "txtExePath";
            txtExePath.Size = new Size(400, 23);
            txtExePath.TabIndex = 1;
            // 
            // txtArguments
            // 
            txtArguments.Location = new Point(130, 42);
            txtArguments.Name = "txtArguments";
            txtArguments.Size = new Size(400, 23);
            txtArguments.TabIndex = 3;
            // 
            // txtUpdateScript
            // 
            txtUpdateScript.Location = new Point(130, 72);
            txtUpdateScript.Name = "txtUpdateScript";
            txtUpdateScript.Size = new Size(400, 23);
            txtUpdateScript.TabIndex = 5;
            // 
            // numUpdateHour
            // 
            numUpdateHour.Location = new Point(130, 102);
            numUpdateHour.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            numUpdateHour.Name = "numUpdateHour";
            numUpdateHour.Size = new Size(35, 23);
            numUpdateHour.TabIndex = 7;
            // 
            // numUpdateMinute
            // 
            numUpdateMinute.Location = new Point(171, 102);
            numUpdateMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            numUpdateMinute.Name = "numUpdateMinute";
            numUpdateMinute.Size = new Size(35, 23);
            numUpdateMinute.TabIndex = 8;
            // 
            // lblUpdateTime
            // 
            lblUpdateTime.Location = new Point(12, 105);
            lblUpdateTime.Name = "lblUpdateTime";
            lblUpdateTime.Size = new Size(100, 23);
            lblUpdateTime.TabIndex = 6;
            lblUpdateTime.Text = "Update Time (HH:MM):";
            // 
            // chkUpdateBeforeStart
            // 
            chkUpdateBeforeStart.Location = new Point(212, 100);
            chkUpdateBeforeStart.Name = "chkUpdateBeforeStart";
            chkUpdateBeforeStart.Size = new Size(134, 24);
            chkUpdateBeforeStart.TabIndex = 9;
            chkUpdateBeforeStart.Text = "Update Before Start";
            // 
            // chkUpdateOnStop
            // 
            chkUpdateOnStop.Location = new Point(352, 100);
            chkUpdateOnStop.Name = "chkUpdateOnStop";
            chkUpdateOnStop.Size = new Size(124, 24);
            chkUpdateOnStop.TabIndex = 10;
            chkUpdateOnStop.Text = "Update On Stop";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(15, 140);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 11;
            btnStart.Text = "Start Server";
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(96, 140);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 23);
            btnStop.TabIndex = 12;
            btnStop.Text = "Stop Server";
            btnStop.Click += btnStop_Click;
            // 
            // btnRestartUpdate
            // 
            btnRestartUpdate.Location = new Point(177, 140);
            btnRestartUpdate.Name = "btnRestartUpdate";
            btnRestartUpdate.Size = new Size(126, 23);
            btnRestartUpdate.TabIndex = 13;
            btnRestartUpdate.Text = "Restart && Update Now";
            btnRestartUpdate.Click += btnRestartUpdate_Click;
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.Location = new Point(455, 140);
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.Size = new Size(75, 23);
            btnSaveSettings.TabIndex = 14;
            btnSaveSettings.Text = "Save Settings";
            btnSaveSettings.Click += btnSaveSettings_Click;
            // 
            // lblExePath
            // 
            lblExePath.Location = new Point(12, 15);
            lblExePath.Name = "lblExePath";
            lblExePath.Size = new Size(100, 23);
            lblExePath.TabIndex = 0;
            lblExePath.Text = "Game Executable:";
            // 
            // lblArguments
            // 
            lblArguments.Location = new Point(12, 45);
            lblArguments.Name = "lblArguments";
            lblArguments.Size = new Size(100, 23);
            lblArguments.TabIndex = 2;
            lblArguments.Text = "Arguments:";
            // 
            // lblUpdateScript
            // 
            lblUpdateScript.Location = new Point(12, 75);
            lblUpdateScript.Name = "lblUpdateScript";
            lblUpdateScript.Size = new Size(100, 23);
            lblUpdateScript.TabIndex = 4;
            lblUpdateScript.Text = "Update Script:";
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(15, 180);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(500, 23);
            lblStatus.TabIndex = 15;
            lblStatus.Text = "Status: Unknown";
            // 
            // MainForm
            // 
            ClientSize = new Size(560, 220);
            Controls.Add(lblExePath);
            Controls.Add(txtExePath);
            Controls.Add(lblArguments);
            Controls.Add(txtArguments);
            Controls.Add(lblUpdateScript);
            Controls.Add(txtUpdateScript);
            Controls.Add(lblUpdateTime);
            Controls.Add(numUpdateHour);
            Controls.Add(numUpdateMinute);
            Controls.Add(chkUpdateBeforeStart);
            Controls.Add(chkUpdateOnStop);
            Controls.Add(btnStart);
            Controls.Add(btnStop);
            Controls.Add(btnRestartUpdate);
            Controls.Add(btnSaveSettings);
            Controls.Add(lblStatus);
            Name = "MainForm";
            Text = "Game Server Helper";
            ((System.ComponentModel.ISupportInitialize)numUpdateHour).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUpdateMinute).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }

    public class AppSettings
    {
        public string ExePath { get; set; }
        public string Arguments { get; set; }
        public string UpdateScriptPath { get; set; }
        public int UpdateHour { get; set; } = 3;
        public int UpdateMinute { get; set; } = 0;
        public bool UpdateBeforeStart { get; set; } = false;
        public bool UpdateOnStop { get; set; } = false;
        public int LastProcessId { get; set; } = 0;
    }
}
