//------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation, 2008
//
//  File: QuarticEase.cs
//------------------------------------------------------------------------------

namespace BendUI.Controls.Animation.Easing
{
	/// <summary>
	///     This class implements an easing function that gives a quartic curve toward the destination
	/// </summary>
	public class QuarticEase : EasingFunctionBase
	{
		protected override double EaseInCore(double normalizedTime)
		{
			return normalizedTime * normalizedTime * normalizedTime * normalizedTime;
		}
	}
}
