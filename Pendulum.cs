using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group_2_assignment4;

public class Pendulum
{
    public Texture2D animal;
    private Texture2D stick;
    public Vector2 position;
    public float scale;
    public float stickScale;

    private float currentAngle;
    private float swingSpeed;
    private float maxAngle;
    private float phaseOffset;
    
    public float jiggle;
    private float jiggleDirection = 1f;
    private float jiggleSpeed = 0.03f;
    private float jiggleLimit = 0.35f;
    
    
    // constructor
    public Pendulum(Texture2D animal, Texture2D stick, Vector2 position, float scale, float stickScale, float swingSpeed, float maxAngle, float phaseOffset)
    {
        this.animal = animal;
        this.stick = stick;
        this.position = position;
        this.scale = scale;
        this.stickScale = stickScale;
        this.swingSpeed = swingSpeed;
        this.maxAngle = maxAngle;
        this.phaseOffset = phaseOffset;
    }

    
    public void Update(GameTime gameTime)
    {
        float timeElasped = (float)gameTime.TotalGameTime.TotalSeconds;
        
        // swing of pendulum
        currentAngle = MathF.Sin((timeElasped * swingSpeed) + phaseOffset) * maxAngle;
        
        // movement of animals
        jiggle += jiggleSpeed * jiggleDirection;
        if (Math.Abs(jiggle) > jiggleLimit)
        {
            jiggleDirection *= -1;
        }
    }


    public void Display(SpriteBatch spriteBatch, Matrix parentMatrix)
    {
        // pendulum logic
        Matrix localSwing = Matrix.CreateRotationZ(currentAngle) * Matrix.CreateTranslation(position.X, position.Y, 0);
        
        // clockface hierarchy
        Matrix worldMatrix = localSwing * parentMatrix;
        
        spriteBatch.Begin(transformMatrix: worldMatrix);
        // draw sticks
        Vector2 stickOrigin = new Vector2(stick.Width / 2f, 0);
        spriteBatch.Draw(stick, Vector2.Zero, null, Color.White, 0f, stickOrigin, stickScale, SpriteEffects.None, 0f);
        //draw animals
        Vector2 animalPos = new Vector2(0, stick.Height *stickScale);
        Vector2 animalOrigin = new Vector2(animal.Width / 2f, animal.Height / 2f);
        spriteBatch.Draw(animal, animalPos, null, Color.White, jiggle, animalOrigin, scale, SpriteEffects.None, 0f);

        spriteBatch.End();
    }
    
}