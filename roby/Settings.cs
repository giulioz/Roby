using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace roby
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            this.Text = Program.locale.GetString("Settings");
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = Program.unix;
            textBox2.Enabled = Program.unix;

            numericUpDown1.Value = Program.monitorIndex;
            textBox1.Text = Program.monitor0Size.Width + "x" + Program.monitor0Size.Height;
            textBox2.Text = Program.monitor1Size.Width + "x" + Program.monitor1Size.Height;
            checkBox1.Checked = Program.single;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.monitorIndex = (int)numericUpDown1.Value;
            Program.monitor0Size = new Size(int.Parse(textBox1.Text.Split('x')[0]), int.Parse(textBox1.Text.Split('x')[1]));
            Program.monitor1Size = new Size(int.Parse(textBox2.Text.Split('x')[0]), int.Parse(textBox2.Text.Split('x')[1]));
            Program.single = checkBox1.Checked;
            Program.SaveSettings();
            this.Close();
        }
    }
}
