using A1r.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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

        bool editingText = false;

        ToolType selectedTool;
        float doubleclick = 0;
        Item selectedItem;

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
                    selectedItem = presentation.current.ItemClicked(im.GetMousePosition());
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
                        
                        if (selectedItem != null)
                        {
                            selectedItem.Edit(this);
                        }
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

        public bool TryConvertKeyboardInput(out char key)
        {
            //credit to Roy T. 2010, modified to work with penguinpoints by Fiz 2023
            Keys[] keys = im.currentKeyboardState.GetPressedKeys();
            bool shift = im.currentKeyboardState.IsKeyDown(Keys.LeftShift) || im.currentKeyboardState.IsKeyDown(Keys.RightShift);

            if (keys.Length > 0 && !im.previousKeyboardState.IsKeyDown(keys[0]))
            {
                switch (keys[0])
                {
                    //Alphabet keys
                    case Keys.A: if (shift) { key = 'A'; } else { key = 'a'; } return true;
                    case Keys.B: if (shift) { key = 'B'; } else { key = 'b'; } return true;
                    case Keys.C: if (shift) { key = 'C'; } else { key = 'c'; } return true;
                    case Keys.D: if (shift) { key = 'D'; } else { key = 'd'; } return true;
                    case Keys.E: if (shift) { key = 'E'; } else { key = 'e'; } return true;
                    case Keys.F: if (shift) { key = 'F'; } else { key = 'f'; } return true;
                    case Keys.G: if (shift) { key = 'G'; } else { key = 'g'; } return true;
                    case Keys.H: if (shift) { key = 'H'; } else { key = 'h'; } return true;
                    case Keys.I: if (shift) { key = 'I'; } else { key = 'i'; } return true;
                    case Keys.J: if (shift) { key = 'J'; } else { key = 'j'; } return true;
                    case Keys.K: if (shift) { key = 'K'; } else { key = 'k'; } return true;
                    case Keys.L: if (shift) { key = 'L'; } else { key = 'l'; } return true;
                    case Keys.M: if (shift) { key = 'M'; } else { key = 'm'; } return true;
                    case Keys.N: if (shift) { key = 'N'; } else { key = 'n'; } return true;
                    case Keys.O: if (shift) { key = 'O'; } else { key = 'o'; } return true;
                    case Keys.P: if (shift) { key = 'P'; } else { key = 'p'; } return true;
                    case Keys.Q: if (shift) { key = 'Q'; } else { key = 'q'; } return true;
                    case Keys.R: if (shift) { key = 'R'; } else { key = 'r'; } return true;
                    case Keys.S: if (shift) { key = 'S'; } else { key = 's'; } return true;
                    case Keys.T: if (shift) { key = 'T'; } else { key = 't'; } return true;
                    case Keys.U: if (shift) { key = 'U'; } else { key = 'u'; } return true;
                    case Keys.V: if (shift) { key = 'V'; } else { key = 'v'; } return true;
                    case Keys.W: if (shift) { key = 'W'; } else { key = 'w'; } return true;
                    case Keys.X: if (shift) { key = 'X'; } else { key = 'x'; } return true;
                    case Keys.Y: if (shift) { key = 'Y'; } else { key = 'y'; } return true;
                    case Keys.Z: if (shift) { key = 'Z'; } else { key = 'z'; } return true;

                    //Decimal keys
                    case Keys.D0: if (shift) { key = ')'; } else { key = '0'; } return true;
                    case Keys.D1: if (shift) { key = '!'; } else { key = '1'; } return true;
                    case Keys.D2: if (shift) { key = '@'; } else { key = '2'; } return true;
                    case Keys.D3: if (shift) { key = '#'; } else { key = '3'; } return true;
                    case Keys.D4: if (shift) { key = '$'; } else { key = '4'; } return true;
                    case Keys.D5: if (shift) { key = '%'; } else { key = '5'; } return true;
                    case Keys.D6: if (shift) { key = '^'; } else { key = '6'; } return true;
                    case Keys.D7: if (shift) { key = '/'; } else { key = '7'; } return true;
                    case Keys.D8: if (shift) { key = '*'; } else { key = '8'; } return true;
                    case Keys.D9: if (shift) { key = '('; } else { key = '9'; } return true;

                    //Decimal numpad keys
                    case Keys.NumPad0: key = '0'; return true;
                    case Keys.NumPad1: key = '1'; return true;
                    case Keys.NumPad2: key = '2'; return true;
                    case Keys.NumPad3: key = '3'; return true;
                    case Keys.NumPad4: key = '4'; return true;
                    case Keys.NumPad5: key = '5'; return true;
                    case Keys.NumPad6: key = '6'; return true;
                    case Keys.NumPad7: key = '7'; return true;
                    case Keys.NumPad8: key = '8'; return true;
                    case Keys.NumPad9: key = '9'; return true;

                    //Special keys
                    case Keys.OemTilde: if (shift) { key = '~'; } else { key = '`'; } return true;
                    case Keys.OemSemicolon: if (shift) { key = ':'; } else { key = ';'; } return true;
                    case Keys.OemQuotes: if (shift) { key = '"'; } else { key = '\''; } return true;
                    case Keys.OemQuestion: if (shift) { key = '?'; } else { key = '/'; } return true;
                    case Keys.OemPlus: if (shift) { key = '+'; } else { key = '='; } return true;
                    case Keys.OemPipe: if (shift) { key = '|'; } else { key = '\\'; } return true;
                    case Keys.OemPeriod: if (shift) { key = '>'; } else { key = '.'; } return true;
                    case Keys.OemOpenBrackets: if (shift) { key = '{'; } else { key = '['; } return true;
                    case Keys.OemCloseBrackets: if (shift) { key = '}'; } else { key = ']'; } return true;
                    case Keys.OemMinus: if (shift) { key = '_'; } else { key = '-'; } return true;
                    case Keys.OemComma: if (shift) { key = '<'; } else { key = ','; } return true;
                    case Keys.Space: key = ' '; return true;
                }
            }

            key = (char)0;
            return false;
        }

    }

    
}
