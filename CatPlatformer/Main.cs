using System;
using System.Collections.Generic;
using System.Collections;
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
using System.IO;

//VARIABLE DICTIONARY.Begin()
//------------------------------------------------------------------------------------------------
//Main.cs
//---GraphicsDeviceManager graphics = managing graphics
//---Viewport viewportrect = size of screen
//---SpriteBatch spriteBatch = used for drawing
//---ContentManager Content = loading in content
//---Random r = random number generator
//---Texture2D[8] claw = sprites for claw attack
//---KeyboardState previouskeyboardstate = used to identify keyboardstate on last update
//---bool gameover = game status
//---bool gamewon = won status
//---Texture2D dead = text for game over screen
//---Texture2D won = text for win screen
//---SpriteFont font = shows win score
//------------------------------------------------------------------------------------------------
//Background.cs
//---Background[3, 4] background = background objects
//------public Texture2D image = background sprite
//------public Vector2 location = background location
//------public Color colour = colour overlay
//------public Vector2 move = speed the background layer moves at
//------------------------------------------------------------------------------------------------
//Cat.cs
//---Cat Cat = player object
//------public Texture2D sprite = drawn cat sprite
//------Texture2D walking = walking cat sprites
//------Texture2D jumping = jumping cat sprite
//------Texture2D falling = falling cat sprite
//------public Texture2D claw = claw sprite
//------public Rectangle location = drawn location
//------public Rectangle sourcerect = rectangle for portion of sprite to draw
//------Rectangle walkingrect = size of rectangle for walking sprites
//------Rectangle jumpingrect = size of rectangle for jumping sprite
//------public Rectangle clawlocation = drawn claw location
//------int frame = frame of walking animation
//------bool back = direction in which frame animation goes
//------public SpriteEffects direction = direction sprite is facing
//------double elapsedwalktime = walk time
//------double elapsedjumptime = jump time
//------double elapsedfalltime = fall time
//------double elapsedattacktime = attack time
//------double elapsedinvincibilitytime = invincibility time
//------public bool isjumping = jump bool
//------public bool isfalling = fall bool
//------public bool isattacking = attack bool
//------public bool isinvincible = invincible bool
//------------------------------------------------------------------------------------------------
//Sounds.cs
//---Sounds Sounds = sound playing object
//------public SoundEffect meow0 = fall off stage
//------public SoundEffect meow1 = fish eat
//------public SoundEffect meow2 = hurt
//------public SoundEffect meow3 = random meow
//------public SoundEffect bell = walking sound
//------public SoundEffect scratch0 = scratch sound 1
//------public SoundEffect scratch1 = scratch sound 2
//------public SoundEffect scratch2 = enemy hit
//------public SoundEffect step0 = land on ground
//------public SoundEffect step1 = no real use
//------public Song music = background music
//------------------------------------------------------------------------------------------------
//Health.cs
//---Health Health = health object
//------public Texture2D[9] healthbar = sprites for healthbar;
//------public int health = current health
//------------------------------------------------------------------------------------------------
//Tiling.cs
//---Tiling Tiling = level making object
//------public int start = ground object that marks the beginning of the level
//------public int end = ground object that marks the end of the level
//------------------------------------------------------------------------------------------------
//Score.cs
//---Score Score = scoring object
//------public Texture2D[10] numbers = sprites for numbers 0-9
//------public Texture2D[20] score = drawn sprites
//------public Vector2[20] location = drawn locations
//------public int currentscore = current score
//------public int digits = digits in current score
//------------------------------------------------------------------------------------------------
//Level.cs
//---Level Level = level controlling object
//------public int level = current level
//------public Texture2D signsprite = sprite for sign
//------public Rectangle signlocation = drawn location for sign
//------public Color background = background colour
//------public int score = score for the game
//------------------------------------------------------------------------------------------------
//Enemies.cs
//---Enemies[30] Enemies = enemy objects
//------public Texture2D sprite = drawn sprite for enemy
//------Texture2D movingsprite = floating sprites
//------Texture2D deadsprite = pop sprite
//------public Rectangle location = drawn locations
//------public Rectangle sourcerect = portion of the sprite to be drawn
//------public Color colour = colour of enemy
//------public SpriteEffects direction = direction sprite is facing
//------int frame = frame of walking/poping animation
//------int back = direction in which frame animation goes & float up and down speed
//------double seconds = time between frame animations
//------double walkseconds = time between direction changes
//------int velocity = speed object moves at
//------public int damage = health points deducted when collided with object
//------int hits = number of hit before object is dead
//------public bool dead = mortality bool
//------public int score = score given when object is killed
//------------------------------------------------------------------------------------------------
//Fish.cs
//---Fish[30] Fish = fish objects
//------public Texture2D sprite = fish sprite
//------public Rectangle location = location drawn
//------public Color colour = colour of fish
//------public int pointval = score value of fish
//------------------------------------------------------------------------------------------------
//Ground.cs
//---Ground[100] Ground = foreground objects
//------public Texture2D sprite = ground sprite
//------public Rectangle location = drawn location
//------public bool edge = if ground is an edge piece or not
//------------------------------------------------------------------------------------------------
//VARIABLE DICTIONARY.End()

