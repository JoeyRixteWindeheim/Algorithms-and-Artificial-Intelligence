using AAIFinalAssignment.Grid;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.Pathfinding
{
    public class PriorityQueueDumb
    {

        private SortedRegionListNode Top;

        public void Add(float sortValue, Region value)
        {
            SortedRegionListNode added = new SortedRegionListNode(sortValue, value);

            if(Top == null)
            {
                Top = added;
                return;
            }

            SortedRegionListNode current = Top;

            while(current.Next != null && current.Next.SortValue < added.SortValue)
            {
                current = current.Next;
            }

            added.Next = current.Next;
            current.Next = added;
        }

        public Region Pop()
        {
            if (Top == null)
                return null;

            Region returnValue = Top.Value;
            Top = Top.Next;
            return returnValue;
        }
    }

    public class SortedRegionListNode
    {
        public SortedRegionListNode Next;
        public float SortValue;
        public Region Value;

        public SortedRegionListNode(float sortValue, Region value){
            SortValue = sortValue;
            Value = value;
        }
    }

}
