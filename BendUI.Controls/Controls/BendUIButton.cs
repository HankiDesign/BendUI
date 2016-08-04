using BendUI.Controls.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BendUI.Controls
{
	[ToolboxItem(true)]
    [DefaultEvent("Click")]
	[DefaultProperty("Text")]
    [Description("Clickable control that raises an event on user action.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class BendUIButton : ControlBase, IButtonControl
    {
		private DialogResult _dialogResult;

		public BendUIButton()
		{
			_dialogResult = DialogResult.None;
		}

		[Category("Behavior")]
		[Description("The value of the dialog box result for the form")]
		[DefaultValue(typeof(DialogResult), "None")]
		public DialogResult DialogResult
		{
			get
			{
				return _dialogResult;
			}

			set
			{
				if (Enum.IsDefined(typeof (DialogResult), value))
				{
					_dialogResult = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the text associated with this control. 
		/// </summary>
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public override string Text { get; set; }

		public void NotifyDefault(bool value)
		{
			//throw new NotImplementedException();
		}

		public void PerformClick()
		{
			//throw new NotImplementedException();
		}

		/// <summary>
		/// Raises the Click event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		/// <exception cref="InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		protected override void OnClick(EventArgs e)
		{
			// Retrieve the form that the control is on.
			var parent = FindForm();

			if (parent != null)
			{
				// Update the parent's DialogResult property
				parent.DialogResult = DialogResult;
			}
		}
    }
}