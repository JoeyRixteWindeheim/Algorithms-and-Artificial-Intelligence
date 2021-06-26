using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine.states
{
    class FindFoodState : EntityState
    {
        public override ExecutionState OnStateEnter()
        {
            ownerFiniteStateMachine.LookingForFood = true;
            return base.OnStateEnter();
        }
        public FindFoodState(EntityStateMachine ownerFiniteStateMachine) : base(ownerFiniteStateMachine)
        {

/*            steeringBehaviours.Add(new SeekBehaviour(OwnerEntity.SeekTarget, OwnerEntity));
            steeringBehaviours.Add(new DistancingBehaviour(OwnerEntity));
            steeringBehaviours.Add(new ObstacleAvoidance(OwnerEntity));*/
            Name = "FindFood";
            
        }

        public override ExecutionState OnStateEnd()
        {
            ownerFiniteStateMachine.LookingForFood = false;
            return base.OnStateEnd();
        }

    }
}
