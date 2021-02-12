using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    class AtractBehaviour : SteeringBehaviour
    {
        public double Range { get; set; }
        public Game1 Game { get; set; }
        public AtractBehaviour(Game1 game, double range, MovingEntity ownEntity) : base(ownEntity)
        {
            Range = range;
            Game = game;
        }
        public override Vector2 CalculateResultingVector()
        {
            var entities = Game.GetEntitiesInRange(Range, ownEntity);

            Vector2 ResultingVector = new Vector2();

            foreach (BaseEntity entity in entities)
            {

                Vector2 target = Vector2.Subtract(entity.Position, ownEntity.Position);
                target.Normalize();
                target *= (Single)Range;
                ResultingVector += BehaviourUtil.CalculateSeekVector(ownEntity.Position, target);
            }
            return ResultingVector;
        }

        protected override bool CheckIfShouldDisable()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckIfShouldEnable()
        {
            throw new NotImplementedException();
        }
    }
}
