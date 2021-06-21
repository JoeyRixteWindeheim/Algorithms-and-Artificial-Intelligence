using AAIFinalAssignment.Grid;
using System.Collections.Generic;

namespace AAIFinalAssignment.Pathfinding
{
    public class PriorityQueue
    {
        private Node _root;

        public void Add(float key, Region value)
        {
            Node before = null, after = _root;

            while (after != null)
            {
                before = after;
                if (key < after.Key) //Is new node in left tree? 
                    after = after.Smaller;
                else if (key > after.Key) //Is new node in right tree?
                    after = after.Bigger;
                else
                {
                    after.Regions.Add(value);
                    return;
                }
            }

            Node newNode = new Node();
            newNode.Key = key;
            newNode.Regions.Add(value);

            if (_root == null)//Tree ise empty
                _root = newNode;
            else
            {
                if (key < before.Key)
                    before.Smaller = newNode;
                else
                    before.Bigger = newNode;
            }
        }
        public Region Pop()
        {
            if (_root == null)
            {
                return null;
            }
            Node smallest = _root;
            Node parent = null;
            while(smallest.Smaller != null)
            {
                parent = smallest;
                smallest = smallest.Smaller;
            }

            Region region = smallest.Regions[0];
            smallest.Regions.RemoveAt(0);
            if(parent == null)
            {
                _root = null;
            }
            else if(smallest.Regions.Count == 0)
            {
                parent.Smaller = smallest.Bigger;
            }

            return region;
        }
    }

    public class Node
    {
        public Node()
        {
            Regions = new List<Region>();
        }
        public Node Smaller { get; set; }
        public Node Bigger { get; set; }


        public float Key { get; set; }
        public List<Region> Regions { get; set; }
    }
}




