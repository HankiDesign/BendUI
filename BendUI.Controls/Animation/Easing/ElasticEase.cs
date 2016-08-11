//------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation, 2008
//
//  File: ElasticEase.cs
//------------------------------------------------------------------------------

using System;
using BendUI.Controls.Extensions;

namespace BendUI.Controls.Animation.Easing
{
	/// <summary>
	///     This class implements an easing function that gives an elastic/springy curve
	/// </summary>
	public class ElasticEase : EasingFunctionBase
	{
		/// <summary>
		/// Specifies the number of oscillations
		/// </summary>
		public int Oscillations { get; set; }

		/// <summary>
		/// Specifies the amount of springiness
		/// </summary>
		public double Springiness { get; set; }

		protected override double EaseInCore(double normalizedTime)
		{
			var oscillations = Math.Max(0.0, Oscillations);
			var springiness = Math.Max(0.0, Springiness);
			double expo;
			
			if (springiness.IsZero())
			{
				expo = normalizedTime;
			}

			else
			{
				expo = (Math.Exp(springiness * normalizedTime) - 1.0) / (Math.Exp(springiness) - 1.0);
			}

			return expo * (Math.Sin((Math.PI * 2.0 * oscillations + Math.PI * 0.5) * normalizedTime));
		}
	}
}
