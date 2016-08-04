using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BendUI.Controls.Drawing
{
	public class BorderLayer : UILayer
	{
		private BorderColoring _coloring;

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
		public Color Color { get; set; }

		[Category("Appearance")]
		[Description("Corner radius in degrees")]
		public double CornerRadius { get; set; }

		[Category("Appearance")]
		[Description("Thickness of the border in pixels")]
		public double BorderThickness { get; set; }

		public override void Paint(Graphics graphics, SizeF size)
		{
			switch (_coloring)
			{
				case BorderColoring.Gradient:
					throw new NotImplementedException();
					break;

				case BorderColoring.Solid:
					throw new NotImplementedException();
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
	}

	public enum BorderColoring
	{
		Solid,
		Gradient
	}
}
