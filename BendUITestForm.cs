using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BendUI.Controls.Drawing;

namespace BendUI
{
	public partial class BendUITestForm : Form
	{
		public BendUITestForm()
		{
			InitializeComponent();

			bendUIButton1.Layers.AddRange(
				new[]
				{
					new BorderLayer
					{
						Color = Color.Blue,
						Coloring = BorderColoring.Solid,
						CornerRadius = 10,
						Distance = 0,
						BorderThickness = 10
					},

					new BorderLayer
					{
						Color = Color.Red,
						Coloring = BorderColoring.Solid,
						CornerRadius = 10,
						Distance = 20,
						BorderThickness = 10
					},

					new BorderLayer
					{
						Color = Color.Green,
						Coloring = BorderColoring.Solid,
						CornerRadius = 10,
						Distance = 40,
						BorderThickness = 10
					},
				});
		}
	}
}
