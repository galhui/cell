using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellExe.Element
{
    public class Position
    {
        public Position(int px, int py)
        {
            dx = x = px;
            dy = y = py;
        }

        public void DoubleToInt()
        {
            x = Convert.ToInt32(dx);
            y = Convert.ToInt32(dy);
        }

        public int x { get; set; }
        public double dx { get; set; }
        public int y { get; set; }

        public double dy { get; set; }
    }

    public class Size
    {
        public Size(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public int Height { get; set; }

        public int Width { get; set; }
    }

    public class Vector
    {
        public double Direction { get; set; }

        public double Speed { get; set; }
    }

}
