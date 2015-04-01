#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace SpaceTest001
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private scBackground starBG;
        Player playerShip;
        Rectangle viewportRect;

        Texture2D playerShotTexture;
        List<Shot> playerShotList = new List<Shot>();

        float timeSinceLastShot;
        const float TIME_BETWEEN_SHOT = 0.35f;

        const int maxAsteroids = 10;
        const float maxAsteroidWidth = 0.0f;
        const float minAsteroidWidth = 1.0f;
        const float maxAsteroidVelocity = 3.0f;
        const float minAsteroidVelocity = 1.0f;

        const int maxEnemies = 4;
        const float maxEnemyWidth = 0.0f;
        const float minEnemyWidth = 1.0f;
        const float maxEnemyVelocity = 3.0f;
        const float minEnemyVelocity = 1.0f;

        Random random = new Random();

       // Sprite[] asteroids;

        Asteroid[] asteroidss; 

        Sprite[] enemies;

        float G = 180000000;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //texture, postition, velocity, origin, scale
            playerShip = new Player(Content.Load<Texture2D>("Images/playerShip"), new Vector2(GraphicsDevice.Viewport.Width / 2, 400), new Vector2(5, 5), true, 0.07f);

            starBG = new scBackground();

            Texture2D background = Content.Load<Texture2D>("Images/spaceBG");

            starBG.Load(GraphicsDevice, background);

            playerShotTexture = Content.Load<Texture2D>("Images/playerShot");

            //TODO: replace this witht he version below it 
            //asteroids = new Sprite[maxAsteroids];
            //for (int i = 0; i < maxAsteroids; i++)
            //{
            //    asteroids[i] = new Sprite(Content.Load<Texture2D>("Images/asteroid"));
            //}

            //TODO: replace above with below 
            asteroidss = new Asteroid[maxAsteroids];
            for (int i = 0; i < maxAsteroids; i++) {
                asteroidss[i] = new Asteroid(Content.Load<Texture2D>("Images/asteroid"));
                /*Vector2 tPos;
                tPos.Y = asteroidss[i].Position.Y;
                tPos.X = asteroidss[i].Position.X;
                tPos.X += i * 10;
                asteroidss[i].Position = tPos;*/
            }


            enemies = new Sprite[maxEnemies];
            for (int i = 0; i < maxEnemies; i++)
            {
                enemies[i] = new Sprite(Content.Load<Texture2D>("Images/enemyship2"));
            }
                //drawable area of the game screen.
                viewportRect = new Rectangle(0, 0,
                    graphics.GraphicsDevice.Viewport.Width,
                    graphics.GraphicsDevice.Viewport.Height);

            base.LoadContent();

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateInput();
            playerShip.Update(gameTime, GraphicsDevice);
            starBG.Update(elapsed * 25);

            for (int i = 0; i < playerShotList.Count; i++)
            {
                playerShotList[i].Update(gameTime);
            }

            //float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            timeSinceLastShot += elapsed;

            foreach (Shot defaultShot in playerShotList)
            {
                if (defaultShot.Alive)
                {
                    Rectangle shotRect = new Rectangle((int)defaultShot.Position.X, (int)defaultShot.Position.Y, defaultShot.TextureImage.Width - 10, defaultShot.TextureImage.Height - 10);

                    foreach (Asteroid asteroid in asteroidss)
                    {
                        Rectangle asteroidRect = new Rectangle((int)asteroid.Position.X, (int)asteroid.Position.Y, asteroid.Image.Width - 10, asteroid.Image.Height - 10);

                            if (shotRect.Intersects(asteroidRect))
                            {
                                defaultShot.Alive = false;
                                asteroid.Alive = false;
                                break;
                            }
                        }

                    foreach (Sprite enemy in enemies)
                    {
                        Rectangle enemyRect = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Image.Width - 10, enemy.Image.Height - 10);

                        if (shotRect.Intersects(enemyRect))
                        {
                            defaultShot.Alive = false;
                            enemy.Alive = false;
                            break;
                        }
                    }
                }
            }



            UpdateAsteroids(gameTime);
            UpdateEnemies(elapsed);

            base.Update(gameTime);
        }

        public void UpdateAsteroids(GameTime gameTime)
        {
            //foreach (Sprite asteroid in asteroids)
            foreach (Asteroid asteroid in asteroidss)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

                asteroid.Update(elapsed, ref playerShip, ref viewportRect);


            //    if (asteroid.Alive)
            //    {
            //        asteroid.Position += asteroid.Velocity;
            //        asteroid.Rotation += random.Next(-1, 1) * elapsed;

            //        Vector2 dir = playerShip.Position - asteroid.Position;
            //        float radius = dir.Length();
            //        float forceMag = (G * asteroid.aMass * playerShip.psMass) / (float)Math.Pow(radius, 2);
            //        dir.Normalize();
            //        Vector2 gForce = forceMag * dir;
                   
            //        asteroid.Force = gForce;
            //        asteroid.Acceleration = asteroid.Force / asteroid.aMass;
            //        asteroid.Position += ((asteroid.Velocity * elapsed) + (0.5f * (asteroid.Acceleration * (elapsed * elapsed))));

            //        if (radius <= 75)
            //        {

            //            //respawn = 0; mv^2/r

            //            asteroid.Position = new Vector2(playerShip.Position.X, playerShip.Position.Y);
            //            asteroid.Center = new Vector2(60, 60);

            //            /*asteroid.Velocity = new Vector2(dir.Y, -dir.X);
            //            asteroid.Acceleration = new Vector2(0, 0);
            //            asteroid.Position += ((asteroid.Velocity * elapsed) + (0.5f * (asteroid.Acceleration * (elapsed * elapsed))));*/
            //            asteroid.Rotation -= 0.02f;
            //            Console.WriteLine("Asteroid: " + asteroid.Rotation);
            //            asteroid.Rotation = MathHelper.Clamp(asteroid.Rotation, -MathHelper.Pi * 360, 360);
            //           // asteroid.Belt = true;
                        
            //        }

            //        if (!viewportRect.Contains(new Point((int)asteroid.Position.X, (int)asteroid.Position.Y)))
            //        {
            //            asteroid.Alive = false;
            //        }
            //    }
            //    else
            //    {
            //        asteroid.Alive = true;

                 //   asteroid.Position = new Vector2(MathHelper.Lerp ((float)viewportRect.Width * minAsteroidWidth, (float)viewportRect.Width * maxAsteroidWidth, (float)random.NextDouble()), viewportRect.Top);
                //    asteroid.Velocity = new Vector2(random.Next(-2, 2), MathHelper.Lerp(minAsteroidVelocity, maxAsteroidVelocity, (float)random.NextDouble()));
                   
            //    }
            }
        }

        public void UpdateAsteroidsBelt(GameTime gameTime)
        {
            //foreach (Asteroid asteroid in asteroidss)
            //{
            //    float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            //    if (asteroid.Alive)
            //    {
            //        asteroid.Center = new Vector2(asteroid.Image.Width / 2, asteroid.Image.Height / 2);
            //        asteroid.Position = new Vector2(asteroid.Position.X, asteroid.Position.Y);
            //        Vector2 dir = playerShip.Position - asteroid.Position;
            //        float radius = dir.Length();
            //        float forceMag = (G * asteroid.aMass * playerShip.psMass) / (float)Math.Pow(radius, 2);
            //        dir.Normalize();
            //        Vector2 gForce = forceMag * dir;

            //        asteroid.Force = -gForce;
            //        asteroid.Acceleration = asteroid.Force / asteroid.aMass;
            //        asteroid.Position += -asteroid.Velocity;
            //        //asteroid.Position += ((asteroid.Velocity * elapsed) + (0.5f * (asteroid.Acceleration * (elapsed * elapsed))));
            //    }
               
            //}
        }

        public void UpdateEnemies(float elapsedTime)
        {
            foreach (Sprite enemy in enemies)
            {
                if (enemy.Alive)
                {
                    enemy.Position += enemy.Velocity;

                    if (!viewportRect.Contains(new Point((int)enemy.Position.X, (int)enemy.Position.Y)))
                    {
                        enemy.Alive = false;
                    }
                }
                else
                {
                    enemy.Alive = true;

                    enemy.Position = new Vector2(MathHelper.Lerp((float)viewportRect.Width * minEnemyWidth, (float)viewportRect.Width * maxEnemyWidth, (float)random.NextDouble()), viewportRect.Top);
                    enemy.Velocity = new Vector2(0, MathHelper.Lerp(minEnemyVelocity, maxEnemyVelocity, (float)random.NextDouble()));
                }
            }
        }

        private void UpdateInput()
        {

            //bool keyPressed = false;
            KeyboardState keyState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (keyState.IsKeyDown(Keys.Up)
              || keyState.IsKeyDown(Keys.W)
              || gamePadState.DPad.Up == ButtonState.Pressed
              || gamePadState.ThumbSticks.Left.Y > 0)
            {
                playerShip.Up();
                //keyPressed = true;
            }
            else
            {
                if(playerShip.Velocity.Y < 0)
                    playerShip.Velocity *= new Vector2(1, 0.95f);
            }
            if (keyState.IsKeyDown(Keys.Down)
              || keyState.IsKeyDown(Keys.S)
              || gamePadState.DPad.Down == ButtonState.Pressed
              || gamePadState.ThumbSticks.Left.Y < -0.5f)
            {
                playerShip.Down();
                //keyPressed = true;
            }
            else
            {
                if (playerShip.Velocity.Y > 0)
                    playerShip.Velocity *= new Vector2(1, 0.95f);
            }
            if (keyState.IsKeyDown(Keys.Left)
              || keyState.IsKeyDown(Keys.A)
              || gamePadState.DPad.Left == ButtonState.Pressed
              || gamePadState.ThumbSticks.Left.X < -0.5f)
            {
                playerShip.Left();
                //keyPressed = true;
            }
            else
            {
                if (playerShip.Velocity.X < 0)
                    playerShip.Velocity *= new Vector2(0.95f, 1);
            }
            if (keyState.IsKeyDown(Keys.Right)
              || keyState.IsKeyDown(Keys.D)
              || gamePadState.DPad.Right == ButtonState.Pressed
              || gamePadState.ThumbSticks.Left.X > 0.5f)
            {
                playerShip.Right();
                //keyPressed = true;
            }
            else
            {
                if (playerShip.Velocity.X > 0)
                    playerShip.Velocity *= new Vector2(0.95f, 1);
            }
            /*if (!keyPressed)
            {
                playerShip.Idle();
            }*/

            if (keyState.IsKeyDown(Keys.Space))
            {
                if (timeSinceLastShot >= TIME_BETWEEN_SHOT)
                {
                    Shot shot = new Shot(playerShotTexture, new Vector2(playerShip.Position.X, playerShip.Position.Y - 30), -600);
                    playerShotList.Add(shot);
                    timeSinceLastShot = 0f;
                }
            }

            if (keyState.IsKeyDown(Keys.T))
            {

            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            starBG.Draw(spriteBatch);
            playerShip.Draw(spriteBatch);

            foreach (Shot defaultShot in playerShotList)
            {
                defaultShot.Draw(spriteBatch);
            }

            foreach (Asteroid asteroid in asteroidss)
            {
                if (asteroid.Alive)
                {
                    spriteBatch.Draw(asteroid.Image, asteroid.Position, null, Color.White, asteroid.Rotation, asteroid.Center, 1.0f, SpriteEffects.None, 0.0f);
                }
            }

            foreach (Sprite enemy in enemies)
            {
                if (enemy.Alive)
                {
                    spriteBatch.Draw(enemy.Image, enemy.Position, null, Color.White, enemy.Rotation, enemy.Center, 1.0f, SpriteEffects.None, 0.0f);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
