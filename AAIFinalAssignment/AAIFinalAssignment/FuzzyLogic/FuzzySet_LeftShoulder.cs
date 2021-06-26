using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.FuzzyLogic
{
    class FuzzySet_LeftShoulder : FuzzySet
    {
        // Shape values of FLV
        double peakPoint;
        double leftOffset;
        double rightOffset;

        public FuzzySet_LeftShoulder(double mid, double left, double right) : base(((mid + left) + mid) / 2)
        {
            peakPoint = mid;
            leftOffset = left;
            rightOffset = right;
        }

        public override double CalculateDOM(double valueOfFLV)
        {
            // check for 0 offset
            if (rightOffset == 0 && valueOfFLV == peakPoint)
            {
                degreeOfMembership = 1.0;
            }

            // find DOM if left of center -> then its plateaud on 1
            if ((valueOfFLV <= peakPoint) && (valueOfFLV >= (peakPoint - leftOffset)))
            {
                degreeOfMembership = 1.0;
            }
            // if right of center, return 1 because plateau'd on 1
            else if (valueOfFLV > peakPoint)
            {
                double grad = 1.0 / -rightOffset;
                degreeOfMembership = grad * (valueOfFLV - peakPoint) + 1;
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
