using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.FuzzyLogic
{
    // For example, hunger
    class FuzzyLinguisticVariable
    {
        // The sets that comprise this variable
        public Dictionary<string, FuzzySet> memberSets = new Dictionary<string, FuzzySet>();

        double minRangeOfVariable;
        double maxRangeOfVariable;

        public FuzzyLinguisticVariable()
        {
            minRangeOfVariable = 0.0;
            maxRangeOfVariable = 0.0;
        }
        // Called each time a new set is added to adjust min/max range accordingly
        void AdjustRangeToFit(double minOfSet, double maxOfSet)
        {
            if (minOfSet < minRangeOfVariable)
            {
                minRangeOfVariable = minOfSet;
            }
            if (maxOfSet < maxRangeOfVariable)
            {
                maxRangeOfVariable = maxOfSet;
            }
        }

        // Following methods create sets. AdjustRangeToFit() is called each time a set is created.
        // Gives the sets a name and adds it to the memberSets dict.
        public FuzzySet AddLeftShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            FuzzySet newLeftShoulderSet = new FuzzySet_LeftShoulder(peak, minBound, maxBound);
            memberSets.Add(name, newLeftShoulderSet);
            AdjustRangeToFit(minBound, maxBound);
            return memberSets[name];
        }

        public FuzzySet AddRightShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            FuzzySet newRightShoulderSet = new FuzzySet_RightShoulder(peak, minBound, maxBound);
            memberSets.Add(name, newRightShoulderSet);
            AdjustRangeToFit(minBound, maxBound);
            return memberSets[name];
        }

        public FuzzySet AddTriangularSet(string name, double minBound, double peak, double maxBound)
        {
            FuzzySet newTriangularSet = new FuzzySet_Triangle(peak, minBound, maxBound);
            memberSets.Add(name, newTriangularSet);
            AdjustRangeToFit(minBound, maxBound);
            return memberSets[name];
        }

        // fuzzify by calculating DOM of each FLV subset
        public void Fuzzify(double valueOfFLV)
        {
            foreach (KeyValuePair<string, FuzzySet> fuzzySet in memberSets)
            {
                fuzzySet.Value.CalculateDOM(valueOfFLV);
            }
        }
    }
}
