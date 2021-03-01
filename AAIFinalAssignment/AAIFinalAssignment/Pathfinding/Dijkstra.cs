using AAIFinalAssignment.Grid;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.Pathfinding
{
    public class Dijkstra
    {
        public static Vector2[] Run(Vector2 location, Vector2 target)
        {
            return PathfindingUtils.Run(location, target, GetDistance);
        }
        public static Vector2[] Run(Vector2 location, Vector2[] targets)
        {
            return PathfindingUtils.Run(location, targets, GetDistance);
        }

        private static float GetDistance(Vector2 start, Vector2 current, Vector2 end)
        {
            return Vector2.DistanceSquared(start, Game1.GetClosestCoords(start, current));
        }
    }
}
