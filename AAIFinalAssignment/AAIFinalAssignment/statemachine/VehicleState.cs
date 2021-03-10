using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine
{
    public abstract class VehicleState : State
    {

        internal VehicleWithStates OwnerVehicle { get; set; }

        public List<SteeringBehaviour> steeringBehaviours = new List<SteeringBehaviour>();

        public VehicleState(VehicleFiniteStateMachine ownerFiniteStateMachine)
        {
            OwnerVehicle = ownerFiniteStateMachine.OwnerVehicle;
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


                if (steering.LengthSquared() > Math.Pow(OwnerVehicle.maxAccel, 2))
                {
                    steering.Normalize();
                    steering = steering * (Single)OwnerVehicle.maxAccel;
                }
                OwnerVehicle.velocity = OwnerVehicle.velocity * (Single)OwnerVehicle.drag + steering;

                // check if velocity > 0 before moving
                if (OwnerVehicle.velocity.Length() != 0)
                {
                    //TODO: FIX
                    //Vector2.Divide(velocity, mass);
                    //velocity = Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);

                    // constraints velocity
                    // Cannot edit velocity X and Y directly. Roundabout way of doing this is making a new vector2

                    Vector2 direction = new Vector2(OwnerVehicle.velocity.X, OwnerVehicle.velocity.Y);
                    direction.Normalize();
                    Vector2 max = direction * (Single)OwnerVehicle.maxSpeed;
                    Vector2 min = direction * (Single)OwnerVehicle.minSpeed;
                    if (max.LengthSquared() < OwnerVehicle.velocity.LengthSquared())
                    {
                        OwnerVehicle.velocity = new Vector2(max.X, max.Y);

                    }
                    else if (min.LengthSquared() > OwnerVehicle.velocity.LengthSquared())
                    {
                        OwnerVehicle.velocity = min;
                    }


                    OwnerVehicle.Position = Vector2.Add(OwnerVehicle.Position, OwnerVehicle.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }
        }
    }
}
