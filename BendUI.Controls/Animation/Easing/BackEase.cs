//------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation, 2008
//
//  File: BackEase.cs
//------------------------------------------------------------------------------

using System;

namespace BendUI.Controls.Animation.Easing
{
	/// <summary>
	///     This class implements an easing function that backs up before going to the destination.
	/// </summary>
	public class BackEase : EasingFunctionBase
	{
		/// <summary>
		/// Specifies how much the function will pull back
		/// </summary>
		public double Amplitude { get; set; }

		protected override double EaseInCore(double normalizedTime)
		{
			var amp = Math.Max(0.0, Amplitude);
			return Math.Pow(normalizedTime, 3.0) - normalizedTime * amp * Math.Sin(Math.PI * normalizedTime);
		}
	}
}
