﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Score scoreInstance;
    private Texture2D whiteTexture;
    private Snake _snake;
    private Apple _apple;
    private SpriteFont largeFont;
    private SpriteFont normalFont;
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
        largeFont = Content.Load<SpriteFont>("TextFont");
        normalFont = Content.Load<SpriteFont>("NormalFont");
        whiteTexture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        whiteTexture.SetData(new[] {Color.White});
        _snake = new Snake(whiteTexture);
        _apple = new Apple(whiteTexture);
        scoreInstance= new Score();
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
        _snake.Draw(_spriteBatch, largeFont);
        _apple.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
