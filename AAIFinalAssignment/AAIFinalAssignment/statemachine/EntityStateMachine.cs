using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public void Render(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            if(CurrentState is EntityState)
            {
                ((EntityState)CurrentState).RenderBehaviour(gameTime, spriteBatch, position);
            }
        }
    }
}
