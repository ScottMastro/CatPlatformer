using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatPlatformer
{
    class Background
    {
        public Texture2D image;
        public Vector2 location;
        public Color colour;
        public Vector2 move;

        //constructor
        public Background()
        {
        }

        //loads backgrounds
        public void LoadBack(Texture2D sprite, Color col, Vector2 velocity, Viewport viewport, int r, int c)
        {
            //sets values
            image = sprite;
            location = new Vector2(-image.Width + r * image.Width, viewport.Height - image.Height);
            colour = col;
            move = velocity;

            //adjust position
            if (c == 2)
                location.Y += 150f;
            if (c == 0)
                location.Y = 0f;
        }
    }
}
