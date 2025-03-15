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

		/*
		_direction = new Vector2(1, 0);
		_direction.Normalize();
		*/
		_direction = GameManager.Instance.GetRandomVel();
		//_direction = _direction.RotateDegrees(45);

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
			updateRect();
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

	public void CheckCollisionWithPaddle(Paddle paddle, int dir, double delta) //dir < 0 joueur gauche (joueur), dir > 0 joueur droit (ai)
	{
		//Debug.WriteLine(string.Format("Ball top : {0} Ball Bottom : {1} | Paddle Top : {2} Paddle Bottom : {3}", _ballRect.Top, _ballRect.Bottom, paddle.PaddleRect.Top,paddle.PaddleRect.Bottom));

		if (paddle.PaddleRect.Intersects(_ballRect))
		{
			//bool isSideColliding = dir < 0 ? _ballRect.Right < (paddle.PaddleRect.Left + paddle.PaddleRect.Width / 2) : _ballRect.Left > (paddle.PaddleRect.Right - paddle.PaddleRect.Width / 2);

			bool topCollision = _ballRect.Bottom <= (paddle.PaddleRect.Top + 5);
			bool bottomCollison = _ballRect.Top >= (paddle.PaddleRect.Bottom - 5);

			if (topCollision || bottomCollison) //collision trop à gauche
			{
				/* Side Bounce */
				Debug.WriteLine(topCollision ? "top col" : "bottom col");
				_direction.Y   = topCollision ? -1 : 1;
				_position.Y += topCollision ? _ballRect.Bottom - paddle.PaddleRect.Top  - 5 - paddle.Speed * (float) delta: _ballRect.Top - paddle.PaddleRect.Bottom + 5 + paddle.Speed * (float) delta;
				updateRect();
				BounceSFX.Play();
			} else
			{

				bool behindPaddle = dir < 0 ? _ballRect.Right < (paddle.PaddleRect.Left + paddle.PaddleRect.Width / 2) : _ballRect.Left > (paddle.PaddleRect.Right - paddle.PaddleRect.Width / 2);
				if (behindPaddle) return;

				/* Face Bounce */
				Debug.WriteLine("side collision");

				if (dir < 0) GameManager.Instance.AiPaddle.BallBounceCallback();
				else if (dir > 0) paddle.BallBounceCallback();
				_direction.X *= -1;
				_position.X += dir < 0 ? paddle.PaddleRect.Right - _ballRect.Left : paddle.PaddleRect.Left - _ballRect.Right;
				updateRect();
				BounceSFX.Play();
			}

		}
	}

	public void DrawBall(ShapeBatch batch)
	{
		batch.DrawRectangle(new Vector2(_ballRect.X, _ballRect.Y), new Vector2(_ballRect.Width, _ballRect.Height), Color.White, Color.Transparent, 1);
	}
}