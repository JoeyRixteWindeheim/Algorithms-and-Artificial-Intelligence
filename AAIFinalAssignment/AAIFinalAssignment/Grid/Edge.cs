using AAIFinalAssignment.behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.Grid
{
    public class Edge
    {
        public Region[] Conections;
        public Color color { get; set; }
        public bool active;
        public int Length;
        private Vector2 drawvector;


        public Edge(Region region, Region neighbour)
        {
            Conections = new Region[] {region, neighbour};
            color = Color.White;
            active = true;
            drawvector = Conections[1].Middle - Game1.GetClosestCoords(Conections[1].Middle, Conections[0].Middle);
        }


        public void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            if (Settings.RenderGrid && active)
            {
                

                foreach (Vector2 position in Game1.CalculateRenderPosition(Conections[0].Middle))
                {
                    
                    BehaviourUtil.RenderVectorPrecise(_spriteBatch, drawvector, position, Length, color);

                }
            }
        }
    }
}
