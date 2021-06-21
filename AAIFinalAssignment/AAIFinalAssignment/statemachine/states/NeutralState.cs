using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine.states
{
    class NeutralState : VehicleState
    {
        public NeutralState(VehicleFiniteStateMachine ownerFiniteStateMachine) : base(ownerFiniteStateMachine)
        {
            steeringBehaviours.Add(new SeekBehaviour(OwnerVehicle.SeekTarget, OwnerVehicle));
            steeringBehaviours.Add(new DistancingBehaviour(OwnerVehicle));
            steeringBehaviours.Add(new GroupPressureBehaviour(OwnerVehicle));
        }

    }
}
