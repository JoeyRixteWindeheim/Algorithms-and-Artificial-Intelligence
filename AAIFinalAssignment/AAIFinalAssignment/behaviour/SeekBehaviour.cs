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

        // This will determine the amount of force added through this behaviour.
        // For example, if an object almost colliding with another, the urgency to avoid others is high.
        // If the object is only slightly moving towards another, the urgency is low.
        // in the first case, you want the entity to move faster than in the second case.
        // Is probably going to get used in fuzzy logic(??)
        // TODO: Implement urgency.
        float urgency;

        public SeekBehaviour(BaseEntity target, MovingEntity ownEntity) : base(ownEntity)
        {
            Target = target;

        }

        public override Vector2 CalculateResultingVector()
        {
            Vector2 resultingVector = Vector2.Subtract(Target.Position, ownEntity.Position);
            resultingVector.Normalize();

            //TODO: Implement urgency.
            System.Diagnostics.Debug.WriteLine(resultingVector);
            return resultingVector;
        }

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
