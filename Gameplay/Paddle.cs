using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Paddle
{
	public Vector2 PaddlePosition
	{
		get { return _paddlePosition; }
		set { 
			_paddlePosition = value;
			updateRect();
		}
	}

	private Vector2 _paddlePosition;

	public float Speed
	{
		get
		{
			return _speed;
		}
		set
		{
			_speed = value;
		}
	}

	private float _speed = 200f;

	private Rectangle _paddleRect;

	private Vector2 _screenDimension;

	public Paddle(Rectangle rect, GraphicsDevice graphic, float speed = 200f)
	{
		_speed = speed;
		_paddleRect = rect;
		_paddlePosition = new Vector2(rect.X, rect.Y);

		_screenDimension = new Vector2(graphic.Viewport.Width, graphic.Viewport.Height);
	}

	public void MovePaddle(int direction, double delta)  //-1 is up, +1 is down
	{
		_paddlePosition.Y += direction * (float) delta * _speed;
		clampPaddleToScreen();
	}

	public void DrawPaddle(ShapeBatch batch)
	{
		batch.DrawRectangle(new Vector2(_paddleRect.X, _paddleRect.Y), new Vector2(_paddleRect.Width, _paddleRect.Height), Color.White, Color.Transparent, 1);
	}

	private void updateRect()
	{
		_paddleRect.X = (int)_paddlePosition.X; _paddleRect.Y = (int)_paddlePosition.Y;
	}

	private void clampPaddleToScreen()
	{
		_paddlePosition.X = Math.Clamp(_paddlePosition.X, 0, (_screenDimension.X - _paddleRect.Width));
		_paddlePosition.Y = Math.Clamp(_paddlePosition.Y, 0, (_screenDimension.Y - _paddleRect.Height));
		updateRect();
	}
}