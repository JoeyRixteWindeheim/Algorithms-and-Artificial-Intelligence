using AAIFinalAssignment.behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment
{
    public class SpriteSheet
    {
        public Texture2D Fishes;

        public int width;
        public int height;

        public static readonly int[] Fishx = { 0, 1, 2, 1 };
        public static readonly int[] xStart = { 0, 3, 6, 9, 0, 3, 6, 9 };
        public static readonly int[] yStart = { 3, 3, 3, 3, 7, 7, 7, 7 };
        public SpriteSheet(Texture2D texture, int size)
        {
            Fishes = texture;
            width = height = size;

        }

        public void RenderFish(float gameTime, SpriteBatch _spriteBatch, Vector2 vector, Vector2 position,int texturenumber)
        {
            int currentsprite = (int)(gameTime*2) % 4;
            Rectangle rectangle = new Rectangle((Fishx[currentsprite]+xStart[texturenumber]) *width, yStart[texturenumber] *height, width, height);
            _spriteBatch.Draw(Fishes, position, rectangle, Color.White, BehaviourUtil.GetRotation(vector)+(float)(Math.PI/2), new Vector2(width/2,height/2), Vector2.One, SpriteEffects.None, 0);
        }

    }
}
