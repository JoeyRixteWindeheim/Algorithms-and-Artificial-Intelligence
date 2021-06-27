
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
        WanderBehaviour wanderBehaviour;
        bool isWandering = false;
        public override ExecutionState OnStateEnter()
        {
            ownerFiniteStateMachine.LookingForFood = true;
            return base.OnStateEnter();
        }
        public FindFoodState(EntityStateMachine ownerFiniteStateMachine) : base(ownerFiniteStateMachine)
        {
            SeekBehaviour seekBehaviour = new SeekBehaviour(OwnerEntity.SeekTarget, OwnerEntity);
            wanderBehaviour = new WanderBehaviour(OwnerEntity);
            steeringBehaviours.Add(seekBehaviour);
            steeringBehaviours.Add(new DistancingBehaviour(OwnerEntity));
            steeringBehaviours.Add(new ObstacleAvoidance(OwnerEntity));
            Name = "FindFood";
            
            // If the entity doesn't have a target for some reason, try to find one
            if (OwnerEntity.SeekTarget == null)
            {
                // try to find new target
                seekBehaviour.NewTarget(OwnerEntity.SeekTarget);
                // If it's still Null, no targets exist

            }
        }

        public override void OnStateUpdate(GameTime gameTime)
        {

            // If the seekTarget is null, there are no valid targets. In that case,
            // default back to wandering
            // SeekTarget automatically tries to get a new target in the getter
            if (OwnerEntity.SeekTarget == null && !isWandering)
            {
                steeringBehaviours.Add(wanderBehaviour);
                isWandering = true;
            }
            else
            {
                // If it found a target, no longer wander
                steeringBehaviours.Remove(wanderBehaviour);
                isWandering = false;
                SeekBehaviour seekbehaviour = (SeekBehaviour)steeringBehaviours[0];
                seekbehaviour.NewTarget(OwnerEntity.SeekTarget);
            }
            base.OnStateUpdate(gameTime);
        }

        public override ExecutionState OnStateEnd()
        {
            return base.OnStateEnd();
        }

    }
}
