using AAIFinalAssignment.behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.entity
{
    class FishFood : BaseEntity
    {
        public override Vector2 Position { get; set; }
        public override Texture2D Texture { get; set; }

        public FishFood(Vector2 position)
        {
            Position = position;
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            foreach (Vector2 position in Game1.CalculateRenderPosition(Game1.getWithinField(Position)))
                BehaviourUtil.RenderCircle(_spriteBatch, position, 5, Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
