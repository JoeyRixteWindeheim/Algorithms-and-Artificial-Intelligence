using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    public class FleeBehaviour : SteeringBehaviour
    {

        public BaseEntity Target { get; set; }
        public FleeBehaviour(BaseEntity target, MovingEntity ownEntity) : base(ownEntity)
        {
            Target = target;
            urgency = 1;
        }

        public override Vector2 CalculateResultingVector()
        {
            return BehaviourUtil.CalculateSeekVector(ownEntity.Position, Target.Position) * -1; 
        }

        protected override bool CheckIfShouldDisable()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckIfShouldEnable()
        {
            throw new NotImplementedException();
        }
    }
}
