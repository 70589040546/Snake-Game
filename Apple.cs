using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Apple
{
    public static Vector2 Position;
    Texture2D t;
    public Apple(Texture2D t)
    {
        Position = new Vector2(320, 320);
        this.t = t;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (Globals.state == Globals.GameState.Playing)
        {
            spriteBatch.Draw(t, new Rectangle((int)Position.X, (int)Position.Y, 32, 32), Color.Red);
        }
    }
    public static void ChangeApplePositon()
    {
        Position = new Vector2(Globals.random.Next(0, 25) * 32, Globals.random.Next(0, 25) * 32);
    }
}