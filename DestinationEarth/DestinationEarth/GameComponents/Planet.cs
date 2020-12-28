using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.GameComponents
{
    /// <summary>
    /// Defines the planet object which appears
    /// at the end of the game level. Is an ICollidable.
    /// </summary>
    class Planet : DrawableGameComponent, ICollidable
    {
        const int SPEED = 2;

        Texture2D texture;
        Vector2 position;

        /// <summary>
        /// Defines the rectangle boundary around the planet
        /// used to check for collisions with the player
        /// to end the level.
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

        public Planet(Game game) 
            : base(game)
        {
            this.position = new Vector2(Game.GraphicsDevice.Viewport.Width, 0);
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("planet2");

            base.LoadContent();
        }

        /// <summary>
        /// Move the planet slowly across the screen and
        /// check if the player collides with the planet,
        /// ending the level.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position.X -= SPEED;
            CheckForCollision();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the planet on the screen.
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
        /// If the player rectangle intersects with the planet's,
        /// then remove the player, and show the WinScene.
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

                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<WinScene>().Show();
                }
            }
        }

        public void RemoveOffScreenObjects()
        {
            
        }
    }
}
