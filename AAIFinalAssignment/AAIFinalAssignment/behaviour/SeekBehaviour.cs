using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework.Graphics;
using AAIFinalAssignment.Pathfinding;

namespace AAIFinalAssignment.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public BaseEntity Target { get; set; }

        public Vector2 Currentvector { get; set; }

        private Vector2[] Waypoints;
        private int currentWaypoint;

        public SeekBehaviour(BaseEntity target, MovingEntity ownEntity) : base(ownEntity)
        {
            Target = target;
            urgency = 1;
            Waypoints = AStar.Run(ownEntity.Position, target.Position);
            currentWaypoint = 0;
        }

        private void UpdateWaypoint()
        {
            if(currentWaypoint < Waypoints.Length && Vector2.DistanceSquared(ownEntity.Position, Game1.GetClosestCoords(ownEntity.Position, Waypoints[currentWaypoint])) < Settings.WaypointSwitchDistance * Settings.WaypointSwitchDistance)
            {
                currentWaypoint++;
            }
        }

        public override Vector2 CalculateResultingVector()
        {
            if(currentWaypoint < Waypoints.Length -1)
            {
                UpdateWaypoint();
                return Currentvector = BehaviourUtil.CalculateSeekVector(ownEntity.Position, Game1.GetClosestCoords(ownEntity.Position, Waypoints[currentWaypoint]));
            }
            return Currentvector = BehaviourUtil.CalculateSeekVector(ownEntity.Position, Game1.GetClosestCoords(ownEntity.Position, Target.Position));
        }

        // TODO: Implement
        protected override bool CheckIfShouldEnable()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckIfShouldDisable()
        {
            throw new NotImplementedException();
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch,Vector2 Position)
        {
            if(Settings.RenderSeeking)
                BehaviourUtil.RenderVector(_spriteBatch, Currentvector, Position,20, Color.White);
        }
    }
}
