using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.Pathfinding
{
    public class AStar
    {
        public static Vector2[] Run(Vector2 location, Vector2 target)
        {
            return PathfindingUtils.Run(location, target, GetDistance);
        }

        private static float GetDistance(Vector2 start, Vector2 current, Vector2 end)
        {
            return Vector2.DistanceSquared(end, Game1.GetClosestCoords(end, current));
        }
    }
}
