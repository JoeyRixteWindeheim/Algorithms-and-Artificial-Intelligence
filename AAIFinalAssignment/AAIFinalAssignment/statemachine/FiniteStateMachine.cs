using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine
{
    public class FiniteStateMachine
    {
        State startingState;
        State previousState;
        State currentState;
       

        public virtual void Start()
        {
            if (startingState != null)
            {
                SetState(startingState);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (currentState != null)
            {
                currentState.OnStateUpdate(gameTime);
            }
        }

        #region State Management
        public void SetState(State nextState)
        {
            // Make sure nextState is instantiated
            if (nextState != null)
            {
                // used in backing up 
                State BackupPreviousState = previousState;
                previousState = currentState;
                currentState = nextState;
                ExecutionState stateStatus = currentState.OnStateEnter();
                
                // Make sure the state can be entered. If not, rollback to before the state changed
                if (stateStatus == ExecutionState.TERMINATED)
                {
                    currentState = previousState;
                    previousState = BackupPreviousState;
                    currentState.OnStateEnter();
                }
            }
        }
        #endregion
    }
}
