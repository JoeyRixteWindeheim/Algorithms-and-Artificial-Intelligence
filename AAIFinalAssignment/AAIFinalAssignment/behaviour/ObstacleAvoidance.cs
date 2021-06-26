using AAIFinalAssignment.entity;
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

        private Vector2[] feelerArray1;
        private Vector2[] feelerArray2;
        private Vector2[] feelerArray3;

        private Vector2 resultingVector;

        private int lastDirection;

        private void SetFeelers()
        {
            Vector2 feeler2 = Vector2.Normalize(ownEntity.velocity);
            Vector2 feeler1 = BehaviourUtil.RotateVector(feeler2, -Math.PI / 6);
            Vector2 feeler3 = BehaviourUtil.RotateVector(feeler2, Math.PI / 6);

            feelerArray1 = new Vector2[5];
            feelerArray2 = new Vector2[5];
            feelerArray3 = new Vector2[5];

            for(int i = 0; i<5; i++)
            {
                feelerArray1[i] = feeler1 * (i+2) * 20;
                feelerArray2[i] = feeler2 * (i+2) * 20;
                feelerArray3[i] = feeler3 * (i+2) * 20;
            }
        }

        private void CheckFeelers()
        {
            HashSet<Obstacle> obstacles = new HashSet<Obstacle>();
            for(int i = 0; i<5;i++)
            {
                obstacles.UnionWith(Game1.GetObstaclesInRange(feelerArray1[i] + ownEntity.Position));
                obstacles.UnionWith(Game1.GetObstaclesInRange(feelerArray2[i] + ownEntity.Position));
                obstacles.UnionWith(Game1.GetObstaclesInRange(feelerArray3[i] + ownEntity.Position));
            }

            feeler1hit = feeler2hit = feeler3hit = false;
            foreach (Obstacle obstacle in obstacles)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (obstacle.DoIHit(feelerArray1[i] + ownEntity.Position))
                        feeler1hit = true;
                    if (obstacle.DoIHit(feelerArray2[i] + ownEntity.Position))
                        feeler2hit = true;
                    if (obstacle.DoIHit(feelerArray3[i] + ownEntity.Position))
                        feeler3hit = true;
                }
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
                lastDirection = 0;
                return resultingVector;
            }

            int strength = 10;
            if (feeler2hit)
            {
                strength = 20;
            }
            if (feeler1hit && feeler2hit && feeler3hit)
            {
                strength = 30;
            }

            Vector2 vector = Vector2.Normalize(ownEntity.velocity);

            



            if (feeler1hit && !feeler3hit)
            {
                vector = BehaviourUtil.RotateVector(vector, Math.PI / 2);
                lastDirection += 1;
            }
            else if(!feeler1hit && feeler3hit)
            {
                vector = BehaviourUtil.RotateVector(vector, -Math.PI / 2);
                lastDirection -= 1;
            } else
            {
                if(lastDirection> 0)
                {
                    vector = BehaviourUtil.RotateVector(vector, Math.PI / 2);
                    lastDirection += 1;
                } else
                {
                    vector = BehaviourUtil.RotateVector(vector, -Math.PI / 2);
                    lastDirection -= 1;
                }
            }

            vector *= strength;
            resultingVector = vector;
            return vector;

        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch, Vector2 Position)
        {
            BehaviourUtil.RenderVector(_spriteBatch, resultingVector, Position, 1, Color.Red);

            for(int i = 0; i < 5; i++)
            {
                renderPoint(_spriteBatch, feelerArray1[i] + Position, feeler1hit);
                renderPoint(_spriteBatch, feelerArray2[i] + Position, feeler2hit);
                renderPoint(_spriteBatch, feelerArray3[i] + Position, feeler3hit);

            }
        }

        public void renderPoint(SpriteBatch _spriteBatch, Vector2 position, bool isred)
        {
            if (isred)
            {
                BehaviourUtil.RenderPoint(_spriteBatch, position, Color.Red);
            }
            else
            {
                BehaviourUtil.RenderPoint(_spriteBatch, position, Color.Green);
            }
        }
    }
}
