using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    public class WanderBehaviour : SteeringBehaviour
    {
        public WanderBehaviour(MovingEntity ownEntity) : base(ownEntity)
        {

        }


        public Vector2 wanderVector;
        public Vector2 CurrentVector;

        private DateTime lastupdate = DateTime.MinValue;

        public override Vector2 CalculateResultingVector()
        {
            if(DateTime.Now - lastupdate < TimeSpan.FromMilliseconds(1000))
            {
                return CurrentVector;
            }
            lastupdate = DateTime.Now;
            wanderVector = ownEntity.velocity;

            if (wanderVector == Vector2.Zero)
            {
                wanderVector.X = ((float)Game1.Random.Next(-100, 100))+0.05f;
                wanderVector.Y = ((float)Game1.Random.Next(-100, 100))+0.01f;
            }


            wanderVector.Normalize();

            wanderVector *= 2;

            CurrentVector = new Vector2(0,0);

            CurrentVector.X += Game1.Random.Next(-100, 100);
            CurrentVector.Y += Game1.Random.Next(-100, 100);

            CurrentVector.Normalize();

            CurrentVector = CurrentVector + wanderVector;
            
            return CurrentVector;

        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch, Vector2 Position)
        {
            if (Settings.RenderWander)
            {
                //BehaviourUtil.RenderCircle(_spriteBatch, wanderVector + ownEntity.Position, 10, Color.Yellow);
                BehaviourUtil.RenderVector(_spriteBatch, CurrentVector, Position, 20, Color.Yellow);
            }
                
        }
    }
}
