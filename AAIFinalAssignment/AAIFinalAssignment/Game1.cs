using AAIFinalAssignment.entity;
using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace AAIFinalAssignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Vehicle> vehicles = new List<Vehicle>();
        private ClickHandler clickHandler = new ClickHandler();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Make 2 new vehicles and make #1 target #2
            vehicles.Add(new Vehicle(50, 200, new Vector2(0, 0)));
            vehicles.Add(new Vehicle(50, 0, new Vector2(300, 300)));
            vehicles[0].steeringBehaviours.Add(new SeekBehaviour(vehicles[1], vehicles[0]));

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.Update(gameTime);
            }

            // Move target to click
            if (clickHandler.CheckMouseClicked() == true)
            {
                vehicles[1].Position = clickHandler.GetMousePosition();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.Render(gameTime, _spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
