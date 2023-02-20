using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using A1r.Input;

namespace PenguinPoints
{
    //Är linux lärare
    //försöker ta upp en presentation
    // * Min homebrew shitbox techpad 9000 startar upp efter 10 min
    // * min självmoddade ubuntu 1.0.12.0 sprakar ur sig 10 datorfönster som berättar alla detaljer om datorn
    // * Elev frågar var Powerpointen är
    // * Tfw powerpoints är bara för sheeple jag har linux penguinpoints
    // * klickar på penguinpoints
    // * Datorn börjar flåsa som ett mindre flygplan, eleverna längst fram börjar klaga
    // * Ubuntu börjar spruta ur sig error meddelande, jag glömde compilea all min custom penguinpoint kod som hade visat en ascool gif i min powerpoint
    // * börjar arg gråta
    // * tfw penguinpoints krashar ubuntu så att värmesensorn stängs av, min techbad börjar brinna
    // * elever skriker och gråter
    // * Niagara måste evakueras efter brand i sal A367
    // * tfw still better than sheeple mac user
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public InputManager inputManager;
        Controller controller;

        public static SpriteFont font;
        public static Texture2D icon, interfacebar, icon_small;
        public static Game1 self;

        float deltaTime = 0;

        Presentation presentation;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = false;
            self = this;
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

            font = Content.Load<SpriteFont>(@"font");
            icon = Content.Load<Texture2D>(@"penguinpoints");
            icon_small = Content.Load<Texture2D>(@"default_small");
            interfacebar = Content.Load<Texture2D>(@"interfacebar");

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
            controller.Draw(spriteBatch);

            if (controller.editing)
            {
                spriteBatch.Draw(interfacebar, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, interfacebar.Height), Color.White);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public Texture2D GenerateTexture(int width, int height)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Length; pixel++)
            {
                data[pixel] = Color.Transparent;
                if (pixel < width)
                {
                    if(pixel % 3 == 1)
                    {
                        data[pixel] = Color.Black;
                    }
                    else
                    {
                        data[pixel] = Color.Transparent;
                    }
                }

                if(pixel%height == 0)
                {
                    if (pixel % 3 == 1)
                    {
                        data[pixel] = Color.Black;
                    }
                    else
                    {
                        data[pixel] = Color.Transparent;
                    }
                }

                if (pixel % height == width - 1)
                {
                    if (pixel % 3 == 1)
                    {
                        data[pixel] = Color.Black;
                    }
                    else
                    {
                        data[pixel] = Color.Transparent;
                    }
                }

                if (pixel > width * (height - 1))
                {
                    if (pixel % 3 == 1)
                    {
                        data[pixel] = Color.Black;
                    }
                    else
                    {
                        data[pixel] = Color.Transparent;
                    }
                }

                
            }

            texture.SetData(data);

            return texture;
        }

    }
}
