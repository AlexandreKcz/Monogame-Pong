using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

    /* Manager */

    private GameManager _gameManager;

    /* Game Objects */
    private Paddle _playerPaddle;

    private AiPaddle _aiPaddle;

    private Rectangle _demarcationLine;

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

        _gameManager = new GameManager(GraphicsDevice);

        _graphics.SynchronizeWithVerticalRetrace = true;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 144d);
        _graphics.ApplyChanges();

        _playerPaddle = new Paddle(new Rectangle(20, GraphicsDevice.Viewport.Height / 4, 20, 150), GraphicsDevice, 600f);

		_aiPaddle = new AiPaddle(new Rectangle(GraphicsDevice.Viewport.Width - 40, GraphicsDevice.Viewport.Height / 4, 20, 150), GraphicsDevice, 300f);

		_ball = new Ball(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 20, 1200f, GraphicsDevice);

        _demarcationLine = new Rectangle(GraphicsDevice.Viewport.Width / 2, 0, 5, GraphicsDevice.Viewport.Height);

		base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

		_shapeBatch = new ShapeBatch(GraphicsDevice, Content);

        _gameManager.GoalSFX = Content.Load<SoundEffect>("Audio/goal");
        _gameManager.SetSFX = Content.Load<SoundEffect>("Audio/set");
        _ball.BounceSFX = Content.Load<SoundEffect>("Audio/bounce");
	}

    protected override void Update(GameTime gameTime)
    {
		double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

        _gameManager.UpdateTimer(deltaTime, _ball);

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

        _aiPaddle.UpdateAIBehaviour(_ball.Position, _ball.Radius, deltaTime);

        _ball.CheckCollisionWithPaddle(_playerPaddle, -1);
        _ball.CheckCollisionWithPaddle(_aiPaddle, 1);

		base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _shapeBatch.Begin();

        _playerPaddle.DrawPaddle(_shapeBatch);

        _aiPaddle.DrawPaddle(_shapeBatch);

		_ball.DrawBall(_shapeBatch);

        _shapeBatch.DrawRectangle(new Vector2(_demarcationLine.X, _demarcationLine.Y), new Vector2(_demarcationLine.Width, _demarcationLine.Height), Color.White, Color.Transparent, 1);

		_shapeBatch.End();

        base.Draw(gameTime);
    }
}
