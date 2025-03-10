using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace HospitalManagement
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Check if user is logged in
            // If not, redirect to LoginActivity
            StartActivity(typeof(LoginActivity));
            Finish();
        }
    }
}
