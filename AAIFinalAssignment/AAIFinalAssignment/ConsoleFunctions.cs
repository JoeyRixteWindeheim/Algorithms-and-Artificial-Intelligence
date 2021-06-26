using AAIFinalAssignment.entity;
using AAIFinalAssignment.Pathfinding;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment
{
    public class ConsoleFunctions
    {
        private static int[] x = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 6, 7, 8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 7, 8, 9, 10, 11, 12, 4, 3, 2, 1, 0, -1, -2, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12 };
        private static int[] y = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 1, 1, 1, 1, 1, 1, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 5, 5, 5, 5, 5, 5, 5, 14, 14, 14, 14, 14, 14, 14, 14, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
        public static void AddObstacle(int x, int y, int size)
        {
            Obstacle obstacle = new Obstacle();
            obstacle.Position = new Vector2(x, y);
            obstacle.Radius = size / 2;
            Game1.AddObstacle(obstacle);
        }

        public static void AddObstacles()
        {
            for (int i = 0; i < x.Length; i++)
            {
                AddObstacle((int)(x[i] * Game1.Grid.RegionSize), (int)(y[i] * Game1.Grid.RegionSize), (int)Game1.Grid.RegionSize);
            }
        }

        public static void RunDijkstra(int x1, int y1, int x2, int y2)
        {
            Dijkstra.Run(new Vector2(x1, y1), new Vector2(x2, y2));
        }

        public static void RunAStar(int x1, int y1, int x2, int y2)
        {
            AStar.Run(new Vector2(x1, y1), new Vector2(x2, y2));
        }

        public static void ResetGridColor() => Game1.Grid.ResetGridColor();
    }
}
