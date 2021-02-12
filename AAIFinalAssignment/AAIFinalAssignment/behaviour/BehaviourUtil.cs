using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    public class BehaviourUtil
    {
        public static Vector2 CalculateSeekVector(Vector2 start, Vector2 target)
        {
            Vector2 resultingVector = new Vector2(0, 0);
            // Get the distance between self and target
            if (target != start)
            {
                resultingVector = Vector2.Subtract(target, start);


                // Increase vector if far away, decrease when near
                double urgencyFactor = (resultingVector.Length() + 5) / (resultingVector.Length() + 2);

                // Normalise distance -> convert to direction
                resultingVector.Normalize();

                resultingVector = Vector2.Multiply(resultingVector, (Single)urgencyFactor);


            }

            // If the method reaches this point, the vehicle is at the position of the target.
            // So, there's no point in moving

            return resultingVector;



        }
    }
}
