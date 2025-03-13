using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Utils
{
	public static Vector2 Rotate(this Vector2 vector, float angleRadians)
	{
		float cos = (float) Math.Cos(angleRadians);
		float sin = (float) Math.Sin(angleRadians);

		return new Vector2(
			vector.X * cos - vector.Y * sin, 
			vector.X * sin + vector.Y * cos);
	}

	public static Vector2 RotateDegrees(this Vector2 vector, float angleDegrees)
	{
		return Rotate(vector, MathHelper.ToRadians(angleDegrees));
	}
}
