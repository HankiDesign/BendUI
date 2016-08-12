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
using Plasmoid.Extensions;

namespace BendUI
{
	public partial class BendUITestForm : Form
	{
		public BendUITestForm()
		{
			InitializeComponent();

			bendUIButton1.Layers.Add(new BorderLayer
			{
				DefaultColor = Color.Blue,
				HoverColor = Color.Yellow,
				Coloring = BorderColoring.Solid,
				CornerRadius = 10,
				Distance = 0,
				BorderThickness = 20,
				ZIndex = 0,
				Parent = bendUIButton1
			});

			bendUIButton1.Layers.Add(new BorderLayer
			{
				DefaultColor = Color.Red,
				HoverColor = Color.Yellow,
				Coloring = BorderColoring.Solid,
				CornerRadius = 40,
				Distance = 90,
				BorderThickness = 10,
				ZIndex = 1,
				RoundedCorners = RoundedCorners.BottomLeft | RoundedCorners.TopRight,
				Parent = bendUIButton1
			});

			bendUIButton1.Layers.Add(new BorderLayer
			{
				DefaultColor = Color.Green,
				HoverColor = Color.Yellow,
				Coloring = BorderColoring.Solid,
				CornerRadius = 100,
				Distance = 5,
				BorderThickness = 100,
				ZIndex = -5,
				RoundedCorners = RoundedCorners.BottomRight | RoundedCorners.TopLeft,
				Parent = bendUIButton1
			});
		}

		private void BendUITestForm_Load(object sender, EventArgs e)
		{
			
		}
	}
}
