﻿using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAIFinalAssignment.FuzzyLogic;
using AAIFinalAssignment.statemachine.states;
using System.Threading.Tasks;

namespace AAIFinalAssignment.statemachine
{
    public class EntityStateMachine : FiniteStateMachine
    {
        public MovingEntityWithStates OwnerEntity { get; set; }

        // Used to keep track of parent state, if it has one.
        // Used in recursive state machines
        public State ParentState { get; set; }

        private bool lookingForFood;

        DateTime lastFoodUpdate = DateTime.Now;

        public EntityStateMachine(MovingEntityWithStates ownerVehicle, State startingState = null, State parentState = null)
        {
            OwnerEntity = ownerVehicle;
            this.StartingState = startingState;
            this.ParentState = parentState;
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (OwnerEntity.FoodEaten > -5)
            {
                ThinkIfShouldEatTimer();
            }

            bool shouldeat;



            if (Settings.DoFuzzyLogic)
            {

                shouldeat = ThinkIfShouldEat();
            }
            else
            {
                shouldeat = OwnerEntity.FoodEaten < 0;
            }

            float sharkDistance = float.MaxValue;
            Shark closestShark = null;
            if (OwnerEntity is Fish)
            {
                closestShark = ((Fish)OwnerEntity).ReturnClosestShark(out sharkDistance);
            }

            // distance in pixels squared
            if (sharkDistance < 40000 && OwnerEntity.FoodEaten > -5)
            {
                SetState(new FleeFromObjectState(this,closestShark));
            } 
            else if (shouldeat)
            {
                SetState(new FindFoodState(this));
            }
            else
            {
                SetState(new NeutralState(this));
            }


            

        }

        public void Render(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            ((EntityState)CurrentState).Render(gameTime, spriteBatch, position);


        }

        public void ThinkIfShouldEatTimer()
        {
            if (DateTime.Now.Subtract(lastFoodUpdate).TotalSeconds >= 1)
            {
                OwnerEntity.FoodEaten--;
                lastFoodUpdate = DateTime.Now;
            }
        }

        // TODO: REFACTOR Split this up into multiple methods
        // and call it only when neccesarry; this is too heavy now
        // Do not have time to split it up
        bool ThinkIfShouldEat()
        {
            FuzzyModule fuzzyModule = new FuzzyModule();

            /// define FLVs and underlying sets

            FuzzyLinguisticVariable distToFood = fuzzyModule.CreateFLV("DistToFood");
            // distance numbers are measured in pixels. For a visualisation of this FLV,
            // see the "DistanceFromFood" antecedent FLV graph in the documentation.
            FuzzySet food_ShortDistance = distToFood.AddLeftShoulderSet("ShortDistance", 0, 1, 200);
            FuzzySet food_MediumDistance = distToFood.AddTriangularSet("MediumDistance", 100, 200, 300);
            FuzzySet food_LongDistance = distToFood.AddRightShoulderSet("LongDistance", 200, 200, 400);

            FuzzyLinguisticVariable hunger = fuzzyModule.CreateFLV("Hunger");
            FuzzySet hunger_full = distToFood.AddLeftShoulderSet("Full", 0, 1, 50);
            FuzzySet hunger_hungry = distToFood.AddTriangularSet("Hungry", 30, 50, 70);
            FuzzySet hunger_starving = distToFood.AddRightShoulderSet("Starving", 50, 100, 100);

            FuzzyLinguisticVariable foodDesirability = fuzzyModule.CreateFLV("DesirabilityToEatFood");
            FuzzySet desirability_Undesirable = distToFood.AddLeftShoulderSet("Undesirable", 0, 1, 50);
            FuzzySet desirability_Desirable = distToFood.AddTriangularSet("Desirable", 25, 50, 75);
            FuzzySet desirability_VeryDesirable = distToFood.AddRightShoulderSet("VeryDesirable", 50, 100, 100);

            // implement rules
            List<FuzzySet> rule1Antecedent = new List<FuzzySet>() { food_ShortDistance, hunger_full };
            fuzzyModule.FuzzyRules.Add(new FuzzyRule(rule1Antecedent, desirability_Undesirable));

            List<FuzzySet> rule2Antecedent = new List<FuzzySet>() { food_ShortDistance, hunger_hungry };
            fuzzyModule.FuzzyRules.Add(new FuzzyRule(rule2Antecedent, desirability_VeryDesirable));

            List<FuzzySet> rule3Antecedent = new List<FuzzySet>() { food_ShortDistance, hunger_starving };
            fuzzyModule.FuzzyRules.Add(new FuzzyRule(rule3Antecedent, desirability_VeryDesirable));

            List<FuzzySet> rule4Antecedent = new List<FuzzySet>() { food_MediumDistance, hunger_full };
            fuzzyModule.FuzzyRules.Add(new FuzzyRule(rule4Antecedent, desirability_Undesirable));

            List<FuzzySet> rule5Antecedent = new List<FuzzySet>() { food_MediumDistance, hunger_hungry };
            fuzzyModule.FuzzyRules.Add(new FuzzyRule(rule5Antecedent, desirability_Desirable));

            List<FuzzySet> rule6Antecedent = new List<FuzzySet>() { food_MediumDistance, hunger_starving };
            fuzzyModule.FuzzyRules.Add(new FuzzyRule(rule6Antecedent, desirability_VeryDesirable));

            List<FuzzySet> rule7Antecedent = new List<FuzzySet>() { food_LongDistance, hunger_full };
            fuzzyModule.FuzzyRules.Add(new FuzzyRule(rule7Antecedent, desirability_Undesirable));

            List<FuzzySet> rule8Antecedent = new List<FuzzySet>() { food_LongDistance, hunger_hungry };
            fuzzyModule.FuzzyRules.Add(new FuzzyRule(rule8Antecedent, desirability_Undesirable));

            List<FuzzySet> rule9Antecedent = new List<FuzzySet>() { food_LongDistance, hunger_starving };
            fuzzyModule.FuzzyRules.Add(new FuzzyRule(rule9Antecedent, desirability_VeryDesirable));

            /// pass variables
            //TODO: Actually get distance to food in-game
            // Not implemented (its used for debugging) because it 
            // seemingly doesn't accuratly produce wanted results
            // To implement, get the nearest food source from an entity
            // and get the distance squared towards target
            double pixelsFromFood = 300;
            distToFood.Fuzzify(pixelsFromFood);
            
            //TODO actually implement hunger
            // Does not change the defuzzified value for some reason.
            // I don't know how and I do not have the time to fix it.
            // First step would be to check if it changes the consequent value, etc.
            double hungerAmount = -1 * OwnerEntity.FoodEaten;
            hunger.Fuzzify(hungerAmount);

            fuzzyModule.RunRules();

            double defuzzifiedValue = fuzzyModule.DeFuzzify("DesirabilityToEatFood");


            // I don't know what I did wrong here. I don't have time to fix it.
            // Value does not correspond to the value in the docs.
            // Unpredictable results.
            return defuzzifiedValue < 109;

            

        }
        public bool LookingForFood { get => lookingForFood; set => lookingForFood = value; }
    }
}
