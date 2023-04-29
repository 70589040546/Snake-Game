using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

public class Snake
{
    public enum GameState
    {
        Playing,
        Stopped,
        GameOver
    }
    GameState state;
    public Vector2 Direction;
    private float movementCounter;
    private List<Vector2> snakeParts;
    public Vector2 snakeHead;
    Texture2D t;

    public Snake(Texture2D t)
    {
        state = GameState.Playing;
        this.t = t;
       
        snakeParts = new List<Vector2>();
        snakeParts.Add(new Vector2(0, 256));
        snakeParts.Add(new Vector2(-32, 256));
        snakeParts.Add(new Vector2(-64, 256));
        snakeHead = snakeParts[snakeParts.Count - 1];
        Direction = new Vector2(1, 0);
    }
    public void Update(GameTime gameTime)
    {

        switch (state)
        {
            case GameState.Playing:

                movementCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //if( Keyboard.GetState().GetPressedKeyCount()){}  ***Preverting multiple key presses at same time. 
                if (Keyboard.GetState().IsKeyDown(Keys.W) && Direction != new Vector2(0, 1)) { Direction = new Vector2(0, -1); }
                else if (Keyboard.GetState().IsKeyDown(Keys.S) && Direction != new Vector2(0, -1)) { Direction = new Vector2(0, 1); }
                else if (Keyboard.GetState().IsKeyDown(Keys.D) && Direction != new Vector2(-1, 0)) { Direction = new Vector2(1, 0); }// True
                else if (Keyboard.GetState().IsKeyDown(Keys.A) && Direction != new Vector2(1, 0)) { Direction = new Vector2(-1, 0); }


                if (movementCounter >= .15f)
                {
                    movementCounter = 0f;
                    snakeParts[0] += Direction * 32f;

                    for (int i = 1; i < snakeParts.Count; i++)
                    {
                        var oldPos = snakeParts[i];
                        snakeParts[i] += Direction * 32f;
                        snakeParts[i - 1] = oldPos;
                    }
                    snakeHead = snakeParts[snakeParts.Count - 1];
                    if(snakeHead == Apple.Position){Apple.ChangeApplePositon(); snakeParts.Insert(0,new Vector2(snakeParts.Last().X - 32 * Direction.X, snakeParts.Last().Y - 32 *Direction.Y));}
                    if (snakeHead.X > 800 || snakeHead.Y < 0 || snakeHead.Y > 800 || snakeHead.X < 0) { state = GameState.GameOver; }  
                }

                break;
            case GameState.Stopped:

                break;
            case GameState.GameOver:
                if (Keyboard.GetState().IsKeyDown(Keys.R)) { InitializeGame();}
                break;
            default:
                break;
        }

    }

    public void Draw(SpriteBatch _spriteBatch, SpriteFont font)
    {
        switch (state)
        {
            case GameState.Playing:
                for (int i = 0; i < 800; i += 25)
                {
                    for (int j = 0; j < 800; j += 32)
                    {
                        _spriteBatch.Draw(t, new Rectangle(i, j, 32, 1), Color.White);
                        _spriteBatch.Draw(t, new Rectangle(j, i, 1, 32), Color.White);
                    }
                }

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
                break;
            case GameState.GameOver:
                Vector2 bounds = font.MeasureString("Game Over!");
                _spriteBatch.DrawString(font, "Game Over!", new Vector2(400 - bounds.X / 2, 300 - bounds.Y / 2), Color.Red);
                bounds = font.MeasureString("Press R to Play Again");
                _spriteBatch.DrawString(font, "Press R to Play Again", new Vector2(600 - bounds.X / 2, 450 - bounds.Y / 2), Color.Red, 0f, Vector2.Zero, .5f, SpriteEffects.None, 1f);
                break;
            default:
                break;
        }


    }
    private void InitializeGame()
    {
        state = GameState.Playing;
        snakeParts.Clear();
        snakeParts.Add(new Vector2(768, 256));
        Direction = new Vector2(-1, 0); 
        movementCounter = 0f;
    }


}
