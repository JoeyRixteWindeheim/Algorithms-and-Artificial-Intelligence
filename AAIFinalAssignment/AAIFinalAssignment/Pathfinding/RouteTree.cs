using AAIFinalAssignment.Grid;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.Pathfinding
{
    public class RouteTree
    {
        private Dictionary<Region, RouteTreeNode> tree;

        public RouteTree()
        {
            tree = new Dictionary<Region, RouteTreeNode>();
        }

        public bool Contains(Region region)
        {
            return tree.ContainsKey(region);
        }

        public void AddFirst(Region region)
        {
            RouteTreeNode node = new RouteTreeNode();
            node.DistanceToOrigin = 0;
            node.Parent = null;
            node.edge = null;
            tree.Add(region, node);
        }

        public void Add(Region region,Region parent,int distance, Edge edge)
        {

            distance = tree[parent].DistanceToOrigin + distance;
            if (tree.ContainsKey(region))
            {
                RouteTreeNode node = tree[region];
                
                if (node.DistanceToOrigin > distance)
                {
                    node.DistanceToOrigin = distance;
                    node.Parent = parent;
                    node.edge = edge;
                    edge.color = Color.DarkRed;
                }
            }
            else
            {
                RouteTreeNode node = new RouteTreeNode();
                node.DistanceToOrigin = distance;
                node.Parent = parent;
                node.edge = edge;
                edge.color = Color.Red;
                tree.Add(region, node);
            }
        }

        public Vector2[] GetPath(Region from)
        {
            List<Vector2> returnList = new List<Vector2>();
            Region CurrentRegion = from;
            while(CurrentRegion != null)
            {
                returnList.Add(CurrentRegion.Middle);
                if (tree[CurrentRegion].edge != null)
                    tree[CurrentRegion].edge.color = Color.Green;
                CurrentRegion = tree[CurrentRegion].Parent;
            }
            var returnArray = returnList.ToArray();
            Array.Reverse(returnArray);
            return returnArray;
        }
    }

    public class RouteTreeNode
    {
        public Region Parent;
        public int DistanceToOrigin;
        public Edge edge;
    }
}
