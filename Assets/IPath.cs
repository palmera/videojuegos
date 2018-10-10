using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public abstract class IPath
    {
        public float sStart;
        public float sEnd;
        public string name;
        public abstract Vector2 GetPos(float s);
        public abstract Vector2 GetEndPoint();
        public abstract Vector2 GetCurrentEndPoint();
        public abstract float GetLength();
        public List<KeyValuePair<float, Vector2>> points;
    }
}
