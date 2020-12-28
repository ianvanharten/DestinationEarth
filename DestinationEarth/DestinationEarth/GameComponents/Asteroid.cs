using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.GameComponents;
using DestinationEarth.MenuComponents;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    /// <summary>
    /// Defines the four different types of
    /// asteroids used for the Asteroid object 
    /// in the game.
    /// </summary>
    public enum AsteroidType
    {
        Brown,
        Gray,
        Dark,
        Square,
    }

    /// <summary>
    /// One of the space objects the player must avoid in the game.
    /// Is an ICollidable with the necessary Collision and
    /// RemoveOffScreenObject methods.
    /// </summary>
    public class Asteroid : DrawableGameComponent, ICollidable  
    {
        /// <summary>
        /// Used to determine the texture and size of the
        /// Asteroid object.
        /// </summary>
        AsteroidType type;

        Texture2D texture;

        /// <summary>
        /// Defines the speed at which the Asteroid moves
        /// across the screen.
        /// </summary>
        int speed;

        /// <summary>
        /// Holds the value for the position of the Asteroid
        /// on the game screen.
        /// </summary>
        public Vector2 position;

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

        public Asteroid(Game game, Vector2 position, int speed, AsteroidType type) 
            : base(game)
        {
            this.type = type;
            this.position = position;
            this.speed = speed;
        }

        protected override void LoadContent()
        {
            // Each AsteroidType has its own specific texture image.

            if (type == AsteroidType.Brown)
            {
                texture = Game.Content.Load<Texture2D>("asteroid_brown");
            }

            if (type == AsteroidType.Gray)
            {
                texture = Game.Content.Load<Texture2D>("asteroid_gray");
            }

            if (type == AsteroidType.Dark)
            {
                texture = Game.Content.Load<Texture2D>("asteroid_dark");
            }

            if (type == AsteroidType.Square)
            {
                texture = Game.Content.Load<Texture2D>("asteroid_square");
            }
            
            base.LoadContent();
        }

        /// <summary>
        /// Updates the Asteroid object over time. Moves
        /// the asteroid across the screen according to its speed,
        /// checks for collisions, and removes the asteroid once
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
        /// Draws the asteroid to the screen. The specific texture
        /// is determined by the AsteroidType.
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
        /// Checks if the Asteroid rectangle intersects with
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
                    Game.Components.Remove(player);
                    Game.Services.RemoveService(player.GetType());
                    Game.Components.Add(new Explosion(Game, player.playerHitBox.Center.ToVector2()));
                }
            }
        }

        /// <summary>
        /// If the Asteroid position goes offscreen,
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
