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

    private float radius;
    private Vector2 circlePosition;

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

        radius = 0;
        circlePosition = Vector2.Zero;

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
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        circlePosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        radius += .1f;

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _shapeBatch.Begin();
        _shapeBatch.DrawCircle(circlePosition, radius, Color.Transparent, Color.White, 1f);
        _shapeBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
