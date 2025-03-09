using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
using System;

namespace HospitalManagement
{
    [Activity(Label = "Login", Theme = "@style/AppTheme")]
    public class LoginActivity : AppCompatActivity
    {
        private TextInputEditText emailInput;
        private TextInputEditText passwordInput;
        private Button loginButton;
        private TextView forgotPasswordText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            // Initialize views
            emailInput = FindViewById<TextInputEditText>(Resource.Id.emailInput);
            passwordInput = FindViewById<TextInputEditText>(Resource.Id.passwordInput);
            loginButton = FindViewById<Button>(Resource.Id.loginButton);
            forgotPasswordText = FindViewById<TextView>(Resource.Id.forgotPasswordText);

            // Set up click handlers
            loginButton.Click += LoginButton_Click;
            forgotPasswordText.Click += ForgotPasswordText_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string email = emailInput.Text;
                string password = passwordInput.Text;

                // Validate input
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    Toast.MakeText(this, "Please enter both email and password", ToastLength.Short).Show();
                    return;
                }

                // TODO: Implement actual authentication
                // For now, just redirect to Dashboard
                StartActivity(typeof(DashboardActivity));
                Finish();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Login failed: " + ex.Message, ToastLength.Long).Show();
            }
        }

        private void ForgotPasswordText_Click(object sender, EventArgs e)
        {
            // TODO: Implement forgot password functionality
            Toast.MakeText(this, "Forgot password functionality coming soon", ToastLength.Short).Show();
        }
    }
}
