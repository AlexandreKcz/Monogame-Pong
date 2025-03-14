using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class AiPaddle : Paddle
{
	public AiPaddle(Rectangle rect, GraphicsDevice graphic, float speed = 200) : base(rect, graphic, speed)
	{
	}

	public void UpdateAIBehaviour(Vector2 ballPosition, double deltaTime)
	{
		if (ballPosition.Y > _paddlePosition.Y) MovePaddle(-1, deltaTime);
		else if(ballPosition.Y < (_paddlePosition.Y + _paddleRect.Height)) MovePaddle(+1, deltaTime);
	}
}