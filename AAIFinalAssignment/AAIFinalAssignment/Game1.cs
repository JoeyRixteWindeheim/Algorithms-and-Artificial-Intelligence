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

        public static List<Vehicle> vehicles = new List<Vehicle>();
        public static List<Obstacle> Obstacles = new List<Obstacle>();
        private Vehicle target = new Vehicle(new Vector2(20,20));
        private ClickHandler clickHandler = new ClickHandler();

        public const int MinCoords = -10;
        public const int MaxCoords = 1200;
        public const int Mapsize =  MinCoords * -1 + MaxCoords;


        public static Settings settings = new Settings();

        public static SpriteFont spriteFont;

        public static Console Console;
        public static List<Keys> LockedKeys;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Console = new Console();
            LockedKeys = new List<Keys>();

            //BehaviourUtil.RenderVector(_spriteBatch, new Vector2(5, 5), new Vector2(10, 10));

            // Make 2 new vehicles and make #1 target #2
            /*
            vehicles.Add(new Vehicle(50, 200, new Vector2(0, 0)));
            vehicles.Add(new Vehicle(50, 0, new Vector2(300, 300)));
            vehicles[0].steeringBehaviours.Add(new SeekBehaviour(vehicles[1], vehicles[0]));
            */

            for (int x = 0; x<2; x++)
            {
                for (int y = 0; y <1; y++)
                {
                    AddFlockVehicle(new Vector2(500 + x*100, 250 + y*100));
                }
            }


            for (int x = 1; x < 6; x++)
            {
                for (int y = 1; y < 6; y++)
                {
                    Obstacle obstacle = new Obstacle();
                    obstacle.Position = new Vector2(200*x, 200*y);
                    obstacle.Radius = 30;
                    Obstacles.Add(obstacle);
                }
            }
            for (int x = 10; x < 20; x++)
            {
                Obstacle obstacle = new Obstacle();
                obstacle.Position = new Vector2(30 * x, 500);
                obstacle.Radius = 30;
                Obstacles.Add(obstacle);
            }
            

        }

        public void AddFlockVehicle(Vector2 position)
        {
            Vehicle vehicle = new Vehicle(position);
            //vehicle.steeringBehaviours.Add(new SeekBehaviour(target, vehicle));
            //vehicle.steeringBehaviours.Add(new FleeBehaviour(target, vehicle));
            vehicle.steeringBehaviours.Add(new WanderBehaviour(vehicle));
            vehicle.steeringBehaviours.Add(new DistancingBehaviour(vehicle));
            vehicle.steeringBehaviours.Add(new GroupPressureBehaviour(vehicle));
            vehicle.steeringBehaviours.Add(new ObstacleAvoidance(vehicle));
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
            spriteFont = Content.Load<SpriteFont>("Ariel");


            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.LoadContent(Content);
            }
            target.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            HandleKeyboard();


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

        private void HandleKeyboard()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            if (state.IsKeyDown(Keys.LeftAlt) && !LockedKeys.Contains(Keys.LeftAlt))
            {
                LockedKeys.Add(Keys.LeftAlt);
                Console.Open = !Console.Open;
            }

            if (Console.Open)
            {
                Console.HandleKeys(state.GetPressedKeys());
            }

            List<Keys> StillLocked = new List<Keys>();

            for (int i = 0; i < LockedKeys.Count; i++)
            {
                if (!state.IsKeyUp(LockedKeys[i]))
                {
                    StillLocked.Add(LockedKeys[i]);
                }
            }

            LockedKeys = StillLocked;
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
            foreach (Obstacle obstacle in Obstacles)
            {
                obstacle.Render(gameTime, _spriteBatch);
            }
            target.Render(gameTime, _spriteBatch);

            Console.Render(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public static List<MovingEntity> GetMovingEntitiesInRange(double range, BaseEntity center)
        {
            List<MovingEntity> inRange = new List<MovingEntity>();

            range = Math.Pow(range, 2);

            foreach(MovingEntity entity in vehicles)
            {
                if(entity != center)
                {
                    if (range > Vector2.DistanceSquared(entity.GetClosestCoords(center.Position), center.Position))
                    {
                        inRange.Add(entity);
                    }
                }
            }

            return inRange;
        }

        public static List<Obstacle> GetObstaclesInRange(double range, Vector2 center)
        {
            List<Obstacle> inRange = new List<Obstacle>();

            range = Math.Pow(range, 2);

            foreach (Obstacle obstacle in Obstacles)
            {
                if (range > Vector2.DistanceSquared(obstacle.GetClosestCoords(center), center))
                {
                    inRange.Add(obstacle);
                }
            }

            return inRange;
        }

    }
}
