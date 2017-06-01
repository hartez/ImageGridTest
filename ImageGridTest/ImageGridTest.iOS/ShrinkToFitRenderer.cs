using CoreGraphics;
using ImageGridTest.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Image), typeof(ShrinkToFitRenderer))]
namespace ImageGridTest.iOS
{
	public class ShrinkToFitRenderer : ImageRenderer
	{
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
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

		static CGSize GetBitmapDisplaySize(UIImageView imageView)
		{
			if (imageView?.Image == null)
			{
				return CGSize.Empty;
			}

			return AVFoundation.AVUtilities.WithAspectRatio(imageView.Bounds, imageView.Image.Size).Size;
		}

		void ShrinkIfNecessary()
		{
			if (!IsElementShrinkable() || Control == null)
			{
				return;
			}

			var size = GetBitmapDisplaySize(Control);

			if (size.Width <= 0 || size.Height <= 0)
			{
				return;
			}

			const double tolerance = 2;

			var heightDelta = Element.Height - size.Height;

			if (heightDelta > tolerance)
			{

				Element.HeightRequest = size.Height;
				Element.InvalidateMeasureNonVirtual(InvalidationTrigger.SizeRequestChanged);
				return;
			}

			var widthDelta = Element.Width - size.Width;

			if (widthDelta > tolerance)
			{

				Element.WidthRequest = size.Width;
				Element.InvalidateMeasureNonVirtual(InvalidationTrigger.SizeRequestChanged);
			}
		}
	}
}