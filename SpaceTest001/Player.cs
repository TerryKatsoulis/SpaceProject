using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceTest001
{
    public class Player
    {
        public float psMass = 1.2f;

        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Texture2D TextureImage { get; set; }

        protected Vector2 spriteOrigin;
        public Vector2 SpriteOrigin
        {
            get { return spriteOrigin; }
            set { spriteOrigin = value; }
        }

        public virtual Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - SpriteOrigin.X * Scale), (int)(Position.Y - SpriteOrigin.Y * Scale),
                    (int)(TextureImage.Width * Scale), (int)(TextureImage.Height * Scale));
            }
        }

        protected Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        //initial velocity will be used for a power up later on
        protected Vector2 initialVelocity;
        public Vector2 InitialVelocity
        {
            get { return initialVelocity; }
            set { initialVelocity = value; }
        }
        //whether to use the Origin or not
        public bool SetOrigin { get; set; }
        public float Scale { get; set; }
        protected SpriteEffects Spriteeffect { get; set; }

        //is he active or not (should he be updated and drawn?)
        public bool Alive { get; set; }

        // base version
        public Player(Texture2D textureImage, Vector2 position, Vector2 velocity, bool setOrigin, float scale)
        {
            Position = position;
            TextureImage = textureImage;
            InitialVelocity = velocity;
            Velocity = velocity;
            SetOrigin = setOrigin;
            if (SetOrigin)
            {
                SpriteOrigin = new Vector2(TextureImage.Width / 2, TextureImage.Height / 2);
            }
            Scale = scale;
            Alive = true;
        }
        //version that does not keep sprite on screen
        public virtual void Update(GameTime gameTime)
        {
            if (Alive)
            {
                // time between frames
                float timeLapse = (float)(gameTime.ElapsedGameTime.TotalSeconds);
                //move the sprite
                position += Velocity * timeLapse;
            }
        }
        //version that keeps sprite on screen
        public virtual void Update(GameTime gameTime, GraphicsDevice Device)
        {
            if (Alive)
            {
                //call overload to do rotation and basic movement
                //Update(gameTime);
                //position.X = MathHelper.Clamp(Position.X, 0 + SpriteOrigin.X * Scale, Device.Viewport.Width - SpriteOrigin.X * Scale);
                //position.Y = MathHelper.Clamp(Position.Y, 0 + SpriteOrigin.Y * Scale, Device.Viewport.Height - SpriteOrigin.Y * Scale);

                ////keep on screen
                if (Position.X > (Device.Viewport.Width - 10) - SpriteOrigin.X * Scale)
                {
                    position.X = (Device.Viewport.Width - 10) - SpriteOrigin.X * Scale;
                    velocity.X = -velocity.X * 0.05f;
                }
                else if (Position.X < (SpriteOrigin.X * Scale) + 10)
                {
                    position.X = (SpriteOrigin.X * Scale) + 10;
                    velocity.X = -velocity.X * 0.05f;
                }

                if (Position.Y > (Device.Viewport.Height - 10) - SpriteOrigin.Y * Scale)
                {
                    position.Y = (Device.Viewport.Height - 10) - SpriteOrigin.Y * Scale;
                    velocity.Y = -velocity.Y * 0.05f;

                }
                else if (Position.Y < (SpriteOrigin.Y * Scale) + 10)
                {
                    position.Y = (SpriteOrigin.Y * Scale) + 10;
                    velocity.Y = -velocity.Y * 0.05f;
                }
                Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Alive)
            {
                spriteBatch.Draw(TextureImage,
                     position,
                     null,
                     Microsoft.Xna.Framework.Color.White,
                     0f,
                     SpriteOrigin,
                     Scale,
                     Spriteeffect,
                     0);
            }
        }

        //is there a collision with another sprite?
        public bool CollisionSprite(Player sprite)
        {
            return CollisionRectangle.Intersects(sprite.CollisionRectangle);
        }
        //is there a collision with the mouse?
        public bool CollisionMouse(int x, int y)
        {
            return CollisionRectangle.Contains(x, y);
        }

        //// These match up with the Arrow keys
        //public virtual void Up()
        //{
        //    /*velocity.Y = -225;
        //    velocity.X = 0;*/
        //    velocity = new Vector2(0, -225);
        //}

        //public virtual void Down()
        //{
        //    /*velocity.Y = 225;
        //    velocity.X = 0;*/
        //    velocity = new Vector2(0, 225);
        //}

        //public virtual void Right()
        //{
        //    /*velocity.X = 200;
        //    velocity.Y = 0;*/
        //    velocity = new Vector2(200, 0);

        //}

        //public virtual void Left()
        //{
        //    /*velocity.X = -180;
        //    velocity.Y = 0;*/
        //    velocity = new Vector2(-180, 0);
        //}

        //public virtual void Idle()
        //{
        //    velocity = Vector2.Zero;
        //}
        // These match up with the Arrow keys
        public void Up()
        {
            velocity.Y -= InitialVelocity.Y * .99f;
        }

        public void Down()
        {
            velocity.Y += InitialVelocity.Y * .99f;
        }

        public virtual void Right()
        {
            velocity.X += InitialVelocity.X;
        }

        public virtual void Left()
        {
            velocity.X -= InitialVelocity.X;
        }

        public virtual void Idle()
        {
            Velocity = Velocity * .75f;
        }
    }
}
