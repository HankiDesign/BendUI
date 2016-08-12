using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BendUI.Controls.Controls;

namespace BendUI.Controls.Drawing
{
	public interface IDrawable
	{
		void Paint(Graphics graphics, Rectangle size);
		void Resize();
		void StartTransition(ControlState start, ControlState end);
	}
}
