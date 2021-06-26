using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.FuzzyLogic
{
    class FuzzySet_Triangle : FuzzySet
    {
        // Shape values of FLV
        double peakPoint;
        double leftOffset;
        double rightOffset;

        public FuzzySet_Triangle(double mid, double left, double right) : base(mid)
        {
            peakPoint = mid;
            leftOffset = left;
            rightOffset = right;
        }

        public override double CalculateDOM(double valueOfFLV)
        {
            // Test for left or right offset being 0, Avoids dividing by 0
            if (rightOffset == 0 && peakPoint == valueOfFLV ||
                leftOffset == 0 && peakPoint == valueOfFLV)
            {
                degreeOfMembership = 1.0;
            }

            // find DOM if value is left of (or equal to) peak
            // also needs to be within bounds of the FLV
            if ((valueOfFLV <= peakPoint) && (valueOfFLV >= (peakPoint - leftOffset)))
            {
                double grad = 1.0 / leftOffset;
                degreeOfMembership = grad * (valueOfFLV - (peakPoint -= leftOffset));
            }
            // else check if it's to the right of the center
            else if ((valueOfFLV > peakPoint) && (valueOfFLV < (peakPoint + leftOffset)))
            {
                double grad = 1.0 / -rightOffset;
                degreeOfMembership = grad * (valueOfFLV - peakPoint) + 1;
            }
            // If neither, then out of range of the FLV, so membership is 0
            else
            {
                degreeOfMembership = 0.0;
            }

            return degreeOfMembership;
        }

        public override void ClearDOM()
        {
            throw new NotImplementedException();
        }

    }
}
