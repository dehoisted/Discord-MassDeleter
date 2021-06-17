using System;
using System.Threading;
using System.Windows.Forms;

namespace DiscordMassDeleter
{
    public partial class Form1 : Form
    {
        private readonly static string github_link = "https://github.com/dehoisted/Discord-MassDeleter";
        private readonly static string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
        private static bool UseSeconds = false;

        public Form1()
        {
            InitializeComponent();
            MassDelete.Init(github_link);
        }

        public static void CheckValid(string arg)
        {
            int val = 0;

            for (int i = 0; i < 10; i++)
            {
                if (arg.Contains(nums[i]))
                {
                    val++;
                    continue;
                }

                switch (arg)
                {
                    case "":
                        val++;
                        break;
                    case " ":
                        val++;
                        break;
                    case "  ":
                        val++;
                        break;
                }
            }

            if (val == 0)
            {
                MessageBox.Show("Input numbers only!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            CheckValid(guna2TextBox1.Text);
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            CheckValid(guna2TextBox2.Text);
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            CheckValid(guna2TextBox3.Text);
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            switch (guna2CheckBox1.Checked)
            {
                case true:
                    UseSeconds = true;
                    guna2TextBox2.Show();
                    guna2TextBox1.Hide();
                    break;

                case false:
                    UseSeconds = false;
                    guna2TextBox1.Show();
                    guna2TextBox2.Hide();
                    break;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int result, seconds, sleeptime;

            try
            {
                Int32.TryParse(guna2TextBox1.Text, out result);
                Int32.TryParse(guna2TextBox2.Text, out seconds);
                Int32.TryParse(guna2TextBox3.Text, out sleeptime);
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Tab into discord, press \"OK\" and it will start in 5 seconds", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Thread.Sleep(5000);

            switch (UseSeconds)
            {
                case false:
                    MassDelete.M1(sleeptime, result);
                    break;

                case true:
                    MassDelete.M2(sleeptime, seconds);
                    break;
            }
        }
    }
}