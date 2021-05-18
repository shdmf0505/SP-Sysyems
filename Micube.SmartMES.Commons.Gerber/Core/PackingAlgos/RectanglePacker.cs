using Micube.SmartMES.Commons.Gerber.Core.Primitives;
using System.Collections.Generic;

namespace Micube.SmartMES.Commons.Gerber
{
    public class RectangleD
    {
        //public PointD TopLeft = new PointD(0, 0);
        //public PointD BottomRight = new PointD(0, 0);
        public double Width;
        public double Height;
        public double X;
        public double Y;
    }

    public class RectanglePackerObject : RectangleD
    {
        public object ReferencedObject;
        public RectanglePackerObject lft;
        public RectanglePackerObject rgt;
        public bool used = false;

    }

    public class RectanglePacker
    {
        RectanglePackerObject root = new RectanglePackerObject();
        public List<RectanglePackerObject> AllNodes = new List<RectanglePackerObject>();

        public double GetEmptySpace()
        {
            double R = 0;
            foreach(var a in AllNodes)
            {
                if (a.used == true)
                {
                    R += a.Width * a.Height;
                }
            }

            return (root.Width*root.Height)- R;
        }

        private double usedHeight = 0;
        private double usedWidth = 0;

        public RectanglePacker(double width, double height)
        {
            Reset(width, height);
            AllNodes.Add(root);
        }

        private void Reset(double width, double height)
        {
            root.X = 0;
            root.Y = 0;
            root.Width = width;
            root.Height = height;
            root.lft = null;
            root.rgt = null;

            usedWidth = 0;
            usedHeight = 0;
        }

        private PointD GetDimensions() => new PointD(usedWidth, usedHeight);

        private RectanglePackerObject CloneNode(RectanglePackerObject n)
        {
            var P =new RectanglePackerObject() { ReferencedObject = n.ReferencedObject, X = n.X, Y = n.Y, Width = n.Width, Height = n.Height };
            AllNodes.Add(P);

            return P;
        }

        private PointD RecursiveFindCoords(RectanglePackerObject node, double w, double h)
        {
            if (node.lft != null)
            {
                var coords = RecursiveFindCoords(node.lft, w, h);
                if (coords == null && (node.rgt != null))
                {
                    coords = RecursiveFindCoords(node.rgt, w, h);
                }

                return coords;
            }
            else
            {
                if (node.used || w > node.Width || h > node.Height)
                {
                    return null;
                }

                if (w == node.Width && h == node.Height)
                {
                    node.used = true;
                    return new PointD(node.X, node.Y);
                }

                node.lft = CloneNode(node);
                node.rgt = CloneNode(node);

                if (node.Width - w > node.Height - h)
                {
                    node.lft.Width = w;
                    node.rgt.X = node.X + w;
                    node.rgt.Width = node.Width - w;
                }
                else
                {
                    node.lft.Height = h;
                    node.rgt.Y = node.Y + h;
                    node.rgt.Height = node.Height - h;
                }

                return RecursiveFindCoords(node.lft, w, h);
            }
        }

        public PointD FindCoords(double w, double h)
        {
            var coords = RecursiveFindCoords(root, w, h);
            // var_dump(root);

            if (coords != null)
            {
                if (usedWidth < coords.X + w) usedWidth = coords.X + w;
                if (usedHeight < coords.Y + h) usedHeight = coords.Y + h;
            }

            return coords;
        }
    }
}
