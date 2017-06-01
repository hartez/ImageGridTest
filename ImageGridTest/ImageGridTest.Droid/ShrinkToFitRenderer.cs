using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using ImageGridTest.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using AImageView = Android.Widget.ImageView;

[assembly: ExportRenderer(typeof(Image), typeof(ShrinkToFitRenderer))]
namespace ImageGridTest.Droid
{
	public class ShrinkToFitRenderer : ImageRenderer
	{
		private bool _disposed;

		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);

			Control.LayoutChange += ControlOnLayoutChange;
		}

		protected override void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			_disposed = true;

			if (disposing)
			{
				if (Control != null)
				{
					Control.LayoutChange -= ControlOnLayoutChange;
				}
			}

			base.Dispose(disposing);
		}

		private void ControlOnLayoutChange(object sender, LayoutChangeEventArgs layoutChangeEventArgs)
		{
			ShrinkIfNecessary();
		}

		bool IsElementShrinkable()
		{
			return Element != null
				   && Element.Aspect == Aspect.AspectFit
				   && Element.HeightRequest <= -1
				   && Element.WidthRequest <= -1
				   && Element.Height > 1
				   && Element.Width > 1;
		}

		void ShrinkIfNecessary()
		{
			if (!IsElementShrinkable())
			{
				return;
			}

			// The the size of the image on screen
			var size = GetBitmapDisplaySize(Control);

			if (size.Width <= 0 || size.Height <= 0)
			{
				return;
			}

			// Figure out the device-independent size
			var independentSize = new Size(Context.FromPixels(size.Width), Context.FromPixels(size.Height));

			const double tolerance = 2;

			var heightDelta = Element.Height - independentSize.Height;

			if (heightDelta > tolerance)
			{
				Element.HeightRequest = independentSize.Height;
				Element.InvalidateMeasureNonVirtual(InvalidationTrigger.SizeRequestChanged);
				return;
			}

			var widthDelta = Element.Width - independentSize.Width;

			if (widthDelta > tolerance)
			{
				Element.WidthRequest = independentSize.Width;
				Element.InvalidateMeasureNonVirtual(InvalidationTrigger.SizeRequestChanged);
			}
		}

		static Size GetBitmapDisplaySize(AImageView imageView)
		{
			if (imageView?.Drawable == null)
			{
				return Size.Zero;
			}

			var matrixValues = new float[9];
			imageView.ImageMatrix.GetValues(matrixValues);

			// Extract the scale values using the constants (if aspect ratio maintained, scaleX == scaleY)
			float scaleX = matrixValues[Matrix.MscaleX];
			float scaleY = matrixValues[Matrix.MscaleY];

			// Get the size of the drawable if it weren't scaled/fit
			Drawable drawable = imageView.Drawable;
			int width = drawable.IntrinsicWidth;
			int height = drawable.IntrinsicHeight;

			// Figure out the current width/height of the drawable
			double currentWidth = Math.Round(width * scaleX);
			double currentHeight = Math.Round(height * scaleY);

			return new Size(currentWidth, currentHeight);
		}
	}
}