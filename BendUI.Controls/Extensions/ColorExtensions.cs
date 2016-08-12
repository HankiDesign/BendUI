using System.Collections.Generic;
using System.Drawing;

namespace BendUI.Controls.Extensions
{
	public static class ColorExtensions
	{
		public static Color LerpRGB(Color a, Color b, float t)
		{
			return Color.FromArgb(
				(int)(a.A + (b.A - a.A)*t),
				(int)(a.R + (b.R - a.R)*t),
				(int)(a.G + (b.G - a.G)*t),
				(int)(a.B + (b.B - a.B)*t)
				);
		}
	}
}