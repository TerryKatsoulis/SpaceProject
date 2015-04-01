using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace SpaceTest001
{
    public class Sprite
    {

        public float aMass = 1.0f;

        public Texture2D Image { get; set; }

        //created a new initial velocity 
        protected Vector2 initialvelocity;
        public Vector2 InitialVelocity
        {
            get { return initialvelocity; }
            set { initialvelocity = value; }
        }

        //created a new force
        protected Vector2 force;
        public Vector2 Force
        {
            get { return force; }
            set { force = value; }
        }

        //created a new acceleration
        protected Vector2 acceleration;
        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        protected Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public float Rotation { get; set; }
        public float Scale { get; set; }

        protected Vector2 center;
        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        public bool Alive { get; set; }
       

        public Sprite(Texture2D loadedTexture)
        {
            Rotation = 0.0f;
            position = Vector2.Zero;
            Image = loadedTexture;
            center = new Vector2(Image.Width / 2, Image.Height / 2);
            initialvelocity = Vector2.Zero;//changed this to initial velocity as we are using that to initialize movement instead of velocity
            Alive = false;
        }
    }
}
