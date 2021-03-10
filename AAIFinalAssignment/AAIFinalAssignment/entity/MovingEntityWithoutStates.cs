using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;

namespace AAIFinalAssignment.entity
{
    public abstract class MovingEntityWithoutStates : MovingEntity
    {
        public List<SteeringBehaviour> steeringBehaviours = new List<SteeringBehaviour>();
    }
}
