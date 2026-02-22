using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group_2_assignment4;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    
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

        // TODO: use this.Content to load your game content here
        
        _graphics.PreferredBackBufferHeight = 1000;
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.ApplyChanges();
        
        
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
        
        //update movement
        foxClock.Update(gameTime);
        birdClock.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        
        foxClock.Display(_spriteBatch, clockFaceMatrix);
        birdClock.Display(_spriteBatch, clockFaceMatrix);

        base.Draw(gameTime);
    }
}