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
    public class Asteroid : Sprite {
       
       
       private Rectangle viewPort;
       private Random random = new Random();
       float G = 180000000;

        //TODO: Constructor / Initialize / Update / Draw / Orbit / accessors & mutators (getters / setters) 
        public bool inBelt { get; set; }
        private float mass { get; set; }


        public void Update(float elapsedTime, ref Player thePlayer) {
            //TODO: playerPos will be used in Orbit as the center variable 

            if (inBelt) {
                //TODO: call orbit to update position 
            } else {
               //TODO:  Update position 
            }




            if (this.Alive) {
                this.Position += this.Velocity;
                this.Rotation += random.Next(-1, 1) * elapsedTime;

                Vector2 dir = thePlayer.Position - this.Position;
                float radius = dir.Length();
                float forceMag = (G * this.mass * thePlayer.psMass) / (float)Math.Pow(radius, 2);
                dir.Normalize();
                Vector2 gForce = forceMag * dir;

                this.Force = gForce;
                this.Acceleration = this.Force / this.mass;
                this.Position += ((this.Velocity * elapsedTime) + (0.5f * (this.Acceleration * (elapsedTime * elapsedTime))));

                if (radius <= 75) {

                    //respawn = 0; mv^2/r

                    this.Position = new Vector2(thePlayer.Position.X, thePlayer.Position.Y);
                    this.Center = new Vector2(60, 60);

                    /*asteroid.Velocity = new Vector2(dir.Y, -dir.X);
                    asteroid.Acceleration = new Vector2(0, 0);
                    asteroid.Position += ((asteroid.Velocity * elapsed) + (0.5f * (asteroid.Acceleration * (elapsed * elapsed))));*/
                    this.Rotation -= 0.02f;
                    Console.WriteLine("Asteroid: " + this.Rotation);
                    this.Rotation = MathHelper.Clamp(this.Rotation, -MathHelper.Pi * 360, 360);
                    inBelt = true;

                }

                if (!viewportRect.Contains(new Point((int)this.Position.X, (int)this.Position.Y))) {
                    this.Alive = false;
                }
            } else {
                this.Alive = true;

                this.Position = new Vector2(MathHelper.Lerp((float)viewportRect.Width * minAsteroidWidth, (float)viewportRect.Width * maxAsteroidWidth, (float)random.NextDouble()), viewportRect.Top);
                this.Velocity = new Vector2(random.Next(-2, 2), MathHelper.Lerp(minAsteroidVelocity, maxAsteroidVelocity, (float)random.NextDouble()));

            }

        }

        public Asteroid(Texture2D textureImage) : base(textureImage) {
            Initialize();

        }

        // future constructor for Boss that can create and throw asteroids 
        public Asteroid(Texture2D textureImage, Vector2 position, Vector2 velocity, bool setOrigin, float scale) : base(textureImage) {
            Initialize(false, 1.0f); // TODO: finish this to match the function (below)
        }

        // main initialization function called during asteroid creation 
        private void Initialize(bool inBlt = false, float m = 1.0f /*, more stuff as you go */  ) {
            //viewPort =
            inBelt = inBlt;
            mass = m;
            viewPort = 
            return;
        }


        private static Vector2 Orbit(Vector2 center, Vector2 startPos, float speed, float time) {
            // positive speed means CCW around the center, negative means CW.
            var radiusVec = (startPos - center);
            var radius = radiusVec.Length();
            var angularVelocity = speed / radius; // Add check for divide by zero
            Vector2 perpendicularVec = new Vector2(-radiusVec.Y, radiusVec.X);
            return center + (float)Math.Cos(time * angularVelocity) * radiusVec + (float)Math.Sin(time * angularVelocity) * perpendicularVec;
        }


        private static float OrbitSpaceing(int total, float radius) {
            float distance = 0.0f; // degrees in radians

            return distance; 
        }


    }
}
