using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMR
{
    public class OMRConfiguration
    {
        public List<OMRQuestionBlock> Blocks { get; set; }
        public OMRAdjustmentBlock AdjustmentBlock { get; set; }
        public OMRAdjustmentBlock WhiteBlock { get; set; }
        public double DarkFactor { get; set; }
        public double FillFactor { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public OMRConfiguration()
        {
            this.Blocks = new List<OMRQuestionBlock>();
        }
    }
}
