using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.FuzzyLogic
{
    // for example, AND
    public class FuzzyRule
    {
        private List<FuzzySet> antecedents = new List<FuzzySet>();
        private FuzzySet consequent;
        protected FzAND fuzzyRealRule = new FzAND();



        public FuzzyRule(List<FuzzySet> antecedents, FuzzySet consequent)
        {
            this.Antecedents = antecedents;
            this.Consequent = consequent;
        }
        
        public void SetConfidenceOfConsequentToZero()
        {
            Consequent.ClearDOM();
        }

        // updates DOM of consequent
        public double Calculate()
        {
            // We only use FzAND in this project
            foreach (FuzzySet antecedent in Antecedents)
            {
                fuzzyRealRule.antecedentSets.Add(antecedent);
            }
            double confidence = fuzzyRealRule.GetDOM();
            consequent.DegreeOfMembership = confidence;
            return confidence;
            
        }
        public FzAND FuzzyRealRule { get => fuzzyRealRule; set => fuzzyRealRule = value; }
        public List<FuzzySet> Antecedents { get => antecedents; set => antecedents = value; }
        public FuzzySet Consequent { get => consequent; set => consequent = value; }
    }

    
}
