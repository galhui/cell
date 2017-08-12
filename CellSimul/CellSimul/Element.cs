using System;

namespace CellSimul
{
    static class Constants
    {
        public const double TimeBalance = 100;     // don't change this value. 
    }

    public class Position
    {
        public Position()
        {
            dx = x = 0;
            dy = y = 0;
        }

        public Position(Position ps)
        {
            dx = x = ps.x;
            dy = y = ps.y;
        }

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

        public double GetDistance(Position ps)
        {
            return Math.Sqrt(Math.Pow(ps.dx- this.dx, 2) + Math.Pow(ps.dy-this.dy, 2));
        }
    }

    public class Extent
    {
        public Extent(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public int Height { get; set; }

        public int Width { get; set; }

        public double Radius
        {
            get
            {
                return Math.Sqrt(Math.Pow(Height, 2) + Math.Pow(Width, 2));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Extent p = obj as Extent;
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

        public Position GetNextPosition(Position ps)
        {
            return GetNextPosition(ps, this);
        }

        public Position GetNextPosition(Position ps, Vector vector)
        {
            Position ret = new Position();

            ret.dx = ps.dx + (Math.Sin(vector.Direction / 180.0 * Math.PI) * vector.Speed / Constants.TimeBalance);
            ret.dy = ps.dy + (Math.Cos(vector.Direction / 180.0 * Math.PI) * vector.Speed / Constants.TimeBalance);

            ret.DoubleToInt();

            return ret;
        }
    }
    
}
