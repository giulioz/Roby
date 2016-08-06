using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace roby
{
    static class Program
    {
        public static ResourceManager locale;

        [STAThread]
        static void Main()
        {
            locale = new ResourceManager(typeof(Italian));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WhiteboardForm());
        }
    }
}
