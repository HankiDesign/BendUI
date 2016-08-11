//------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation, 2008
//
//  File: QuadraticEase.cs
//------------------------------------------------------------------------------

namespace BendUI.Controls.Animation.Easing
{
	/// <summary>
	///     This class implements an easing function that gives a quadratic curve toward the destination
	/// </summary>
	public class QuadraticEase : EasingFunctionBase
	{
		protected override double EaseInCore(double normalizedTime)
		{
			return normalizedTime * normalizedTime;
		}
	}
}
