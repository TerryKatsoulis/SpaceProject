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
        public class Waypoint
        {
            public Vector2 pos;

            public Waypoint(Vector2 wayvec)
            {
                pos = wayvec;
            }
        }

    }