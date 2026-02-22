//Jaewoo Kang | jk49356
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group_2_assignment4;

public class ClockHand
{
    //Angle in radians, as suggested was convention
    public float Angle { get; set; }
    public Vector2 Origin { get; set; }
    public float Dtheta { get; set; }
    public float Scale { get; set; }
    public Vector2 Pivot { get; set; }
    public Matrix Tick { get; set; }
    public Texture2D Texture { get; set; }
    
    // Constructor for a clock hand with angle (updated through gametime),
    // origin of needle,
    // and change in angle theta per unit gametime
    public ClockHand(Texture2D texture, float angle,  Vector2 origin,  float dtheta,  Vector2 pivot, float scale)
    {
        Texture = texture;
        Angle = angle;
        Origin = origin;
        Dtheta = dtheta;
        Pivot = pivot;
        Scale = scale;
        
        MakeTick();
    }

    // The movements of the clock hand are in the degree change wrt the origin of the needle
    public void Move(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // each minute = 1 sec
        Angle += Dtheta *  dt;
        
        MakeTick();
    }

    private void MakeTick()
    {
        var rot =  Matrix.CreateRotationZ(Angle);
        var trans = Matrix.CreateTranslation(Pivot.X,  Pivot.Y, 0);
        
        Tick = Matrix.Multiply(rot, trans);
    }

    // The draw method has a rotation field, but I prefer to update it in Move() in Game1 update logic
    public void Display(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(transformMatrix: Tick);
        spriteBatch.Draw(Texture,
            Vector2.Zero,
            null,
            Color.White,
            0f,
            Origin,
            Scale,
            SpriteEffects.None,
            0f);
        spriteBatch.End();
    }
}