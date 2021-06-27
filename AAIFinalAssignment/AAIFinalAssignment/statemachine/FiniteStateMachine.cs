using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine
{
    public class FiniteStateMachine
    {
        private State startingState;
        private State previousState;
        private State currentState;

        public virtual void Start()
        {
            if (StartingState != null)
            {
                SetState(StartingState);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (CurrentState != null)
            {
                CurrentState.OnStateUpdate(gameTime);
            }
        }

        #region State Management
        public void SetState(State nextState)
        {
            // Make sure nextState is instantiated
            if (nextState != null)
            {
                if (CurrentState != null)
                {
                    CurrentState.OnStateEnd();
                }

                // used in backing up 
                State BackupPreviousState = previousState;
                previousState = CurrentState;
                CurrentState = nextState;
                ExecutionState stateStatus = CurrentState.OnStateEnter();

                
                // Make sure the state can be entered. If not, rollback to before the state changed
                if (stateStatus == ExecutionState.TERMINATED)
                {
                    CurrentState = previousState;
                    previousState = BackupPreviousState;
                    CurrentState.OnStateEnter();
                }
            }
        }
        #endregion

        protected State StartingState { get => startingState; set => startingState = value; }
        protected State PreviousState { get => previousState; set => previousState = value; }
        protected State CurrentState { get => currentState; set => currentState = value; }
    }
}
