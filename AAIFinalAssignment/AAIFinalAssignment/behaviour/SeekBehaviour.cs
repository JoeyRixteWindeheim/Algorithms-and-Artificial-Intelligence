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


        // TODO: Implement urgency.

        public SeekBehaviour(BaseEntity target, MovingEntity ownEntity) : base(ownEntity)
        {
            Target = target;
            urgency = 1;
        }

        public override Vector2 CalculateResultingVector()
        {
            Vector2 resultingVector = new Vector2(0, 0);
            // Get the distance between self and target
            if (Target.Position != ownEntity.Position)
            {
                resultingVector = Vector2.Subtract(Target.Position, ownEntity.Position);

                
                // Increase vector if far away, decrease when near
                double urgencyFactor = Math.Pow(resultingVector.Length(), 2) / 1000;

                // Normalise distance -> convert to direction
                resultingVector.Normalize();

                resultingVector = Vector2.Multiply(resultingVector, (Single)urgencyFactor);

                //TODO: Speed not capped because I can't seem to override resultingVector.
                if (resultingVector.X > ownEntity.maxSpeed)
                { 
                    resultingVector = new Vector2((float)ownEntity.maxSpeed, resultingVector.Y); 
                }
                if (resultingVector.Y > ownEntity.maxSpeed)
                { resultingVector = new Vector2(resultingVector.X, (float)ownEntity.maxSpeed); }

                
            }

            // If the method reaches this point, the vehicle is at the position of the target.
            // So, there's no point in moving

            return resultingVector;



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
