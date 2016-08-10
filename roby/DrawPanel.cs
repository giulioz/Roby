using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace roby
{
    public enum DrawMode
    {
        Pen,
        Line,
        Square,
    }

    public partial class DrawPanel : UserControl
    {
        public Pen drawingPen;
        public bool penDown = false;
        public List<Tuple<List<Point>, PenInfo>> _strokes = new List<Tuple<List<Point>, PenInfo>>();
        public DrawMode mode;
        List<Point> _currStroke;

        public DrawPanel()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            penDown = true;
            ((WhiteboardForm)Parent).undo.Push(new List<Tuple<List<Point>, PenInfo>>(_strokes));
            _currStroke = new List<Point>();
            _currStroke.Add(e.Location);
            if (mode == DrawMode.Line) _currStroke.Add(e.Location);
            if (mode == DrawMode.Square)
            {
                _currStroke.Add(e.Location);
                _currStroke.Add(e.Location);
                _currStroke.Add(e.Location);
                _currStroke.Add(e.Location);
            }
            _strokes.Add(Tuple.Create(_currStroke, new PenInfo() { color = drawingPen.Color, width = drawingPen.Width }));
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            penDown = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            penDown = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (penDown)
            {
                if (mode == DrawMode.Pen)
                    _currStroke.Add(e.Location);
                else if (mode == DrawMode.Line)
                    _currStroke[1] = e.Location;
                else if (mode == DrawMode.Square)
                {
                    _currStroke[1] = new Point(e.X, _currStroke[0].Y);
                    _currStroke[3] = new Point(_currStroke[0].X, e.Y);
                    _currStroke[2] = e.Location;
                    _currStroke[4] = _currStroke[0];
                }
                Refresh();
            }
            base.OnMouseMove(e);
        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (Tuple<List<Point>, PenInfo> stroke in _strokes.Where(x => x.Item1.Count > 1))
            {
                Pen pen = new Pen(new SolidBrush(stroke.Item2.color), stroke.Item2.width);
                e.Graphics.DrawLines(pen, stroke.Item1.ToArray());
            }
        }
    }

    [Serializable]
    public class PenInfo
    {
        public Color color;
        public float width;
    }
}
