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
        List<StringBuilder> texts = new List<StringBuilder>();
        StringBuilder longestline = new StringBuilder("i");
        Color color = Color.Black;
        public Text(string text, Vector2 position)
        {
            texts.Add(new StringBuilder(text));
            this.position = position;
            size = new Rectangle(position.ToPoint(), Game1.font.MeasureString(texts[^1]).ToPoint());
        }

        public Text()
        {
            
        }

        public override void Edit(Controller control)
        {
            if(texts[^1].Equals("New Text"))
            {
                texts[^1].Clear();
            }
            string addChar;
            if(control.TryConvertKeyboardInput(out addChar))
            {
                if(addChar.Equals("™") && texts[^1].Length > 0)
                {
                    texts[^1].Remove(texts[^1].Length - 1, 1);
                }
                else if(addChar.Equals("™") && texts.Count > 1)
                {
                    texts.RemoveAt(texts.Count - 1);
                }
                else if(addChar.Equals("Ã"))
                {
                    texts.Add(new StringBuilder());
                }
                else
                {
                    texts[^1].Append(addChar);
                }

                if(longestline.Length < texts[^1].Length)
                {
                    longestline = texts[^1];
                }
            }

            size = new Rectangle(position.ToPoint(), new Point((int)Game1.font.MeasureString(longestline).X + 3, 3 + ((int)Game1.font.MeasureString("A").Y * texts.Count)));

        }

        public override void Drag(Vector2 mousepos)
        {
            position = mousepos;
            size.Location = position.ToPoint();
        }

        public override void Draw(SpriteBatch sb)
        {
            for(int i = 0; i < texts.Count; i++)
            {
                sb.DrawString(Game1.font, texts[i], position + new Vector2(0, i * (0 + Game1.font.MeasureString("A").Y)), color);
            }
            
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
