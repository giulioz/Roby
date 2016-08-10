using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
		public static bool single;

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
                    file = System.IO.File.ReadAllLines("/home/" + Environment.UserName + "roby.conf");
                else
                    file = System.IO.File.ReadAllLines("roby.conf");

                monitorIndex = int.Parse(file[0]);
                monitor0Size = new Size(int.Parse(file[1]), int.Parse(file[2]));
                monitor1Size = new Size(int.Parse(file[3]), int.Parse(file[4]));
				single = bool.Parse(file[5]);
            }
            catch (Exception)
            {
                monitorIndex = 0;
                monitor0Size = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size;
                monitor1Size = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size;
				single = true;
            }

            if (CultureInfo.InstalledUICulture.TwoLetterISOLanguageName == "it")
                locale = new ResourceManager(typeof(Italian));
            else
                locale = new ResourceManager(typeof(English));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WhiteboardForm());
        }

        public static void SaveSettings()
        {
            string[] file = new string[] { monitorIndex.ToString(), monitor0Size.Width.ToString(), monitor0Size.Height.ToString(),
                                            monitor1Size.Width.ToString(), monitor1Size.Height.ToString(), single.ToString()};
            if (unix)
                System.IO.File.WriteAllLines("/home/" + Environment.UserName + "roby.conf", file);
            else
                System.IO.File.WriteAllLines("roby.conf", file);
        }
    }
}
