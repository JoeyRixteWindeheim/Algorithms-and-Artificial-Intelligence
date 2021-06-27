using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAIFinalAssignment.statemachine.states
{
    public class NeutralState : EntityState
    {
        DateTime lastTimeSwitched;
        bool isSchoolingTogether = true;
        public NeutralState(EntityStateMachine ownerFiniteStateMachine) : base(ownerFiniteStateMachine)
        {
            Name = "Wander";
            childStateMachine = new EntityStateMachine(OwnerEntity);
            childStateMachine.ParentState = this;
            childStateMachine.SetState(new SchoolTogetherState(childStateMachine));
            
        }

        public override void OnStateUpdate(GameTime gameTime)
        {
            childStateMachine.Update(gameTime);

            if (DateTime.Now.Subtract(lastTimeSwitched).TotalSeconds >= 20)
            {
                if (isSchoolingTogether)
                {
                    childStateMachine.SetState(new DontSchoolTogetherState(childStateMachine));
                }
                else
                {
                    childStateMachine.SetState(new SchoolTogetherState(childStateMachine));
                }
                isSchoolingTogether = !isSchoolingTogether;
                lastTimeSwitched = DateTime.Now;
            }
        }

        public override void Render(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            Vector2 positionToRenderStateText = new Vector2(position.X, position.Y - 15);
            RenderBehaviours(gameTime, spriteBatch, position);
            RenderStates(gameTime, spriteBatch, positionToRenderStateText);
            if (childStateMachine != null)
            {
                childStateMachine.Render(gameTime, spriteBatch, position);
            }
        }

    }
}
