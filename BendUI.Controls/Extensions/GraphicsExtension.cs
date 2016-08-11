/* Font related stuff was removed from this class to ensure portability to other platforms (it was using p/invoke to Windows dll's).
 * The license is also attached here as a comment to conform to the original MS-PL license chosen for this code.
 *
 * Microsoft Public License (MS-PL)
 * This license governs use of the accompanying software. If you use the software, you
 * accept this license. If you do not accept the license, do not use the software.
 * 
 * 1. Definitions
 * The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
 * same meaning here as under U.S. copyright law.
 * A "contribution" is the original software, or any additions or changes to the software.
 * A "contributor" is any person that distributes its contribution under this license.
 * "Licensed patents" are a contributor's patent claims that read directly on its contribution.
 * 
 * 2. Grant of Rights
 * (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
 * (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
 * 
 * 3. Conditions and Limitations
 * (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
 * (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
 * (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
 * (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
 * (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
 */

/* GraphicsExtension - [Extended Graphics]
 * Author name:           Arun Reginald Zaheeruddin
 * Current version:       1.0.0.4 (12b)
 * Release documentation: http://www.codeproject.com
 * License information:   Microsoft Public License (Ms-PL) [http://www.opensource.org/licenses/ms-pl.html]
 * 
 * Enhancements and history
 * ------------------------
 * 1.0.0.1 (20 Jul 2009): Initial release with modified code from previous CodeProject article.
 * 1.0.0.2 (25 Jul 2009): Added functionality that allows selected corners on a rectangle to be rounded.
 *                        Modified code to adapt to an anti-aliased output while drawing and filling rounded rectangles.
 * 1.0.0.3 (26 Jul 2009): Added DrawRoundedRectangle and FillRoundedRectangle methods that take a Rectangle and RectangleF object.
 * 1.0.0.4 (27 Jul 2009): Added font metrics and measuring utility that measures a font's height, leading, ascent, etc.
 * 
 * Issues addressed
 * ----------------
 * 1. Rounded rectangles - rounding edges of a rectangle.
 * 2. Font Metrics - Measuring a font's height, leading, ascent, etc.
 */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Plasmoid.Extensions
{
	static class GraphicsExtension
	{
		private static GraphicsPath GenerateRoundedRectangle(
			this Graphics graphics,
			RectangleF rectangle,
			float radius,
			RoundedCorners filter)
		{
			var path = new GraphicsPath();
			
			if (radius <= 0.0F || filter == RoundedCorners.None)
			{
				path.AddRectangle(rectangle);
				path.CloseFigure();
				return path;
			}

			if (radius >= (Math.Min(rectangle.Width, rectangle.Height))/2.0)
				return graphics.GenerateCapsule(rectangle);

			var diameter = radius * 2.0F;
			var sizeF = new SizeF(diameter, diameter);
			var arc = new RectangleF(rectangle.Location, sizeF);
			
			if ((RoundedCorners.TopLeft & filter) == RoundedCorners.TopLeft)
				path.AddArc(arc, 180, 90);
			
			else
			{
				path.AddLine(arc.X, arc.Y + arc.Height, arc.X, arc.Y);
				path.AddLine(arc.X, arc.Y, arc.X + arc.Width, arc.Y);
			}
			
			arc.X = rectangle.Right - diameter;
			
			if ((RoundedCorners.TopRight & filter) == RoundedCorners.TopRight)
				path.AddArc(arc, 270, 90);
			
			else
			{
				path.AddLine(arc.X, arc.Y, arc.X + arc.Width, arc.Y);
				path.AddLine(arc.X + arc.Width, arc.Y + arc.Height, arc.X + arc.Width, arc.Y);
			}
			
			arc.Y = rectangle.Bottom - diameter;
			
			if ((RoundedCorners.BottomRight & filter) == RoundedCorners.BottomRight)
				path.AddArc(arc, 0, 90);
			
			else
			{
				path.AddLine(arc.X + arc.Width, arc.Y, arc.X + arc.Width, arc.Y + arc.Height);
				path.AddLine(arc.X, arc.Y + arc.Height, arc.X + arc.Width, arc.Y + arc.Height);
			}

			arc.X = rectangle.Left;

			if ((RoundedCorners.BottomLeft & filter) == RoundedCorners.BottomLeft)
				path.AddArc(arc, 90, 90);

			else
			{
				path.AddLine(arc.X + arc.Width, arc.Y + arc.Height, arc.X, arc.Y + arc.Height);
				path.AddLine(arc.X, arc.Y + arc.Height, arc.X, arc.Y);
			}

			path.CloseFigure();
			return path;
		}

		private static GraphicsPath GenerateCapsule(
				this Graphics graphics,
				RectangleF rectangle)
		{
			var path = new GraphicsPath();
			try
			{
				if (rectangle.Width > rectangle.Height)
				{
					var diameter = rectangle.Height;
					var sizeF = new SizeF(diameter, diameter);
					var arc = new RectangleF(rectangle.Location, sizeF);
					path.AddArc(arc, 90, 180);
					arc.X = rectangle.Right - diameter;
					path.AddArc(arc, 270, 180);
				}
				else if (rectangle.Width < rectangle.Height)
				{
					var diameter = rectangle.Width;
					var sizeF = new SizeF(diameter, diameter);
					var arc = new RectangleF(rectangle.Location, sizeF);
					path.AddArc(arc, 180, 180);
					arc.Y = rectangle.Bottom - diameter;
					path.AddArc(arc, 0, 180);
				}
				else path.AddEllipse(rectangle);
			}
			catch { path.AddEllipse(rectangle); }
			finally { path.CloseFigure(); }
			return path;
		}

		public static void DrawRoundedRectangle(
				this Graphics graphics,
				Pen pen,
				float x,
				float y,
				float width,
				float height,
				float radius,
				RoundedCorners filter)
		{
			var rectangle = new RectangleF(x, y, width, height);
			var path = graphics.GenerateRoundedRectangle(rectangle, radius, filter);
			var old = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.DrawPath(pen, path);
			graphics.SmoothingMode = old;
		}

		public static void DrawRoundedRectangle(
				this Graphics graphics,
				Pen pen,
				float x,
				float y,
				float width,
				float height,
				float radius)
		{
			graphics.DrawRoundedRectangle(
					pen,
					x,
					y,
					width,
					height,
					radius,
					RoundedCorners.All);
		}

		public static void DrawRoundedRectangle(
				this Graphics graphics,
				Pen pen,
				int x,
				int y,
				int width,
				int height,
				int radius)
		{
			graphics.DrawRoundedRectangle(
					pen,
					Convert.ToSingle(x),
					Convert.ToSingle(y),
					Convert.ToSingle(width),
					Convert.ToSingle(height),
					Convert.ToSingle(radius));
		}

		public static void DrawRoundedRectangle(
			this Graphics graphics,
			Pen pen,
			Rectangle rectangle,
			int radius,
			RoundedCorners filter)
		{
			graphics.DrawRoundedRectangle(
				pen,
				rectangle.X,
				rectangle.Y,
				rectangle.Width,
				rectangle.Height,
				radius,
				filter);
		}

		public static void DrawRoundedRectangle(
			this Graphics graphics,
			Pen pen,
			Rectangle rectangle,
			int radius)
		{
			graphics.DrawRoundedRectangle(
				pen,
				rectangle.X,
				rectangle.Y,
				rectangle.Width,
				rectangle.Height,
				radius,
				RoundedCorners.All);
		}

		public static void DrawRoundedRectangle(
			this Graphics graphics,
			Pen pen,
			RectangleF rectangle,
			int radius,
			RoundedCorners filter)
		{
			graphics.DrawRoundedRectangle(
				pen,
				rectangle.X,
				rectangle.Y,
				rectangle.Width,
				rectangle.Height,
				radius,
				filter);
		}

		public static void DrawRoundedRectangle(
			this Graphics graphics,
			Pen pen,
			RectangleF rectangle,
			int radius)
		{
			graphics.DrawRoundedRectangle(
				pen,
				rectangle.X,
				rectangle.Y,
				rectangle.Width,
				rectangle.Height,
				radius,
				RoundedCorners.All);
		}

		public static void FillRoundedRectangle(
				this Graphics graphics,
				Brush brush,
				float x,
				float y,
				float width,
				float height,
				float radius,
				RoundedCorners filter)
		{
			var rectangle = new RectangleF(x, y, width, height);
			var path = graphics.GenerateRoundedRectangle(rectangle, radius, filter);
			var old = graphics.SmoothingMode;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.FillPath(brush, path);
			graphics.SmoothingMode = old;
		}

		public static void FillRoundedRectangle(
				this Graphics graphics,
				Brush brush,
				float x,
				float y,
				float width,
				float height,
				float radius)
		{
			graphics.FillRoundedRectangle(
					brush,
					x,
					y,
					width,
					height,
					radius,
					RoundedCorners.All);
		}

		public static void FillRoundedRectangle(
				this Graphics graphics,
				Brush brush,
				int x,
				int y,
				int width,
				int height,
				int radius)
		{
			graphics.FillRoundedRectangle(
					brush,
					Convert.ToSingle(x),
					Convert.ToSingle(y),
					Convert.ToSingle(width),
					Convert.ToSingle(height),
					Convert.ToSingle(radius));
		}

		public static void FillRoundedRectangle(
			this Graphics graphics,
			Brush brush,
			Rectangle rectangle,
			int radius,
			RoundedCorners filter)
		{
			graphics.FillRoundedRectangle(
				brush,
				rectangle.X,
				rectangle.Y,
				rectangle.Width,
				rectangle.Height,
				radius,
				filter);
		}

		public static void FillRoundedRectangle(
			this Graphics graphics,
			Brush brush,
			Rectangle rectangle,
			int radius)
		{
			graphics.FillRoundedRectangle(
				brush,
				rectangle.X,
				rectangle.Y,
				rectangle.Width,
				rectangle.Height,
				radius,
				RoundedCorners.All);
		}

		public static void FillRoundedRectangle(
			this Graphics graphics,
			Brush brush,
			RectangleF rectangle,
			int radius,
			RoundedCorners filter)
		{
			graphics.FillRoundedRectangle(
				brush,
				rectangle.X,
				rectangle.Y,
				rectangle.Width,
				rectangle.Height,
				radius,
				filter);
		}

		public static void FillRoundedRectangle(
			this Graphics graphics,
			Brush brush,
			RectangleF rectangle,
			int radius)
		{
			graphics.FillRoundedRectangle(
				brush,
				rectangle.X,
				rectangle.Y,
				rectangle.Width,
				rectangle.Height,
				radius,
				RoundedCorners.All);
		}
	}

	[Flags]
	public enum RoundedCorners
	{
		None = 0,
		TopLeft = 1,
		TopRight = 2,
		BottomLeft = 4,
		BottomRight = 8,
		All = TopLeft | TopRight | BottomLeft | BottomRight
	}
}