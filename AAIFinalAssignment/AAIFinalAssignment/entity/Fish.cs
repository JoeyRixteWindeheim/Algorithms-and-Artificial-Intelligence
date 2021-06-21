using AAIFinalAssignment.behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AAIFinalAssignment.entity
{
    public class Fish : MovingEntityWithStates
    {
        public override Vector2 velocity { get; set; }
        public override Vector2 Position { get; set; }
        public override Texture2D Texture { get; set; }

        public int TextureNumber { get; set; }

        public float AnimationOfset { get; set; }

        public Fish(Vector2 Position)
        {
            this.Position = Position;
            AnimationOfset = (float)Game1.Random.NextDouble()*2;
            TextureNumber = Game1.Random.Next(1, 8);
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            
            List<Vector2> randomVecList = Game1.CalculateRenderPosition(Position);
            foreach (Vector2 position in Game1.CalculateRenderPosition(Position))
            {
                
                Game1.FishSprites.RenderFish((float)gameTime.TotalGameTime.TotalSeconds + AnimationOfset, _spriteBatch, velocity, position, TextureNumber);
                 
                
                

                if (Settings.RenderBehaviour)
                {
                    foreach (SteeringBehaviour behaviour in steeringBehaviours)
                    {
                        behaviour.Render(gameTime, _spriteBatch, position);
                    }
                    BehaviourUtil.RenderVector(_spriteBatch, velocity, position, 0.01, Color.Red);
                }

            }
        }

    }
}
