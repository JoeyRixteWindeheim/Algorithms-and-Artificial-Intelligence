using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    public class GroupPressureBehaviour : SteeringBehaviour
    {
        public int Range { get; set; }
        public float Strength { get; set; }
        public Vector2 CurrentVector { get; set; }

        public Game1 Game { get; set; }

        public GroupPressureBehaviour(MovingEntity ownEntity,float strength, int range, Game1 game) : base(ownEntity)
        {
            Game = game;
            Range = range;
            Strength = strength;

        }
        public override Vector2 CalculateResultingVector()
        {
            var entities = Game.GetMovingEntitiesInRange(Range, ownEntity);

            CurrentVector = new Vector2();
            foreach (MovingEntity entity in entities)
            {
                CurrentVector += Vector2.Normalize( entity.velocity);
            }
            if (float.IsNaN(CurrentVector.X) || CurrentVector == Vector2.Zero)
                return Vector2.Zero;
            CurrentVector.Normalize();
            CurrentVector *= Strength;

            return CurrentVector;

        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            if (Game1.RenderGroupPressure)
                BehaviourUtil.RenderVector(_spriteBatch, CurrentVector, ownEntity.Position, 20, Color.White);
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
