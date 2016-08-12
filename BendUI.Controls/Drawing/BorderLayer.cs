using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BendUI.Controls.Controls;
using BendUI.Controls.Extensions;
using Plasmoid.Extensions;

namespace BendUI.Controls.Drawing
{
	public class BorderLayer : UILayer
	{
		private BorderColoring _coloring;
		private Pen _borderPen;
		private Color _previousColor;
		private Color _color;
		private Color _defaultColor;
		private Color _disabledColor;
		private Color _hoverColor;
		private Color _downColor;
		private float _borderThickness;
		private ControlState _previousState;
		private ControlState _nextState;
		private Color _nextColor;

		public BorderLayer(ControlBase parent) : base(UILayerType.Border, parent)
		{
			
		}

		public BorderLayer()
			: base(UILayerType.Border)
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
		public Color DefaultColor
		{
			get { return _defaultColor; }
			set
			{
				_defaultColor = value;
				RefreshDrawingTools(0);
			}
		}

		[Category("Appearance")]
		[Description("Solid color of the disabled border")]
		public Color DisabledColor
		{
			get { return _disabledColor; }
			set
			{
				_disabledColor = value;
				RefreshDrawingTools(0);
			}
		}

		[Category("Appearance")]
		[Description("Solid color of the border being hovered by the mouse cursor")]
		public Color HoverColor
		{
			get { return _hoverColor; }
			set
			{
				_hoverColor = value;
				RefreshDrawingTools(0);
			}
		}

		[Category("Appearance")]
		[Description("Solid color of the border when the mouse cursor is down")]
		public Color DownColor
		{
			get { return _downColor; }
			set
			{
				_downColor = value;
				RefreshDrawingTools(0);
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
				RefreshDrawingTools(0);
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

		// When animating transitions, drawing tools will be refreshed every millisecond
		public override void RefreshDrawingTools(int currentPercentage)
		{
			if (currentPercentage == 0)
			{
				_color = DefaultColor;
				_borderPen = new Pen(_color, BorderThickness);
			}

			// If the transition is going on, the colors have most likely changed and the Pen needs to be recreated.
			if (!Parent.IsAnimated || !Parent.IsTransitioning) return;

			_color = ColorExtensions.LerpRGB(_previousColor, _nextColor, currentPercentage);
			_borderPen = new Pen(_color, BorderThickness);
		}

		public override void StartTransition(ControlState start, ControlState end)
		{
			_previousState = start;
			_nextState = end;
			_previousColor = _color;

			switch (_nextState)
			{
				case ControlState.Disabled:
					_nextColor = DisabledColor;
					break;

				case ControlState.Default:
					_nextColor = DefaultColor;
					break;

				case ControlState.MouseEntered:
					_nextColor = HoverColor;
					break;

				case ControlState.MouseDown:
					_nextColor = DownColor;
					break;

				case ControlState.MouseUp:
					_nextColor = HoverColor;
					break;

				case ControlState.MouseLeft:
					_nextColor = DefaultColor;
					break;
			}
		}
	}

	public enum BorderColoring
	{
		Solid,
		Gradient
	}
}
