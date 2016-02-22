using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace CatPlatformer
{
    class Level
    {

        public int level;
        public Texture2D signsprite;
        public Rectangle signlocation;
        public Color background;
        public int score;

        //constructor
        public Level()
        {
        }

        //load in a new sign for the level
        public void NewSign(Texture2D sprite, Vector2 location)
        {
            signsprite = sprite;
            signlocation = new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), sprite.Width, sprite.Height);
        }

        //change level
        public void LoadNewLevel(bool next)
        {
            if (level == 0)
                level = 1;
            else if (next)
                level++;

            GetBackgroundColour();
        }

        //change background colour
        private void GetBackgroundColour()
        {
            if (level == 2)
                background = Color.RoyalBlue;
            else if (level == 3)
                background = Color.Violet;
            else if (level == 4)
                background = Color.LightGray;
            else
                background = Color.SkyBlue;
        }
    }
}
