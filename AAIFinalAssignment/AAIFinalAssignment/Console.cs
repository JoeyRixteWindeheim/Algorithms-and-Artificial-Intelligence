using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AAIFinalAssignment
{
    public class Console
    {
        public bool Open { get; set; }
        public string input { get; set; }

        private HashSet<string> variables;
        private HashSet<string> functions;

        private ConsoleFunctions ConsoleFunctions = new ConsoleFunctions();

        private static List<popup> popups = new List<popup>();
        public Console()
        {
            Open = false;
            input = "";

            variables = new HashSet<string>();
            functions = new HashSet<string>();

            PropertyInfo[] Properties = Game1.settings.GetType().GetProperties();
            for (int i = 0; i < Properties.Length; i++)
            {
                variables.Add(Properties[i].Name);
            }

            MethodInfo[] Methods = ConsoleFunctions.GetType().GetMethods();
            for (int i = 0; i < Methods.Length; i++)
            {
                functions.Add(Methods[i].Name);
            }
        }

        public void HandleKeys(Keys[] PressedKeys)
        {
            foreach (Keys key in PressedKeys)
            {
                if (!Game1.LockedKeys.Contains(key))
                {
                    Game1.LockedKeys.Add(key);
                    int k = (int)key;
                    //letters
                    if (k >= 65 && k <= 90)
                    {
                        if (Game1.LockedKeys.Contains(Keys.LeftShift) || Game1.LockedKeys.Contains(Keys.RightShift))
                            input += key.ToString();
                        else
                            input += key.ToString().ToLower();
                    }

                    //numbers + numpad
                    if ((k >= 48 && k <= 57) || (k >= 96 && k <= 105))
                    {
                        input += k % 48;
                    }

                    //spacebar
                    if (k == 32)
                    {
                        input += " ";
                    }

                    //dot
                    if(k == 190)
                    {
                        input += ".";
                    }

                    //backspace
                    if (k == 8 && input != "")
                    {
                        input = input.Remove(input.Length - 1);
                    }

                    //enter
                    if (k == 13)
                    {
                        ExecuteCommand();
                    }
                }
            }
        }

        public void ExecuteCommand()
        {
            string[] inputarray = input.Split(' ');

            if (variables.Contains(inputarray[0]))
            {
                SetVariable(inputarray);
            }
            else if (functions.Contains(inputarray[0]))
            {
                ExecuteFunction(inputarray);
            }
            else
            {
                Popup(5, "Unknown command");
            }
        }

        private void ExecuteFunction(string[] values)
        {
            var method = ConsoleFunctions.GetType().GetMethod(values[0]);
            var par = method.GetParameters();
            object[] parameters = new object[par.Length];

            if(values.Length <= par.Length)
            {
                Popup(5, "missing parameters");
                return;
            }

            for (int i = 0; i < par.Length; i++) 
            {
                if (par[i].ParameterType == typeof(bool))
                {
                    bool parameter;
                    if (!bool.TryParse(values[i + 1], out parameter))
                    {
                        Popup(5, "could not parse " + values[i + 1]);
                        return;
                    }
                    parameters[i] = parameter;
                }
                if (par[i].ParameterType == typeof(int))
                {
                    int parameter;
                    if (!int.TryParse(values[i + 1], out parameter))
                    {
                        Popup(5, "could not parse " + values[i + 1]);
                        return;
                    }
                    parameters[i] = parameter;
                }
                if (par[i].ParameterType == typeof(float))
                {
                    float parameter;
                    if (!float.TryParse(values[i + 1], out parameter))
                    {
                        Popup(5, "could not parse " + values[i + 1]);
                        return;
                    }
                    parameters[i] = parameter;
                }

            }
            method.Invoke(ConsoleFunctions,parameters);
            input = "";
        }

        private void SetVariable(string[] inputarray)
        {
            var variable = Game1.settings.GetType().GetProperty(inputarray[0]);

            var type = variable.PropertyType;

            if (type == typeof(bool))
            {
                if(inputarray.Length == 1)
                {
                    variable.SetValue(Game1.settings, !(bool)variable.GetValue(Game1.settings));
                    input = "";
                    Popup(5, "succes");
                }
                else
                {
                    bool result;
                    if (bool.TryParse(inputarray[1], out result))
                    {
                        variable.SetValue(Game1.settings, result);
                        input = "";
                        Popup(5, "succes");
                    }
                    else
                    {
                        Popup(5, "could not parse value");
                    }
                }
                
            }
            if (type == typeof(int))
            {
                int result;
                if (int.TryParse(inputarray[1], out result))
                {
                    variable.SetValue(Game1.settings, result);
                    input = "";
                    Popup(5, "succes");
                }
                else
                {
                    Popup(5, "could not parse value");
                }
            }
            if (type == typeof(float))
            {
                float result;
                if (float.TryParse(inputarray[1], out result))
                {
                    variable.SetValue(Game1.settings, result);
                    input = "";
                    Popup(5, "succes");
                }
                else
                {
                    Popup(5, "could not parse value");
                }
            }
        }

        public static void Popup(int timeSeconds, string message)
        {
            popups.Add(new popup(DateTime.Now.AddSeconds(timeSeconds), message));
        }

        public void Render(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            if (Open)
            {
                _spriteBatch.DrawString(Game1.spriteFont, "< " + input + " >", new Vector2(0, 0), Color.Black);


                int Yposition = 10;

                for (int i = popups.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Now < popups[i].expireTime)
                    {
                        _spriteBatch.DrawString(Game1.spriteFont, popups[i].message, new Vector2(0, Yposition), Color.Black);
                        Yposition += 10;
                    }
                    else
                    {
                        popups.RemoveAt(0);
                        continue;
                    }
                }

            }
        }
    }

    struct popup
    {
        public popup(DateTime ExpireTime, string Message)
        {
            expireTime = ExpireTime;
            message = Message;
        }
        public DateTime expireTime;
        public string message;
    }
}
