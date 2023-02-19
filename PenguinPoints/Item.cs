using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PenguinPoints
{
    abstract class Item
    {
        protected Vector2 position;
        protected Rectangle size;

        public Vector2 Position { get => position; }
        public Rectangle Size { get => size; }

        public abstract void Edit(Controller control);

        public abstract void Drag(Vector2 mousepos);

        public abstract void Draw(SpriteBatch sb);
    }

    class Text : Item
    {
        StringBuilder text;
        Color color = Color.Black;
        public Text(string text, Vector2 position)
        {
            this.text = new StringBuilder(text);
            this.position = position;
            size = new Rectangle(position.ToPoint(), new Point(60, 60));
        }

        public Text()
        {

        }

        public override void Edit(Controller control)
        {
            if(text.Equals("New Text"))
            {
                text.Clear();
            }
            string addChar;
            if(control.TryConvertKeyboardInput(out addChar))
            {
                if(addChar.Equals("™") && text.Length > 0)
                {
                    text.Remove(text.Length - 1, 1);
                }
                else
                {
                    text.Append(addChar);

                }
            }
        }

        public override void Drag(Vector2 mousepos)
        {
            position = mousepos;
            //size.Location = position.ToPoint();
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Game1.font, text, position, color);
        }
    }

    class Image : Item
    {
        Texture2D image;
        public Image(Texture2D image, Vector2 position)
        {
            this.position = position - new Vector2(image.Width / 2, image.Height / 2);
            this.image = image;
            size = new Rectangle(position.ToPoint(), new Point(image.Width, image.Height));
        }

        

        public override void Drag(Vector2 mousepos)
        {
            position = mousepos;
            size.Location = position.ToPoint();
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(image, position, Color.White);
        }

        public override void Edit(Controller control)
        {
            //throw new NotImplementedException();
        }
    }
}
