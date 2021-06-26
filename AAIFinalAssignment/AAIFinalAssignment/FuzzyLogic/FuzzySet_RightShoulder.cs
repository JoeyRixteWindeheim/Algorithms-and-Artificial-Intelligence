using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.FuzzyLogic
{
    class FuzzySet_RightShoulder : FuzzySet
    {
        // Shape values of FLV
        double peakPoint;
        double leftOffset;
        double rightOffset;

        public FuzzySet_RightShoulder(double mid, double left, double right) : base(((mid + right) + mid) / 2)
        {
            peakPoint = mid;
            leftOffset = left;
            rightOffset = right;
        }

        public override double CalculateDOM(double valueOfFLV)
        {
            // check for 0 offset
            if (leftOffset == 0 && valueOfFLV == peakPoint)
            {
                degreeOfMembership = 1.0;
            }

            // find DOM if left of center
            if ((valueOfFLV <= peakPoint) && (valueOfFLV >= (peakPoint - leftOffset)))
            {
                double grad = 1.0 / leftOffset;
                degreeOfMembership = grad * (valueOfFLV - (peakPoint -= leftOffset));
            }
            // if right of center, return 1 because plateau'd on 1
            else if (valueOfFLV > peakPoint)
            {
                degreeOfMembership = 1.0;
            }
            // if out of range, DOM is 0 
            else
            {
                degreeOfMembership = 0.0;
            }

            return degreeOfMembership;
        }
    }
}
