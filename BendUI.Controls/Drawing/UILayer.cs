using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BendUI.Controls.Controls;

namespace BendUI.Controls.Drawing
{
	/// <summary>
	/// Represents a UI layer that can be drawn on a control. Layers represent
	/// different parts of controls and each control supports multiple layers of
	/// the same type.
	/// </summary>
	public abstract class UILayer : IDrawable, IDisposable
	{
		private ControlBase _parent;

		public ControlBase Parent
		{
			get { return this._parent; }
			set
			{
				this._parent = value;
			}
		}

		protected UILayer(UILayerType layerType, ControlBase parent)
		{
			LayerType = layerType;
			Parent = parent;
		}

		protected UILayer(UILayerType layerType)
		{
			LayerType = layerType;
		}

		public UILayerType LayerType { get; set; }

		/// <summary>
		/// Determines the position of the layer within the stack of layers. Layers
		/// with bigger z values are on top.
		/// </summary>
		public int ZIndex { get; set; }

		public abstract void Paint(Graphics graphics, Rectangle size);
		public abstract void Resize();
		public abstract void Dispose();
		public abstract void StartTransition(ControlState start, ControlState end);
		public abstract void RefreshDrawingTools(int currentPercentage);
	}

	public enum UILayerType
	{
		Background,
		Border,
		Foreground,
		Text,
		Bitmap,
		Animation
	}
}
