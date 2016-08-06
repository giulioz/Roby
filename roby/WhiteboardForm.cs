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
        public WhiteboardForm()
        {
            InitializeComponent();
        }

        private void WhiteboardForm_Load(object sender, EventArgs e)
        {
            this.Location = Screen.AllScreens[0].WorkingArea.Location;
            this.Size = Screen.AllScreens[0].WorkingArea.Size;
        }
    }
}
