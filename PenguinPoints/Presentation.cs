using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using A1r.Input;

namespace PenguinPoints
{
    class Presentation
    {
        Slide start;
        public Slide current;
        bool presenting = false;

        public bool Presenting { get => presenting; }

        public Presentation(Slide start)
        {
            this.start = start;
        }

        public void StartPresentation()
        {
            presenting = true;
            current = start;
        }

        public void PreviousSlide()
        {
            if(current.Previous == null)
            {
                presenting = false;
                //do booing and tomato throw
            }
            else
            {
                current = current.Previous;
            }
        }
        public void NextSlide()
        {
            if(current.Next == null)
            {
                presenting = false;
                //do confetti
            }
            else
            {
                current = current.Next;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            current.Draw(sb);
        }
    }
}
