using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    /// <summary>
    /// Defines the two different types
    /// of SpaceShip objects.
    /// </summary>
    enum SpaceShipType
    {
        Destroyer,
        Cruiser
    }

    /// <summary>
    /// One of the space objects the player must avoid in the game.
    /// Is an ICollidable with the necessary Collision and
    /// RemoveOffScreenObject methods.
    /// </summary>
    class SpaceShip : DrawableGameComponent, ICollidable
    {
        /// <summary>
        /// Used to determine the texture and size
        /// of the SpaceShip object.
        /// </summary>
        SpaceShipType type;

        Texture2D texture;

        /// <summary>
        /// Defines the speed at which the SpaceShip
        /// moves across the screen.
        /// </summary>
        int speed;

        /// <summary>
        /// Holds the value for the position of the Asteroid
        /// on the game screen.
        /// </summary>
        Vector2 position;

        /// <summary>
        /// Defines the rectangle boundary around the object
        /// used to check for collisions with the player.
        /// </summary>
        public Rectangle HitBox
        {
            get
            {
                Rectangle hitBox = texture.Bounds;
                hitBox.Location = position.ToPoint();
                return hitBox;
            }
        }

        bool explosionPlaying = false;

        public SpaceShip(Game game, Vector2 position, int speed, SpaceShipType type) 
            : base(game)
        {            
            this.position = position;
            this.speed = speed;
            this.type = type;
        }

        protected override void LoadContent()
        {
            // Each SpaceShipType has its owns specific texture image.

            if (type == SpaceShipType.Cruiser)
            {
                texture = Game.Content.Load<Texture2D>("Cruiser");
            }

            if (type == SpaceShipType.Destroyer)
            {
                texture = Game.Content.Load<Texture2D>("Destroyer");
            }

            base.LoadContent();
        }

        /// <summary>
        /// Updates the SpaceShip object over time. Moves
        /// the SpaceShip across the screen according to its speed,
        /// checks for collisions, and removes it once
        /// it goes off screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position.X -= speed;
            CheckForCollision();
            RemoveOffScreenObjects();
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the SpaceShip to the screen. The specific texture
        /// is determined by the SpaceShipType.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(texture, position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks if the SpaceShip rectangle intersects with
        /// the player rectangle. If it intersects, the player dies.
        /// Remove the player and add an explosion.
        /// </summary>
        public void CheckForCollision()
        {
            Player player = Game.Services.GetService<Player>();
            if (player != null)
            {
                if (HitBox.Intersects(player.playerHitBox))
                {
                    if (explosionPlaying == false)
                    {
                        explosionPlaying = true;
                        Game.Components.Remove(player);
                        Game.Components.Add(new Explosion(Game, player.playerHitBox.Center.ToVector2()));                       
                    }                  
                }
            }
        }

        /// <summary>
        /// If the SpaceShip position goes offscreen,
        /// then remove it from the game.
        /// </summary>
        public void RemoveOffScreenObjects()
        {
            if (position.X + texture.Width < 0)
            {
                Game.Components.Remove(this);
            }
        }
    }
}
