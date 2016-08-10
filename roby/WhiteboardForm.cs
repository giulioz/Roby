using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
        public List<List<Tuple<List<Point>, PenInfo>>> pages;
        public Stack<List<Tuple<List<Point>, PenInfo>>> undo = new Stack<List<Tuple<List<Point>, PenInfo>>>(),
            redo = new Stack<List<Tuple<List<Point>, PenInfo>>>();
        public int selectedPage = 1;

        public WhiteboardForm()
        {
            InitializeComponent();
            foreach (ToolStripMenuItem item in colorDropDownButton1.DropDownItems)
            {
                item.Click += (object sender, EventArgs e) => drawPanel1.drawingPen = new Pen(
                    Color.FromArgb(drawPanel1.drawingPen.Color.A, ((ToolStripMenuItem)sender).BackColor), drawPanel1.drawingPen.Width);
            }
        }

        private void WhiteboardForm_Load(object sender, EventArgs e)
        {
            // Form Position
            if (Program.unix)
            {
                this.Location = Program.monitorIndex == 0 ? Point.Empty : new Point(Program.monitor0Size);
                this.Size = Program.monitorIndex == 0 ? Program.monitor0Size : Program.monitor1Size;
            }
            else
            {
                // In windows is much simpler...
                this.Location = Screen.AllScreens[Program.monitorIndex].WorkingArea.Location;
                this.Size = Screen.AllScreens[Program.monitorIndex].WorkingArea.Size;
            }

            drawPanel1.drawingPen = pen;
            pages = new List<List<Tuple<List<Point>, PenInfo>>>();
            pages.Add(new List<Tuple<List<Point>, PenInfo>>());
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
            this.fileButton.Text = Program.locale.GetString("File");
            this.openButton.Text = Program.locale.GetString("Open");
            this.saveButton.Text = Program.locale.GetString("Save");
            pageLabel.Text = Program.locale.GetString("Page") + ": " + selectedPage.ToString() + " " + Program.locale.GetString("of") + " " + pages.Count;
        }

        private void penButton_Click(object sender, EventArgs e)
        {
            drawPanel1.mode = DrawMode.Pen;
            highlButton.Checked = false;
            rubberButton.Checked = false;
            drawPanel1.drawingPen = pen;
        }

        private void highlButton_Click(object sender, EventArgs e)
        {
            drawPanel1.mode = DrawMode.Pen;
            penButton.Checked = false;
            rubberButton.Checked = false;
            drawPanel1.drawingPen = highlighter;
        }

        private void rubberButton_Click(object sender, EventArgs e)
        {
            drawPanel1.mode = DrawMode.Pen;
            penButton.Checked = false;
            highlButton.Checked = false;
            drawPanel1.drawingPen = rubber;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            drawPanel1._strokes = new List<Tuple<List<Point>, PenInfo>>();
            drawPanel1.Refresh();
        }

        private void pforwardButton_Click(object sender, EventArgs e)
        {
            if (selectedPage == pages.Count)
            {
                pages.Add(new List<Tuple<List<Point>, PenInfo>>());
            }
            pages[selectedPage - 1] = drawPanel1._strokes;
            selectedPage++;
            pageLabel.Text = Program.locale.GetString("Page") + ": " + selectedPage.ToString() + " " + Program.locale.GetString("of") + " " + pages.Count;
            drawPanel1._strokes = pages[selectedPage - 1];
            drawPanel1.Refresh();
        }

        private void pbackButton_Click(object sender, EventArgs e)
        {
            if (selectedPage != 1)
            {
                pages[selectedPage - 1] = drawPanel1._strokes;
                selectedPage--;
                pageLabel.Text = Program.locale.GetString("Page") + ": " + selectedPage.ToString() + " " + Program.locale.GetString("of") + " " + pages.Count;
                drawPanel1._strokes = pages[selectedPage - 1];
                drawPanel1.Refresh();
            }
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            if (undo.Count != 0)
            {
                redo.Push(new List<Tuple<List<Point>, PenInfo>>(drawPanel1._strokes));
                drawPanel1._strokes = undo.Pop();
                drawPanel1.Refresh();
            }
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            if (redo.Count != 0)
            {
                undo.Push(new List<Tuple<List<Point>, PenInfo>>(drawPanel1._strokes));
                drawPanel1._strokes = redo.Pop();
                drawPanel1.Refresh();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "ROBY Files (*.rob)|*.rob";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream st = File.OpenRead(dlg.FileName);
                object result = formatter.Deserialize(st);
                st.Close();
                pages = (List<List<Tuple<List<Point>, PenInfo>>>)result;
                selectedPage = 1;
                drawPanel1._strokes = pages[selectedPage - 1];
                drawPanel1.Refresh();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "ROBY Files (*.rob)|*.rob";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                pages[selectedPage - 1] = drawPanel1._strokes;
                BinaryFormatter formatter = new BinaryFormatter();
                Stream st = File.OpenWrite(dlg.FileName);
                formatter.Serialize(st, pages);
                st.Flush();
                st.Close();
            }
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawPanel1.mode = DrawMode.Line;
            penButton.Checked = false;
            highlButton.Checked = false;
            rubberButton.Checked = false;
            drawPanel1.drawingPen = pen;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawPanel1.mode = DrawMode.Square;
            penButton.Checked = false;
            highlButton.Checked = false;
            rubberButton.Checked = false;
            drawPanel1.drawingPen = pen;
        }
    }
}
