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
            return new Vector2(mouseState.Position.X, mouseState.Position.Y);
        }
    }
}
