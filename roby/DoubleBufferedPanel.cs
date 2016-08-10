using System.ComponentModel;
using System.Windows.Forms;

namespace roby
{
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel() : base ()
        {
            this.DoubleBuffered = true;
        }
    }
}
