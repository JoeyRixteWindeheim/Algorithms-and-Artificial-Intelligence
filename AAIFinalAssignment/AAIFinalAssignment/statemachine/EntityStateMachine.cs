using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine
{
    public class EntityStateMachine : FiniteStateMachine
    {

        public MovingEntityWithStates OwnerEntity { get; set; }

        public EntityStateMachine(MovingEntityWithStates ownerVehicle)
        {
            OwnerEntity = ownerVehicle;
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
