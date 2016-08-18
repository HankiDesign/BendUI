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
				Parent = bendUIButton1,
				DefaultColor = Color.Blue,
				HoverColor = Color.LightSkyBlue,
				DownColor = Color.Navy,
				Coloring = BorderColoring.Solid,
				CornerRadius = 10,
				Distance = 0,
				BorderThickness = 20,
				ZIndex = 0,
				
			});

			bendUIButton1.Layers.Add(new BorderLayer
			{
				Parent = bendUIButton1,
				DefaultColor = Color.Red,
				HoverColor = Color.LightCoral,
				DownColor = Color.Crimson,
				Coloring = BorderColoring.Solid,
				CornerRadius = 40,
				Distance = 90,
				BorderThickness = 10,
				ZIndex = 1,
				RoundedCorners = RoundedCorners.BottomLeft | RoundedCorners.TopRight
			});

			bendUIButton1.Layers.Add(new BorderLayer
			{
				Parent = bendUIButton1,
				DefaultColor = Color.OliveDrab,
				HoverColor = Color.LightGreen,
				DownColor = Color.DarkGreen,
				Coloring = BorderColoring.Solid,
				CornerRadius = 100,
				Distance = 5,
				BorderThickness = 100,
				ZIndex = -5,
				RoundedCorners = RoundedCorners.BottomRight | RoundedCorners.TopLeft
			});
		}

		private void BendUITestForm_Load(object sender, EventArgs e)
		{
			
		}
	}
}
