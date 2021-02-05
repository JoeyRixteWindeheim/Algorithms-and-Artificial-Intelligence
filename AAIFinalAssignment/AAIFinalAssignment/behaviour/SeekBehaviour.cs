using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using AAIFinalAssignment.entity;

namespace AAIFinalAssignment.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public BaseEntity Target { get; set; }
        public SeekBehaviour(BaseEntity target, MovingEntity ownEntity) : base(ownEntity)
        {
            Target = target;
            urgency = 1;
        }

        public override Vector2 CalculateResultingVector()
        {
            return BehaviourUtil.CalculateSeekVector(ownEntity.Position, Target.Position);
        }

        // TODO: Implement
        protected override bool CheckIfShouldEnable()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckIfShouldDisable()
        {
            throw new NotImplementedException();
        }
    }
}
