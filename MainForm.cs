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
                    MessageBox.Show("Server started successfully.");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping server:\n{ex.Message}");
            }
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
                            CreateNoWindow = false,
                            RedirectStandardOutput = false,
                            RedirectStandardError = false
                        }
                    };

                    update.Start();
                    update.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error running update script:\n{ex.Message}");
                }
            }
        }


        private void RestartAndUpdate()
        {
            StopServer();
            RunUpdateScript();
            StartServer();
        }
    }
}
