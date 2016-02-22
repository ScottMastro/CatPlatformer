using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CatPlatformer
{
    class Health
    {
        public Texture2D[] healthbar = new Texture2D[9];
        public int health;

        //constructor
        public Health()
        {
        }

        //load healthbar
        public void LoadHealth(ContentManager content)
        {
            //load sprites
            for (int i = 0; i <= 8; i++)
                healthbar[i] = content.Load<Texture2D>("sprites\\health\\health" + i.ToString());

            //set health to full
            health = 8;
        }

        public bool HealthChange(int amount)
        {
            //over = no health
            bool over = false;

            //increase or decrease health
            health += amount;

            //avoids going out of array bounds
            if (health <= 0)
            {
                health = 0;
                //game over
                over = true;
            }
            else if (health > 8)
                health = 8;

            return over;
        }
    }
}
