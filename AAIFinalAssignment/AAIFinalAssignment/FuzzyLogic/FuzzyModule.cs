using System;
using System.Collections.Generic;
using System.Text;

// Taken from the "Programming game AI by example" book by Mat Buckland.

namespace AAIFinalAssignment.FuzzyLogic
{
    // This class only supports the MaxAv defuzzification method.
    class FuzzyModule
    {
        Dictionary<string, FuzzyLinguisticVariable> fuzzyVariables = new Dictionary<string, FuzzyLinguisticVariable>();
        List<FuzzyRule> fuzzyRules = new List<FuzzyRule>();



        public FuzzyModule()
        {

        }
        //zeros the DOMs of the consequents of each rule. Used by Defuzzify()
        void SetConfidencesOfConsequentsToZero()
        {
            throw new NotImplementedException();
        }

        public FuzzyLinguisticVariable CreateFLV(string variableName)
        {
            FuzzyLinguisticVariable variableToReturn = new FuzzyLinguisticVariable();
            fuzzyVariables.Add(variableName, variableToReturn);
            return variableToReturn;
        }

        public void AddFuzzyRuleToModule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            throw new NotImplementedException();
        }

        public void Fuzzify(string keyOfFLV, double val)
        {
            foreach(KeyValuePair<string, FuzzySet> fuzzySet in fuzzyVariables[keyOfFLV].memberSets)
            {
                fuzzySet.Value.CalculateDOM(val);
            }
            RunRules();
        }

        // Always uses MaxAv.
        public double DeFuzzify(string keyOfFLV)
        {
            RunRules();
            FuzzyLinguisticVariable consequentToDefuzzify = fuzzyVariables[keyOfFLV];
            // calculation is (sum of: (representative value * confidence) / (sum of confidence)
            double sumRepValueConfidence = 0;
            double sumOfConfidence = 0;
            
            foreach (FuzzyRule rule in fuzzyRules)
            {
                // calculate sum of: (representative value * confidence)
                sumRepValueConfidence += rule.FuzzyRealRule.DegreeOfMembership * rule.Consequent.MidPointMembershipValue;
                // calculation of: (sum of confidence)
                sumOfConfidence += rule.FuzzyRealRule.DegreeOfMembership;
            }

            // calculate defuzzified crisp value
            double defuzzifiedValue = sumRepValueConfidence / sumOfConfidence;

            return defuzzifiedValue;
        }

        public void RunRules()
        {
            foreach (FuzzyRule rule in fuzzyRules)
            {
                rule.Calculate();
            }
        }

        public List<FuzzyRule> FuzzyRules { get => fuzzyRules; set => fuzzyRules = value; }

    }
}
