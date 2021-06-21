using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.statemachine;

namespace AAIFinalAssignment.entity
{
    public class VehicleWithStates : MovingEntityWithStates
    {
        public override Vector2 velocity { get; set; }
        public double mass { get; set; }
        public double maxSpeed { get; set; }
        public double minSpeed = 0;
        public double maxAccel = 1;
        public double drag = 0.99;
        public override Vector2 Position { get; set; }
        public override Texture2D Texture { get; set; }
        public VehicleFiniteStateMachine StateMachine { get; set; }
        public BaseEntity SeekTarget { get; set; }
        public Game1 RunningGame { get; set; }

        public VehicleWithStates(double mass, double maxSpeed, Vector2 Position, BaseEntity SeekTarget, Game1 RunningGame)
        {
            StateMachine = new VehicleFiniteStateMachine(this);
            this.mass = mass;
            this.maxSpeed = maxSpeed;
            this.Position = Position;
            this.SeekTarget = SeekTarget;
            this.RunningGame = RunningGame;
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("circle");
        }

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine(Position);
            StateMachine.Update(gameTime);
        }

        public override void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            // Draws the sprite. origin of sprite is at position; NOT top left of sprite
            _spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                0f,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );
            // TODO: implement in State
/*            foreach(SteeringBehaviour behaviour in steeringBehaviours)
            {
                behaviour.Render(gameTime, _spriteBatch);
            }*/
            BehaviourUtil.RenderVector(_spriteBatch, velocity, Position, 0.01, Color.Red);
        }

    }
}
