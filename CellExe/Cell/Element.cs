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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Position p = obj as Position;
            if ((System.Object)p == null)
                return false;

            return this.dx == p.dx && this.dy == p.dy;
        }
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Size p = obj as Size;
            if ((System.Object)p == null)
                return false;

            return this.Height==p.Height && this.Width==p.Width;
        }
    }

    public class Vector
    {
        public double Direction { get; set; }

        public double Speed { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Vector p = obj as Vector;
            if ((System.Object)p == null)
                return false;

            return this.Direction == p.Direction && this.Speed == p.Speed;
        }
    }

}
