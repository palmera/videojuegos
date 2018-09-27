using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Experimental.U2D.TriangleNet.Geometry;

namespace Assets
{
    public class BezierPath : IPath
    {
        public Point start;
        public Point checkpoint1;
        public Point checkpoint2;
        public Point end;
        public BezierPath(Point start, Point checkpoint1, Point checkpoint2, Point end) {
            this.start = start;
            this.checkpoint1 = checkpoint1;
            this.checkpoint2 = checkpoint2;
            this.end = end;
        }

        private float Distance(Point origin, Point destiny) {
            return (float)Math.Sqrt(Math.Pow((destiny.X - origin.X), 2) + Math.Pow((destiny.Y - origin.Y), 2));
        }
    
        override public float GetLength()
        {
            float d1 = Distance(this.start, this.checkpoint1);
            float d2 = Distance(this.checkpoint1, this.checkpoint2);
            float d3 = Distance(this.checkpoint2, this.end);
            float d4 = Distance(this.start, this.end);

            return (d1 + d2 + d3 + d4) / 2;
        }

        override public Point GetPos(float s)
        {
            double x = start.X * Math.Pow((1 - s), 3) + checkpoint1.X * Math.Pow((1 - s), 2) * 3 * s + checkpoint2.X * Math.Pow((1 - s), 3) * Math.Pow(s, 2) + end.X * Math.Pow(s, 3);
            double y = start.Y * Math.Pow((1 - s), 3) + checkpoint1.Y * Math.Pow((1 - s), 2) * 3 * s + checkpoint2.Y * Math.Pow((1 - s), 3) * Math.Pow(s, 2) + end.Y * Math.Pow(s, 3);

            return new Point(x, y);
        }

        public override Point GetEndPoint()
        {
            return end;
        }

        public override Point GetCurrentEndPoint()
        {
            return end;
        }
    }
}
