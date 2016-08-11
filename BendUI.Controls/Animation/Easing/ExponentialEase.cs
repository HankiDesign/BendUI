//------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation, 2008
//
//  File: ExponentialEase.cs
//------------------------------------------------------------------------------


using System;
using BendUI.Controls.Extensions;

namespace BendUI.Controls.Animation.Easing
{
	/// <summary>
	///     This class implements an easing function that gives an exponential curve
	/// </summary>
	public class ExponentialEase : EasingFunctionBase
	{
		/// <summary>
		/// Specifies the factor which controls the shape of easing.
		/// </summary>
		public double Exponent { get; set; }

		protected override double EaseInCore(double normalizedTime)
		{
			var factor = Exponent;
			
			if (factor.IsZero())
			{
				return normalizedTime;
			}

				return (Math.Exp(factor * normalizedTime) - 1.0) / (Math.Exp(factor) - 1.0);
		}
	}
}
