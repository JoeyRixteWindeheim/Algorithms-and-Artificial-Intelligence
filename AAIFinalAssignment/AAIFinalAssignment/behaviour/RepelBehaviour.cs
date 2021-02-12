using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    public class RepelBehaviour : SteeringBehaviour
    {
        public double Range { get; set; }
        public Game1 Game { get; set; }
        public RepelBehaviour(Game1 game,double range, MovingEntity ownEntity) : base(ownEntity)
        {
            Range = range;
            Game = game;
        }
        public override Vector2 CalculateResultingVector()
        {
            var entities = Game.GetEntitiesInRange(Range, ownEntity);

            Vector2 ResultingVector = new Vector2();

            foreach(BaseEntity entity in entities)
            {
                ResultingVector += BehaviourUtil.CalculateSeekVector(ownEntity.Position, entity.Position)*-1;
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
