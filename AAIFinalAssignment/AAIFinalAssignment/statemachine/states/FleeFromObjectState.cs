using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine.states
{
    public class FleeFromObjectState : EntityState
    {

        public FleeFromObjectState(EntityStateMachine ownerFiniteStateMachine, BaseEntity targetToFleeFrom) : base(ownerFiniteStateMachine)
        {
            steeringBehaviours.Add(new FleeBehaviour(targetToFleeFrom, ownerFiniteStateMachine.OwnerEntity));
        }
    }
}
