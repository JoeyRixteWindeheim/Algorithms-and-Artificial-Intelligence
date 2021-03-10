using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.Grid
{
    public class MapGrid
    {
        public Dictionary<Vector2,Region> Regions { get; set; }
        private List<Edge> Edges;

        private int[] xn = new int[] { 1, 0, -1, -1, -1, 0, 1, 1 };
        private int[] yn = new int[] { 1, 1, 1, 0, -1, -1, -1, 0 };

        public List<Obstacle> Obstacles;

        public float RegionSize { get; set; }
        public int diagonal;

        public MapGrid(int amount) 
        {
            Regions = new Dictionary<Vector2, Region>();
            Edges = new List<Edge>();

            RegionSize = Game1.Mapsize / amount;
            diagonal = (int)Math.Round( Math.Sqrt(Math.Pow(RegionSize, 2) * 2));

            //generate regions
            for(int X = 0; X < amount; X++)
            {
                for (int Y = 0; Y < amount; Y++)
                {
                    Vector2 position = new Vector2(X * RegionSize, Y * RegionSize);
                    Region region = new Region(RegionSize, position);
                    Regions.Add(position, region);
                }
            }

            //add neighbours to regions
            
            foreach (KeyValuePair<Vector2,Region> region in Regions)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 vector = new Vector2(region.Value.Middle.X + RegionSize * xn[i], region.Value.Middle.Y + RegionSize * yn[i]);
                    vector = Game1.getWithinField(vector);
                    Region neighbour = getRegion(vector);
                    if (neighbour != null)
                    {
                        Edge edge = region.Value.AddNeighbour(neighbour);
                        if (i % 2 == 0)
                            edge.Length = diagonal;
                        else
                            edge.Length = (int)RegionSize;

                        Edges.Add(edge);
                    }
                }
            }
        }

        public Region getRegion(Vector2 vector2)
        {
            vector2 = Game1.getWithinField(vector2);
            vector2.X -= vector2.X % RegionSize;
            vector2.Y -= vector2.Y % RegionSize;

            if (Regions.ContainsKey(vector2))
                return Regions[vector2];
            else
                return null;
        }

        public void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            foreach (Edge edge in Edges)
                edge.Render(gameTime, _spriteBatch);
        }

        public void ResetGridColor()
        {
            foreach (Edge edge in Edges)
                edge.color = Color.White;
        }
    }
}
