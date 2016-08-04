using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendUI.Controls.Drawing
{
	public class ColorStop
	{
		public ColorStop(double location, Color color)
		{
			Location = location;
			Color = color;
		}

		/// <summary>
		/// Color's location inside the gradient in percentages.
		/// </summary>
		public double Location { get; set; }

		/// <summary>
		/// Color at the specific point of the gradient.
		/// </summary>
		public Color Color { get; set; }
	}
}
