using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

class GameManager
{
	public static GameManager Instance { get { return _instance; } }
	private static GameManager _instance;

	public (int p1, int p2) Score { get {  return _score; } }
	private (int p1, int p2) _score;

	private float _waitTime;
	private float _currentWaitTime = 0;
	public bool Timer { get {  return _timer; } }
	private bool _timer = false;

	public SoundEffect GoalSFX;
	public SoundEffect SetSFX;

	private Vector2 _screenDimension;

	public GameManager(GraphicsDevice graphics)
	{
		if(_instance == null) _instance = this;
		_score = (p1: 0, p2:0);

		_waitTime = 1.5f;
		_currentWaitTime = 0;

		_screenDimension = new Vector2(graphics.Viewport.Width, graphics.Viewport.Height);
	}

	public void UpdateScore(int scoreValue, PlayersEnum player)
	{
		if (player == PlayersEnum.Player1) _score.p1 += scoreValue;
		else if (player == PlayersEnum.Player2) _score.p2 += scoreValue;
		Debug.WriteLine(string.Format("Score : p1 {0} | p2 {1}", _score.p1, _score.p2));

		GoalSFX.Play();
	}

	public void StartTimer()
	{
		_timer = true;
		_currentWaitTime = _waitTime;
	}

	public void ResetGame(Ball ball)
	{
		ball.Position = new Vector2(_screenDimension.X / 2, _screenDimension.Y / 2);

		Vector2 vel = Vector2.Zero;

		if (_score.p1 > _score.p2) vel.X = 1;
		else if (_score.p1 < _score.p2) vel.X = -1;
		else
		{
			int randomVelX = new Random().Next(-1, 2);
			while(randomVelX == 0) randomVelX = new Random().Next(-1, 2);
			vel.X = randomVelX;
		}

		int randomVelY = new Random().Next(-1, 2);
		while (randomVelY == 0) randomVelY = new Random().Next(-1, 2);
		vel.Y = randomVelY;
		vel.Normalize();

		ball.Direction = vel;

		SetSFX.Play();
	}

	public void UpdateTimer(double deltaTime, Ball ball)
	{
		if (!_timer) return;

		if (_currentWaitTime <= _waitTime && _currentWaitTime > 0)
		{
			_currentWaitTime -= (float)deltaTime;
		}
		
		if (_currentWaitTime <= 0)
		{
			_currentWaitTime = 0;
			ResetGame(ball);
			_timer = false;
		}

		//Debug.WriteLine(string.Format("Current timer : {0}", _currentWaitTime));
	}
}