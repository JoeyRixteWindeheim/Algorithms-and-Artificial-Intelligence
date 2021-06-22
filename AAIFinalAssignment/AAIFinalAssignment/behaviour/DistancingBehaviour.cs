using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    public class DistancingBehaviour : SteeringBehaviour
    {

        public List<Vector2> targets { get; set; }

        public Vector2 currentVector { get; set; }
        public DistancingBehaviour(MovingEntity ownEntity) : base(ownEntity)
        {
        }
        public override Vector2 CalculateResultingVector()
        {
            var entities = Game1.GetMovingEntitiesInRange(Settings.DistancingRange, ownEntity);

            currentVector = new Vector2();
            targets = new List<Vector2>();

            foreach(BaseEntity entity in entities)
            {
                if(ownEntity.GetType() == entity.GetType())
                {
                    Vector2 target = Vector2.Subtract(ownEntity.Position, entity.GetClosestCoords(ownEntity.Position));
                    target = Vector2.Normalize(target);
                    target *= (float)Settings.DistancingRange / 2;
                    targets.Add(target);
                    currentVector += BehaviourUtil.CalculateSeekVector(ownEntity.Position, Vector2.Add(target, entity.Position));
                }
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

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch,Vector2 Position)
        {
            if (Settings.RenderDistancing)
            {
                BehaviourUtil.RenderVector(_spriteBatch, currentVector, Position, 20, Color.Blue);
                foreach(Vector2 target in targets)
                {
                    BehaviourUtil.RenderPoint(_spriteBatch, Vector2.Subtract(Position, target), Color.Yellow);
                }
            }
                
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
        }
    }
}
