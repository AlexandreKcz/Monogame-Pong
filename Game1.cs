using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Mono_Pong;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ShapeBatch _shapeBatch;

    private Paddle _playerPaddle;

    private Ball _ball;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.GraphicsProfile = GraphicsProfile.HiDef;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        _graphics.SynchronizeWithVerticalRetrace = true;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 144d);
        _graphics.ApplyChanges();

        _playerPaddle = new Paddle(new Rectangle(20, GraphicsDevice.Viewport.Height / 4, 20, 200), GraphicsDevice, 400f);

        _ball = new Ball(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 20, 800f, GraphicsDevice);

		base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

		_shapeBatch = new ShapeBatch(GraphicsDevice, Content);
	}

    protected override void Update(GameTime gameTime)
    {
		double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

		if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            _playerPaddle.MovePaddle(-1, deltaTime);
		}
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
			_playerPaddle.MovePaddle(+1, deltaTime);
		}

        _ball.UpdateBall(deltaTime);

        _ball.CheckCollisionWithPaddle(_playerPaddle, -1);

		base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _shapeBatch.Begin();

        _playerPaddle.DrawPaddle(_shapeBatch);

		_ball.DrawBall(_shapeBatch);


		_shapeBatch.End();

        base.Draw(gameTime);
    }
}
