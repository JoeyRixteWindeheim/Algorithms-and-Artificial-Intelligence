﻿using AAIFinalAssignment.behaviour;
using AAIFinalAssignment.entity;
using AAIFinalAssignment.Grid;
using AAIFinalAssignment.util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using AAIFinalAssignment.statemachine.states;

namespace AAIFinalAssignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

<<<<<<< HEAD
        private List<Vehicle> vehicles = new List<Vehicle>();
        private List<VehicleWithStates> vehiclesWithStates = new List<VehicleWithStates>();

        private Vehicle target = new Vehicle(10,0,new Vector2(20,20));
=======
        public static SpriteSheet FishSprites;
        public static SpriteSheet SharkSprites;

        public static Random Random;

        public static List<MovingEntity> MovingEntities = new List<MovingEntity>();
        public static List<Obstacle> Obstacles = new List<Obstacle>();
        private Target target = new Target();
>>>>>>> 63c802434a527f84c42fba632961f6e9fa7b4d09
        private ClickHandler clickHandler = new ClickHandler();

        public const int MinCoords = 0;
        public const int MaxCoords = 1000;
        public const int Mapsize = MinCoords * -1 + MaxCoords;


        public static Settings settings = new Settings();

        public static SpriteFont spriteFont;

        public static Console Console;
        public static List<Keys> LockedKeys;

        public static MapGrid Grid;

        public static Vector2 TopLeftScreen;

        public static float ScreenTop { get => TopLeftScreen.Y; set => TopLeftScreen.Y = value; }
        public static float ScreenBottom => ScreenTop + ScreenHeight;
        public static float ScreenHeight => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public static float ScreenLeft { get => TopLeftScreen.X; set => TopLeftScreen.X = value; }
        public static float ScreenRight => ScreenLeft + ScreenWidth;
        public static float ScreenWidth => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;


        public Game1()
        {

            Random = new Random((int)(DateTime.Now.Ticks));
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            TopLeftScreen = new Vector2(0, 0);
            Grid = new MapGrid(20);

            Console = new Console();
            LockedKeys = new List<Keys>();
            AddShark(Vector2.Zero);

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    AddFlockFish(new Vector2(500 + x * 100, 250 + y * 100));
                    //AddShark(new Vector2(500 + x * 100, 250 + y * 100));
                }
            }
        }

        public static void AddObstacle(Obstacle obstacle)
        {
            obstacle.Position = Game1.getWithinField(obstacle.Position);

            int range = (int)(obstacle.Radius / Grid.RegionSize) + 1;

            Vector2 MiddleShift = new Vector2(Grid.RegionSize / 2, Grid.RegionSize / 2);
            double Range = Math.Pow(Grid.RegionSize / 2 + obstacle.Radius, 2);

            for (int x = -range; x <= range; x++)
            {
                for (int y = -range; y <= range; y++)
                {
                    Vector2 RegionVector = new Vector2(x * Grid.RegionSize + obstacle.Position.X, y * Grid.RegionSize + obstacle.Position.Y);
                    Vector2 RegionMiddle = getWithinField(RegionVector + MiddleShift);
                    double distance = Vector2.DistanceSquared(RegionMiddle, obstacle.Position);
                    if (distance < Range)
                    {
                        Grid.getRegion(RegionMiddle).AddObstacle(obstacle);
                    }
                }
            }

            Obstacles.Add(obstacle);
        }



        public static List<Vector2> CalculateRenderPosition(Vector2 position)
        {
<<<<<<< HEAD
            //Vehicle vehicle = new Vehicle(50, 200, position);

            //vehicle.steeringBehaviours.Add(new AtractBehaviour(this, 100, vehicle));
            //vehicle.steeringBehaviours.Add(new SeekBehaviour(target, vehicle));
            //vehicle.steeringBehaviours.Add(new FlockingBehaviour(this, 100, vehicle));
            // vehicles.Add(vehicle);

            VehicleWithStates vehicle = new VehicleWithStates(50, 200, position, target, this);
            vehicle.StateMachine.SetState(new FindFoodState(vehicle.StateMachine));
            vehiclesWithStates.Add(vehicle);
            
=======
            Vector2 shift = new Vector2(ScreenLeft % Mapsize, ScreenTop % Mapsize);

            List<Vector2> returnVectors = new List<Vector2>();


            int MaxX = (int)(ScreenWidth / Mapsize) + 1;
            int MaxY = (int)(ScreenHeight / Mapsize) + 1;

            for (int x = -1; x <= MaxX; x++)
            {
                for (int y = -1; y <= MaxY; y++)
                {
                    Vector2 vector = new Vector2(position.X + Mapsize * x, position.Y + Mapsize * y);
                    vector -= shift;
                    returnVectors.Add(vector);
                }
            }

            return returnVectors;
        }

        public void AddFlockFish(Vector2 position)
        {
            Fish fish = new Fish(position);
            //vehicle.steeringBehaviours.Add(new SeekBehaviour(target, vehicle));
            //vehicle.steeringBehaviours.Add(new FleeBehaviour(target, vehicle));
            fish.steeringBehaviours.Add(new WanderBehaviour(fish));
            fish.steeringBehaviours.Add(new DistancingBehaviour(fish));
            fish.steeringBehaviours.Add(new GroupPressureBehaviour(fish));
            //vehicle.steeringBehaviours.Add(new ObstacleAvoidance(vehicle));
            MovingEntities.Add(fish);
        }

        public void AddShark(Vector2 position)
        {
            Shark shark = new Shark(position);
            shark.steeringBehaviours.Add(new WanderBehaviour(shark));
            MovingEntities.Add(shark);
>>>>>>> 63c802434a527f84c42fba632961f6e9fa7b4d09
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
            FishSprites = new SpriteSheet(Content.Load<Texture2D>("fishSprites"),48);
            SharkSprites = new SpriteSheet(Content.Load<Texture2D>("SharkSprites"), 96);

<<<<<<< HEAD
            
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.LoadContent(Content);
            }

            foreach (VehicleWithStates vehicle in vehiclesWithStates)
            {
                vehicle.LoadContent(Content);
            }


            target.LoadContent(Content);
