using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SpaceTest001
{
    public class Shot : Player
    {
        public Shot(Texture2D textureImage, Vector2 startPosition, float velocity)
            : base(textureImage, startPosition, new Vector2(0, velocity), true, 0.05f)
        {
            //add positions for collisions
        }
    }
}
