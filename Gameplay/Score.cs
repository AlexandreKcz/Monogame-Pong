using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Score
{
	public static Score Instance { get { return _instance; } }
	private static Score _instance;

	public SpriteFont Font;

	public (int p1, int p2) ScoreTuple { get { return _scoreTuple; } set { _scoreTuple = value; } }
	private (int p1, int p2) _scoreTuple;

	private Vector2 _screenDimensions;

	public Score(GraphicsDevice graphics)
	{
		_instance = this;
		_scoreTuple = (p1: 0, p2: 0);
		_screenDimensions = new Vector2(graphics.Viewport.Width, graphics.Viewport.Height);
	}

	public void DrawScore(SpriteBatch spriteBatch)
	{
		spriteBatch.DrawString(Font, _scoreTuple.p1 + "", new Vector2(_screenDimensions.X * 0.25f, 20), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 1f);
		spriteBatch.DrawString(Font, _scoreTuple.p2 + "", new Vector2(_screenDimensions.X * 0.75f, 20), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 1f);
	}
}
