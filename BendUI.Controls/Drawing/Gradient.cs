using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendUI.Controls.Drawing
{
	public class Gradient
	{
		public Gradient(double angle, params ColorStop[] colorStops)
		{
			Angle = angle;
			ColorStops = new List<ColorStop>();

			if (colorStops != null)
			{
				ColorStops.AddRange(colorStops);
			}
		}

		public Gradient(params ColorStop[] colorStops) : this(0, colorStops) { }
		public Gradient(double angle) : this(angle, null) { }
		public Gradient() : this(0, null) { }

		/// <summary>
		/// List of the colors and their location inside the gradient
		/// </summary>
		public List<ColorStop> ColorStops { get; set; }

		/// <summary>
		/// Angle of the gradient in degrees
		/// </summary>
		public double Angle { get; set; }
	}
}
