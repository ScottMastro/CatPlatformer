using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using System.IO;

namespace CatPlatformer
{
    class Tiling
    {
        public int start;
        public int end;

        //constructor
        public Tiling()
        {
        }

        public bool ReadIn(Enemies[] enemies, Fish[] fish, Ground[] ground, string path, ContentManager content, Viewport viewport, Level level)
        {
            //out of levels
            bool nomorelevels = false;

            //finds file location and opens
            string file = Path.Combine(content.RootDirectory.ToString(),  "levels", path);
            if (File.Exists(file))
            {
                StreamReader read = new StreamReader(file);
                
                string[] line = new string[7];
                float[] x = new float[7];
                float[] y = new float[7];

                //sets y-coordinate for each line
                y[1] = viewport.Height - 400;
                y[2] = viewport.Height - 350;
                y[3] = viewport.Height - 250;
                y[4] = viewport.Height - 200;
                y[5] = viewport.Height - 100;
                y[6] = viewport.Height - 50;

                //reads file and sets x-coordinate to 0
                for (int n = 1; n <= 6; n++)
                {
                    line[n] = read.ReadLine();
                    x[n] = 0f;
                }

                read.Close();

                //count for class arrays
                int g = 0;
                int f = 0;
                int e = 0;

                for (int j = 1; j <= 6; j++)
                {
                    for (int i = 1; i <= line[j].Length; i++)
                    {
                        //sets start position for level (Ground[g]
                        if (j == 6 && start == 0)
                            start = g;

                        string chara = "";

                        //breaks apart line into character
                        if (line[j] != "")
                            chara = line[j].Substring(i - 1, 1);

                        //* = 3 digits proceeding it represent space in pixels
                        if (chara == "*")
                        {
                            x[j] += float.Parse(line[j].Substring(i, 3));
                            i += 2;
                        }

                        //X = bottom flat ground
                        if (chara == "X")
                        {
                            ground[g] = new Ground();
                            ground[g].NewGround(content.Load<Texture2D>("sprites\\backgrounds\\ground0"), new Vector2(x[j], y[j]), false);
                            x[j] += 337;
                            g++;
                        }
                        //R = right side of edge
                        else if (chara == "R")
                        {
                            ground[g] = new Ground();
                            ground[g].NewGround(content.Load<Texture2D>("sprites\\backgrounds\\ground1"), new Vector2(x[j], y[j]), true);
                            x[j] += 78;
                            g++;
                        }
                        //L = left side of edge
                        else if (chara == "L")
                        {
                            ground[g] = new Ground();
                            ground[g].NewGround(content.Load<Texture2D>("sprites\\backgrounds\\ground2"), new Vector2(x[j], y[j]), true);
                            x[j] += 78;
                            g++;
                        }
                        //H = left floating edge
                        else if (chara == "H")
                        {
                            ground[g] = new Ground();
                            ground[g].NewGround(content.Load<Texture2D>("sprites\\backgrounds\\floatgroundleft"), new Vector2(x[j], y[j]), true);
                            x[j] += 12;
                            g++;
                        }
                        //J = left middle floating piece
                        else if (chara == "J")
                        {
                            ground[g] = new Ground();
                            ground[g].NewGround(content.Load<Texture2D>("sprites\\backgrounds\\floatmidleft"), new Vector2(x[j], y[j]), false);
                            x[j] += 43;
                            g++;
                        }
                        //K = right middle floating piece
                        else if (chara == "K")
                        {
                            ground[g] = new Ground();
                            ground[g].NewGround(content.Load<Texture2D>("sprites\\backgrounds\\floatmidright"), new Vector2(x[j], y[j]), false);
                            x[j] += 43;
                            g++;
                        }
                        //; = right floating edge
                        else if (chara == ";")
                        {
                            ground[g] = new Ground();
                            ground[g].NewGround(content.Load<Texture2D>("sprites\\backgrounds\\floatgroundright"), new Vector2(x[j], y[j]), true);
                            x[j] += 12;
                            g++;
                        }

                        //Y = yellow fish
                        if (chara == "Y")
                        {
                            fish[f] = new Fish();
                            fish[f].NewFish(content.Load<Texture2D>("sprites\\fish"), new Vector2(x[j], y[j]), 500, Color.Yellow);
                            x[j] += 60;
                            f++;
                        }
                        //G = green fish
                        else if (chara == "G")
                        {
                            fish[f] = new Fish();
                            fish[f].NewFish(content.Load<Texture2D>("sprites\\fish"), new Vector2(x[j], y[j]), 100, Color.LimeGreen);
                            x[j] += 60;
                            f++;
                        }
                        //S = red fish
                        else if (chara == "S")
                        {
                            fish[f] = new Fish();
                            fish[f].NewFish(content.Load<Texture2D>("sprites\\fish"), new Vector2(x[j], y[j]), 200, Color.Red);
                            x[j] += 60;
                            f++;
                        }
                        //B = blue fish
                        else if (chara == "B")
                        {
                            fish[f] = new Fish();
                            fish[f].NewFish(content.Load<Texture2D>("sprites\\fish"), new Vector2(x[j], y[j]), 300, Color.Blue);
                            x[j] += 60;
                            f++;
                        }

                        //Q = orange balloon
                        if (chara == "Q")
                        {
                            enemies[e] = new Enemies();
                            enemies[e].NewEnemy(content.Load<Texture2D>("sprites\\balloon\\dog"), content.Load<Texture2D>("sprites\\balloon\\pop"),
                                new Vector2(x[j], y[j] - 70), Color.Orange, 2, 250, 1, 1);
                            x[j] += 52;
                            e++;
                        }
                        //W = pink balloon
                        else if (chara == "W")
                        {
                            enemies[e] = new Enemies();
                            enemies[e].NewEnemy(content.Load<Texture2D>("sprites\\balloon\\dog"), content.Load<Texture2D>("sprites\\balloon\\pop"),
                                new Vector2(x[j], y[j] - 70), Color.Pink, 1, 100, 1, 1);
                            x[j] += 52;
                            e++;
                        }
                        //E = green balloon
                        else if (chara == "E")
                        {
                            enemies[e] = new Enemies();
                            enemies[e].NewEnemy(content.Load<Texture2D>("sprites\\balloon\\dog"), content.Load<Texture2D>("sprites\\balloon\\pop"),
                                new Vector2(x[j], y[j] - 70), Color.SeaGreen, 3, 500, 2, 2);
                            x[j] += 52;
                            e++;
                        }
                        //Q = grey balloon
                        else if (chara == "A")
                        {
                            enemies[e] = new Enemies();
                            enemies[e].NewEnemy(content.Load<Texture2D>("sprites\\balloon\\dog"), content.Load<Texture2D>("sprites\\balloon\\pop"),
                                new Vector2(x[j], y[j] - 70), Color.Silver, 4, 1000, 2, 2);
                            x[j] += 52;
                            e++;
                        }
                        //C = sign
                        else if (chara == "C")
                        {
                            level.NewSign(content.Load<Texture2D>("sprites\\sign"), new Vector2(x[j], y[j] - 56));
                            x[j] += 112;
                        }

                    }
                }

                //sets end position of level (Ground[g])
                end = g - 1;
            }
            else
                nomorelevels = true;

            return nomorelevels;
        }
    }
}