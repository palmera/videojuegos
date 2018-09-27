using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Experimental.U2D.TriangleNet.Geometry;

namespace Assets
{
    public abstract class IPath
    {
        public float sStart;
        public float sEnd;
        public string name;
        public abstract Point GetPos(float s);
        public abstract Point GetEndPoint();
        public abstract Point GetCurrentEndPoint();
        public abstract float GetLength();
    }
}
