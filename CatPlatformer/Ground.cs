using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatPlatformer
{
    class Ground
    {
        public Texture2D sprite;
        public Rectangle location;
        public bool edge;

        //constructor
        public Ground()
        {
        }

        //load new ground piece
        public void NewGround(Texture2D image, Vector2 loc, bool edg)
        {
            //load and set properties
            edge = edg;
            sprite = image;
            location = new Rectangle(Convert.ToInt32(loc.X), Convert.ToInt32(loc.Y), sprite.Width, sprite.Height);
        }
    }
}
