
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace CatPlatformer
{
    class Cat
    {
        public Texture2D sprite;
        Texture2D walking;
        Texture2D jumping;
        Texture2D falling;
        public Texture2D claw;

        public Rectangle location;
        public Rectangle sourcerect;
        Rectangle walkingrect;
        Rectangle jumpingrect;
        public Rectangle clawlocation;

        int frame;
        bool back;
        public SpriteEffects direction;

        double elapsedwalktime;
        double elapsedjumptime;
        double elapsedfalltime;
        double elapsedattacktime;
        double elapsedinvincibilitytime;

        public bool isjumping;
        public bool isfalling;
        public bool isattacking;
        public bool isinvincible;
        
        //constructor
        public Cat()
        {
        }

        //sets defaults for beginning of game
        public void Load(Texture2D walk, Texture2D jump, Texture2D fall, Rectangle rect, Rectangle loc)
        {
            //set default values for all
            sprite = walk;
            walking = walk;
            jumping = jump;
            falling = fall;

            location = loc;
            sourcerect = rect;
            walkingrect = rect;
            jumpingrect = new Rectangle(0, 0, jump.Width, jump.Height);

            frame = 1;
            back = false;
            direction = SpriteEffects.None;

            elapsedwalktime = 0;
            elapsedjumptime = 0;
            elapsedfalltime = 0;
            elapsedattacktime = 0;
            elapsedinvincibilitytime = 0;

            isjumping = false;
            isfalling = false;
            isattacking = false;
            isinvincible = false;
        }

        public void Walk(SpriteEffects dir, float xvelocity, double elapsedtime, SoundEffect bell)
        {
            //if not falling or jumping, change rectangle and sprite size
            if (!isfalling && !isjumping)
                SpriteChange(walking, walkingrect, false);

            //direction sprite is facing
            direction = dir;

            //move according to velocity
            location.X += Convert.ToInt32(xvelocity);

            //add to walktime
            elapsedwalktime += elapsedtime;

            if (elapsedwalktime > 0.15)
            {
                //moves frames >>>
                if (frame < 5 && !back)
                {
                    sourcerect.X = (sprite.Width / 6) * frame;
                    frame++;

                    if (frame == 5)
                        back = true;
                }
                //moves frames <<<
                else
                {
                    sourcerect.X = (sprite.Width / 6) * frame;
                    frame--;

                    if (frame == 0)
                        back = false;
                }

                //plays sound every certain frame
                if (frame % 3 == 0 && !isfalling && !isjumping)
                    bell.Play();

                //set time to 0 to prepare for next frame animation
                elapsedwalktime = 0;
            }
        }

        public void Attack(double elapsedtime, int reset, Texture2D clw)
        {
            //obviously
            isattacking = true;

            //sets claw sprite
            claw = clw;

            //sets claw position depending on direction facing
            if (direction == SpriteEffects.None)
                clawlocation = new Rectangle(location.X + sourcerect.Width, location.Y + 30, clw.Width, clw.Height);
            else
                clawlocation = new Rectangle(location.X - 45, location.Y + 30, clw.Width, clw.Height);

            //adds to attack time
            elapsedattacktime += elapsedtime;

            //sets time to 0 if spacebar was pressed again
            elapsedattacktime *= reset;

            //attack lasts for 0.1 seconds
            if (elapsedattacktime > 0.1)
                isattacking = false;
        }

        public void Jump(double elapsedtime, Ground[] ground)
        {
            //change sprite and rectangle to jump
            SpriteChange(jumping, jumpingrect, true);

            //adds to jump time
            elapsedjumptime += elapsedtime;

            //if starting jump, move up to prevent ground collision
            if (!isjumping)
                location.Y -= 5;

            //move up, gravity included
            if (isjumping)
                location.Y -= Convert.ToInt32(8 - elapsedjumptime * 10);

            //if jump reaches apex
            if (elapsedjumptime > .8)
            {
                //start falling
                isjumping = false;
                elapsedjumptime = 0;
                isfalling = true;
            }
            //otherwise, continue jumping
            else
                isjumping = true;
        }

        public void Fall(Ground[] ground, SoundEffect land, double elapsedtime)
        {
            bool wasfalling = isfalling;
            int yloc = location.Y;

            //sets sprite and rectangle to falling
            if (isfalling)
                SpriteChange(falling, jumpingrect, true);
            //sets sprite and rectangle to walking
            else
                SpriteChange(walking, walkingrect, false);

            //add to fall time
            elapsedfalltime += elapsedtime;

            //collision flag
            bool flag = true;

            foreach (Ground grd in ground)
            {
                if (grd != null && new Rectangle(grd.location.X, grd.location.Y, grd.location.Width, 11).Intersects(new Rectangle
                    (location.X, location.Y + location.Height - 1, walkingrect.Width, 1)))
                {
                    if (!grd.edge)
                    {
                        flag = false;
                        //move up to proper position
                        yloc = grd.location.Y - location.Height + 5;
                    }
                }
            }

            //set isfalling to collision flag
            isfalling = flag;

            //if faling, move down, gravity included
            if (isfalling)
                location.Y += Convert.ToInt32(elapsedfalltime * 10);
            else
            {
                elapsedfalltime = 0;

                //landing
                if (wasfalling)
                {
                    location.Y = yloc;
                    isfalling = false;
                    land.Play();
                }
            }
        }

        private void SpriteChange(Texture2D sprte, Rectangle srcrct, bool xy)
        {
            //change current sprite
            sprite = sprte;

            //xy = change x and y positions of the rectangle as well as the width and height

            //change source rectangle
            if (xy)
                sourcerect = srcrct;
            else
            {
                sourcerect.Width = srcrct.Width;
                sourcerect.Height = srcrct.Height;
            }

            //change object rectangle
            if (!xy)
                location = new Rectangle(location.X, location.Y + (location.Height - srcrct.Height), srcrct.Width, srcrct.Height);
            else
            {
                location.Width = srcrct.Width;
                location.Height = srcrct.Height;
            }
        }

        public void Invincibility(double seconds)
        {
            //add to invincibility time
            elapsedinvincibilitytime += seconds;

            //blink every 0.3 seconds for 0.1 seconds
            for (double i = 0; i <= 2; i += 0.3)
            {
                if (elapsedinvincibilitytime > i - 0.1 && elapsedinvincibilitytime < i)
                {
                    sourcerect.Width = 0;
                    sourcerect.Height = 0;
                }
            }

            //if been invincible for 2 seconds, set to false
            if (elapsedinvincibilitytime > 2)
            {
                elapsedinvincibilitytime = 0;
                isinvincible = false;
            }
        }
    }
}

