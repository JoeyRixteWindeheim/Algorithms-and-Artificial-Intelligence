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
        public double drag = 0.99;
        public override Vector2 Position { get; set; }
        public override Texture2D Texture { get; set; }

        

        public Vehicle( Vector2 Position)
        {
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
                Vector2 steering = new Vector2();
                foreach (SteeringBehaviour steeringBehaviour in steeringBehaviours)
                {
                    Vector2 behaviourResultingVector = steeringBehaviour.CalculateResultingVector();

                    steering = Vector2.Add(steering, behaviourResultingVector);
                }


                if(steering.LengthSquared() > Math.Pow(Settings.MaxAccel, 2))
                {
                    steering.Normalize();
                    steering = steering * Settings.MaxAccel;
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
                    Vector2 max = direction * Settings.MaxSpeed;
                    Vector2 min = direction * Settings.MinSpeed;
                    if(max.LengthSquared() < velocity.LengthSquared())
                    {
                        velocity = new Vector2(max.X,max.Y);

                    }
                    else if (min.LengthSquared() > velocity.LengthSquared())
                    {
                        velocity = min;
                    }
                    

                    Vector2 NewPosition = Vector2.Add(Position, velocity* (float)gameTime.ElapsedGameTime.TotalSeconds);

                    foreach(Obstacle obstacle in Game1.GetObstaclesInRange(35, NewPosition))
                    {
                        if (obstacle.DoIHit(NewPosition))
                        {
                            velocity = Vector2.Zero;
                            return;
                        }
                    }
                    Position = NewPosition;
                    
                    if (Position.X > Game1.MaxCoords)
                    {
                        Position = new Vector2(Position.X - Game1.Mapsize, Position.Y);
                    }
                    if (Position.Y > Game1.MaxCoords)
                    {
                        Position = new Vector2(Position.X, Position.Y - Game1.Mapsize);
                    }
                    if (Position.X < Game1.MinCoords)
                    {
                        Position = new Vector2(Position.X + Game1.Mapsize, Position.Y);
                    }
                    if (Position.Y < Game1.MinCoords)
                    {
                        Position = new Vector2(Position.X, Position.Y + Game1.Mapsize);
                    }
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
            if (Settings.RenderBehaviour)
            {
                foreach (SteeringBehaviour behaviour in steeringBehaviours)
                {
                    behaviour.Render(gameTime, _spriteBatch);
                }
                BehaviourUtil.RenderVector(_spriteBatch, velocity, Position, 0.01, Color.Red);
            }
        }

    }
}
