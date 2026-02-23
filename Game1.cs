using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group_2_assignment4;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ClockHand _minutehand;
    private ClockHand _hourhand;
    
    //Dharma
    private CelestialBackground _earthSky;
    private CelestialBackground _mysticSky;

    //Xinlin
    private Texture2D fox;
    private Texture2D stick;
    private Texture2D bird;
    
    private Pendulum foxClock;
    private Pendulum birdClock;

    private Matrix clockFaceMatrix; /// IN LOAD CONTENT I JUST MADE THIS THE MIDDLE ISH OF THE SCREEN RN, EDIT WITH ACTUAL CLOCK CENTER
    
    private Vector2 clockCenter;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D texture = Content.Load<Texture2D>("clockhand");
        _minutehand = new ClockHand(texture,
            0f,
            new Vector2(texture.Width/2,
                texture.Height - (texture.Height / 20f)),
            MathHelper.TwoPi / 60f,
            new Vector2(_graphics.PreferredBackBufferWidth / 2f,
                _graphics.PreferredBackBufferHeight / 2f), // Hardcoded for now, change to clock center
            0.15f);
        _hourhand = new ClockHand(texture,
            0f,
            new Vector2(texture.Width/2,
                texture.Height - (texture.Height / 20f)),
            _minutehand.Dtheta / 12f,
            new Vector2(_graphics.PreferredBackBufferWidth / 2f,
                _graphics.PreferredBackBufferHeight / 2f), // Again, should align with clock bg
            0.09f);
        
        //Dharma
        Texture2D sun = Content.Load<Texture2D>("sun");
        Texture2D moon = Content.Load<Texture2D>("moon");
        Texture2D star = Content.Load<Texture2D>("stars"); 
        Texture2D cloud = Content.Load<Texture2D>("clouds");

        _earthSky = new CelestialBackground(sun, moon, 1.0f, Color.White, 0.5f);

        _mysticSky = new CelestialBackground(star, cloud, 0.6f, Color.LightBlue, 1.2f);

        // Xinlin
        ///////////////////// CLOCK CENTER/ CLOCK MATRIX ///////////////////////////////////////////////////////
        clockCenter = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 3);
        clockFaceMatrix = Matrix.CreateTranslation(clockCenter.X, clockCenter.Y, 0);
        ///////////////////////////////////////////////////////////////////////////////////////////////
        
        
        fox = Content.Load<Texture2D>("fox");
        stick = Content.Load<Texture2D>("pendulumstick");
        bird = Content.Load<Texture2D>("bird");
        
        // construct the pendulumns, change scale etc as needed
        foxClock = new Pendulum(fox, stick, Vector2.Zero, 1.0f, 1.3f, 2, 1.0f, 0.5f);
        birdClock = new Pendulum(bird, stick, Vector2.Zero, 1.0f, 1.0f, 2, 1.5f, 0);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _minutehand.Move(gameTime);
        _hourhand.Move(gameTime);
        
        //Dharma
        _earthSky.Update(gameTime);
        _mysticSky.Update(gameTime);
        
        //Xinlin
        foxClock.Update(gameTime);
        birdClock.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _minutehand.Display(_spriteBatch);
        _hourhand.Display(_spriteBatch);
        
        //Dharma
        float centerX = GraphicsDevice.Viewport.Width / 2f;
        Vector2 center = new Vector2(centerX, 200);
        _earthSky.Draw(_spriteBatch, center);
        Vector2 offsetPosition = center + new Vector2(-200, -100);
        _mysticSky.Draw(_spriteBatch, offsetPosition);

        //Xinlin
        foxClock.Display(_spriteBatch, clockFaceMatrix);
        birdClock.Display(_spriteBatch, clockFaceMatrix);
        base.Draw(gameTime);
    }
}