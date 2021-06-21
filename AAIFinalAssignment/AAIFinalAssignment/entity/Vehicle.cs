using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AAIFinalAssignment.behaviour;

namespace AAIFinalAssignment.entity
{
    public class Vehicle : MovingEntityWithoutStates
    {
        public override Vector2 velocity { get; set; }
        public double mass { get; set; }
        public double maxSpeed { get; set; }
        public double minSpeed = 0;
        public double maxAccel = 1;
        public double drag = 0.99;
        public override Vector2 Position { get; set; }
        public override Texture2D Texture { get; set; }
        

        

        public Vehicle(double mass, double maxSpeed, Vector2 Position)
        {
            this.mass = mass;
            this.maxSpeed = maxSpeed;
            this.Position = Position;
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("circle");
        }

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine(Position);
            if (steeringBehaviours.Count != 0)
            {
                Vector2 steering = new Vector2();
                foreach (SteeringBehaviour steeringBehaviour in steeringBehaviours)
                {
                    Vector2 behaviourResultingVector = steeringBehaviour.CalculateResultingVector();

                    steering = Vector2.Add(steering, behaviourResultingVector);
                }


                if(steering.LengthSquared() > Math.Pow(maxAccel, 2))
                {
                    steering.Normalize();
                    steering = steering * (Single)maxAccel;
                }
                velocity = velocity*(Single)drag + steering;

                // check if velocity > 0 before moving
                if (velocity.Length() != 0)
                {
                    //TODO: FIX
                    //Vector2.Divide(velocity, mass);
                    //velocity = Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);

                    // constraints velocity
                    // Cannot edit velocity X and Y directly. Roundabout way of doing this is making a new vector2
                    
                    Vector2 direction = new Vector2(velocity.X, velocity.Y);
                    direction.Normalize();
                    Vector2 max = direction * (Single)maxSpeed;
                    Vector2 min = direction * (Single)minSpeed;
                    if(max.LengthSquared() < velocity.LengthSquared())
                    {
                        velocity = new Vector2(max.X,max.Y);

                    }
                    else if (min.LengthSquared() > velocity.LengthSquared())
                    {
                        velocity = min;
                    }
                    

                    Position = Vector2.Add(Position, velocity* (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

              
                


                
            }
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            // Draws the sprite. origin of sprite is at position; NOT top left of sprite
            _spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                0f,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );
            foreach(SteeringBehaviour behaviour in steeringBehaviours)
            {
                behaviour.Render(gameTime, _spriteBatch);
            }
            BehaviourUtil.RenderVector(_spriteBatch, velocity, Position, 0.01, Color.Red);
        }

    }
}
