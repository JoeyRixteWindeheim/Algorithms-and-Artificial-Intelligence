using AAIFinalAssignment.behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.entity
{
    public class Obstacle : BaseEntity
    {
        public override Vector2 Position { get; set; }
        public override Texture2D Texture { get; set; }

        public int Radius { get; set; }

        public override void LoadContent(ContentManager Content)
        {
            
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            BehaviourUtil.RenderCircle(_spriteBatch, Position, Radius, Color.Green);
        }

        public bool DoIHit(Vector2 vector)
        {
            return Vector2.DistanceSquared(vector, Position) < Math.Pow(Radius, 2);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
