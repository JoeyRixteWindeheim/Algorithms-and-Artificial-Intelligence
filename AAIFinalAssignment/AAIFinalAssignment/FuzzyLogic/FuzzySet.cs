using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.FuzzyLogic
{
    public abstract class FuzzySet
    {
        protected double degreeOfMembership;

        // On plateau's, this is the center value of the plateau. 
        // On triangles, this is the top of the triangle
        double midPointMembershipValue;

        public FuzzySet(double midPointMembershipValue)
        {
            this.DegreeOfMembership = 0.0;
            this.MidPointMembershipValue = midPointMembershipValue;
        }

        // in the class in the book, it doesn't change the degreeOfMembership variable.
        // Since we only use MaxVal, we _do_ change it in this method.
        // Returns the degree of membership in this set of the given value.
        public abstract double CalculateDOM(double valueOfFLV);

        public virtual void ClearDOM()
        {
            DegreeOfMembership = 0.0;
        }

        public double DegreeOfMembership { get => degreeOfMembership; set => degreeOfMembership = value; }
        public double MidPointMembershipValue { get => midPointMembershipValue; set => midPointMembershipValue = value; }
    }
}
