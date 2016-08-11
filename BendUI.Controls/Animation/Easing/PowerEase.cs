//------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation, 2008
//
//  File: PowerEase.cs
//------------------------------------------------------------------------------

using System;

namespace BendUI.Controls.Animation.Easing
{
	/// <summary>
	///     This class implements an easing function that gives a polynomial curve of arbitrary degree.
	///     If the curve you desire is cubic, quadratic, quartic, or quintic it is better to use the 
	///     specialized easing functions.
	/// </summary>
	public class PowerEase : EasingFunctionBase
	{
		/// <summary>
		/// Specifies the power for the polynomial equation.
		/// </summary>
		public double Power { get; set; }

		protected override double EaseInCore(double normalizedTime)
		{
			var power = Math.Max(0.0, Power);
			return Math.Pow(normalizedTime, power);
		}
	}
}