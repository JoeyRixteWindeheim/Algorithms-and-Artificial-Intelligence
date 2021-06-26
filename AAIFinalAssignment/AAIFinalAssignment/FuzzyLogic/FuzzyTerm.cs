using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.FuzzyLogic
{

    // In this project, only the AND operator is used in fuzzy logic rules. Adding the other operators (OR, VERY, FAIRLY) would be too much overhead for this project.
    // This project also only uses one AND operator per rule.
    public abstract class FuzzyTerm
    {
        private double degreeOfMembership;



        public abstract double GetDOM();
        public virtual void ClearDOM()
        {
            degreeOfMembership = 0;
        }

        public double DegreeOfMembership { get => degreeOfMembership; set => degreeOfMembership = value; }
    }
}
