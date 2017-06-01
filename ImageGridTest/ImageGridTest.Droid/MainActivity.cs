using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ImageGridTest.Droid
{
	[Activity (Label = "ImageGridTest", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new ImageGridTest.App ());
		}
	}
}

