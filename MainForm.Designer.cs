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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtExePath = new System.Windows.Forms.TextBox();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.txtUpdateScript = new System.Windows.Forms.TextBox();
            this.numUpdateHour = new System.Windows.Forms.NumericUpDown();
            this.numUpdateMinute = new System.Windows.Forms.NumericUpDown();
            this.lblUpdateTime = new System.Windows.Forms.Label();
            this.chkUpdateBeforeStart = new System.Windows.Forms.CheckBox();
            this.chkUpdateOnStop = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRestartUpdate = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.lblExePath = new System.Windows.Forms.Label();
            this.lblArguments = new System.Windows.Forms.Label();
            this.lblUpdateScript = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateMinute)).BeginInit();
            this.SuspendLayout();

            this.lblExePath.Text = "Game Executable:";
            this.lblExePath.Location = new System.Drawing.Point(12, 15);
            this.lblExePath.Size = new System.Drawing.Size(100, 20);

            this.txtExePath.Location = new System.Drawing.Point(130, 12);
            this.txtExePath.Size = new System.Drawing.Size(400, 20);

            this.lblArguments.Text = "Arguments:";
            this.lblArguments.Location = new System.Drawing.Point(12, 45);
            this.lblArguments.Size = new System.Drawing.Size(100, 20);

            this.txtArguments.Location = new System.Drawing.Point(130, 42);
            this.txtArguments.Size = new System.Drawing.Size(400, 20);

            this.lblUpdateScript.Text = "Update Script:";
            this.lblUpdateScript.Location = new System.Drawing.Point(12, 75);
            this.lblUpdateScript.Size = new System.Drawing.Size(100, 20);

            this.txtUpdateScript.Location = new System.Drawing.Point(130, 72);
            this.txtUpdateScript.Size = new System.Drawing.Size(400, 20);

            this.lblUpdateTime.Text = "Update Time (HH:MM):";
            this.lblUpdateTime.Location = new System.Drawing.Point(12, 105);
            this.lblUpdateTime.Size = new System.Drawing.Size(120, 20);

            this.numUpdateHour.Location = new System.Drawing.Point(140, 102);
            this.numUpdateHour.Maximum = 23;
            this.numUpdateHour.Minimum = 0;
            this.numUpdateHour.Size = new System.Drawing.Size(50, 20);

            this.numUpdateMinute.Location = new System.Drawing.Point(200, 102);
            this.numUpdateMinute.Maximum = 59;
            this.numUpdateMinute.Minimum = 0;
            this.numUpdateMinute.Size = new System.Drawing.Size(50, 20);

            this.chkUpdateBeforeStart.Text = "Update Before Start";
            this.chkUpdateBeforeStart.Location = new System.Drawing.Point(270, 100);
            this.chkUpdateBeforeStart.Size = new System.Drawing.Size(130, 24);

            this.chkUpdateOnStop.Text = "Update On Stop";
            this.chkUpdateOnStop.Location = new System.Drawing.Point(400, 100);
            this.chkUpdateOnStop.Size = new System.Drawing.Size(130, 24);

            this.btnStart.Text = "Start Server";
            this.btnStart.Location = new System.Drawing.Point(15, 140);
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            this.btnStop.Text = "Stop Server";
            this.btnStop.Location = new System.Drawing.Point(120, 140);
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            this.btnRestartUpdate.Text = "Restart & Update Now";
            this.btnRestartUpdate.Location = new System.Drawing.Point(225, 140);
            this.btnRestartUpdate.Click += new System.EventHandler(this.btnRestartUpdate_Click);

            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.Location = new System.Drawing.Point(390, 140);
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);

            this.ClientSize = new System.Drawing.Size(550, 180);
            this.Controls.Add(this.lblExePath);
            this.Controls.Add(this.txtExePath);
            this.Controls.Add(this.lblArguments);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.lblUpdateScript);
            this.Controls.Add(this.txtUpdateScript);
            this.Controls.Add(this.lblUpdateTime);
            this.Controls.Add(this.numUpdateHour);
            this.Controls.Add(this.numUpdateMinute);
            this.Controls.Add(this.chkUpdateBeforeStart);
            this.Controls.Add(this.chkUpdateOnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRestartUpdate);
            this.Controls.Add(this.btnSaveSettings);
            this.Text = "Game Server Helper";
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateMinute)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SaveSettings()
        {
            settings.ExePath = txtExePath.Text;
            settings.Arguments = txtArguments.Text;
            settings.UpdateScriptPath = txtUpdateScript.Text;
            settings.UpdateHour = (int)numUpdateHour.Value;
            settings.UpdateMinute = (int)numUpdateMinute.Value;
            settings.UpdateBeforeStart = chkUpdateBeforeStart.Checked;
            settings.UpdateOnStop = chkUpdateOnStop.Checked;

            var json = System.Text.Json.JsonSerializer.Serialize(settings);
            System.IO.File.WriteAllText("settings.json", json);
        }

        private void LoadSettings()
        {
            if (System.IO.File.Exists("settings.json"))
            {
                var json = System.IO.File.ReadAllText("settings.json");
                settings = System.Text.Json.JsonSerializer.Deserialize<AppSettings>(json);
            }
            else
            {
                settings = new AppSettings();
            }

            txtExePath.Text = settings.ExePath;
            txtArguments.Text = settings.Arguments;
            txtUpdateScript.Text = settings.UpdateScriptPath;
            numUpdateHour.Value = settings.UpdateHour;
            numUpdateMinute.Value = settings.UpdateMinute;
            chkUpdateBeforeStart.Checked = settings.UpdateBeforeStart;
            chkUpdateOnStop.Checked = settings.UpdateOnStop;
        }

        private void SetupTimer()
        {
            dailyTimer = new System.Windows.Forms.Timer { Interval = 60 * 1000 };
            dailyTimer.Tick += (s, e) =>
            {
                var now = DateTime.Now;
                if (now.Hour == settings.UpdateHour && now.Minute == settings.UpdateMinute)
                {
                    RestartAndUpdate();
                }
            };
            dailyTimer.Start();
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
    }
}
