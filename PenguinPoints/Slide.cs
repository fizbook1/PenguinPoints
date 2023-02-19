using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PenguinPoints
{
    class Slide
    {
        Slide previous;
        Color backgroundColor = Color.White;

        public Color BackgroundColor { get => backgroundColor; }

        public Slide Previous { get => previous; }

        Slide next;

        List<Item> items;

        public Slide Next { get => next; }
        

        public Slide()
        {
            items = new List<Item>();
        }

        public void SetParent(Slide parent)
        {
            previous = parent;
            parent.SetChild(this);
        }

        public Item ItemClicked(Point mousePos)
        {
            foreach(Item i in items)
            {
                if(i.Size.Contains(mousePos))
                {
                    return i;
                }
            }

            return null;
        }

        private void SetChild(Slide child)
        {
            next = child;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void DeleteItem(Item item)
        {
            items.Remove(item);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach(Item i in items)
            {
                i.Draw(sb);
            }
        }
        
    }
}
