using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Apple{
    public static Vector2 Position;
    Texture2D t;
    public static Random rnd;
    public Apple(Texture2D t){
        Position = new Vector2(320, 320);
        this.t = t;
        rnd = new Random();
    }
    public void Update(Vector2 snakePosition){

    }
    public void Draw(SpriteBatch spriteBatch){
        spriteBatch.Draw(t, new Rectangle((int)Position.X, (int)Position.Y, 32,32), Color.Red);
    }
    public static void ChangeApplePositon(){
        Position = new Vector2(rnd.Next(0, 25) * 32,rnd.Next(0, 25) * 32);
    }
}