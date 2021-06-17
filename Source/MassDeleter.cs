using System;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using WindowsInput;

namespace DiscordMassDeleter
{
    internal enum Keys
    {
        nil,
        ArrowUP = WindowsInput.Native.VirtualKeyCode.UP,
        Backspace = WindowsInput.Native.VirtualKeyCode.BACK,
        Enter = WindowsInput.Native.VirtualKeyCode.RETURN,
    }

    public class MassDelete
    {
        public static string procname = "";
        private readonly static string log_file = "MassDeleteLog.txt";

        private static void GetProc()
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                if (p.MainWindowTitle.Contains("Discord"))
                {
                    procname = p.MainWindowTitle;
                    if (procname.Contains("Mass Deleter"))
                    {
                        procname = "";
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }

            switch (procname)
            {
                case "":
                    MessageBox.Show("Couldn't find discord window", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        public static void Init(string github_link)
        {
            if (!File.Exists(log_file))
            {
                File.Create(log_file);
            }

            MessageBox.Show("Made by orlando - " + github_link, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // M1: Using amount of messages
        public static void M1(int sleeptime, int result)
        {
            GetProc();
            var input = new WindowsInput.InputSimulator();

            using (StreamWriter writer = new StreamWriter(log_file))
            {
                DateTime time = DateTime.Now;
                writer.WriteLine(time.ToString("F"));
                writer.WriteLine("Discord window name: " + procname + "\n");

                for (int i = 0; i < result; i++)
                {
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.UP);
                    Thread.Sleep(sleeptime);
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.BACK);
                    Thread.Sleep(sleeptime);
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                    writer.WriteLine("Deleted: " + i + "\n");
                    Thread.Sleep(sleeptime);
                }
                writer.WriteLine(time.ToString("F") + ": Finished deleting!");
            }
            Thread.Sleep(1000);
            MessageBox.Show("Finished deleting!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // M2: Using amount of seconds
        public static void M2(int sleeptime, int seconds)
        {
            GetProc();
            var input = new WindowsInput.InputSimulator();
            int num = 0;

            using (StreamWriter writer = new StreamWriter(log_file))
            {
                Stopwatch timer = new Stopwatch();
                DateTime time = DateTime.Now;
                timer.Start();

                while (timer.Elapsed.TotalSeconds < seconds)
                {
                    writer.WriteLine(time.ToString("F"));
                    writer.WriteLine("Discord window name: " + procname + "\n");
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.UP);
                    Thread.Sleep(sleeptime);
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.BACK);
                    Thread.Sleep(sleeptime);
                    input.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                    num++;
                    writer.WriteLine("Amount deleted: " + num + "\n");
                    Thread.Sleep(sleeptime);
                }
                timer.Stop();
                writer.WriteLine(time.ToString("F") + ": Finished deleting!");
            }
            Thread.Sleep(1000);
            MessageBox.Show("Finished deleting!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}