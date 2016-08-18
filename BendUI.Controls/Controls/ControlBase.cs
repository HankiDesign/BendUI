using BendUI.Controls.Drawing;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BendUI.Controls.Animation.Easing;

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
		private Timer _transitionTimer;
		private int _currentTransitionTime = 0; // Determines for how long the current transition animation has run
		private int _currentTransitionEndTime = 0; // How many milliseconds the transition runs to be completed
		private float _transitionPercentage = 0; // This is calculated for each transition update so that all the layers can use the value

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ControlState NextState { get; set; }

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ControlState PreviousState { get; set; }

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool TransitioningState { get; set; }

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsTransitioning { get; set; }

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

		public bool IsAnimated { get; set; }

		[Category("Animation")]
		[Description("Determines the easing mode to be used for state transition animation when the mouse cursor enters the control")]
		public EasingMode MouseEnterEasingMode { get; set; }

		[Category("Animation")]
		[Description("Determines the easing mode to be used for state transition animation when the mouse cursor leaves the control")]
		public EasingMode MouseLeaveEasingMode { get; set; }

		[Category("Animation")]
		[Description("Determines the easing mode to be used for state transition animation when the mouse cursor clicks down on the control")]
		public EasingMode MouseDownEasingMode { get; set; }

		[Category("Animation")]
		[Description("Determines the easing mode to be used for state transition animation when the mouse cursor's click is let go on the control")]
		public EasingMode MouseUpEasingMode { get; set; }

		[Category("Animation")]
		[Description("Determines the easing mode to be used for state transition animation when the control is disabled")]
		public int DisabledEasingMode { get; set; }

		[Category("Animation")]
		[Description("Determines the easing mode to be used for state transition animation when the control is returning to the default (unfocused and enabled) state")]
		public int DefaultEasingMode { get; set; }

		[Category("Animation")]
		[Description("Determines the easing function to be used for state transition animation when the mouse cursor enters the control")]
		public EasingFunction MouseEnterEasingFunction { get; set; }

		[Category("Animation")]
		[Description("Determines the easing function to be used for state transition animation when the mouse cursor leaves the control")]
		public EasingFunction MouseLeaveEasingFunction { get; set; }

		[Category("Animation")]
		[Description("Determines the easing function to be used for state transition animation when the mouse cursor clicks down on the control")]
		public EasingFunction MouseDownEasingFunction { get; set; }

		[Category("Animation")]
		[Description("Determines the easing function to be used for state transition animation when the mouse cursor's click is let go on the control")]
		public EasingFunction MouseUpEasingFunction { get; set; }

		[Category("Animation")]
		[Description("Determines the easing function to be used for state transition animation when the control is disabled")]
		public EasingFunction DisabledEasingFunction { get; set; }

		[Category("Animation")]
		[Description("Determines the easing function to be used for state transition animation when the control is returning to the default (unfocused and enabled) state")]
		public EasingFunction DefaultEasingFunction { get; set; }

		[Category("Animation")]
		[Description("Determines how many milliseconds the control is animated when the mouse cursor enters its bounds")]
		public int MouseEnterTransitionDuration { get; set; }

		[Category("Animation")]
		[Description("Determines how many milliseconds the control is animated when the mouse cursor leaves its bounds")]
		public int MouseLeaveTransitionDuration { get; set; }

		[Category("Animation")]
		[Description("Determines how many milliseconds the control is animated when the mouse cursor is pressed down")]
		public int MouseDownTransitionDuration { get; set; }

		[Category("Animation")]
		[Description("Determines how many milliseconds the control is animated when the mouse cursor is lifted up")]
		public int MouseUpTransitionDuration { get; set; }

		[Category("Animation")]
		[Description("Determines how many milliseconds the control is animated when it's disabled")]
		public int DisabledTransitionDuration { get; set; }

		[Category("Animation")]
		[Description("Determines how many milliseconds the control is animated when it's returning to the default (unfocused and enabled) state")]
		public int DefaultTransitionDuration { get; set; }

		protected ControlBase()
		{
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
				  ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);

			SetUpTransitionTools();

			_layers = new ObservableCollection<UILayer>();
			_layers.CollectionChanged += Layers_CollectionChanged;

			// Set defaults
			NextState = this.Enabled ? ControlState.Default : ControlState.Disabled;
		}

		protected void SetUpTransitionTools()
		{
			_transitionTimer = new Timer
			{
				Interval = 1
			};

			_transitionTimer.Tick += TransitionTimer_Tick;
		}

		void TransitionTimer_Tick(object sender, EventArgs e)
		{
			if (_currentTransitionTime < _currentTransitionEndTime)
			{
				_currentTransitionTime++;
				_transitionPercentage = (float)_currentTransitionTime/_currentTransitionEndTime;

				// Draw each layer separately 
				foreach (var layer in _layers)
				{
					layer.RefreshDrawingTools(_transitionPercentage);
				}

				// Force the control to redraw itself
				Invalidate();
			}

			else
			{
				_transitionTimer.Stop();
				IsTransitioning = false;
			}
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

			TransitionState(NextState, ControlState.MouseEntered);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			TransitionState(NextState, ControlState.MouseLeft);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			TransitionState(NextState, ControlState.MouseDown);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			TransitionState(NextState, ControlState.MouseUp);
		}

		protected void TransitionState(ControlState oldState, ControlState newState)
		{
			// Do nothing, if the state doesn't change
			if (oldState == newState) return;

			IsTransitioning = true;

			// New transition is starting, set the time to 0 and get the max time depending on newState
			_currentTransitionTime = 0;

			// Get the transition animation length depending on the state the control is transitioning to
			switch (newState)
			{
				case ControlState.Disabled:
					_currentTransitionEndTime = DisabledTransitionDuration;
					break;

				case ControlState.Default:
					_currentTransitionEndTime = DefaultTransitionDuration;
					break;

				case ControlState.MouseEntered:
					_currentTransitionEndTime = MouseEnterTransitionDuration;
					break;

				case ControlState.MouseDown:
					_currentTransitionEndTime = MouseDownTransitionDuration;
					break;

				case ControlState.MouseUp:
					_currentTransitionEndTime = MouseUpTransitionDuration;
					break;

				case ControlState.MouseLeft:
					_currentTransitionEndTime = MouseLeaveTransitionDuration;
					break;
			}

			foreach (var layer in _layers)
			{
				layer.StartTransition(oldState, newState);
			}

			_transitionTimer.Start();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			_transitionTimer.Tick -= TransitionTimer_Tick;
		}
	}

	public enum ControlState
	{
		Disabled,
		Default,
		MouseEntered,
		MouseDown,
		MouseUp,
		MouseLeft
	}
}