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
        public Vector2 CurrentVector { get; set; }


        public GroupPressureBehaviour(MovingEntity ownEntity) : base(ownEntity)
        {

        }
        public override Vector2 CalculateResultingVector()
        {
            var entities = Game1.GetMovingEntitiesInRange(Settings.GroupPressureRange, ownEntity);

            CurrentVector = new Vector2();
            foreach (MovingEntity entity in entities)
            {
                if(ownEntity.GetType() == entity.GetType())
                {
                    CurrentVector += Vector2.Normalize(entity.velocity);
                }
            }
            if (float.IsNaN(CurrentVector.X) || CurrentVector == Vector2.Zero)
                return Vector2.Zero;
            CurrentVector.Normalize();

            return CurrentVector;

        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch, Vector2 Position)
        {
            if (Settings.RenderGroupPressure)
                BehaviourUtil.RenderVector(_spriteBatch, CurrentVector, Position, 1, Color.White);
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
