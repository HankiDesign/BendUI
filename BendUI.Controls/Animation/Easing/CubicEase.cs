//------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation, 2008
//
//  File: CubicEase.cs
//------------------------------------------------------------------------------

namespace BendUI.Controls.Animation.Easing
{
	/// <summary>
	///     This class implements an easing function that gives a cubic curve toward the destination
	/// </summary>
	public class CubicEase : EasingFunctionBase
	{
		protected override double EaseInCore(double normalizedTime)
		{
			return normalizedTime * normalizedTime * normalizedTime;
		}
	}
}
