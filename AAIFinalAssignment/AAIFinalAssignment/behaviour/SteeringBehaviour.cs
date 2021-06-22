using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework.Graphics;

namespace AAIFinalAssignment.behaviour
{
    public abstract class SteeringBehaviour
    {
        protected MovingEntity ownEntity;

        // This will determine the amount of force added through this behaviour.
        // For example, if an object almost colliding with another, the urgency to avoid others is high.
        // If the object is only slightly moving towards another, the urgency is low.
        // in the first case, you want the entity to move faster than in the second case.
        // Is probably going to get used in fuzzy logic(??)
        protected double urgency;

        public SteeringBehaviour(MovingEntity ownEntity)
        {
            this.ownEntity = ownEntity;
        }

        public abstract Vector2 CalculateResultingVector();
        public virtual void Render(GameTime gameTime, SpriteBatch _spriteBatch, Vector2 position) { }
    }
}
