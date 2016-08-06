namespace roby
{
    partial class WhiteboardForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhiteboardForm));
            this.penButton = new System.Windows.Forms.ToolStripButton();
            this.highlButton = new System.Windows.Forms.ToolStripButton();
            this.rubberButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pforwardButton = new System.Windows.Forms.ToolStripButton();
            this.pbackButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.shapeButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.colorDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.magentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cyanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.drawPanel1 = new roby.DrawPanel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // penButton
            // 
            this.penButton.Checked = true;
            this.penButton.CheckOnClick = true;
            this.penButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.penButton.Image = ((System.Drawing.Image)(resources.GetObject("penButton.Image")));
            this.penButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.penButton.Name = "penButton";
            this.penButton.Size = new System.Drawing.Size(63, 44);
            this.penButton.Text = "Pen";
            this.penButton.Click += new System.EventHandler(this.penButton_Click);
            // 
            // highlButton
            // 
            this.highlButton.CheckOnClick = true;
            this.highlButton.Image = ((System.Drawing.Image)(resources.GetObject("highlButton.Image")));
            this.highlButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.highlButton.Name = "highlButton";
            this.highlButton.Size = new System.Drawing.Size(103, 44);
            this.highlButton.Text = "Highlighter";
            this.highlButton.Click += new System.EventHandler(this.highlButton_Click);
            // 
            // rubberButton
            // 
            this.rubberButton.CheckOnClick = true;
            this.rubberButton.Image = ((System.Drawing.Image)(resources.GetObject("rubberButton.Image")));
            this.rubberButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rubberButton.Name = "rubberButton";
            this.rubberButton.Size = new System.Drawing.Size(81, 44);
            this.rubberButton.Text = "Rubber";
            this.rubberButton.Click += new System.EventHandler(this.rubberButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 47);
            // 
            // pforwardButton
            // 
            this.pforwardButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pforwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pforwardButton.Image = ((System.Drawing.Image)(resources.GetObject("pforwardButton.Image")));
            this.pforwardButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pforwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pforwardButton.Name = "pforwardButton";
            this.pforwardButton.Size = new System.Drawing.Size(23, 44);
            this.pforwardButton.Text = ">";
            // 
            // pbackButton
            // 
            this.pbackButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pbackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pbackButton.Image = ((System.Drawing.Image)(resources.GetObject("pbackButton.Image")));
            this.pbackButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pbackButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pbackButton.Name = "pbackButton";
            this.pbackButton.Size = new System.Drawing.Size(23, 44);
            this.pbackButton.Text = "<";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel1.Image")));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(100, 44);
            this.toolStripLabel1.Text = "Page: 1 of 1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.penButton,
            this.highlButton,
            this.rubberButton,
            this.shapeButton,
            this.toolStripSeparator1,
            this.pforwardButton,
            this.pbackButton,
            this.toolStripLabel1,
            this.colorDropDownButton1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.undoButton,
            this.redoButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 563);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(856, 47);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // shapeButton
            // 
            this.shapeButton.Image = ((System.Drawing.Image)(resources.GetObject("shapeButton.Image")));
            this.shapeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shapeButton.Name = "shapeButton";
            this.shapeButton.Size = new System.Drawing.Size(84, 44);
            this.shapeButton.Text = "Shape";
            // 
            // colorDropDownButton1
            // 
            this.colorDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blackToolStripMenuItem,
            this.redToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.blueToolStripMenuItem,
            this.yellowToolStripMenuItem,
            this.magentaToolStripMenuItem,
            this.cyanToolStripMenuItem});
            this.colorDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("colorDropDownButton1.Image")));
            this.colorDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.colorDropDownButton1.Name = "colorDropDownButton1";
            this.colorDropDownButton1.Size = new System.Drawing.Size(81, 44);
            this.colorDropDownButton1.Text = "Color";
            // 
            // blackToolStripMenuItem
            // 
            this.blackToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.blackToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            this.blackToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.blackToolStripMenuItem.Text = "Black";
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.redToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.redToolStripMenuItem.Text = "Red";
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.BackColor = System.Drawing.Color.LimeGreen;
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.greenToolStripMenuItem.Text = "Green";
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.BackColor = System.Drawing.Color.Blue;
            this.blueToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            // 
            // yellowToolStripMenuItem
            // 
            this.yellowToolStripMenuItem.BackColor = System.Drawing.Color.Gold;
            this.yellowToolStripMenuItem.Name = "yellowToolStripMenuItem";
            this.yellowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.yellowToolStripMenuItem.Text = "Yellow";
            // 
            // magentaToolStripMenuItem
            // 
            this.magentaToolStripMenuItem.BackColor = System.Drawing.Color.Magenta;
            this.magentaToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.magentaToolStripMenuItem.Name = "magentaToolStripMenuItem";
            this.magentaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.magentaToolStripMenuItem.Text = "Magenta";
            // 
            // cyanToolStripMenuItem
            // 
            this.cyanToolStripMenuItem.BackColor = System.Drawing.Color.Cyan;
            this.cyanToolStripMenuItem.Name = "cyanToolStripMenuItem";
            this.cyanToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cyanToolStripMenuItem.Text = "Cyan";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 44);
            this.toolStripButton2.Text = "Width";
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
            this.redoButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(28, 44);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
            this.undoButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(28, 44);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 47);
            // 
            // drawPanel1
            // 
            this.drawPanel1.BackColor = System.Drawing.Color.White;
            this.drawPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawPanel1.Location = new System.Drawing.Point(0, 0);
            this.drawPanel1.Name = "drawPanel1";
            this.drawPanel1.Size = new System.Drawing.Size(856, 563);
            this.drawPanel1.TabIndex = 2;
            // 
            // WhiteboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 610);
            this.ControlBox = false;
            this.Controls.Add(this.drawPanel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WhiteboardForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Roby Whiteboard";
            this.Load += new System.EventHandler(this.WhiteboardForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DrawPanel drawPanel1;
        private System.Windows.Forms.ToolStripButton penButton;
        private System.Windows.Forms.ToolStripButton highlButton;
        private System.Windows.Forms.ToolStripButton rubberButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton pforwardButton;
        private System.Windows.Forms.ToolStripButton pbackButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton colorDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem blackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem magentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cyanToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton shapeButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton undoButton;
        private System.Windows.Forms.ToolStripButton redoButton;
    }
}

