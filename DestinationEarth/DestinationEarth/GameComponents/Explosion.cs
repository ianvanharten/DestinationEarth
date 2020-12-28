using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    /// <summary>
    /// Defines the explosion that occurs
    /// if the player collides with an asteroid
    /// or spaceship.
    /// </summary>
    class Explosion : DrawableGameComponent
    {
        const int WIDTH = 96;
        const int HEIGHT = 96;
        const float VOLUME = 0.2f;
        const float FRAME_DURATION = 0.1f;

        /// <summary>
        /// A spritesheet of an explosion animation
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// Used to cycle through the frames in the spritesheet
        /// </summary>
        List<Rectangle> sourceRects;

        int currentFrame;
        double frameTimer;

        static SoundEffect explosionSFX;
        Vector2 position;

        double explosionTimer;

        public Explosion(Game game, Vector2 position) 
            : base(game)
        {
            this.position = position;
            this.explosionTimer = 0;
        }

        /// <summary>
        /// Load the spritesheet, and create a list of rectanges
        /// for each frame of the animation. Also load and play
        /// the explosion sound effect.
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Explosion");
            sourceRects = new List<Rectangle>();
            int explosionFrames = 12;

            for (int frame = 0; frame < explosionFrames; frame++)
            {
                sourceRects.Add(new Rectangle(frame * WIDTH, 0, WIDTH, HEIGHT));
            }

            explosionSFX = Game.Content.Load<SoundEffect>("Chunky Explosion");

            explosionSFX.Play(VOLUME, 0, 0);

            base.LoadContent();
        }

        /// <summary>
        /// Update cycles through the frames of the animation.
        /// At the end of the animation, reset the game.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_DURATION)
            {
                currentFrame++;
                frameTimer = 0;
            }

            if (currentFrame >= sourceRects.Count)
            {
                Game.Components.Remove(this);
                ResetLevel();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the explosion to the screen, using the current frame
        /// of the animation.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(texture, position, sourceRects[currentFrame], Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// After an explosion occurs, the game is over and must
        /// be reset. Remove the player and the point meter from the game
        /// and set them to null. Then call the Reset method from Game1
        /// and show the Game Over scene.
        /// </summary>
        private void ResetLevel()
        {
            Player player = Game.Services.GetService<Player>();
            PointMeter pointMeter = Game.Services.GetService<PointMeter>();

            if (player != null)
            {
                Game.Services.RemoveService(player.GetType());
                player = null;
            }
            
            Game.Services.RemoveService(pointMeter.GetType());           
            pointMeter = null;

            ((Game1)Game).Reset(Game.Services.GetService<LevelOneScene>());
            Game.Services.GetService<GameOverScene>().Show();
        }
    }
}
