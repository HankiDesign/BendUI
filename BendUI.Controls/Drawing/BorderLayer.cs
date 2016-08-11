using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plasmoid.Extensions;

namespace BendUI.Controls.Drawing
{
	public class BorderLayer : UILayer
	{
		private BorderColoring _coloring;
		private Pen _borderPen;
		private Color _color;
		private float _borderThickness;

		public BorderLayer() : base(UILayerType.Border)
		{
			
		}

		[Category("Appearance")]
		[Description("Defines how the border is painted")]
		[DefaultValue(typeof(BorderColoring), "Solid")]
		public BorderColoring Coloring
		{
			get { return _coloring; }
			set
			{
				if (Enum.IsDefined(typeof(BorderColoring), value))
				{
					_coloring = value;
				}
			}
		}

		[Category("Appearance")]
		[Description("Border gradient with color stops and an angle")]
		public Gradient Gradient { get; set; }

		[Category("Appearance")]
		[Description("Solid color of the border")]
		public Color Color
		{
			get { return _color; }
			set
			{
				_color = value;
				CreateTools();
			}
		}

		[Category("Appearance")]
		[Description("Corner radius in degrees")]
		public int CornerRadius { get; set; }

		[Category("Appearance")]
		[Description("Thickness of the border in pixels")]
		public float BorderThickness 
		{
			get { return _borderThickness; }

			set
			{
				_borderThickness = value;
				CreateTools();
			}
		}

		[Category("Appearance")]
		[Description("Determines which corners, if any, of the rectangle are rounded")]
		public RoundedCorners RoundedCorners { get; set; }

		[Category("Distance")]
		[Description("Distance of the border from the bounding rectangle of the parent control")]
		public int Distance { get; set; }

		public override void Paint(Graphics graphics, Rectangle bounds)
		{
			switch (_coloring)
			{
				case BorderColoring.Gradient:
					throw new NotImplementedException();
					break;

				case BorderColoring.Solid:
					graphics.DrawRoundedRectangle(_borderPen, Rectangle.Inflate(bounds, -Distance, -Distance), CornerRadius, RoundedCorners);
					break;
			}
		}

		public override void Resize()
		{
			throw new NotImplementedException();
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}

		public override void CreateTools()
		{
			_borderPen = new Pen(Color, BorderThickness);
		}
	}

	public enum BorderColoring
	{
		Solid,
		Gradient
	}
}
