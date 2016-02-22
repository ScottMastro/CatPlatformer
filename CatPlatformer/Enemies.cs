using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace CatPlatformer
{
    class Enemies
    {
        public Texture2D sprite;
        Texture2D movingsprite;
        Texture2D deadsprite;

        public Rectangle location;
        public Rectangle sourcerect;

        public Color colour;
        public SpriteEffects direction;

        int frame;
        int back;
        double seconds;
        double walkseconds;

        int velocity;
        public int damage;
        int hits;
        public bool dead;
        public int score;

        //constructor
        public Enemies()
        {
        }

        public void NewEnemy(Texture2D moving, Texture2D die, Vector2 loc, Color color, int health, int points, int speed, int attack)
        {
            //load in and set values to default
            sprite = moving;
            movingsprite = moving;
            deadsprite = die;

            sourcerect = new Rectangle(0, 0, moving.Width / 5, moving.Height);
            location = new Rectangle(Convert.ToInt32(loc.X), Convert.ToInt32(loc.Y), sourcerect.Width, sourcerect.Height);

            colour = color;
            direction = SpriteEffects.None;

            frame = 0;
            back = 1;
            seconds = 0;
            walkseconds = 0;

            velocity = speed;
            damage = attack;
            hits = health;
            dead = false;
            score = points;
        }

        public void Hit(SoundEffect ht)
        {
            //lower health
            hits--;
            ht.Play();

            //if no more health, enemy is dead
            if (hits <= 0)
                dead = true;
        }

        public void Update(double time)
        {
            //update walk and frame time
            walkseconds += time;
            seconds += time;

            //if not dead
            if (!dead)
            {
                //move up and down determined by back
                location.Y += back;

                //move left and right determined by direction sprite is facing
                if (direction == SpriteEffects.None)
                    location.X -= velocity;
                else if (direction == SpriteEffects.FlipHorizontally)
                    location.X += velocity;

                //if moving for longer than it's speed
                if (walkseconds > velocity)
                {
                    //reset walk time
                    walkseconds = 0;

                    //flip in other direction
                    if (direction == SpriteEffects.None)
                        direction = SpriteEffects.FlipHorizontally;
                    else
                        direction = SpriteEffects.None;
                }

                //animate
                if (seconds > 0.5)
                {
                    seconds = 0;

                    //moves frame back or forward 1
                    frame += back;
                    //adjust source rectangle
                    sourcerect.X = (movingsprite.Width / 5) * frame;

                    //change back
                    if (frame == 2)
                        back = -1;
                    if (frame == 0)
                        back = 1;
                }
            }
            else if (dead)
            {
                //start dying animation
                if (frame < 3)
                {
                    frame = 3;
                    seconds = 0;
                }

                //move to next dying frame
                if (seconds < 0.5)
                {
                    if (seconds > 0.25)
                        frame = 4;

                    //adjust source rectangle
                    sourcerect.X = (movingsprite.Width / 5) * frame;
                }
                //replace dying sprite with pop sprite
                else if (seconds >= 0.5 && seconds < 1)
                {
                    //replace sprite
                    sprite = deadsprite;

                    //adjust sourcerect and location rectangles
                    sourcerect = new Rectangle(0, 0, deadsprite.Width, deadsprite.Height);
                    location.Width = sourcerect.Width;
                    location.Height = sourcerect.Height;

                    //don't flip
                    direction = SpriteEffects.None;
                }
                //after shown, move off-screen
                else if (seconds >= 1)
                    location.Y = -500;
            }
        }

    }
}
