using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ImageGridTest.Droid
{
	[Activity (Label = "ImageGridTest", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Store off the device sizes, so we can access them within Xamarin Forms
            App.DisplayScreenWidth = (double)Resources.DisplayMetrics.WidthPixels / (double)Resources.DisplayMetrics.Density; // Width = WidthPixels / Density
            App.DisplayScreenHeight = (double)Resources.DisplayMetrics.HeightPixels / (double)Resources.DisplayMetrics.Density; // Height = HeightPixels / Density
            App.DisplayScaleFactor = (double)Resources.DisplayMetrics.Density;



            global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new ImageGridTest.App ());
		}
	}
}

