using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace roby
{
    public partial class WhiteboardForm : Form
    {
        public Pen  pen = new Pen(Color.Black, 2f),
                    highlighter = new Pen(Color.FromArgb(150, 255, 255, 0), 20f),
                    rubber = new Pen(Color.White, 20f);

        public WhiteboardForm()
        {
            InitializeComponent();
            foreach (ToolStripMenuItem item in colorDropDownButton1.DropDownItems)
            {
                item.Click += (object sender, EventArgs e) => drawPanel1.drawingPen.Color = ((ToolStripMenuItem)sender).BackColor;
            }
        }

        private void WhiteboardForm_Load(object sender, EventArgs e)
        {
            this.Location = Screen.AllScreens[0].WorkingArea.Location;
            this.Size = Screen.AllScreens[0].WorkingArea.Size;
        }

        private void penButton_Click(object sender, EventArgs e)
        {
            highlButton.Checked = false;
            rubberButton.Checked = false;
            drawPanel1.drawingPen = pen;
        }

        private void highlButton_Click(object sender, EventArgs e)
        {
            penButton.Checked = false;
            rubberButton.Checked = false;
            drawPanel1.drawingPen = highlighter;
        }

        private void rubberButton_Click(object sender, EventArgs e)
        {
            penButton.Checked = false;
            highlButton.Checked = false;
            drawPanel1.drawingPen = rubber;
        }
    }
}
