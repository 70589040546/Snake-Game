using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D t;
    private Snake _snake;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        this._graphics.PreferredBackBufferWidth = 800;
        this._graphics.PreferredBackBufferHeight = 800;
        this._graphics.ApplyChanges();
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _snake = new Snake(_spriteBatch);
        t = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        t.SetData(new[] {Color.White});
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _snake.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin();
        for (int i = 0; i < 800; i += 25)
        {
            for (int j = 0; j < 800; j+=32)
            {
                _spriteBatch.Draw(t, new Rectangle(i, j, 32, 1), Color.White);
                _spriteBatch.Draw(t, new Rectangle(j,i, 1, 32), Color.White);
            }
        }
        _snake.Draw(_spriteBatch, gameTime);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
