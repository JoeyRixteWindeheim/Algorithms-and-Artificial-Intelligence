using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.statemachine;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine
{
    public enum ExecutionState
    {
        NONE,
        ACTIVE,
        COMPLETED,
        TERMINATED
    }

    public abstract class State
    {
        public ExecutionState executionState { get; protected set; } = ExecutionState.NONE;

        public virtual ExecutionState OnStateEnter()
        {
            executionState = ExecutionState.ACTIVE;
            return executionState;
        }

        public abstract void OnStateUpdate(GameTime gameTime);

        public virtual ExecutionState OnStateEnd()
        {
            executionState = ExecutionState.COMPLETED;
            return executionState;
        }
    }
}
