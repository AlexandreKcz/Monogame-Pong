using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class AiPaddle : Paddle
{
	private float _initialSpeed = 0f;

	public AiPaddle(Rectangle rect, GraphicsDevice graphic, float speed = 200) : base(rect, graphic, speed)
	{
		_initialSpeed = speed;
	}

	public void UpdateAIBehaviour(Vector2 ballPosition, int ballRadius, double deltaTime)
	{
		if (GameManager.Instance.Timer) return;

		//Debug.WriteLine(string.Format("ballPos : {0}, paddlePos : {1}, ballRadius : {2}, paddleRectHeight {3}", ballPosition, _paddlePosition, ballRadius, _paddleRect.Height));

		//Debug.WriteLine(ballPosition.Y + (ballRadius / 2) < _paddlePosition.Y || ballPosition.Y + (ballRadius / 2) > (_paddlePosition.Y + _paddleRect.Height) ? "in paddle" : "outside paddle");

		//int rndmMove = new Random().Next(0, 2);

		if (ballPosition.Y + (ballRadius / 2) < _paddlePosition.Y) MovePaddle(-1, deltaTime);
		else if(ballPosition.Y + (ballRadius / 2) > (_paddlePosition.Y + _paddleRect.Height)) MovePaddle(+1, deltaTime);
	}

	public override void BallBounceCallback()
	{
		//base.BallBounceCallback();
		_speed = _initialSpeed * (new Random().Next(4, 14) * .1f);
		Debug.WriteLine(_speed);
	}
}