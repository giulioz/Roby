using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace roby
{
    public partial class WhiteboardForm : Form
    {
        public Pen  pen = new Pen(Color.Black, 2f),
                    highlighter = new Pen(new SolidBrush(Color.FromArgb(150, 255, 255, 0)), 30f),
                    rubber = new Pen(Color.White, 40f);
        List<Bitmap> pages;
        int selectedPage = 1;

        public WhiteboardForm()
        {
            InitializeComponent();
            foreach (ToolStripMenuItem item in colorDropDownButton1.DropDownItems)
            {
                item.Click += (object sender, EventArgs e) => drawPanel1.drawingPen.Color =
                    Color.FromArgb(drawPanel1.drawingPen.Color.A, ((ToolStripMenuItem)sender).BackColor);
            }
        }

        private void WhiteboardForm_Load(object sender, EventArgs e)
        {
            this.Location = Screen.AllScreens[0].WorkingArea.Location;
            this.Size = Screen.AllScreens[0].WorkingArea.Size;
            drawPanel1.drawingPen = pen;
            pages = new List<Bitmap>();
            pages.Add(new Bitmap(drawPanel1.Width, drawPanel1.Height, PixelFormat.Format32bppArgb));
            using (Graphics bgrap = Graphics.FromImage(pages[pages.Count - 1]))
            {
                bgrap.Clear(Color.White);
            }
            LoadLocale();
        }

        private void LoadLocale()
        {
            this.penButton.Text = Program.locale.GetString("Pen");
            this.highlButton.Text = Program.locale.GetString("Highlighter");
            this.rubberButton.Text = Program.locale.GetString("Rubber");
            this.shapeButton.Text = Program.locale.GetString("Shape");
            this.colorDropDownButton1.Text = Program.locale.GetString("Color");
            this.blackToolStripMenuItem.Text = Program.locale.GetString("Black");
            this.redToolStripMenuItem.Text = Program.locale.GetString("Red");
            this.greenToolStripMenuItem.Text = Program.locale.GetString("Green");
            this.blueToolStripMenuItem.Text = Program.locale.GetString("Blue");
            this.yellowToolStripMenuItem.Text = Program.locale.GetString("Yellow");
            this.magentaToolStripMenuItem.Text = Program.locale.GetString("Magenta");
            this.cyanToolStripMenuItem.Text = Program.locale.GetString("Cyan");
            this.clearButton.Text = Program.locale.GetString("Clear");
            this.undoButton.Text = Program.locale.GetString("Undo");
            this.redoButton.Text = Program.locale.GetString("Redo");
            //this.toolStripDropDownButton1.Text = Program.locale.GetString("File");
            this.openButton.Text = Program.locale.GetString("Open");
            this.saveButton.Text = Program.locale.GetString("Save");
            pageLabel.Text = Program.locale.GetString("Page") + ": " + selectedPage.ToString() + " " + Program.locale.GetString("of") + " " + pages.Count;
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

        private void clearButton_Click(object sender, EventArgs e)
        {
            drawPanel1.panelGraphics.Clear(Color.White);
        }

        private void pforwardButton_Click(object sender, EventArgs e)
        {
            if (selectedPage == pages.Count)
            {
                pages.Add(new Bitmap(drawPanel1.Width, drawPanel1.Height, PixelFormat.Format32bppArgb));
                using (Graphics bgrap = Graphics.FromImage(pages[pages.Count - 1]))
                {
                    bgrap.Clear(Color.White);
                }
            }
            using (Graphics bgrap = Graphics.FromImage(pages[selectedPage - 1]))
            {
                bgrap.CopyFromScreen(PointToScreen(Point.Empty).X, PointToScreen(Point.Empty).Y, 0, 0, drawPanel1.Size, CopyPixelOperation.SourceCopy);
            }
            selectedPage++;
            pageLabel.Text = Program.locale.GetString("Page") + ": " + selectedPage.ToString() + " " + Program.locale.GetString("of") + " " + pages.Count;
            drawPanel1.panelGraphics.DrawImageUnscaled(pages[selectedPage - 1], 0, 0);
        }

        private void pbackButton_Click(object sender, EventArgs e)
        {
            if (selectedPage != 1)
            {
                using (Graphics bgrap = Graphics.FromImage(pages[selectedPage - 1]))
                {
                    bgrap.CopyFromScreen(PointToScreen(Point.Empty).X, PointToScreen(Point.Empty).Y, 0, 0, drawPanel1.Size);
                }
                selectedPage--;
                pageLabel.Text = Program.locale.GetString("Page") + ": " + selectedPage.ToString() + " " + Program.locale.GetString("of") + " " + pages.Count;
                drawPanel1.panelGraphics.DrawImageUnscaled(pages[selectedPage - 1], 0, 0);
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            trayMenuStrip.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drawPanel1.panelGraphics.DrawImage(Image.FromFile(dlg.FileName), 0, 0, drawPanel1.Width, drawPanel1.Height);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Graphics bgrap = Graphics.FromImage(pages[selectedPage - 1]))
            {
                bgrap.CopyFromScreen(PointToScreen(Point.Empty).X, PointToScreen(Point.Empty).Y, 0, 0, drawPanel1.Size);
            }
            
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PNG|*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pages[selectedPage - 1].Save(dlg.FileName);
            }

            drawPanel1.panelGraphics.DrawImageUnscaled(pages[selectedPage - 1], 0, 0);
        }
    }
}
