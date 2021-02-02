using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public BaseGameEntity Target { get; set; }

        public SeekBehaviour(MovingEntity ME, BaseGameEntity Target) : base(ME)
        {
            this.Target = Target;
        }

        public override Vector2D Calculate()
        {
            return Target.Pos.Sub(ME.Pos).Normalize();
        }
    }
}
