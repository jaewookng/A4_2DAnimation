using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group_2_assignment4;

public class Game1 : Game 
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private CelestialBackground _earthSky;
    private CelestialBackground _mysticSky;

    public Game1() 
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
    }

    protected override void LoadContent() 
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    
        Texture2D sun = Content.Load<Texture2D>("sun");
        Texture2D moon = Content.Load<Texture2D>("moon");
        Texture2D star = Content.Load<Texture2D>("stars"); 
        Texture2D cloud = Content.Load<Texture2D>("clouds");

        _earthSky = new CelestialBackground(sun, moon, 1.0f, Color.White, 0.5f);

        _mysticSky = new CelestialBackground(star, cloud, 0.6f, Color.LightBlue, 1.2f);
    }
    protected override void Update(GameTime gameTime) 
    {
        _earthSky.Update(gameTime);
        _mysticSky.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) 
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        float centerX = GraphicsDevice.Viewport.Width / 2f;
        Vector2 center = new Vector2(centerX, 200);
        _earthSky.Draw(_spriteBatch, center);
        Vector2 offsetPosition = center + new Vector2(-200, -100);
        _mysticSky.Draw(_spriteBatch, offsetPosition);

        base.Draw(gameTime);
    }
}