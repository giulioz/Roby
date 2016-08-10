using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace roby
{
    static class Program
    {
        public static ResourceManager locale;
        public static bool unix;

        public static int monitorIndex;
        public static Size monitor0Size, monitor1Size;

        [STAThread]
        static void Main()
        {
            int p = (int)Environment.OSVersion.Platform;
            if ((p == 4) || (p == 6) || (p == 128)) unix = true;
            else unix = false;
            
            string[] file = null;
            try
            {
                if (unix)
                    file = System.IO.File.ReadAllLines("/etc/roby.conf");
                else
                    file = System.IO.File.ReadAllLines("roby.conf");

                monitorIndex = int.Parse(file[0]);
                monitor0Size = new Size(int.Parse(file[1]), int.Parse(file[2]));
                monitor1Size = new Size(int.Parse(file[3]), int.Parse(file[4]));
            }
            catch (Exception)
            {
                monitorIndex = 0;
                monitor0Size = new Size(1024, 768);
                monitor1Size = new Size(1024, 768);
            }

            locale = new ResourceManager(typeof(Italian));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WhiteboardForm());
        }
    }
}