namespace CatPlatformer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Viewport viewportrect;
        SpriteBatch spriteBatch;

        Background[,] background = new Background[3, 4];

        Cat Cat;
        Sounds Sounds;
        Health Health;
        Tiling Tiling;
        Score Score;
        Level Level;

        Enemies[] Enemies = new Enemies[30];
        Fish[] Fish = new Fish[30];
        Ground[] Ground = new Ground[100];

        Random r = new Random();

        Texture2D[] claw = new Texture2D[8];

        KeyboardState previouskeyboardstate;
        bool gameover;
        bool gamewon;
        Texture2D dead;
        Texture2D won;
        SpriteFont font;


        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            //full screen
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //set viewport
            viewportrect = graphics.GraphicsDevice.Viewport;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            if (Level == null)
                Level = new Level();

            Level.LoadNewLevel(!gameover);

            //load over text
            dead = Content.Load<Texture2D>("sprites\\dead");
            won = Content.Load<Texture2D>("sprites\\youwin");
            font = Content.Load<SpriteFont>("font");

            //load cat
            Cat = new Cat();
            Cat.Load(Content.Load<Texture2D>("sprites\\cat\\catwalk"), Content.Load<Texture2D>("sprites\\cat\\catjump"),
                Content.Load<Texture2D>("sprites\\cat\\catfall"), new Rectangle(0, 0, 119, 92), new Rectangle
                    ((viewportrect.Width - 119) / 2, viewportrect.Height - 150, 119, 92));

            //load sound effects
            Sounds = new Sounds();
            Sounds.LoadSounds(Content);

            //load health stuff
            Health = new Health();
            Health.LoadHealth(Content);

            //loads claw sprites
            for (int i = 0; i <= 7; i++)
                claw[i] = Content.Load<Texture2D>("sprites\\claw\\claw" + i.ToString());

            //load in backgrounds
            for (int r = 0; r <= 2; r++)
            {
                for (int c = 0; c <= 3; c++)
                {
                    background[r, c] = new Background();
                    background[r, c].LoadBack(Content.Load<Texture2D>("sprites\\backgrounds\\background" + c.ToString()),
                         Color.WhiteSmoke, new Vector2(c + 0.5f, 0f), viewportrect, r, c);
                }
            }

            //reset everything for next level
            for (int i = 0; i <= 99; i++)
            {
                Ground[i] = null;

                if (i < 30)
                {
                    Fish[i] = null;
                    Enemies[i] = null;
                }
            }

            //loads level tiles
            Tiling = new Tiling();
            gamewon = Tiling.ReadIn(Enemies, Fish, Ground, "level" + Level.level.ToString() + ".txt", Content, viewportrect, Level);

            //loads score
            Score = new Score();
            Score.LoadScore(Content);
            Score.currentscore = Level.score;

            //plays and loops music
            MediaPlayer.IsRepeating = true;
            if (MediaPlayer.State == MediaState.Stopped)
                MediaPlayer.Play(Sounds.music);

            //game playing
            gameover = false;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardstate = Keyboard.GetState();

            //allows the game to exit
            if (keyboardstate.IsKeyDown(Keys.Escape))
                this.Exit();

            if (gamewon)
            {
                //do nothing
            }
            else if (gameover)
            {
                if (keyboardstate.IsKeyDown(Keys.Enter))
                {
                    //divide score by 2
                    Level.score = 0;

                    //reload game
                    this.LoadContent();
                }
            }
            else
            {
                //if both left and right keys are pressed
                if (keyboardstate.IsKeyDown(Keys.Right) && keyboardstate.IsKeyDown(Keys.Left))
                {
                    //do nothing
                }
                //move right
                else if (keyboardstate.IsKeyDown(Keys.Right) && Cat.location.X < viewportrect.Width - 119)
                {
                    if (Cat.location.X < viewportrect.Width - 319 || Ground[Tiling.end].location.X <= viewportrect.Width)
                        Cat.Walk(SpriteEffects.None, 3f, gameTime.ElapsedGameTime.TotalSeconds, Sounds.bell);
                    else
                    {
                        MoveStuff(-3);
                        Cat.Walk(SpriteEffects.None, 0f, gameTime.ElapsedGameTime.TotalSeconds, Sounds.bell);
                    }
                }
                //move left
                else if (keyboardstate.IsKeyDown(Keys.Left) && Cat.location.X > 0)
                {
                    if (Cat.location.X > 200 || Ground[Tiling.start].location.X >= 0)
                        Cat.Walk(SpriteEffects.FlipHorizontally, -3f, gameTime.ElapsedGameTime.TotalSeconds, Sounds.bell);
                    else
                    {
                        MoveStuff(3);
                        Cat.Walk(SpriteEffects.FlipHorizontally, 0f, gameTime.ElapsedGameTime.TotalSeconds, Sounds.bell);
                    }
                }

                //initiate jump
                if (Cat.isjumping && !Cat.isfalling || keyboardstate.IsKeyDown(Keys.Up) && !previouskeyboardstate.IsKeyDown(Keys.Up) && !Cat.isfalling)
                    Cat.Jump(gameTime.ElapsedGameTime.TotalSeconds, Ground);
                //check for fall
                else
                    Cat.Fall(Ground, Sounds.step0, gameTime.ElapsedGameTime.TotalSeconds);

                //if wasn't clawing last update
                if (!previouskeyboardstate.IsKeyDown(Keys.Space))
                {
                    //start clawing
                    if (keyboardstate.IsKeyDown(Keys.Space))
                    {
                        Cat.Attack(gameTime.ElapsedGameTime.TotalSeconds, 0, claw[r.Next(0, 8)]);
                        Sounds.scratch0.Play();
                        Sounds.scratch1.Play();

                        //check enemies if hit
                        foreach (Enemies enm in Enemies)
                        {
                            if (enm != null && enm.location.Intersects(Cat.clawlocation) && !enm.dead)
                            {
                                enm.Hit(Sounds.scratch2);
                                if (enm.dead)
                                    Score.currentscore += enm.score;
                            }
                        }
                    }
                }

                //send time to attack method
                if (Cat.isattacking)
                    Cat.Attack(gameTime.ElapsedGameTime.TotalSeconds, 1, claw[r.Next(0, 8)]);

                //random meow feature
                if (keyboardstate.IsKeyDown(Keys.M) && !previouskeyboardstate.IsKeyDown(Keys.M))
                {
                    Sounds.meow3.Play();
                    Score.currentscore += 1;
                }

                //check for fish collision
                foreach (Fish fsh in Fish)
                {
                    if (fsh != null && fsh.location.Intersects(Cat.location))
                    {
                        Score.currentscore += fsh.pointval;
                        fsh.location.Y = -500;
                        Sounds.meow1.Play();
                    }
                }

                //updates checks for enemy collision
                foreach (Enemies enm in Enemies)
                {
                    if (enm != null)
                    {
                        enm.Update(gameTime.ElapsedGameTime.TotalSeconds);

                        //smaller rectangle for more accurate collision
                        if (!enm.dead && enm.location.Intersects(new Rectangle(Cat.location.X + 20, Cat.location.Y + 20,
                            Cat.location.Width - 40, Cat.location.Height - 40)) && Health.health > 0 && !Cat.isinvincible)
                        {
                            //lower health and set cat to invincible
                            gameover = Health.HealthChange(-enm.damage);
                            Cat.isinvincible = true;

                            Sounds.meow2.Play();
                        }
                    }
                }

                //if collision with sign
                if (Cat.location.Intersects(Level.signlocation))
                {
                    //load in next level
                    Level.score = Score.currentscore;
                    this.LoadContent();
                }

                //if invincible, send time to invincibility method
                if (Cat.isinvincible)
                    Cat.Invincibility(gameTime.ElapsedGameTime.TotalSeconds);

                //loops background
                LoopBackground();

                //if fell off screen, kill
                if (Cat.location.Y > viewportrect.Height + 10)
                {
                    gameover = Health.HealthChange(-1);

                    //if dead, play sound
                    if (gameover)
                        Sounds.meow0.Play();
                }

                //updates score
                Score.Update();

                //sets previous state to current state
                previouskeyboardstate = keyboardstate;

                base.Update(gameTime);
            }
        }

        private void MoveStuff(int direction)
        {
            //moves background
            foreach (Background bg in background)
            {
                if (direction < 0)
                    bg.location -= bg.move;
                else if (direction > 0)
                    bg.location += bg.move;
            }

            //moves foreground
            foreach (Ground grd in Ground)
            {
                if (grd != null)
                    grd.location.X += direction;
            }

            //moves fish
            foreach (Fish fsh in Fish)
            {
                if (fsh != null)
                    fsh.location.X += direction;
            }

            //moves enemies
            foreach (Enemies enm in Enemies)
            {
                if (enm != null)
                    enm.location.X += direction;
            }

            //moves sign
            Level.signlocation.X += direction;

        }

        private void LoopBackground()
        {
            //checks if background is to loop
            for (int i = 0; i <= 3; i++)
            {
                //moves background peices left
                if (background[0, i].location.X >= 0f)
                {
                    background[1, i].location.X = background[0, i].location.X;
                    background[0, i].location.X = background[1, i].location.X - background[0, i].image.Width;
                    background[2, i].location.X = background[1, i].location.X + background[2, i].image.Width;
                }
                //moves background peices right
                else if (background[2, i].location.X + background[2, i].location.X <= viewportrect.Width)
                {
                    background[1, i].location.X = background[2, i].location.X;
                    background[0, i].location.X = background[1, i].location.X - background[0, i].image.Width;
                    background[2, i].location.X = background[1, i].location.X + background[2, i].image.Width;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Level.background);

            spriteBatch.Begin();

            //if game is done
            if (gameover || gamewon)
            {
                //background = black
                GraphicsDevice.Clear(Color.Black);

                if (gameover)
                {
                    //show game over text
                    spriteBatch.Draw(dead, new Vector2((viewportrect.Width - dead.Width) / 2, (viewportrect.Height - dead.Height) / 2), Color.White);
                }
                else
                {
                    //show you won text
                    spriteBatch.Draw(won, new Vector2((viewportrect.Width - won.Width) / 2, (viewportrect.Height - won.Height) / 2), Color.White);
                    spriteBatch.DrawString(font, "Total Score: " + Score.currentscore.ToString(),
                        new Vector2(((viewportrect.Width - won.Width) / 2) + 120, ((viewportrect.Height - won.Height) / 2) + 100), Color.White);
                }
            }
            else
            {
                //draw background in correct order
                for (int c = 0; c <= 3; c++)
                {
                    for (int r = 0; r <= 2; r++)
                    {
                        spriteBatch.Draw(background[r, c].image, background[r, c].location, background[r, c].colour);
                    }
                }

                //draws all foreground pieces
                foreach (Ground grd in Ground)
                {
                    if (grd != null)
                        spriteBatch.Draw(grd.sprite, grd.location, Color.White);
                }

                //draws all fish
                foreach (Fish fsh in Fish)
                {
                    if (fsh != null)
                        spriteBatch.Draw(fsh.sprite, fsh.location, fsh.colour);
                }

                //draws all enemies
                foreach (Enemies enm in Enemies)
                {
                    if (enm != null)
                        spriteBatch.Draw(enm.sprite, enm.location, enm.sourcerect, enm.colour, 0f, new Vector2(0, 0), enm.direction, 0f);
                }

                //draws cat
                spriteBatch.Draw(Cat.sprite, Cat.location, Cat.sourcerect, Color.White, 0f, new Vector2(0, 0), Cat.direction, 0f);

                //if attacking, draws claws
                if (Cat.isattacking)
                {
                    spriteBatch.Draw(Cat.claw, Cat.clawlocation, Color.White);
                    spriteBatch.Draw(Cat.claw, Cat.clawlocation, Color.White);
                }

                //draws sign
                spriteBatch.Draw(Level.signsprite, Level.signlocation, Color.White);

                //draws status stuff
                spriteBatch.Draw(Health.healthbar[Health.health], new Vector2(10f, 10f), Color.White);
                spriteBatch.Draw(Score.score[0], Score.location[0], Color.White);

                for (int i = 1; i <= Score.digits; i++)
                    spriteBatch.Draw(Score.score[i], Score.location[i], Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}