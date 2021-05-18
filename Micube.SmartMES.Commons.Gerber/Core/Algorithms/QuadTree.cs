﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace Micube.SmartMES.Commons.Gerber.Core.Algorithms
{
    public interface IQuadTreeItem
    {
        double GetX();

        double GetY();
    }

    public class QuadTreeNode
    {
        public int contained = 0;

        public double xstart = 0;
        public double ystart = 0;
        public double xend = 0;
        public double yend = 0;

        public List<QuadTreeNode> Children = new List<QuadTreeNode>(4);
        public List<IQuadTreeItem> Items = new List<IQuadTreeItem>(3);

        public bool CallBackInside(RectangleF S, Func<IQuadTreeItem, bool> callback)
        {
            if (S.X >= xend || S.Y >= yend || S.X + S.Width < xstart || S.Y + S.Height < ystart)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    var a = Items[i];
                    if (S.Contains((float)a.GetX(), (float)a.GetY()))
                    {
                        if (callback(a) == false) return false;
                    }
                }

                for (int i = 0; i < Children.Count; i++)
                {
                    Children[i].CallBackInside(S, callback);
                }
            }

            return true;
        }

        public void Insert(IQuadTreeItem Item, int maxdepth) => Insert(Item.GetX(), Item.GetY(), Item, maxdepth);
        
        public void Insert(double x, double y, IQuadTreeItem Item, int maxdepth)
        {
            if (x < xstart || y < ystart || x >= xend || y >= yend)
            {
                return;
            }

            contained++;

            if (maxdepth > 0)
            {
                if (Children.Count == 0) Split();
                for (int i = 0; i < Children.Count; i++)
                {
                    Children[i].Insert(x, y, Item, maxdepth - 1);
                }
            }
            else
            {
                Items.Add(Item);
            }
        }

        public void Draw(IGraphicsInterface graphics)
        {
            graphics.DrawRectangle(contained > 0 ? Color.Red : Color.Yellow, (float)xstart, (float)ystart, (float)xend - (float)xstart , (float)yend - (float)ystart , (float)((xend-xstart)*0.001));
            foreach (var C in Children)
            {
                C.Draw(graphics);
            }
        }

        public void DrawArt(IGraphicsInterface graphics, Color Col)
        {
            if (contained == 0)
            {
                graphics.FillRectangle(Col, (float)xstart, (float)ystart, (float)xend - (float)xstart - 1, (float)yend - (float)ystart - 1);
            }

            foreach (var C in Children)
            {
                C.DrawArt(graphics, Col);
            }
        }

        private void Split()
        {
            double halfy = (yend + ystart) / 2.0;
            double halfx = (xend + xstart) / 2.0;

            QuadTreeNode QTN1 = new QuadTreeNode() { xstart = xstart, ystart = ystart, xend = halfx, yend = halfy };
            QuadTreeNode QTN2 = new QuadTreeNode() { xstart = halfx, ystart = ystart, xend = xend, yend = halfy };
            QuadTreeNode QTN3 = new QuadTreeNode() { xstart = halfx, ystart = halfy, xend = xend, yend = yend };
            QuadTreeNode QTN4 = new QuadTreeNode() { xstart = xstart, ystart = halfy, xend = halfx, yend = yend };
            Children.Add(QTN1);
            Children.Add(QTN2);
            Children.Add(QTN3);
            Children.Add(QTN4);
        }
    }
}


