using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine.states
{
    class FindFoodState : VehicleState
    {
        public FindFoodState(VehicleFiniteStateMachine ownerFiniteStateMachine) : base(ownerFiniteStateMachine)
        {
            steeringBehaviours.Add(new SeekBehaviour(OwnerVehicle.SeekTarget, OwnerVehicle));
            steeringBehaviours.Add(new FlockingBehaviour(OwnerVehicle.RunningGame, 100, OwnerVehicle));
        }

    }
}
