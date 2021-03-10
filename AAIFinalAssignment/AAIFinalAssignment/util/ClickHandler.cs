using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AAIFinalAssignment.util
{
    public class ClickHandler
    {
        MouseState mouseState = Mouse.GetState();
        public bool CheckMouseClicked()
        {
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else { return false; }
        }

        public Vector2 GetMousePosition()
        {
            return Game1.getWithinField( new Vector2(mouseState.Position.X+Game1.ScreenLeft, mouseState.Position.Y+Game1.ScreenTop));
        }
    }
}
