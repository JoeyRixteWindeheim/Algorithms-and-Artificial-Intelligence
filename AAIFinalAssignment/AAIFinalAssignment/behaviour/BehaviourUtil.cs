using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment.behaviour
{
    public class BehaviourUtil
    {

        public static Texture2D texture;
        public static Texture2D Circle;

        public static Vector2 CalculateSeekVector(Vector2 start, Vector2 target)
        {
            Vector2 resultingVector = new Vector2(0, 0);
            // Get the distance between self and target
            var distance = Vector2.DistanceSquared(start, target);



            if (distance > 1)
            {
                resultingVector = Vector2.Subtract(target, start);


                // Increase vector if far away, decrease when near
                double urgencyFactor = (resultingVector.Length() + 5) / (resultingVector.Length() + 2);

                // Normalise distance -> convert to direction
                resultingVector.Normalize();

                resultingVector = Vector2.Multiply(resultingVector, (Single)urgencyFactor);


            }

            // If the method reaches this point, the vehicle is at the position of the target.
            // So, there's no point in moving

            return resultingVector;



        }

        public static Single GetRotation(Vector2 vector)
        {

            // Calculate the angle.
            var Angle = MathF.Atan(vector.Y/ vector.X);

            if (vector.X < 0)
            {
                Angle += (float)Math.PI;
            }
            return Angle;
        }

        public static Vector2 RotateVector(Vector2 vector,double angle)
        {
            Vector2 result = new Vector2(0, 0);
            result.X = (float)(Math.Cos(angle) * vector.X - Math.Sin(angle) * vector.Y);
            result.Y = (float)(Math.Sin(angle) * vector.X + Math.Cos(angle) * vector.Y);
            return result;
        }

        public static Texture2D getTexture(SpriteBatch _spriteBatch)
        {
            if (texture == null)
            {
                texture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                Int32[] pixel = { 0xFFFFFF }; // White. 0xFF is Red, 0xFF0000 is Blue
                texture.SetData<Int32>(pixel, 0, texture.Width * texture.Height);
            }
            return texture;
        }



        public static Texture2D getCircleTexture(SpriteBatch _spriteBatch)
        {
            
            if (Circle != null)
                return Circle;
            int radius = 500;
            Texture2D texture = new Texture2D(_spriteBatch.GraphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);

            Circle = texture;
            return texture;
        }

        public static void RenderVector(SpriteBatch _spriteBatch, Vector2 vector, Vector2 position,double multiplier, Color color)
        {

            Rectangle rectangle = new Rectangle(0, 0, (int)(vector.LengthSquared()* multiplier), 1);
            _spriteBatch.Draw(getTexture(_spriteBatch), position, rectangle, color, GetRotation(vector), Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public static void RenderVectorPrecise(SpriteBatch _spriteBatch, Vector2 vector, Vector2 position, int length, Color color)
        {

            Rectangle rectangle = new Rectangle(0, 0, length, 2);
            _spriteBatch.Draw(getTexture(_spriteBatch), position, rectangle, color, GetRotation(vector), Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public static void RenderPoint(SpriteBatch _spriteBatch,Vector2 position,Color color)
        {
            Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, 3, 3);
            _spriteBatch.Draw(getTexture(_spriteBatch), rectangle, color);
        }

        public static void RenderCircle(SpriteBatch _spriteBatch, Vector2 position, int radius, Color color)
        {
            Rectangle rectangle = new Rectangle((int)position.X-radius, (int)position.Y-radius, radius*2, radius*2);
            _spriteBatch.Draw(getCircleTexture(_spriteBatch), rectangle, color);
        }
    }
}
