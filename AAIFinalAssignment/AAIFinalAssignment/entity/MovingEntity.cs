using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;

namespace AAIFinalAssignment.entity
{
    public abstract class MovingEntity : BaseEntity
    {
        public abstract Vector2 velocity { get; set; }


        public List<SteeringBehaviour> steeringBehaviours = new List<SteeringBehaviour>();

        public override void Update(GameTime gameTime)
        {
            if (steeringBehaviours.Count != 0)
            {
                Vector2 steering = new Vector2();
                foreach (SteeringBehaviour steeringBehaviour in steeringBehaviours)
                {
                    Vector2 behaviourResultingVector = steeringBehaviour.CalculateResultingVector();

                    steering = Vector2.Add(steering, behaviourResultingVector);
                }


                if (steering.LengthSquared() > Math.Pow(Settings.MaxAccel, 2))
                {
                    steering.Normalize();
                    steering = steering * Settings.MaxAccel;
                }
                velocity = velocity * Settings.Drag + steering;

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
                    if (max.LengthSquared() < velocity.LengthSquared())
                    {
                        velocity = new Vector2(max.X, max.Y);

                    }
                    else if (min.LengthSquared() > velocity.LengthSquared())
                    {
                        velocity = min;
                    }


                    Vector2 NewPosition = Vector2.Add(Position, velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);


                    if (float.IsNaN(NewPosition.X) || float.IsInfinity(NewPosition.X) || float.IsNaN(NewPosition.Y) || float.IsInfinity(NewPosition.Y))
                    {

                    }

                    foreach (Obstacle obstacle in Game1.GetObstaclesInRange(NewPosition))
                    {
                        if (obstacle.DoIHit(NewPosition))
                        {
                            velocity = Vector2.Zero;
                            return;
                        }
                    }
                    

                    Position = NewPosition;

                    Position = Game1.getWithinField(Position);
                }






            }
        }
    }
}
