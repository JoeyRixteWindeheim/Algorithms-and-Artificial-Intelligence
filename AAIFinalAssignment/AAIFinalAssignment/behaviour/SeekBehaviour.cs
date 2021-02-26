using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework.Graphics;

namespace AAIFinalAssignment.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public BaseEntity Target { get; set; }

        public Vector2 Currentvector { get; set; }

        public SeekBehaviour(BaseEntity target, MovingEntity ownEntity) : base(ownEntity)
        {
            Target = target;
            urgency = 1;
        }

        public override Vector2 CalculateResultingVector()
        {
            return Currentvector = BehaviourUtil.CalculateSeekVector(ownEntity.Position, Target.GetClosestCoords(ownEntity.Position));
        }

        // TODO: Implement
        protected override bool CheckIfShouldEnable()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckIfShouldDisable()
        {
            throw new NotImplementedException();
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            if(Game1.RenderSeeking)
                BehaviourUtil.RenderVector(_spriteBatch, Currentvector, ownEntity.Position,20, Color.White);
        }
    }
}
