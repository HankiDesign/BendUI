using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BendUI.Controls.Drawing;

namespace BendUI.Controls.Controls
{
	/// <summary>
	/// ControlBase is the base class for all controls in the library. It derives
	/// from System.Windows.Forms.Control class that provides it with all the
	/// necessary tools to handel the lifecycle of a single UI control.
	/// </summary>
	public abstract class ControlBase : Control
	{
		private MethodInfo _transparentBGMethod;

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private ObservableCollection<UILayer> _layers;

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObservableCollection<UILayer> Layers
		{
			get { return _layers; }

			set 
			{ 
				_layers = value;
				_layers.CollectionChanged += Layers_CollectionChanged;
			}
		}

		[Category("Appearance")]
		[Description("Determines how many milliseconds the control is animated when the mouse cursor enters its bounds")]
		public int MouseEnterTransitionDuration { get; set; }

		[Category("Appearance")]
		[Description("Determines how many milliseconds the control is animated when the mouse cursor leaves its bounds")]
		public int MouseLeaveTransitionDuration { get; set; }

		[Category("Appearance")]
		[Description("Determines how many milliseconds the control is animated when the mouse cursor is pressed down")]
		public int MouseDownTransitionDuration { get; set; }

		[Category("Appearance")]
		[Description("Determines how many milliseconds the control is animated when the mouse cursor is lifted up")]
		public int MouseUpTransitionDuration { get; set; }

		protected ControlBase()
		{
			this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
				  ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);

			_layers = new ObservableCollection<UILayer>();
			_layers.CollectionChanged += Layers_CollectionChanged;
		}

		protected void OrderLayers()
		{
			_layers = new ObservableCollection<UILayer>(_layers.OrderBy(x => x.ZIndex));
			_layers.CollectionChanged += Layers_CollectionChanged;
		}

		private void Layers_CollectionChanged(object sender, EventArgs e)
		{
			OrderLayers();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (IsDisposed || Disposing) return;

			PaintTransparentBackground(e);

			// Draw each layer separately 
			foreach (var layer in _layers)
			{
				layer.Paint(e.Graphics, new Rectangle(0, 0, Size.Width, Size.Height));
			}
		}

		/// <summary>
		/// System.Windows.Forms.Control class providers an internal implementation of painting a transparent
		/// background for a control. This method uses reflection to grab the reference to the method and calls
		/// it if necessary.
		/// </summary>
		/// <param name="e"></param>
		private void PaintTransparentBackground(PaintEventArgs e)
		{
			if (Parent != null)
			{
				if (_transparentBGMethod == null)
				{
					// Grab the reference to the internal painting method from Control class
					_transparentBGMethod = typeof (Control).GetMethod("PaintTransparentBackground",
						BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
						null, CallingConventions.HasThis,
						new Type[] {typeof (PaintEventArgs), typeof (Rectangle), typeof (Region)},
						null);
				}

				_transparentBGMethod.Invoke(this, new object[] {e, ClientRectangle, null});
			}
			
			else
			{
				PaintBackground(e.Graphics, SystemBrushes.Control, ClientRectangle);
			}
		}

		/// <summary>
		/// Fill the whole area of the control with a brush
		/// </summary>
		/// <param name="graphics"></param>
		/// <param name="backgroundBrush"></param>
		/// <param name="backgroundRectangle"></param>
		protected virtual void PaintBackground(Graphics graphics, Brush backgroundBrush, Rectangle backgroundRectangle)
		{
			graphics.FillRectangle(backgroundBrush, backgroundRectangle);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);


		}
	}
}