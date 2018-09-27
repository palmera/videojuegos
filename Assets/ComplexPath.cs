using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.U2D.TriangleNet.Geometry;

namespace Assets
{
    public class ComplexPath : IPath
    {
        private List<IPath> paths;
        private IPath currentPath;
        public ComplexPath()
        {
            paths = new List<IPath>();
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

        override public Point GetPos(float s)
        {
            Point resultingPos = new Point();
            float totalLength = this.GetLength(); 
            foreach (IPath path in paths)
            {
                if (path.sStart < s && path.sEnd > s)
                {
                    this.currentPath = path;
                    float dif = path.sEnd - path.sStart;
                    float current = s - path.sStart;
                    float pos = current / dif;

                    Point relativePos = path.GetPos(pos);
                    resultingPos.X += relativePos.X;
                    resultingPos.Y += relativePos.Y;
                }
            }

            return resultingPos;
        }

        public override Point GetEndPoint()
        {
            return paths.Last<IPath>().GetEndPoint();
        }

        public override Point GetCurrentEndPoint()
        {
            return this.currentPath.GetEndPoint();
        }
    }
}
