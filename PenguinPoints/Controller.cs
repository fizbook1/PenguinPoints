using A1r.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        bool editingItem = false;

        ToolType selectedTool;
        float doubleclick = 0;
        Item selectedItem;

        //drag related
        bool grabbed = false;
        Point offset = Point.Zero;

        Texture2D selectionRectangle;

        Rectangle textRectangle, imageRectangle, startRectangle, deleteRectangle;
        public Controller(InputManager input)
        {
            im = input;

            double widthmult = 1 + (1280f - 1021f) / 1021f;
            textRectangle = new Rectangle((int)(417 * widthmult), 0, (int)(38 * widthmult), 43);
            imageRectangle = new Rectangle((int)(455 * widthmult), 0, (int)(41 * widthmult), 43);
            startRectangle = new Rectangle((int)(19 * widthmult), 0, (int)(38 * widthmult), 43);
            deleteRectangle = new Rectangle((int)(494 * widthmult), 0, (int)(38 * widthmult), 43);
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
                if(editingItem)
                {
                    selectedItem.Edit(this);
                }

                if (im.JustPressed(Keys.Enter) && !editingItem)
                {
                    editing = false;
                }

                CheckDelete();

                CheckGrab();

                if (im.JustPressed(MouseInput.LeftButton))
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
                    

                    if (selectedItem == null)
                    {
                        editingItem = false;
                    }
                    else
                    {
                        ItemSelected();
                    }

                    if (doubleclick > 0)
                    {
                        
                        if (selectedItem != null)
                        {
                            editingItem = true;
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

            void CheckDelete()
            {
                if (im.JustPressed(Keys.Delete) && selectedItem != null)
                {
                    presentation.current.DeleteItem(selectedItem);
                    selectedItem = null;
                }

                if (im.JustPressed(MouseInput.LeftButton))
                {
                    if (deleteRectangle.Contains(im.GetMousePosition()) && selectedItem != null)
                    {
                        presentation.current.DeleteItem(selectedItem);
                        selectedItem = null;
                    }
                }
            }

            void CheckGrab()
            {
                if (im.IsHeld(MouseInput.LeftButton) && selectedItem != null)
                {
                    if (selectedItem.Size.Contains(im.GetMousePosition()) && !grabbed)
                    {
                        offset = selectedItem.Position.ToPoint() - im.GetMousePosition();
                        grabbed = true;
                    }

                    if (grabbed)
                    {
                        selectedItem.Drag((im.GetMousePosition() + offset).ToVector2());
                    }
                }
                else
                {
                    grabbed = false;
                }
            }
            
        }

        private void ItemSelected()
        {
            

            selectionRectangle = Game1.self.GenerateTexture(selectedItem.Size.Width, selectedItem.Size.Height);
        }

        public bool TryConvertKeyboardInput(out string chars)
        {
            //credit to Roy T. 2010, modified to work with penguinpoints by Fiz 2023 to support fast typing 
            Keys[] keys = im.currentKeyboardState.GetPressedKeys();
            bool shift = im.currentKeyboardState.IsKeyDown(Keys.LeftShift) || im.currentKeyboardState.IsKeyDown(Keys.RightShift);

            string characters = "";

            if(keys.Length > 2)
            {
                int debug = 5;
            }

            for(int i = 0; i < keys.Length; i++)
            {
                if(!im.previousKeyboardState.IsKeyDown(keys[i]))
                {
                    switch (keys[i])
                    {
                        //Alphabet keys
                        case Keys.A: if (shift) { characters += "A"; } else { characters += 'a'; } break;
                        case Keys.B: if (shift) { characters += 'B'; } else { characters += 'b'; } break;
                        case Keys.C: if (shift) { characters += 'C'; } else { characters += 'c'; } break;
                        case Keys.D: if (shift) { characters += 'D'; } else { characters += 'd'; } break;
                        case Keys.E: if (shift) { characters += 'E'; } else { characters += 'e'; } break;
                        case Keys.F: if (shift) { characters += 'F'; } else { characters += 'f'; } break;
                        case Keys.G: if (shift) { characters += 'G'; } else { characters += 'g'; } break;
                        case Keys.H: if (shift) { characters += 'H'; } else { characters += 'h'; } break;
                        case Keys.I: if (shift) { characters += 'I'; } else { characters += 'i'; } break; 
                        case Keys.J: if (shift) { characters += 'J'; } else { characters += 'j'; } break;
                        case Keys.K: if (shift) { characters += 'K'; } else { characters += 'k'; } break;
                        case Keys.L: if (shift) { characters += 'L'; } else { characters += 'l'; } break;
                        case Keys.M: if (shift) { characters += 'M'; } else { characters += 'm'; } break;
                        case Keys.N: if (shift) { characters += 'N'; } else { characters += 'n'; } break;
                        case Keys.O: if (shift) { characters += 'O'; } else { characters += 'o'; } break;
                        case Keys.P: if (shift) { characters += 'P'; } else { characters += 'p'; } break;
                        case Keys.Q: if (shift) { characters += 'Q'; } else { characters += 'q'; } break;
                        case Keys.R: if (shift) { characters += 'R'; } else { characters += 'r'; } break;
                        case Keys.S: if (shift) { characters += 'S'; } else { characters += 's'; } break;
                        case Keys.T: if (shift) { characters += 'T'; } else { characters += 't'; } break;
                        case Keys.U: if (shift) { characters += 'U'; } else { characters += 'u'; } break;
                        case Keys.V: if (shift) { characters += 'V'; } else { characters += 'v'; } break;
                        case Keys.W: if (shift) { characters += 'W'; } else { characters += 'w'; } break;
                        case Keys.X: if (shift) { characters += 'X'; } else { characters += 'x'; } break;
                        case Keys.Y: if (shift) { characters += 'Y'; } else { characters += 'y'; } break;
                        case Keys.Z: if (shift) { characters += 'Z'; } else { characters += 'z'; } break;

                        //Decimal keys
                        case Keys.D0: if (shift) { characters += ')'; } else { characters += '0'; } break;
                        case Keys.D1: if (shift) { characters += '!'; } else { characters += '1'; } break;
                        case Keys.D2: if (shift) { characters += '@'; } else { characters += '2'; } break;
                        case Keys.D3: if (shift) { characters += '#'; } else { characters += '3'; } break;
                        case Keys.D4: if (shift) { characters += '$'; } else { characters += '4'; } break;
                        case Keys.D5: if (shift) { characters += '%'; } else { characters += '5'; } break;
                        case Keys.D6: if (shift) { characters += '^'; } else { characters += '6'; } break;
                        case Keys.D7: if (shift) { characters += '/'; } else { characters += '7'; } break;
                        case Keys.D8: if (shift) { characters += '*'; } else { characters += '8'; } break;
                        case Keys.D9: if (shift) { characters += '('; } else { characters += '9'; } break;

                        //Decimal numpad keys
                        case Keys.NumPad0: characters += '0'; break;
                        case Keys.NumPad1: characters += '1'; break;
                        case Keys.NumPad2: characters += '2'; break;
                        case Keys.NumPad3: characters += '3'; break;
                        case Keys.NumPad4: characters += '4'; break;
                        case Keys.NumPad5: characters += '5'; break;
                        case Keys.NumPad6: characters += '6'; break;
                        case Keys.NumPad7: characters += '7'; break;
                        case Keys.NumPad8: characters += '8'; break;
                        case Keys.NumPad9: characters += '9'; break;

                        //Special keys
                        case Keys.OemTilde:         if (shift) { characters += '~'; } else { characters += '`'; }   break;
                        case Keys.OemSemicolon:     if (shift) { characters += ':'; } else { characters += ';'; }   break;
                        case Keys.OemQuotes:        if (shift) { characters += '"'; } else { characters += '\''; }  break;
                        case Keys.OemQuestion:      if (shift) { characters += '?'; } else { characters += '/'; }   break;
                        case Keys.OemPlus:          if (shift) { characters += '+'; } else { characters += '='; }   break;
                        case Keys.OemPipe:          if (shift) { characters += '|'; } else { characters += '\\'; }  break;
                        case Keys.OemPeriod:        if (shift) { characters += '>'; } else { characters += '.'; }   break;
                        case Keys.OemOpenBrackets:  if (shift) { characters += '{'; } else { characters += '['; }   break;
                        case Keys.OemCloseBrackets: if (shift) { characters += '}'; } else { characters += ']'; }   break;
                        case Keys.OemMinus:         if (shift) { characters += '_'; } else { characters += '-'; }   break;
                        case Keys.OemComma:         if (shift) { characters += '<'; } else { characters += ','; }   break;
                        case Keys.Space: characters += ' '; break;

                        case Keys.Back: characters = "™"; break;

                        case Keys.Enter: characters = "Ã"; break;
                    }
                }
            }

            chars = characters;
            if (characters.Equals("") || (!characters.Equals("™") && characters.Contains('™')))
            {
                return false;
            }
            
            return true;
        }

        public void Draw(SpriteBatch sb)
        {
            if(selectedItem != null)
            {
                sb.Draw(selectionRectangle, selectedItem.Position, Color.White);
            }
            
        }

    }

    
}
