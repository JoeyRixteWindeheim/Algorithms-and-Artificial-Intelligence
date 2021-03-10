using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AAIFinalAssignment.Grid
{
    public class Region
    {
        public float Size { get; }
        public List<Region> Neighbours { get; set; }
        public List<Edge> edges { get; set; }
        public List<Obstacle> Obstacles { get; }

        
        public Vector2 Position { get; }
        public Vector2 Middle { get; }
        public Region(float size, Vector2 position)
        {
            Size = size;
            Position = position;
            Middle = new Vector2(Position.X + size / 2, Position.Y + size / 2);
            Neighbours = new List<Region>();
            Obstacles = new List<Obstacle>();
            edges = new List<Edge>();
        }

        public bool ContainsObstacle()
        {
            return Obstacles.Count != 0;
        }


        public Edge AddNeighbour(Region neighbour)
        {
            Neighbours.Add(neighbour);
            neighbour.Neighbours.Add(this);
            Edge edge = new Edge(this, neighbour);
            edges.Add(edge);
            neighbour.edges.Add(edge);
            return edge;
        }

        public void AddObstacle(Obstacle obstacle)
        {

            Obstacles.Add(obstacle);
            foreach (Edge edge in edges)
                edge.active = false;
        }
    }
}
