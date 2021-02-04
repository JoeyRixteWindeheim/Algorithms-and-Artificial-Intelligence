using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using AAIFinalAssignment.entity;

namespace AAIFinalAssignment.behaviour
{
    public abstract class SteeringBehaviour
    {
        protected MovingEntity ownEntity;

        protected bool isActive;

        public SteeringBehaviour(MovingEntity ownEntity)
        {
            this.ownEntity = ownEntity;
        }

        public abstract Vector2 CalculateResultingVector();
        
        private void EnableIfAppropriate()
        {
            if (CheckIfShouldEnable() == true)
            {
                isActive = true;
            }
        }

        private void DisableIfAppropriate()
        {
            if (CheckIfShouldDisable() == true)
            {
                isActive = false;
            }
        }

        protected abstract bool CheckIfShouldEnable();

        protected abstract bool CheckIfShouldDisable();

        public void Update()
        {
            if (isActive) { DisableIfAppropriate(); }
            else { EnableIfAppropriate(); }
        }



    }
}
