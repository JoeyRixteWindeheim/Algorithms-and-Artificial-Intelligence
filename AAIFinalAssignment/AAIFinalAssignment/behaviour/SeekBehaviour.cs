﻿using System;
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

        private bool UpdateAstar;

        public SeekBehaviour(BaseEntity target, MovingEntity ownEntity) : base(ownEntity)
        {
            NewTarget(target);
            urgency = 1;
        }

        public void NewTarget(BaseEntity target)
        {
            UpdateAstar = target is MovingEntity;
            Target = target;
            GenerateWaypoints();
        }

        private void GenerateWaypoints()
        {
            if (Target != null)
            {
                Waypoints = AStar.Run(ownEntity.Position, Target.Position);
            }
            currentWaypoint = 0;
        }

        private void UpdateWaypoint()
        {
            if(Waypoints != null && currentWaypoint < Waypoints.Length && Vector2.DistanceSquared(ownEntity.Position, Game1.GetClosestCoords(ownEntity.Position, Waypoints[currentWaypoint])) < Settings.WaypointSwitchDistance * Settings.WaypointSwitchDistance)
            {
                currentWaypoint++;
            }
        }

        public override Vector2? CalculateResultingVector()
        {
            if (Waypoints != null && Target != null)
            {
                if (currentWaypoint < Waypoints.Length - 1)
                {
                    if (UpdateAstar && Vector2.DistanceSquared(Target.Position, Target.GetClosestCoords(Waypoints[Waypoints.Length - 1])) > Game1.Grid.RegionSize * Game1.Grid.RegionSize)
                    {
                        GenerateWaypoints();
                    }
                    UpdateWaypoint();
                    if (Waypoints != null)
                    {
                        return Currentvector = BehaviourUtil.CalculateSeekVector(ownEntity.Position, Game1.GetClosestCoords(ownEntity.Position, Waypoints[currentWaypoint]));
                    }
                    return null;
                    
                }
                return Currentvector = BehaviourUtil.CalculateSeekVector(ownEntity.Position, Game1.GetClosestCoords(ownEntity.Position, Target.Position));
            }
            return null;
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch,Vector2 Position)
        {
            if(Settings.RenderSeeking)
            {
                CalculateResultingVector();
                BehaviourUtil.RenderVector(_spriteBatch, Currentvector, Position, 20, Color.White);
            }
                
        }
    }
}
