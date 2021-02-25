using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    public class FlockingBehaviour : SteeringBehaviour
    {
        public double Range { get; set; }
        public Game1 Game { get; set; }

        public List<Vector2> targets { get; set; }

        public Vector2 currentVector { get; set; }
        public FlockingBehaviour(Game1 game,double range, MovingEntity ownEntity) : base(ownEntity)
        {
            Range = range;
            Game = game;
        }
        public override Vector2 CalculateResultingVector()
        {
            var entities = Game.GetEntitiesInRange(Range, ownEntity);

            currentVector = new Vector2();
            targets = new List<Vector2>();

            foreach(BaseEntity entity in entities)
            {

                Vector2 target = Vector2.Subtract(ownEntity.Position, entity.Position);
                target = Vector2.Normalize(target);
                target *= (float)Range/2;
                target = Vector2.Add(target,entity.Position);
                targets.Add(target);
                currentVector += BehaviourUtil.CalculateSeekVector(ownEntity.Position, target);
            }
            return currentVector;
        }

        protected override bool CheckIfShouldDisable()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckIfShouldEnable()
        {
            throw new NotImplementedException();
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            if (Game1.RenderRepel)
            {
                BehaviourUtil.RenderVector(_spriteBatch, currentVector, ownEntity.Position, 20, Color.Blue);
                foreach(Vector2 target in targets)
                {
                    BehaviourUtil.RenderPoint(_spriteBatch, target, Color.Yellow);
                }
            }
                
        }
    }
}
