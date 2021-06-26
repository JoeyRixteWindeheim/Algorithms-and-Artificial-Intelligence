using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.FuzzyLogic
{
    public class FzAND : FuzzyTerm
    {
        public List<FuzzySet> antecedentSets = new List<FuzzySet>();

        public override double GetDOM()
        {
            double maxDOM = 1;
            foreach (FuzzySet fuzzySet in antecedentSets)
            {
               if (fuzzySet.DegreeOfMembership < maxDOM)
               {
                    maxDOM = fuzzySet.DegreeOfMembership;
               }
            }
            DegreeOfMembership = maxDOM;
            return DegreeOfMembership;
        }

        public override void ClearDOM()
        {
            foreach (FuzzySet fuzzySet in antecedentSets)
            {
                fuzzySet.ClearDOM();
            }
        }
    }
}
