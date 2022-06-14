using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MarathonSkills
{
    public class ManagerNavigation
    {
        public static Frame MainFrame { get; set; }

        public ManagerNavigation()
        {
            MainFrame = new Frame();
        }
    }
}
