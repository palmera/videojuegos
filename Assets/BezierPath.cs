using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class BezierPath : IPath
    {
        public Vector2 start;
        public Vector2 checkpoint1;
        public Vector2 checkpoint2;
        public Vector2 end;
        public BezierPath(Vector2 start, Vector2 checkpoint1, Vector2 checkpoint2, Vector2 end) {
            this.start = start;
            this.checkpoint1 = checkpoint1;
            this.checkpoint2 = checkpoint2;
            this.end = end;
        }

        private float Distance(Vector2 origin, Vector2 destiny) {
            return (float)Math.Sqrt(Math.Pow((destiny.x - origin.x), 2) + Math.Pow((destiny.y - origin.y), 2));
        }
    
        override public float GetLength()
        {
            float d1 = Distance(this.start, this.checkpoint1);
            float d2 = Distance(this.checkpoint1, this.checkpoint2);
            float d3 = Distance(this.checkpoint2, this.end);
            float d4 = Distance(this.start, this.end);

            return (d1 + d2 + d3 + d4) / 2;
        }

        override public Vector2 GetPos(float s)
        {
            double x = start.x * Math.Pow((1 - s), 3) + checkpoint1.x * Math.Pow((1 - s), 2) * 3 * s + 3 * checkpoint2.x * (1 - s) * Math.Pow(s, 2) + end.x * Math.Pow(s, 3);
            double y = start.y * Math.Pow((1 - s), 3) + checkpoint1.y * Math.Pow((1 - s), 2) * 3 * s + 3 * checkpoint2.y * (1 - s) * Math.Pow(s, 2) + end.y * Math.Pow(s, 3);

            return new Vector2((float)x, (float)y);
        }

        public override Vector2 GetEndPoint()
        {
            return end;
        }

        public override Vector2 GetCurrentEndPoint()
        {
            return end;
        }
    }
}
