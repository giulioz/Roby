using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace roby
{
    public partial class WhiteboardForm : Form
    {
        public enum DrawingMode
        {
            Pen,
            Line,
            SnapLine,
            Arrow,
            Circle,
            Rectangle,
        }

        [Serializable]
        public class PenInfo
        {
            public Color color;
            public float width;
        }

        public const double SnapThreshold = 400;
        public Pen  highlighter = new Pen(new SolidBrush(Color.FromArgb(150, 255, 255, 0)), 30f),
                    rubber = new Pen(Color.White, 40f);
        public List<List<Tuple<List<Point>, PenInfo>>> pages;
        public Stack<List<Tuple<List<Point>, PenInfo>>> undo = new Stack<List<Tuple<List<Point>, PenInfo>>>(),
            redo = new Stack<List<Tuple<List<Point>, PenInfo>>>();
        public int selectedPage = 1;
        public Pen drawingPen;
        public DrawingMode mode;
        public bool penDown = false;
        List<Point> _currStroke;
        IntroControl intro;
        bool viewIntro;

        public WhiteboardForm()
        {
            InitializeComponent();
            intro = new IntroControl();
            intro.Click += Intro_Click;
            this.Controls.Add(intro);
            this.Controls.SetChildIndex(intro, 0);
            viewIntro = true;
			this.ShowInTaskbar = Program.single;
            foreach (ToolStripMenuItem item in colorDropDownButton1.DropDownItems)
            {
                item.Click += (object sender, EventArgs e) => drawingPen = new Pen(
                    Color.FromArgb(drawingPen.Color.A, ((ToolStripMenuItem)sender).BackColor), drawingPen.Width);
            }
        }

        private void Intro_Click(object sender, EventArgs e)
        {
            viewIntro = false;
            this.Controls.Remove(intro);
            intro.Dispose();
        }

        private void WhiteboardForm_Load(object sender, EventArgs e)
        {
            // Form Position
            //if (Program.unix)
            {
                this.Location = Program.monitorIndex == 0 ? Point.Empty : new Point(Program.monitor0Size.Width, 0);
                this.Size = Program.monitorIndex == 0 ? Program.monitor0Size : Program.monitor1Size;
            }
            /*else
            {
                // In windows is much simpler...
                this.Location = Screen.AllScreens[Program.monitorIndex].WorkingArea.Location;
                this.Size = Screen.AllScreens[Program.monitorIndex].WorkingArea.Size;
            }*/
            intro.Location = new Point(this.Width / 2 - intro.Width / 2, this.Height / 2 - intro.Width / 2);

            drawingPen = new Pen(Color.Black, 2f);
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
			this.lineButton.Text = Program.locale.GetString("Line");
            this.snaplineButton.Text = Program.locale.GetString("SnapLine");
            this.arrowButton.Text = Program.locale.GetString("Arrow");
            this.rectButton.Text = Program.locale.GetString("Rectangle");
            this.circleButton.Text = Program.locale.GetString("Circle");
            this.exitToolStripMenuItem.Text = Program.locale.GetString("Exit");
            this.settingsToolStripMenuItem.Text = Program.locale.GetString("Settings");
            this.Text = Program.locale.GetString("Title");
            pageLabel.Text = Program.locale.GetString("Page") + ": " + selectedPage.ToString() + " " + Program.locale.GetString("of") + " " + pages.Count;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (viewIntro)
            {
                viewIntro = false;
                this.Controls.Remove(intro);
                intro.Dispose();
            }
            penDown = true;
            undo.Push(new List<Tuple<List<Point>, PenInfo>>(pages[selectedPage - 1]));
            _currStroke = new List<Point>();
            _currStroke.Add(e.Location);
            if (mode == DrawingMode.Line)
            {
                _currStroke[0] = Snap(_currStroke[0]);
                _currStroke.Add(e.Location);
            }
            if (mode == DrawingMode.SnapLine)
            {
                _currStroke[0] = Snap(_currStroke[0]);
                _currStroke.Add(e.Location);
            }
            if (mode == DrawingMode.Rectangle)
            {
                _currStroke[0] = Snap(_currStroke[0]);
                _currStroke.Add(_currStroke[0]);
                _currStroke.Add(_currStroke[0]);
                _currStroke.Add(_currStroke[0]);
                _currStroke.Add(_currStroke[0]);
            }
            if (mode == DrawingMode.Arrow)
            {
                _currStroke[0] = Snap(_currStroke[0]);
                _currStroke.Add(e.Location);
                _currStroke.Add(e.Location);
                _currStroke.Add(e.Location);
                _currStroke.Add(e.Location);
                _currStroke.Add(e.Location);
            }
            pages[selectedPage - 1].Add(Tuple.Create(_currStroke, new PenInfo() { color = drawingPen.Color, width = drawingPen.Width }));
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            penDown = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (penDown)
            {
                if (mode == DrawingMode.Pen) _currStroke.Add(e.Location);
                else if (mode == DrawingMode.Line) _currStroke[1] = Snap(e.Location);
                else if (mode == DrawingMode.SnapLine)
                {
                    double l = Math.Sqrt(Distance(e.Location, _currStroke[0]));
                    float angle = (float)Math.Acos((e.X - _currStroke[0].X) / l);
                    bool invert = e.Y - _currStroke[0].Y > 0;
                    angle = (int)(angle / 0.3927f) * 0.3927f;

                    _currStroke[1] = Snap(new Point((int)(_currStroke[0].X + Math.Cos(angle) * l),
                                    (int)(_currStroke[0].Y + (invert ? 1 : -1) * Math.Sin(angle) * l)));
                }
                else if (mode == DrawingMode.Rectangle)
                {
                    _currStroke[2] = Snap(e.Location);
                    _currStroke[1] = new Point(_currStroke[2].X, _currStroke[0].Y);
                    _currStroke[3] = new Point(_currStroke[0].X, _currStroke[2].Y);
                }
                else if (mode == DrawingMode.Arrow)
                {
                    double l = Math.Sqrt(Distance(e.Location, _currStroke[0]));
                    float angle = (float)Math.Acos((e.X - _currStroke[0].X) / l);
                    bool invert = e.Y - _currStroke[0].Y > 0;
                    _currStroke[1] = Snap(e.Location);
                    _currStroke[2] = new Point((int)(_currStroke[1].X + (float)Math.Cos(angle + 2.5f) * 20f),
                                            (int)(_currStroke[1].Y + (invert ? 1 : -1) * 
                                                    (float)Math.Sin(angle + 2.5f) * 20f));
                    _currStroke[3] = _currStroke[1];
                    _currStroke[4] = new Point((int)(_currStroke[1].X + (float)Math.Cos(angle - 2.5f) * 20f),
                                            (int)(_currStroke[1].Y + (invert ? 1 : -1) * 
                                                    (float)Math.Sin(angle - 2.5f) * 20f));
                    _currStroke[5] = _currStroke[1];
                }
                panel1.Refresh();
            }
        }

        private Point Snap(Point near)
        {
            if (pages[selectedPage - 1].Count <= 1)
                return near;
            
            double min = Distance(near, pages[selectedPage - 1][0].Item1.First());
            Point p = pages[selectedPage - 1][0].Item1.First();
            if (min < SnapThreshold)
                return new Point(p.X, p.Y);
            
            for (int i = 0; i < pages[selectedPage - 1].Count - 1; i++)
            {
                if (Distance(near, pages[selectedPage - 1][i].Item1.First()) < min)
                {
                    min = Distance(near, pages[selectedPage - 1][i].Item1.First());
                    p = pages[selectedPage - 1][i].Item1.First();

                    if (min < SnapThreshold)
                        return new Point(p.X, p.Y);
                }
                if (Distance(near, pages[selectedPage - 1][i].Item1.Last()) < min)
                {
                    min = Distance(near, pages[selectedPage - 1][i].Item1.Last());
                    p = pages[selectedPage - 1][i].Item1.Last();

                    if (min < SnapThreshold)
                        return new Point(p.X, p.Y);
                }
            }
            return near;
        }

        private double Distance(Point a , Point b)
        {
            return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!viewIntro)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                foreach (Tuple<List<Point>, PenInfo> stroke in pages[selectedPage - 1].Where(x => x.Item1.Count > 1))
                {
                    Pen pen = new Pen(new SolidBrush(stroke.Item2.color), stroke.Item2.width);
                    e.Graphics.DrawLines(pen, stroke.Item1.ToArray());
                }
            }
        }

        private void penThinButton_Click(object sender, EventArgs e)
        {
            highlButton.Checked = false;
            rubberButton.Checked = false;
			mode = DrawingMode.Pen;
            drawingPen = new Pen(Color.Black, 2f);
        }

        private void penButton_Click(object sender, EventArgs e)
        {
            highlButton.Checked = false;
            rubberButton.Checked = false;
			mode = DrawingMode.Pen;
            drawingPen = new Pen(Color.Black, 2f);
        }

        private void penThickButton_Click(object sender, EventArgs e)
        {
            highlButton.Checked = false;
            rubberButton.Checked = false;
			mode = DrawingMode.Pen;
            drawingPen = new Pen(Color.Black, 2f);
        }

        private void highlButton_Click(object sender, EventArgs e)
        {
            penButton.Checked = false;
            rubberButton.Checked = false;
			mode = DrawingMode.Pen;
            drawingPen = highlighter;
        }

        private void rubberButton_Click(object sender, EventArgs e)
        {
            penButton.Checked = false;
            highlButton.Checked = false;
			mode = DrawingMode.Pen;
            drawingPen = rubber;

            if (!rubberButton.Checked) {
                undo.Push(new List<Tuple<List<Point>, PenInfo>>(pages[selectedPage - 1]));
                pages[selectedPage - 1] = new List<Tuple<List<Point>, PenInfo>>();
                Refresh();
            }
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            mode = DrawingMode.Line;
            penButton.Checked = false;
            highlButton.Checked = false;
            rubberButton.Checked = false;
        }

        private void snaplineButton_Click(object sender, EventArgs e)
        {
            mode = DrawingMode.SnapLine;
            penButton.Checked = false;
            highlButton.Checked = false;
            rubberButton.Checked = false;
        }

        private void arrowButton_Click(object sender, EventArgs e)
        {
            mode = DrawingMode.Arrow;
            penButton.Checked = false;
            highlButton.Checked = false;
            rubberButton.Checked = false;
        }

        private void rectButton_Click(object sender, EventArgs e)
        {
            mode = DrawingMode.Rectangle;
            penButton.Checked = false;
            highlButton.Checked = false;
            rubberButton.Checked = false;
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            mode = DrawingMode.Circle;
            penButton.Checked = false;
            highlButton.Checked = false;
            rubberButton.Checked = false;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, Program.locale.GetString("MessageSure"), "Roby",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                undo.Push(new List<Tuple<List<Point>, PenInfo>>(pages[selectedPage - 1]));
                pages = new List<List<Tuple<List<Point>, PenInfo>>>();
                pages.Add(new List<Tuple<List<Point>, PenInfo>>());
                selectedPage = 1;
                Refresh();
            }
        }

        private void pforwardButton_Click(object sender, EventArgs e)
        {
            if (selectedPage == pages.Count)
            {
                pages.Add(new List<Tuple<List<Point>, PenInfo>>());
            }
            selectedPage++;
            pageLabel.Text = Program.locale.GetString("Page") + ": " + selectedPage.ToString() + " " + Program.locale.GetString("of") + " " + pages.Count;
            redo = new Stack<List<Tuple<List<Point>, PenInfo>>>();
            undo = new Stack<List<Tuple<List<Point>, PenInfo>>>();
            panel1.Refresh();
        }

        private void pbackButton_Click(object sender, EventArgs e)
        {
            if (selectedPage != 1)
            {
                selectedPage--;
                pageLabel.Text = Program.locale.GetString("Page") + ": " + selectedPage.ToString() + " " + Program.locale.GetString("of") + " " + pages.Count;
                redo = new Stack<List<Tuple<List<Point>, PenInfo>>>();
                undo = new Stack<List<Tuple<List<Point>, PenInfo>>>();
                panel1.Refresh();
            }
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            if (undo.Count != 0)
            {
                redo.Push(new List<Tuple<List<Point>, PenInfo>>(pages[selectedPage - 1]));
                pages[selectedPage - 1] = undo.Pop();
                panel1.Refresh();
            }
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            if (redo.Count != 0)
            {
                undo.Push(new List<Tuple<List<Point>, PenInfo>>(pages[selectedPage - 1]));
                pages[selectedPage - 1] = redo.Pop();
                panel1.Refresh();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                panel1.Refresh();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "ROBY Files (*.rob)|*.rob";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream st = File.OpenWrite(dlg.FileName);
                formatter.Serialize(st, pages);
                st.Flush();
                st.Close();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Settings().ShowDialog(this);
        }
    }
}
