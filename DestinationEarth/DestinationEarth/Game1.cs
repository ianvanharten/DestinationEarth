using DestinationEarth.Scenes;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DestinationEarth.GameComponents;

namespace DestinationEarth
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            MenuScene menuScene = new MenuScene(this);
            this.Components.Add(menuScene);
            this.Services.AddService<MenuScene>(menuScene);

            HighScoreScene highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);
            this.Services.AddService<HighScoreScene>(highScoreScene);

            AboutScene aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);
            this.Services.AddService<AboutScene>(aboutScene);

            HelpScene helpScene = new HelpScene(this);
            this.Components.Add(helpScene);
            this.Services.AddService<HelpScene>(helpScene);

            LevelOneScene levelOneScene = new LevelOneScene(this);
            this.Components.Add(levelOneScene);
            this.Services.AddService<LevelOneScene>(levelOneScene);

            WinScene winScene = new WinScene(this);
            this.Components.Add(winScene);
            this.Services.AddService<WinScene>(winScene);

            GameOverScene gameOverScene = new GameOverScene(this);
            this.Components.Add(gameOverScene);
            this.Services.AddService<GameOverScene>(gameOverScene);

            MusicPlayer musicPlayer = new MusicPlayer(this);
            this.Components.Add(musicPlayer);
            this.Services.AddService<MusicPlayer>(musicPlayer);

            HighScoreWriter highScoreWriter = new HighScoreWriter(this);
            this.Components.Add(highScoreWriter);
            this.Services.AddService<HighScoreWriter>(highScoreWriter);

            base.Initialize();

            HideAllScenes();
            menuScene.Show();
        }

        /// <summary>
        /// Loops through every scene in the game, and uses the GameScene's 
        /// Hide method to hide and disenable each game component.
        /// </summary>
        public void HideAllScenes()
        {
            foreach (GameScene scene in Components.OfType<GameScene>())
            {
                scene.Hide();
            }
        }

        /// <summary>
        /// Removes the level one scene, sets it to null,
        /// then creates a new level so that the game can be 
        /// played again.
        /// </summary>
        /// <param name="levelOne"></param>
        public void Reset(LevelOneScene levelOne)
        {
            this.Services.RemoveService(levelOne.GetType());
            levelOne = null;

            levelOne = new LevelOneScene(this);
            this.Components.Add(levelOne);
            this.Services.AddService<LevelOneScene>(levelOne);
            HideAllScenes();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService<SpriteBatch>(spriteBatch);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
