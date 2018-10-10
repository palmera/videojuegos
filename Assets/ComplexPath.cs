using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class ComplexPath : IPath
    {
        private List<IPath> paths;
        private IPath currentPath;
        public ComplexPath()
        {
            paths = new List<IPath>();
            points = new List<KeyValuePair<float, Vector2>>();
        }

        public List<IPath> GetPaths()
        {
            return this.paths;
        }

        public void AddPathSegment(IPath newPath)
        {
            paths.Add(newPath);
            float sCounter = 0;
            foreach (IPath path in paths)
            {
                path.sStart = sCounter;
                sCounter += (path.GetLength() / this.GetLength());
                path.sEnd = sCounter;
            }
        }

        override public float GetLength()
        {
            float length = 0;
            foreach (IPath path in paths)
            {
                length += path.GetLength();
            }
            return length;
        }

        override public Vector2 GetPos(float s)
        {
            Vector2 resultingPos = new Vector2();
            foreach (IPath path in paths)
            {
                if (path.sStart < s && path.sEnd > s)
                {
                    this.currentPath = path;
                    float dif = path.sEnd - path.sStart;
                    float current = s - path.sStart;
                    float pos = current / dif;

                    Vector2 relativePos = path.GetPos(pos);
                    resultingPos.x += relativePos.x;
                    resultingPos.y += relativePos.y;
                }
            }

            return resultingPos;
        }

        public override Vector2 GetEndPoint()
        {
            return paths.Last<IPath>().GetEndPoint();
        }

        public override Vector2 GetCurrentEndPoint()
        {
            return this.currentPath.GetEndPoint();
        }
    }
}
