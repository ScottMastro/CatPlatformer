using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CatPlatformer
{
    class Score
    {
        public Texture2D[] numbers = new Texture2D[10];
        public Texture2D[] score = new Texture2D[20];
        public Vector2[] location = new Vector2[20];
        public int currentscore;
        public int digits;

        //constructor
        public Score()
        {
        }

        public void LoadScore(ContentManager content)
        {
            //score[0] = sprite of the words "Score:"
            score[0] = content.Load<Texture2D>("sprites\\score\\score");

            //load in number sprites
            for (int i = 0; i <= 9; i++)
                numbers[i] = content.Load<Texture2D>("sprites\\score\\" + i.ToString());

            //set location for score[0]
            location[0] = new Vector2(150f, 25f);

            //set actual score
            currentscore = 0;
            digits = 0;
        }

        public void Update()
        {
            //put current score into string and set number of digits
            string cs = currentscore.ToString();
            digits = cs.Length;

            //for every digit
            for (int i = 1; i <= digits; i++)
            {
                //put digit sprite in corresponding score array position
                int num = Convert.ToInt32(cs.Substring(i - 1, 1));
                score[i] = numbers[num];

                //place digits in correct location
                location[i] = new Vector2(300f + 25*i, 25f);
            }
        }
    }
}
