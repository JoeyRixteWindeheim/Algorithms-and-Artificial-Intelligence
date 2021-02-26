using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment
{
    public class ConsoleFunctions
    {
        public static void AddObstacle(int x,int y, int size)
        {
            Obstacle obstacle = new Obstacle();
            obstacle.Position = new Vector2(x, y);
            obstacle.Radius = size / 2;
            Game1.Obstacles.Add(obstacle);
        }
    }
}