=======
>>>>>>> 63c802434a527f84c42fba632961f6e9fa7b4d09
        }

        protected override void Update(GameTime gameTime)
        {
            HandleKeyboard();


            // TODO: Add your update logic here
            foreach (MovingEntity vehicle in MovingEntities)
            {
                vehicle.Update(gameTime);
            }

            foreach (VehicleWithStates vehicle in vehiclesWithStates)
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

            if (state.IsKeyDown(Keys.Up))
                ScreenTop -= Settings.ScrollSpeed;
            if (state.IsKeyDown(Keys.Down))
                ScreenTop += Settings.ScrollSpeed;
            if (state.IsKeyDown(Keys.Left))
                ScreenLeft -= Settings.ScrollSpeed;
            if (state.IsKeyDown(Keys.Right))
                ScreenLeft += Settings.ScrollSpeed;
            TopLeftScreen = getWithinField(TopLeftScreen);

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

            Grid.Render(gameTime, _spriteBatch);

            foreach (MovingEntity vehicle in MovingEntities)
            {
                vehicle.Render(gameTime, _spriteBatch);
            }
<<<<<<< HEAD

            foreach (VehicleWithStates vehicle in vehiclesWithStates)
            {
                vehicle.Render(gameTime, _spriteBatch);
=======
            foreach (Obstacle obstacle in Obstacles)
            {
                obstacle.Render(gameTime, _spriteBatch);
>>>>>>> 63c802434a527f84c42fba632961f6e9fa7b4d09
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

            foreach (MovingEntity entity in MovingEntities)
            {
                if (entity != center)
                {
                    if (range > Vector2.DistanceSquared(entity.GetClosestCoords(center.Position), center.Position))
                    {
                        inRange.Add(entity);
                    }
                }
            }

            foreach (BaseEntity entity in vehiclesWithStates)
            {
                if (entity != center)
                {
                    if (range > Vector2.DistanceSquared(entity.Position, center.Position))
                    {
                        inRange.Add(entity);
                    }
                }
            }

            return inRange;
        }

        public static List<Obstacle> GetObstaclesInRange(Vector2 center)
        {
            center = getWithinField(center);
            return Grid.getRegion(center).Obstacles;
        }

        public static Vector2 getWithinField(Vector2 position)
        {
            if (position.X > MaxCoords)
            {
                position = new Vector2(position.X - Mapsize, position.Y);
            }
            if (position.Y > MaxCoords)
            {
                position = new Vector2(position.X, position.Y - Mapsize);
            }
            if (position.X < MinCoords)
            {
                position = new Vector2(position.X + Mapsize, position.Y);
            }
            if (position.Y < MinCoords)
            {
                position = new Vector2(position.X, position.Y + Mapsize);
            }
            return position;
        }

        public static Vector2 GetClosestCoords(Vector2 me, Vector2 other)
        {
            float smallestDistance = Vector2.DistanceSquared(me, other);
            Vector2 closest = other;


            int[] x = new int[] { 1, 0, -1, -1, -1, 0, 1, 1 };
            int[] y = new int[] { 1, 1, 1, 0, -1, -1, -1, 0 };

            for (int i = 0; i < 8; i++)
            {
                Vector2 current = other;
                current.X += Game1.Mapsize * x[i];
                current.Y += Game1.Mapsize * y[i];

                float distance = Vector2.DistanceSquared(current, me);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closest = current;
                }
            }
            return closest;
        }

    }
}
