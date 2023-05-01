using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Apple
{
    public static Vector2 Position;
    private Texture2D _texture;
    public Apple(Texture2D texture)
    {
        Position = new Vector2(320, 320);
        _texture = texture;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (Globals.gameState == Globals.GameState.Playing)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, 32, 32), Color.Red);
        }
    }
    public static void ChangeApplePositon()
    {
        Position = new Vector2(Globals.random.Next(0, 25) * 32, Globals.random.Next(0, 25) * 32);
    }
}