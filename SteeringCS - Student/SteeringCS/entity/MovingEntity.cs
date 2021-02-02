using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{

    abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }


        public List<SteeringBehaviour> steeringBehaviours = new List<SteeringBehaviour>();

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 150;
            Velocity = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {
            Console.WriteLine(ToString());
            Vector2D totalVector = new Vector2D();
            foreach (SteeringBehaviour steeringBehaviour in steeringBehaviours)
            {
                Vector2D steeringBehaviourVector = steeringBehaviour.Calculate();
                
                totalVector.Add(steeringBehaviourVector);
            }
            // Acceleratie toevoegen op basis van Mass
            if (totalVector.Length() != 0)
            {
                totalVector = totalVector.Normalize();
                Vector2D acceleration = totalVector.Divide(Mass).Multiply(MaxSpeed);
                
                Velocity = Velocity.Add(totalVector.Multiply(timeElapsed));
                Velocity = Velocity.Truncate(MaxSpeed);
            }
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
