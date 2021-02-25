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
        private Vehicle target = new Vehicle(10,0,new Vector2(20,20));
        private ClickHandler clickHandler = new ClickHandler();


        public static bool RenderSeeking = false;
        public static bool RenderAtract = false;
        public static bool RenderRepel = true;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //BehaviourUtil.RenderVector(_spriteBatch, new Vector2(5, 5), new Vector2(10, 10));

            // Make 2 new vehicles and make #1 target #2
            /*
            vehicles.Add(new Vehicle(50, 200, new Vector2(0, 0)));
            vehicles.Add(new Vehicle(50, 0, new Vector2(300, 300)));
            vehicles[0].steeringBehaviours.Add(new SeekBehaviour(vehicles[1], vehicles[0]));
            */
            /*
            for(int x = 0; x<3; x++)
            {
                for (int y = 0; y <3; y++)
                {
                    AddFlockVehicle(new Vector2(500 + x*10, 250 + y*10));
                }
            }
            */

            AddFlockVehicle(new Vector2(20, 20));
            AddFlockVehicle(new Vector2(200, 200));


        }

        public void AddFlockVehicle(Vector2 position)
        {
            Vehicle vehicle = new Vehicle(50, 200, position);
            //vehicle.steeringBehaviours.Add(new AtractBehaviour(this, 100, vehicle));
            vehicle.steeringBehaviours.Add(new SeekBehaviour(target, vehicle));
            vehicle.steeringBehaviours.Add(new FlockingBehaviour(this, 100, vehicle));
            vehicles.Add(vehicle);
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
            target.LoadContent(Content);
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
                target.Position = clickHandler.GetMousePosition();
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
            target.Render(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public List<BaseEntity> GetEntitiesInRange(double range, BaseEntity center)
        {
            List<BaseEntity> inRange = new List<BaseEntity>();

            range = Math.Pow(range, 2);

            foreach(BaseEntity entity in vehicles)
            {
                if(entity != center)
                {
                    if (range > Vector2.DistanceSquared(entity.Position, center.Position))
                    {
                        inRange.Add(entity);
                    }
                }
            }

            return inRange;
        }
    }
}
