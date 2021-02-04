using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.behaviour;

namespace AAIFinalAssignment.entity
{
    public abstract class MovingEntity : BaseEntity
    {
        public abstract Vector2 velocity { get; set; }
        public abstract double mass { get; set; }
        public abstract double maxSpeed { get; set; }

        public List<SteeringBehaviour> steeringBehaviours = new List<SteeringBehaviour>();
    }
}
