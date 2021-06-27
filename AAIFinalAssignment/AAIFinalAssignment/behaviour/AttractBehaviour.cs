using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AAIFinalAssignment.behaviour
{
    class AttractBehaviour : SteeringBehaviour
    {
        public double Range { get; set; }
        public Game1 Game { get; set; }

        public Vector2 currentVector { get; set; }
        public AttractBehaviour(Game1 game, double range, MovingEntity ownEntity) : base(ownEntity)
        {
            Range = range;
            Game = game;
        }
        public override Vector2? CalculateResultingVector()
        {
            var entities = Game1.GetMovingEntitiesInRange(Range, ownEntity);

            currentVector = new Vector2();
            //get vector for every entity
            foreach (BaseEntity entity in entities)
            {

                currentVector += 20*BehaviourUtil.CalculateSeekVector(ownEntity.Position, entity.Position);

            }
            return currentVector;
        }

    }
}
