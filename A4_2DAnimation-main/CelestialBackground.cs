using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace group_2_assignment4;

public class CelestialBackground 
{
    private Texture2D _sunTex, _moonTex;
    private float _timer;
    private float _orbitRadius = 200f;
    
    public float ScaleFactor { get; set; }
    public Color SkyColor { get; set; }
    public float OrbitSpeed { get; set; }

    public CelestialBackground(Texture2D sun, Texture2D moon, float scale, Color color, float speed) 
    {
        _sunTex = sun;
        _moonTex = moon;
        ScaleFactor = scale;
        SkyColor = color;
        OrbitSpeed = speed;
        _timer = 0f;
    }

    public void Update(GameTime gameTime) 
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds * OrbitSpeed;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 screenPosition) 
    {
        Matrix rootMatrix = Matrix.CreateScale(ScaleFactor) * Matrix.CreateTranslation(new Vector3(screenPosition, 0));

        spriteBatch.Begin(transformMatrix: rootMatrix);

        float sunX = MathF.Cos(_timer) * _orbitRadius;
        float sunY = MathF.Sin(_timer) * _orbitRadius;
        
        float moonX = MathF.Cos(_timer + MathF.PI) * _orbitRadius;
        float moonY = MathF.Sin(_timer + MathF.PI) * _orbitRadius;

        spriteBatch.Draw(_sunTex, new Vector2(sunX, sunY), SkyColor);
        spriteBatch.Draw(_moonTex, new Vector2(moonX, moonY), SkyColor);

        spriteBatch.End();
    }
}