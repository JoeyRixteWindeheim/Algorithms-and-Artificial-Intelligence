using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    public class FleeBehaviour : SteeringBehaviour
    {

        public BaseEntity Target { get; set; }
        public Vector2 Currentvector { get; set; }
        public FleeBehaviour(BaseEntity target, MovingEntity ownEntity) : base(ownEntity)
        {
            Target = target;
            urgency = 1;
        }

        public override Vector2 CalculateResultingVector()
        {
            return Currentvector = BehaviourUtil.CalculateSeekVector(ownEntity.Position, Target.GetClosestCoords(ownEntity.Position)) * -1; 
        }

        protected override bool CheckIfShouldDisable()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckIfShouldEnable()
        {
            throw new NotImplementedException();
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch, Vector2 position)
        {
            if (Settings.RenderSeeking)
                BehaviourUtil.RenderVector(_spriteBatch, Currentvector, position, 20, Color.White);
        }
    }
}
