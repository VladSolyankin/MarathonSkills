using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills
{
    public static class ManagerModel
    {
        public static List<CheckpointFeatures> Checkpoints { get; set; }
        static ManagerModel()
        {
            Checkpoints = new List<CheckpointFeatures>();
        }
    }
}
