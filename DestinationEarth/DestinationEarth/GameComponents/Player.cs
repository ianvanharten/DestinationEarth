using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DestinationEarth
{
    /// <summary>
    /// Defines the player used in the game.
    /// </summary>
    class Player : DrawableGameComponent
    {
        const int SPEED = 4;
        const double FRAME_DURATION = 0.1;

        /// <summary>
        /// List of textures used to animate the player.
        /// </summary>
        List<Texture2D> textures;

        /// <summary>
        /// Holds the position of the player on the game screen.
        /// </summary>
        Vector2 position;

        /// <summary>
        /// Tracks the current frame in the list of textures
        /// for the player animation.
        /// </summary>
        int currentFrame;

        /// <summary>
        /// Keeps track of when to switch to the next frame
        /// in the player animation.
        /// </summary>
        double frameTimer;

        /// <summary>
        /// Defines the rectangle boundary around the player
        /// used to check for collisions with other objects.
        /// </summary>
        public Rectangle playerHitBox
        {
            get
            {
                Rectangle hitBox = textures[currentFrame].Bounds;
                hitBox.Location = position.ToPoint();
                return hitBox;
            }
        }

        public Player(Game game) 
            : base(game)
        {
            position = new Vector2(Game.GraphicsDevice.Viewport.Width / 4,
                Game.GraphicsDevice.Viewport.Height / 2);
            textures = new List<Texture2D>();
        }

        protected override void LoadContent()
        {
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0001"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0002"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0003"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0004"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0005"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0006"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0007"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0008"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0009"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0010"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0011"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0012"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0013"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0014"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0015"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0016"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0017"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0018"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0019"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0020"));

            base.LoadContent();
        }

        /// <summary>
        /// Check for keyboard input to move the player
        /// around the screen. Cycle through the frames
        /// in the player animation. Last, clamp the player
        /// within the boundaries of the game screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Player movement
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Up))
            {
                position.Y -= SPEED;
            }
            else if (ks.IsKeyDown(Keys.Down))
            {
                position.Y += SPEED;
            }

            if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= SPEED;
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                position.X += SPEED;
            }

            // Animation
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_DURATION)
            {
                currentFrame++;
                frameTimer = 0;
            }

            if (currentFrame >= textures.Count)
            {
                currentFrame = 0;
            }

            // Clamp the player
            position.X = MathHelper.Clamp(position.X, 0, Game.GraphicsDevice.Viewport.Width - textures[currentFrame].Width);
            position.Y = MathHelper.Clamp(position.Y, 0, Game.GraphicsDevice.Viewport.Height - textures[currentFrame].Height);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the player to the screen, using the
        /// current frame to determine which texture
        /// from the list of textures.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(textures[currentFrame], position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }

    }
}
