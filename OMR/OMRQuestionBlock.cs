using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMR
{
    public class OMRQuestionBlock
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Slices { get; set; }

        public OMRQuestionBlock() { }

        public OMRQuestionBlock(string name, int x, int y, int width, int height, int slices)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Slices = slices;
        }
    }
}
