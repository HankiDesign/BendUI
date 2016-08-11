//------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation, 2008
//
//  File: QuinticEase.cs
//------------------------------------------------------------------------------

namespace BendUI.Controls.Animation.Easing
{
	/// <summary>
	///     This class implements an easing function that gives a quintic curve toward the destination
	/// </summary>
	public class QuinticEase : EasingFunctionBase
	{
		protected override double EaseInCore(double normalizedTime)
		{
			return normalizedTime * normalizedTime * normalizedTime * normalizedTime * normalizedTime;
		}
	}
}
