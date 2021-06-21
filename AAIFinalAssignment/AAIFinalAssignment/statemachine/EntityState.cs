using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine
{
    public abstract class EntityState : State
    {

        internal MovingEntityWithStates OwnerEntity { get; set; }

        public List<SteeringBehaviour> steeringBehaviours = new List<SteeringBehaviour>();

        public EntityState(EntityStateMachine ownerFiniteStateMachine)
        {
            OwnerEntity = ownerFiniteStateMachine.OwnerEntity;
        }
          
        public override void OnStateUpdate(GameTime gameTime)
        {
            // Get all current behaviors and calculate the resulting vector from all of those behaviors.
            // Make the vehicle move according to the resulting vector.
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
                OwnerEntity.velocity = OwnerEntity.velocity * Settings.Drag + steering;

                // check if velocity > 0 before moving
                if (OwnerEntity.velocity.Length() != 0)
                {
                    //TODO: FIX
                    //Vector2.Divide(velocity, mass);
                    //velocity = Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);

                    // constraints velocity
                    // Cannot edit velocity X and Y directly. Roundabout way of doing this is making a new vector2

                    Vector2 direction = new Vector2(OwnerEntity.velocity.X, OwnerEntity.velocity.Y);
                    direction.Normalize();
                    Vector2 max = direction * Settings.MaxSpeed;
                    Vector2 min = direction * Settings.MinSpeed;
                    if (max.LengthSquared() < OwnerEntity.velocity.LengthSquared())
                    {
                        OwnerEntity.velocity = new Vector2(max.X, max.Y);

                    }
                    else if (min.LengthSquared() > OwnerEntity.velocity.LengthSquared())
                    {
                        OwnerEntity.velocity = min;
                    }


                    OwnerEntity.Position = Vector2.Add(OwnerEntity.Position, OwnerEntity.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    OwnerEntity.Position = Game1.getWithinField(OwnerEntity.Position);
                }
            }
        }
    }
}
