using AAIFinalAssignment.Grid;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.Pathfinding
{
    public delegate float GetDistance(Vector2 start, Vector2 current, Vector2 end);
    public class PathfindingUtils
    {
        public static Vector2[] Run(Vector2 location, Vector2 target, GetDistance getDistance)
        {
            return Run(location, new Vector2[] { target }, getDistance);
        }

        public static Vector2[] Run(Vector2 location, Vector2[] targets, GetDistance getDistance)
        {
            Region StartRegion = Game1.Grid.getRegion(location);

            PriorityQueue sortedRegionList = new PriorityQueue();

            RouteTree routeTree = new RouteTree();
            routeTree.AddFirst(StartRegion);

            List<Region> targetRegions = new List<Region>();
            foreach (Vector2 target in targets)
                targetRegions.Add(Game1.Grid.getRegion(target));

            Region currentRegion = StartRegion;
            while (!targetRegions.Contains(currentRegion))
            {
                for (int i = 0; i < 8; i++)
                {
                    Region region = currentRegion.Neighbours[i];
                    if (region.ContainsObstacle() == false)
                    {
                        if (routeTree.Contains(currentRegion.Neighbours[i]) == false)
                        {
                            var distance = getDistance(StartRegion.Middle, region.Middle, targetRegions[0].Middle);



                            sortedRegionList.Add(distance, region);

                        }
                        routeTree.Add(region, currentRegion, currentRegion.edges[i].Length, currentRegion.edges[i]);



                    }
                }
                currentRegion = sortedRegionList.Pop();
                if (currentRegion == null)
                    return null;
            }

            return routeTree.GetPath(currentRegion);
        }
    }

    


}
