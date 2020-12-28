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
    /// A point object the player wants to collect
    /// in order to gain points in the game. Is an ICollidable 
    /// with the necessary Collision and
    /// RemoveOffScreenObject methods.
    /// </summary>
    class PointsObject : DrawableGameComponent, ICollidable
    {
        Texture2D texture;

        /// <summary>
        /// Holds the value for the position of
        /// the point object on the game screen.
        /// </summary>
        Vector2 position;

        /// <summary>
        /// Defines the speed at which the point object
        /// moves across the screen.
        /// </summary>
        int speed;

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

        public PointsObject(Game game, Vector2 position, int speed) 
            : base(game)
        {
            this.position = position;
            this.speed = speed;
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("sprite_asteroid_2");

            base.LoadContent();
        }

        /// <summary>
        /// Updates the Point object over time. Moves
        /// the object across the screen according to its speed,
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
        /// Draws the Point object to the screen.
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
        /// Checks if the Point bject rectangle intersects with
        /// the player rectangle. If it intersects, the player
        /// score goes up by one point.
        /// </summary>
        public void CheckForCollision()
        {
            Player player = Game.Services.GetService<Player>();
            PointMeter pointMeter = Game.Services.GetService<PointMeter>();

            if (player != null && pointMeter != null)
            {
                if (HitBox.Intersects(player.playerHitBox))
                {
                    pointMeter.playerScore++;
                    Game.Components.Remove(this);
                }
            }
        }

        /// <summary>
        /// If the Point object position goes offscreen,
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
