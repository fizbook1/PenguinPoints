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

        public abstract void Drag(Vector2 mousepos);

        public abstract void Draw(SpriteBatch sb);
    }

    class Text : Item
    {
        string text;
        Color color = Color.Black;
        public Text(string text, Vector2 position)
        {
            this.text = text;
            this.position = position;
        }

        public override void Drag(Vector2 mousepos)
        {
            //throw new NotImplementedException();
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
        }

        public override void Drag(Vector2 mousepos)
        {
            //throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(image, position, Color.White);
        }
    }
}
