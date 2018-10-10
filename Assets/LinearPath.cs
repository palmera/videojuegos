using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class LinearPath : IPath
    {
        public Vector2 origin;
        public Vector2 destiny;
        public LinearPath(Vector2 origin, Vector2 destiny) {
            this.destiny = destiny;
            this.origin = origin;
        }

        public override Vector2 GetCurrentEndPoint()
        {
            return destiny;
        }

        public override Vector2 GetEndPoint()
        {
            return destiny;
        }

        override public float GetLength()
        {
            return (float)Math.Sqrt(Math.Pow((this.destiny.x - this.origin.x), 2) + Math.Pow((this.destiny.y - this.origin.y), 2));
        }

        override public Vector2 GetPos(float s)
        {
            float x = origin.x + (destiny.x - origin.x) * s;
            float y = origin.y + (destiny.y - origin.y) * s;

            return new Vector2(x, y);
        }

        
    }
}
