using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine.states
{
    public class FleeFromObjectState : VehicleState
    {

        public FleeFromObjectState(VehicleFiniteStateMachine ownerFiniteStateMachine, BaseEntity targetToFleeFrom) : base(ownerFiniteStateMachine)
        {
            steeringBehaviours.Add(new FleeBehaviour(targetToFleeFrom, ownerFiniteStateMachine.OwnerVehicle));
        }

        public override void OnStateUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
