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

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _minutehand.Move(gameTime);
        _hourhand.Move(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _minutehand.Display(_spriteBatch);
        _hourhand.Display(_spriteBatch);

        base.Draw(gameTime);
    }
}