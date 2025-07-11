using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

namespace GameServerHelper
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);

        delegate bool ConsoleCtrlDelegate(uint ctrlType);

        const uint CTRL_C_EVENT = 0;
        private Process serverProcess;
        private System.Windows.Forms.Timer dailyTimer;
        private AppSettings settings;
        private const string SettingsFile = "settings.json";

        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
            SetupTimer();
        }

        private void LoadSettings()
        {
            if (File.Exists(SettingsFile))
            {
                var json = File.ReadAllText(SettingsFile);
                settings = JsonSerializer.Deserialize<AppSettings>(json);
            }
            else
            {
                settings = new AppSettings();
            }

            txtExePath.Text = settings.ExePath;
            txtArguments.Text = settings.Arguments;
            txtUpdateScript.Text = settings.UpdateScriptPath;
        }

        private void SaveSettings()
        {
            settings.ExePath = txtExePath.Text;
            settings.Arguments = txtArguments.Text;
            settings.UpdateScriptPath = txtUpdateScript.Text;

            var json = JsonSerializer.Serialize(settings);
            File.WriteAllText(SettingsFile, json);
        }

        private void SetupTimer()
        {
            dailyTimer = new System.Windows.Forms.Timer { Interval = 60 * 1000 };
            dailyTimer.Tick += (s, e) =>
            {
                var now = DateTime.Now;
                if (now.Hour == 3 && now.Minute == 0)
                {
                    RestartAndUpdate();
                }
            };
            dailyTimer.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartServer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void btnRestartUpdate_Click(object sender, EventArgs e)
        {
            RestartAndUpdate();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
            MessageBox.Show("Settings saved.");
        }

        private void StartServer()
        {
            if (serverProcess != null && !serverProcess.HasExited)
            {
                MessageBox.Show("Server is already running.");
                return;
            }

            try
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = settings.ExePath,
                        Arguments = settings.Arguments,
                        UseShellExecute = false,
                        CreateNoWindow = false
                    }
                };

                if (!File.Exists(settings.ExePath))
                {
                    MessageBox.Show("The executable path is invalid or missing.");
                    return;
                }

                if (proc.Start())
                {
                    serverProcess = proc;
                }
                else
                {
                    MessageBox.Show("Failed to start the server.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting server:\n{ex.Message}");
            }
        }


        private void StopServer()
        {
            if (serverProcess == null || serverProcess.HasExited)
            {
                MessageBox.Show("Server is not running.");
                return;
            }

            SetConsoleCtrlHandler(null, true);
            AttachConsole((uint)serverProcess.Id);
            GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0);
            Thread.Sleep(1000);
            FreeConsole();
            SetConsoleCtrlHandler(null, false);

            serverProcess.WaitForExit(10000);
            if (!serverProcess.HasExited)
            {
                serverProcess.Kill();
            }
        }

        private void RestartAndUpdate()
        {
            StopServer();

            if (File.Exists(settings.UpdateScriptPath))
            {
                var update = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = settings.UpdateScriptPath,
                        UseShellExecute = true
                    }
                };
                update.Start();
                update.WaitForExit();
            }

            StartServer();
        }
    }

    public class AppSettings
    {
        public string ExePath { get; set; }
        public string Arguments { get; set; }
        public string UpdateScriptPath { get; set; }
    }
}
