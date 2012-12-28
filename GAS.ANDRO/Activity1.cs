using Android.App;
using Android.OS;
using Android.Widget;
namespace GAS.ANDRO
{
	[Activity(Label = "GAS.ANDRO", MainLauncher = true, Icon = "@drawable/icon")]
	public class Activity1 : Activity
	{
		int count = 1;
		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);
			// Set our view _from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			// Get our button _from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.MyButton);
			button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
		}
	}
}
