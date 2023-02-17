using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using A1r.Input;

namespace PenguinPoints
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public InputManager inputManager;
        Controller controller;

        public static SpriteFont font;
        public static Texture2D icon, interfacebar, icon_small;

        float deltaTime = 0;

        Presentation presentation;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = false;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            inputManager = new InputManager(this);
            controller = new Controller(inputManager);

            {
                Slide slide1, slide2, slide3, slide4, slide5;

                slide1 = new Slide();
                slide1.AddItem(new Text("Welcome to penguinpoints!", new Vector2(200, 120)));

                slide2 = new Slide();
                slide2.AddItem(new Text("Penguinpoints is cool", new Vector2(200, 120)));
                slide2.AddItem(new Text("And you can have lots of text!", new Vector2(240, 160)));
                slide2.AddItem(new Text("So cool and totally useful", new Vector2(280, 200)));
                slide2.SetParent(slide1);

                slide3 = new Slide();
                slide3.AddItem(new Text("and the text can be everywhere.", new Vector2(320, 120)));
                slide3.AddItem(new Text("and soon there will be image support too!", new Vector2(140, 170)));
                slide3.SetParent(slide2);

                slide4 = new Slide();
                slide4.AddItem(new Text("hehehe hohoho", new Vector2(220, 160)));
                slide4.SetParent(slide3);

                slide5 = new Slide();
                slide5.AddItem(new Text("ahahahahahahahahah", new Vector2(220, 160)));
                slide5.SetParent(slide4);

                presentation = new Presentation(slide1);

            }
            
            presentation.StartPresentation();

            font = Content.Load<SpriteFont>(@"font");
            icon = Content.Load<Texture2D>(@"penguinpoints");
            icon_small = Content.Load<Texture2D>(@"default_small");
            interfacebar = Content.Load<Texture2D>(@"interfacebar");
            // TODO: use this.Content to load your game content here

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            inputManager.Update(gameTime);
            controller.Update(presentation, deltaTime);
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(presentation.current.BackgroundColor);

            spriteBatch.Begin();

            

            presentation.Draw(spriteBatch);


            if (controller.editing)
            {
                spriteBatch.Draw(interfacebar, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, interfacebar.Height), Color.White);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
