using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine.states
{
    class SchoolTogetherState : EntityState
    {
        public SchoolTogetherState(EntityStateMachine ownerFiniteStateMachine) : base(ownerFiniteStateMachine)
        {
            steeringBehaviours.Add(new WanderBehaviour(OwnerEntity));
            steeringBehaviours.Add(new DistancingBehaviour(OwnerEntity));
            steeringBehaviours.Add(new GroupPressureBehaviour(OwnerEntity));
            steeringBehaviours.Add(new ObstacleAvoidance(OwnerEntity));
            Name = "SchoolTogetherState";
        }

    }
}
