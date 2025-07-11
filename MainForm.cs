using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (settings.UpdateBeforeStart)
                RunUpdateScript();

            StartServer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopServer();

            if (settings.UpdateOnStop)
                RunUpdateScript();
        }

        private void btnRestartUpdate_Click(object sender, EventArgs e)
        {
            StopServer();
            RunUpdateScript();
            StartServer();
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
                    settings.LastProcessId = serverProcess != null ? serverProcess.Id : 0;
                    SaveSettings();
                    Log($"[INFO] Server started with PID {proc.Id}");
                }
                else
                {
                    MessageBox.Show("Failed to start the server.");
                    Log("[ERROR] Failed to start server.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting server:\n{ex.Message}");
                Log($"[ERROR] StartServer: {ex}");
            }

            UpdateStatusLabel();
        }

        private void StopServer()
        {
            if (serverProcess == null || serverProcess.HasExited)
            {
                MessageBox.Show("Server is not running.");
                UpdateStatusLabel();
                return;
            }

            try
            {
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

                Log($"[INFO] Server stopped.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping server:\n{ex.Message}");
                Log($"[ERROR] StopServer: {ex}");
            }

            UpdateStatusLabel();
        }

        private void RunUpdateScript()
        {
            if (!string.IsNullOrWhiteSpace(settings.UpdateScriptPath) && File.Exists(settings.UpdateScriptPath))
            {
                try
                {
                    var update = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/C \"{settings.UpdateScriptPath}\"",
                            UseShellExecute = false,
                            CreateNoWindow = false
                        }
                    };
                    update.Start();
                    update.WaitForExit();
                    Log("[INFO] Update script completed.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error running update script:\n{ex.Message}");
                    Log($"[ERROR] RunUpdateScript: {ex}");
                }
            }
            else
            {
                Log("[WARN] Update script path not set or missing.");
            }
        }

        private void RestartAndUpdate()
        {
            StopServer();
            RunUpdateScript();
            StartServer();
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
                UpdateStatusLabel();
            };
            dailyTimer.Start();
        }

        private void LoadSettings()
        {
            if (File.Exists(SettingsFile))
            {
                var json = File.ReadAllText(SettingsFile);
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

            try
            {
                if (settings.LastProcessId > 0)
                {
                    serverProcess = Process.GetProcessById(settings.LastProcessId);
                    if (serverProcess.HasExited)
                    {
                        serverProcess = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"[WARN] Failed to get process by ID: {ex.Message}");
                serverProcess = null;
                var exeName = Path.GetFileNameWithoutExtension(settings.ExePath);
                var candidates = Process.GetProcessesByName(exeName);
                if (candidates.Length > 0)
                {
                    serverProcess = candidates[0];
                    settings.LastProcessId = serverProcess.Id;
                    Log($"[INFO] Fallback: found running process {exeName} with PID {serverProcess.Id}");
                }
                else
                {
                    Log("[INFO] No matching process found on fallback.");
                }
            }

            UpdateStatusLabel();
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
            settings.LastProcessId = serverProcess != null ? serverProcess.Id : 0;

            var json = System.Text.Json.JsonSerializer.Serialize(settings);
            File.WriteAllText(SettingsFile, json);
        }

        private void UpdateStatusLabel()
        {
            if (lblStatus == null) return;
            if (serverProcess != null && !serverProcess.HasExited)
            {
                lblStatus.Text = $"Status: Running (PID {serverProcess.Id})";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblStatus.Text = "Status: Not running";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void Log(string message)
        {
            try
            {
                string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                File.AppendAllText("log.txt", logLine);
            }
            catch { /* ignore logging errors */ }
        }
    }
}
