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

        private BaseEntity Target { get; set; }
        private Vector2 Currentvector { get; set; }
        public FleeBehaviour(BaseEntity target, MovingEntity ownEntity) : base(ownEntity)
        {
            Target = target;
            urgency = 1;
        }

        public override Vector2? CalculateResultingVector()
        {
            Currentvector = BehaviourUtil.CalculateSeekVector(ownEntity.Position, Target.GetClosestCoords(ownEntity.Position)) * -1;
            return Currentvector;
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch, Vector2 position)
        {
            if (Settings.RenderSeeking)
            { 
                CalculateResultingVector();
                BehaviourUtil.RenderVector(_spriteBatch, Currentvector, position, 20, Color.Red);
            }
        }
    }
}
