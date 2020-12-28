using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    /// <summary>
    /// Interface implemented for objects the player
    /// can collide with in the game.
    /// </summary>
    interface ICollidable
    {
        Rectangle HitBox { get; }
        void CheckForCollision();
        void RemoveOffScreenObjects();
    }
}
