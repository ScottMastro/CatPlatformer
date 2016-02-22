using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatPlatformer
{
    class Fish
    {
        public Texture2D sprite;
        public Rectangle location;
        public Color colour;
        public int pointval;

        //constructor
        public Fish()
        {
        }

        //load new fish
        public void NewFish(Texture2D image, Vector2 loc, int point, Color color)
        {
            //load and set properties
            sprite = image;
            location = new Rectangle(Convert.ToInt32(loc.X), Convert.ToInt32(loc.Y), image.Width, image.Height);
            pointval = point;
            colour = color;
        }
    }
}
