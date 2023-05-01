using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using static Globals;

public class Snake
{
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
        snakeHead = snakeParts[snakeParts.Count - 1];
        Direction = new Vector2(1, 0);
    }
    public void Update(GameTime gameTime)
    {
        switch (state)
        {
            case GameState.Playing:

                movementCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Keyboard.GetState().GetPressedKeyCount() <= 1)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W) && Direction != new Vector2(0, 1)) { Direction = new Vector2(0, -1); }
                    else if (Keyboard.GetState().IsKeyDown(Keys.S) && Direction != new Vector2(0, -1)) { Direction = new Vector2(0, 1); }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D) && Direction != new Vector2(-1, 0)) { Direction = new Vector2(1, 0); }
                    else if (Keyboard.GetState().IsKeyDown(Keys.A) && Direction != new Vector2(1, 0)) { Direction = new Vector2(-1, 0); }
                }

                if (movementCounter >= .15f)
                {
                    movementCounter = 0f;
                    snakeParts[0] += Direction * CELL_SIZE;

                    for (int i = 1; i < snakeParts.Count; i++)
                    {
                        var oldPos = snakeParts[i];
                        snakeParts[i] += Direction * CELL_SIZE;
                        snakeParts[i - 1] = oldPos;
                    }

                    snakeHead = snakeParts[snakeParts.Count - 1];
                    if (snakeHead == Apple.Position) {
                        Apple.ChangeApplePositon(); 
                        snakeParts.Insert(0, new Vector2(snakeParts.Last().X - CELL_SIZE * Direction.X, snakeParts.Last().Y - CELL_SIZE * Direction.Y));
                        Score.instance.score += Globals.random.Next(0, 200);
                    }
                    if (snakeHead.X > 800 || snakeHead.Y < 0 || snakeHead.Y > 800 || snakeHead.X < 0) { state = GameState.GameOver; }
                    for (int i = snakeParts.Count - 2; i >= 0; i--)
                    {
                        if (snakeHead == snakeParts[i]) { state = GameState.GameOver; }
                    }
                }

                break;
            case GameState.GameOver:
                if (Keyboard.GetState().IsKeyDown(Keys.R)) { InitializeGame(); }
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
                
                for (int i = 0; i < 800; i += 32)
                {
                    for (int j = 0; j < 800; j += 32)
                    {
                        _spriteBatch.Draw(t, new Rectangle(i, j, CELL_SIZE, 1), Color.White);
                        _spriteBatch.Draw(t, new Rectangle(j, i, 1, CELL_SIZE), Color.White);
                    }
                }

                foreach (var item in snakeParts)
                {
                    if (snakeParts.Last() == item)
                    {
                        _spriteBatch.Draw(t, new Rectangle((int)item.X, (int)item.Y, CELL_SIZE, CELL_SIZE), Color.Green);
                    }
                    else
                    {
                        _spriteBatch.Draw(t, new Rectangle((int)item.X, (int)item.Y, CELL_SIZE, CELL_SIZE), Color.GreenYellow);
                    }
                }
                _spriteBatch.DrawString(font, "Score : " + Score.instance.score.ToString(), new Vector2(10, -5), Color.Yellow, 0f, Vector2.Zero, .4f, SpriteEffects.None, 1f);
                break;
            case GameState.GameOver:
                Score.instance.CheckMaxAndCurrentScore();
                Vector2 bounds = font.MeasureString("Game Over!");
                _spriteBatch.DrawString(font, "Game Over!", new Vector2(400 - bounds.X / 2, 300 - bounds.Y / 2), Color.Red);
                bounds = font.MeasureString("Score : " + Score.instance.GetMaxScore().ToString());
                _spriteBatch.DrawString(font, "Score : " + Score.instance.GetMaxScore().ToString(), new Vector2(600 - bounds.X / 2, 450 - bounds.Y / 2), Color.Red, 0f, Vector2.Zero, .5f, SpriteEffects.None, 1f);
                // bounds = font.MeasureString("Press R to Play Again");
                // _spriteBatch.DrawString(font, "Press R to Play Again", new Vector2(600 - bounds.X / 2, 450 - bounds.Y / 2), Color.Red, 0f, Vector2.Zero, .5f, SpriteEffects.None, 1f);
                
                break;
            default:
                break;
        }


    }
    private void InitializeGame()
    {
        state = GameState.Playing;
        snakeParts.Clear();
        snakeParts.Add(new Vector2(0, 256));
        Direction = new Vector2(1, 0);
        movementCounter = 0f;
        Score.instance.score = 0;
    }


}
