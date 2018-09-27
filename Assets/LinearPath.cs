using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Experimental.U2D.TriangleNet.Geometry;

namespace Assets
{
    public class LinearPath : IPath
    {
        public Point origin;
        public Point destiny;
        public LinearPath(Point origin, Point destiny) {
            this.destiny = destiny;
            this.origin = origin;
        }

        public override Point GetCurrentEndPoint()
        {
            return destiny;
        }

        public override Point GetEndPoint()
        {
            return destiny;
        }

        override public float GetLength()
        {
            return (float)Math.Sqrt(Math.Pow((this.destiny.X - this.origin.X), 2) + Math.Pow((this.destiny.Y - this.origin.Y), 2));
        }

        override public Point GetPos(float s)
        {
            double x = origin.X + (destiny.X - origin.X) * s;
            double y = origin.Y + (destiny.Y - origin.Y) * s;

            return new Point(x, y);
        }

        
    }
}
