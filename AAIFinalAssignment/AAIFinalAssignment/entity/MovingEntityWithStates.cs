using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.statemachine;
using AAIFinalAssignment.statemachine.states;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.entity
{
    public abstract class MovingEntityWithStates : MovingEntity
    {
        public EntityStateMachine BehaviourStateMachine { get; set; }

        public MovingEntityWithStates()
        {
            BehaviourStateMachine = new EntityStateMachine(this);
            BehaviourStateMachine.SetState(new NeutralState(BehaviourStateMachine));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            BehaviourStateMachine.Update(gameTime);
            if (PotencialFood != null)
            {
                foreach (BaseEntity food in PotencialFood)
                {
                    if (Vector2.DistanceSquared(food.Position, Position) < GetEatingRange() * GetEatingRange())
                    {
                        FoodEaten += 10;
                        Game1.RemoveEntity(food);
                        break;
                    }
                }
            }
        }

        // Does nothing, gets handeled in states. Needs to be overwritten.
        public override void UpdateMovement(GameTime gameTime)
        {
        }
    }
}
