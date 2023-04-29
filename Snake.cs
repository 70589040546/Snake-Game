using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

public class Snake
{
    enum GameState
    {
        Playing,
        Stopped,
        GameOver
    }
    GameState state;
    public Vector2 Direction;
    private float movementCounter;
    private List<Vector2> snakeParts;
    Vector2 snakeHead;
    Texture2D t;

    public Snake(SpriteBatch _spriteBatch)
    {
        state = GameState.Playing;
        t = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        t.SetData(new[] { Color.White });
        snakeParts = new List<Vector2>();
        snakeParts.Add(new Vector2(128, 256));
        snakeParts.Add(new Vector2(96, 224));
        snakeParts.Add(new Vector2(64, 192));
        Direction = new Vector2(1, 0);

    }
    public void Update(GameTime gameTime)
    {

        switch (state)
        {
            case GameState.Playing:
                movementCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Keyboard.GetState().IsKeyDown(Keys.W) && Direction != new Vector2(0, 1)) { Direction = new Vector2(0, -1); }
                if (Keyboard.GetState().IsKeyDown(Keys.S) && Direction != new Vector2(0, -1)) { Direction = new Vector2(0, 1); }
                if (Keyboard.GetState().IsKeyDown(Keys.D) && Direction != new Vector2(-1, 0)) { Direction = new Vector2(1, 0); }// True
                if (Keyboard.GetState().IsKeyDown(Keys.A) && Direction != new Vector2(1, 0)) { Direction = new Vector2(-1, 0); }


                if (movementCounter >= .10f)
                {
                    movementCounter = 0f;

                    for (int i = 1; i < snakeParts.Count; i++)
                    {
                        var oldPos = snakeParts[i];
                        Vector2 d = Direction * 32f;
                        snakeParts[i] += d;
                        snakeParts[i - 1] = oldPos;
                    }
                    var snakeHead = snakeParts[0];
                    if (snakeHead.X > 800 || snakeHead.Y < 0 || snakeHead.Y > 800 || snakeHead.X < 0) { state = GameState.Stopped; }  // works well
                }

                break;
            case GameState.Stopped:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }

    }
    public void Draw(SpriteBatch _spriteBatch, GameTime gameTime)
    {
        // Draw decision based!!!
        // switch (state)
        // {
        //     case GameState.Playing
        //     default:
        // }
        foreach (var item in snakeParts)
        {
            if (snakeParts.Last() == item)
            {
                _spriteBatch.Draw(t, new Rectangle((int)item.X, (int)item.Y, 32, 32), Color.Green);
            }
            else
            {
                _spriteBatch.Draw(t, new Rectangle((int)item.X, (int)item.Y, 32, 32), Color.GreenYellow);
            }

        }

    }
}