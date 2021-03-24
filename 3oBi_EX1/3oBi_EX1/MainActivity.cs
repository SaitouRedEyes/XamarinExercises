using Android.App;
using Android.Content;
using Android.OS;

namespace _3oBi_EX1
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Intent i = new Intent(this, typeof(CountService));
            StartService(i);
        }
    }
}