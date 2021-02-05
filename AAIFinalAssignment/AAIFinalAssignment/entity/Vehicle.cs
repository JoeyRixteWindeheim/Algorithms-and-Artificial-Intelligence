using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AAIFinalAssignment.behaviour;

namespace AAIFinalAssignment.entity
{
    public class Vehicle : MovingEntity
    {
        public override Vector2 velocity { get; set; }
        public override double mass { get; set; }
        public override double maxSpeed { get; set; }
        public override Vector2 Position { get; set; }
        public override Texture2D Texture { get; set; }

        public Vehicle(double mass, double maxSpeed, Vector2 Position)
        {
            this.mass = mass;
            this.maxSpeed = maxSpeed;
            this.Position = Position;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("circle");
        }

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine(Position);
            if (steeringBehaviours.Count != 0)
            {
                foreach (SteeringBehaviour steeringBehaviour in steeringBehaviours)
                {
                    Vector2 behaviourResultingVector = steeringBehaviour.CalculateResultingVector();
                    velocity = Vector2.Add(velocity, behaviourResultingVector);
                }

                // check if velocity > 0 before moving
                if (velocity.Length() != 0)
                {
                    //TODO: FIX
                    //Vector2.Divide(velocity, mass);
                    Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);

                    // constraints velocity
                    // Cannot edit velocity X and Y directly. Roundabout way of doing this is making a new vector2
                    if (velocity.X > maxSpeed)
                    { velocity = new Vector2((float)maxSpeed, velocity.Y); }
                    if (velocity.Y > maxSpeed)
                    { velocity = new Vector2(velocity.X, (float)maxSpeed); }

                    Position = Vector2.Add(Position, velocity);
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

        }

    }
}
