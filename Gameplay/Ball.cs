using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

class Ball
{
	private Rectangle _ballRect;
	public int Radius { get { return _radius; } }
	private int _radius;
	public Vector2 Direction { get { return _direction; } set { _direction = value; } }
	private Vector2 _direction;
	public Vector2 Position { get { return _position; }
		set { 
			_position = value;
			updateRect();
		} 
	}
	private Vector2 _position;
	private float _speed = 2000f;

	private Vector2 _screenDimension;
	private Rectangle _screenBounds;

	private float _timeToWait = 30f;
	private float _currentTimeToWait = 30f;

	public SoundEffect BounceSFX;

	public Ball(Vector2 position, int radius, float speed, GraphicsDevice graphic)
	{
		_ballRect = new Rectangle((int)position.X, (int)position.Y, radius, radius);
		_position = new Vector2(_ballRect.X, _ballRect.Y);
		_radius = radius;

		_speed = speed;

		_direction = new Vector2(1, 0);
		_direction.Normalize();
		_direction = _direction.RotateDegrees(45);

		_timeToWait = .15f;
		_currentTimeToWait = 0;

		_screenDimension = new Vector2(graphic.Viewport.Width, graphic.Viewport.Height);
		_screenBounds = new Rectangle(radius, radius, ((int) _screenDimension.X - radius), ((int)_screenDimension.Y - radius));
	}

	public void UpdateBall(double delta)
	{
		_position += _direction * (float) delta * _speed;
		updateRect();
		CheckForCollision(delta);
		updateRect();
	}

	private void updateRect()
	{
		_ballRect.X = (int)_position.X; _ballRect.Y = (int)_position.Y;
	}

	public void CheckForCollision(double delta)
	{
		if (GameManager.Instance.Timer) return;
		/*
		if (_position.X + _radius > _screenDimension.X ||
			_position.X < 0 ||
			_position.Y + _radius > _screenDimension.Y ||
			_position.Y < 0) {
			_direction = _direction.RotateDegrees(90);
		}*/

		//if (_position.X + _radius > _screenDimension.X || _position.X < 0) _direction.X *= -1;

		if (_position.X + _radius > _screenDimension.X || _position.X < 0)
		{
			PlayersEnum side = _position.X < 0 ? PlayersEnum.Player2 : PlayersEnum.Player1;
			GameManager.Instance.UpdateScore(1, side);
			GameManager.Instance.StartTimer();
			
			//GameManager.Instance.ResetGame(this);
		}

		if (_position.Y + _radius > _screenDimension.Y || _position.Y < 0) { 
			_direction.Y *= -1;
			BounceSFX.Play();
		}

		/*
		bool inZone = _ballRect.Intersects(_screenBounds);
		if (!inZone && _currentTimeToWait == 0)
		{
			Debug.WriteLine("pong");
			_direction = _direction.RotateDegrees(45);
			_direction.Normalize();
			_currentTimeToWait += (float)delta;
		} else if (_currentTimeToWait > 0)
		{
			Debug.WriteLine("pas pong");
			_currentTimeToWait += (float) delta;
			if (_currentTimeToWait > _timeToWait) _currentTimeToWait = 0;
		}
		*/
	}

	public void CheckCollisionWithPaddle(Paddle paddle, int dir)
	{
		if (paddle.PaddleRect.Intersects(_ballRect))
		{
			_direction.X *= -1;
			_position.X += dir < 0 ? paddle.PaddleRect.Right - _ballRect.Left : paddle.PaddleRect.Left - _ballRect.Right;
			updateRect();
			BounceSFX.Play();
		}
	}

	public void DrawBall(ShapeBatch batch)
	{
		batch.DrawRectangle(new Vector2(_ballRect.X, _ballRect.Y), new Vector2(_ballRect.Width, _ballRect.Height), Color.White, Color.Transparent, 1);
	}
}