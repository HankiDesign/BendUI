using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BendUI.Controls.Drawing
{
	/// <summary>
	/// Represents a UI layer that can be drawn on a control. Layers represent
	/// different parts of controls and each control supports multiple layers of
	/// the same type.
	/// </summary>
	public abstract class UILayer : IDrawable, IDisposable
	{
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

		public abstract void Paint(Graphics graphics, SizeF size);
		public abstract void Resize();
		public abstract void Dispose();

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
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
