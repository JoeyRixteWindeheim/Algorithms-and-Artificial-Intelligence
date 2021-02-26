using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAIFinalAssignment.entity
{
    public abstract class BaseEntity
    {
        public abstract Vector2 Position { get; set; }
        public abstract Texture2D Texture { get; set; }


        public Rectangle BoundingBox
        {
            get 
            {
                return new Rectangle((int)Position.X - Texture.Width / 2,
                                      (int)Position.Y - Texture.Height / 2,
                                      Texture.Width,
                                      Texture.Height); 
            }
        }

        public abstract void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content);
        public abstract void Update(GameTime gameTime);
        public abstract void Render(GameTime gameTime, SpriteBatch _spriteBatch);


        int[] x = { 0, 0, -1, 1 };
        int[] y = { -1, 1, 0, 0 };

        public Vector2 GetClosestCoords(Vector2 me)
        {
            float smallestDistance = Vector2.DistanceSquared(me,Position);
            Vector2 closest = Position;
            int mapsize = Game1.MinCoords * -1 + Game1.MaxCoords;

            for(int i = 0; i <= 3; i+=2)
            {
                Vector2 current = Position;
                current.X += mapsize * x[i];
                current.Y += mapsize * y[i];

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
