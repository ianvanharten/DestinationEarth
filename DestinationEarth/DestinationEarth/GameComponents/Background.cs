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
    /// Defines the moving background image used
    /// in the game
    /// </summary>
    class Background : DrawableGameComponent
    {
        Texture2D texture;
        string textureName;
        int speed;

        List<Rectangle> backgroundRects;

        public Background(Game game, string textureName, int speed) 
            : base(game)
        {
            this.textureName = textureName;
            this.speed = speed;
            backgroundRects = new List<Rectangle>();
        }

        /// <summary>
        /// Creates two back to back rectangles which each
        /// contain the background image. The first fills the 
        /// game screen, the second is beside, just offscreen.
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>(textureName);

            backgroundRects.Add(new Rectangle(0, 0,
                Game.GraphicsDevice.Viewport.Width,
                Game.GraphicsDevice.Viewport.Height));

            backgroundRects.Add(new Rectangle(Game.GraphicsDevice.Viewport.Width, 0,
                Game.GraphicsDevice.Viewport.Width,
                Game.GraphicsDevice.Viewport.Height));

            base.LoadContent();
        }

        /// <summary>
        /// Each rectangle moves across the screen to the left.
        /// If the right edge of the rectangle passes offscreen,
        /// it is removed, and a new one is added to the other side.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < backgroundRects.Count; i++)
            {
                Rectangle rect = backgroundRects[i];
                rect.X -= speed;
                backgroundRects[i] = rect;                
            }

            if (backgroundRects[0].Right <= 0)
            {
                Rectangle first = backgroundRects[0];
                Rectangle last = backgroundRects[backgroundRects.Count - 1];
                first.X = last.Right;
                backgroundRects.RemoveAt(0);
                backgroundRects.Add(first);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the background image
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            foreach (Rectangle rect in backgroundRects)
            {
                sb.Draw(texture, rect, Color.White);
            }            
            sb.End();

            base.Draw(gameTime);
        }
    }
}
