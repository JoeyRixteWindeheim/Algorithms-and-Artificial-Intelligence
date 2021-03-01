using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.Pathfinding;

namespace AAIFinalAssignment
{
    public class ConsoleFunctions
    {
        public static void AddObstacle(int x,int y, int size)
        {
            Obstacle obstacle = new Obstacle();
            obstacle.Position = new Vector2(x, y);
            obstacle.Radius = size / 2;
            Game1.AddObstacle(obstacle);
        }

        public static void RunDijkstra(int x1,int y1,int x2, int y2)
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
