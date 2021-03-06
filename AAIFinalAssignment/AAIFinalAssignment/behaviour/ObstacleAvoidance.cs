﻿using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AAIFinalAssignment.behaviour
{
    class ObstacleAvoidance : SteeringBehaviour
    {

        public ObstacleAvoidance(MovingEntity ownEntity) : base(ownEntity)
        {

        }

        private bool feeler1hit;
        private bool feeler2hit;
        private bool feeler3hit;
        private Vector2 feeler1;
        private Vector2 feeler2;
        private Vector2 feeler3;

        private Vector2 resultingVector;

        private void SetFeelers()
        {
            feeler2 = Vector2.Normalize(ownEntity.velocity);
            feeler2 *= 70;

            feeler1 = BehaviourUtil.RotateVector(feeler2, -Math.PI / 6);
            feeler3 = BehaviourUtil.RotateVector(feeler2, Math.PI / 6);
        }

        private void CheckFeelers()
        {
            HashSet<Obstacle> obstacles = new HashSet<Obstacle>();
            obstacles.UnionWith(Game1.GetObstaclesInRange(feeler1));
            obstacles.UnionWith(Game1.GetObstaclesInRange(feeler2));
            obstacles.UnionWith(Game1.GetObstaclesInRange(feeler3));

            feeler1hit = feeler2hit = feeler3hit = false;
            foreach (Obstacle obstacle in obstacles)
            {
                if (obstacle.DoIHit(feeler1 + ownEntity.Position))
                    feeler1hit = true;
                if (obstacle.DoIHit(feeler2 + ownEntity.Position))
                    feeler2hit = true;
                if (obstacle.DoIHit(feeler3 + ownEntity.Position))
                    feeler3hit = true;
            }
        }

        public override Vector2 CalculateResultingVector()
        {
            if (ownEntity.velocity == Vector2.Zero)
                return Vector2.Zero;
            SetFeelers();
            CheckFeelers();

            if (!(feeler1hit || feeler2hit || feeler3hit))
            {
                resultingVector = Vector2.Zero;
                return resultingVector;
            }

            int strength = 10;
            if (feeler2hit)
                strength = 20;
            if (feeler1hit && feeler2hit && feeler3hit)
                strength = 30;

            Vector2 vector = Vector2.Normalize(ownEntity.velocity);

            if (feeler1hit)
            {
                vector = BehaviourUtil.RotateVector(vector, Math.PI / 2);
            }
            else
            {
                vector = BehaviourUtil.RotateVector(vector, -Math.PI / 2);
            }

            vector *= strength;
            resultingVector = vector;
            return vector;

        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch, Vector2 Position)
        {
            BehaviourUtil.RenderVector(_spriteBatch, resultingVector, Position, 1, Color.Red);


            if (feeler1hit)
                BehaviourUtil.RenderPoint(_spriteBatch, feeler1 + Position, Color.Red);
            else
                BehaviourUtil.RenderPoint(_spriteBatch, feeler1 + Position, Color.Green);
            if (feeler2hit)
                BehaviourUtil.RenderPoint(_spriteBatch, feeler2 + Position, Color.Red);
            else
                BehaviourUtil.RenderPoint(_spriteBatch, feeler2 + Position, Color.Green);
            if (feeler3hit)
                BehaviourUtil.RenderPoint(_spriteBatch, feeler3 + Position, Color.Red);
            else
                BehaviourUtil.RenderPoint(_spriteBatch, feeler3 + Position, Color.Green);
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
