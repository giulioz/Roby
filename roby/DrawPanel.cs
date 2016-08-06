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
    public partial class DrawPanel : UserControl
    {
        public Graphics panelGraphics;
        public Point lastMousePos = Point.Empty;
        public Pen drawingPen;
        public bool penDown = false;

        public DrawPanel()
        {
            InitializeComponent();
        }

        private void DrawPanel_Load(object sender, EventArgs e)
        {
            panelGraphics = this.CreateGraphics();
            panelGraphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        protected override void OnResize(EventArgs e)
        {
            panelGraphics = this.CreateGraphics();
            panelGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            base.OnResize(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            penDown = true;
            lastMousePos = e.Location;
            panelGraphics.DrawLine(drawingPen, e.X, e.Y, e.X + 1, e.Y - 1);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            penDown = false;
            lastMousePos = Point.Empty;
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            penDown = false;
            lastMousePos = Point.Empty;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (penDown)
            {
                panelGraphics.DrawLine(drawingPen, lastMousePos, e.Location);
                lastMousePos = e.Location;
            }
            base.OnMouseMove(e);
        }
    }
}
