using System;
using System.Collections.Generic;
using System.Text;
using AAIFinalAssignment.entity;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.statemachine
{
    public class VehicleFiniteStateMachine : FiniteStateMachine
    {

        public VehicleWithStates OwnerVehicle { get; set; }

        public VehicleFiniteStateMachine(VehicleWithStates ownerVehicle)
        {
            OwnerVehicle = ownerVehicle;
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
