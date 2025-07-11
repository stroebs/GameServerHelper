// MainForm.Designer.cs
namespace GameServerHelper
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.TextBox txtUpdateScript;
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRestartUpdate = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.lblExePath = new System.Windows.Forms.Label();
            this.lblArguments = new System.Windows.Forms.Label();
            this.lblUpdateScript = new System.Windows.Forms.Label();
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

            this.btnStart.Text = "Start Server";
            this.btnStart.Location = new System.Drawing.Point(15, 110);
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            this.btnStop.Text = "Stop Server";
            this.btnStop.Location = new System.Drawing.Point(120, 110);
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            this.btnRestartUpdate.Text = "Restart & Update Now";
            this.btnRestartUpdate.Location = new System.Drawing.Point(225, 110);
            this.btnRestartUpdate.Click += new System.EventHandler(this.btnRestartUpdate_Click);

            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.Location = new System.Drawing.Point(390, 110);
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);

            this.ClientSize = new System.Drawing.Size(550, 160);
            this.Controls.Add(this.lblExePath);
            this.Controls.Add(this.txtExePath);
            this.Controls.Add(this.lblArguments);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.lblUpdateScript);
            this.Controls.Add(this.txtUpdateScript);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRestartUpdate);
            this.Controls.Add(this.btnSaveSettings);
            this.Text = "Game Server Helper";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
