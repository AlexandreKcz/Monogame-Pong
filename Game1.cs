using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_Pong;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ShapeBatch _shapeBatch;

    private float paddleSpeed = 200f;
    private Rectangle paddle;
    private Vector2 paddlePosition;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.GraphicsProfile = GraphicsProfile.HiDef;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        paddle = new Rectangle(20, GraphicsDevice.Viewport.Height / 2, 20, 200);
        paddlePosition = new Vector2(paddle.X, paddle.Y);

		base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

		_shapeBatch = new ShapeBatch(GraphicsDevice, Content);

		// TODO: use this.Content to load your game content here
	}

    protected override void Update(GameTime gameTime)
    {
		double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

		if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
			paddlePosition.Y -= paddleSpeed * (float)deltaTime;
		}
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
			paddlePosition.Y += paddleSpeed * (float)deltaTime;
		}

        paddle.X = (int)paddlePosition.X; paddle.Y = (int) paddlePosition.Y;

		// TODO: Add your update logic here

		base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _shapeBatch.Begin();
        _shapeBatch.DrawRectangle(new Vector2(paddle.X, paddle.Y), new Vector2(paddle.Width, paddle.Height), Color.White, Color.Transparent, 1);
        _shapeBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
