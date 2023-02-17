using A1r.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PenguinPoints
{
    public enum ToolType
    {
        Text, Image
    }
    class Controller
    {
        InputManager im;
        public bool editing = true;
        ToolType selectedTool;
        float doubleclick = 0;
        Rectangle textRectangle, imageRectangle;
        public Controller(InputManager input)
        {
            im = input;

            double widthmult = 1 + (1280f - 1021f) / 1021f;
            textRectangle = new Rectangle((int)(417 * widthmult), 0, (int)(38 * widthmult), 43);
            imageRectangle = new Rectangle((int)(455 * widthmult), 0, (int)(41 * widthmult), 43);
        }

        public void Update(Presentation presentation, float deltaTime)
        {
            doubleclick -= deltaTime;
            if(!editing)
            {
                if (im.JustPressed(Microsoft.Xna.Framework.Input.Keys.Space) || im.JustPressed(MouseInput.LeftButton))
                {
                    presentation.NextSlide();
                }

                if (im.JustPressed(Microsoft.Xna.Framework.Input.Keys.Back) || im.JustPressed(MouseInput.RightButton))
                {
                    presentation.PreviousSlide();
                }
                if (im.JustPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                {
                    editing = true;
                }
            }
            
            if(editing)
            {
                if (im.JustPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    editing = false;
                }

                if(im.JustPressed(MouseInput.LeftButton))
                {
                    if(textRectangle.Contains(im.GetMousePosition()))
                    {
                        selectedTool = ToolType.Text;
                    }
                    if (imageRectangle.Contains(im.GetMousePosition()))
                    {
                        selectedTool = ToolType.Image;
                    }

                    if (doubleclick > 0)
                    {
                        
                    }
                    doubleclick = 400.0f;

                    //if selection null start timer
                }

                if (im.JustPressed(MouseInput.RightButton))
                {
                    switch(selectedTool)
                    {
                        case ToolType.Text:
                            presentation.current.AddItem(new Text("New Text", im.GetMousePosition().ToVector2()));
                            break;

                        case ToolType.Image:
                            presentation.current.AddItem(new Image(Game1.icon_small, im.GetMousePosition().ToVector2()));
                            break;
                    }
                }

            }
            
            
        }
    }
}
