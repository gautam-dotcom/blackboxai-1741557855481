using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.CardView.Widget;
using System;

namespace HospitalManagement
{
    [Activity(Label = "Dashboard", Theme = "@style/AppTheme")]
    public class DashboardActivity : AppCompatActivity
    {
        private CardView patientsCard;
        private CardView appointmentsCard;
        private CardView doctorsCard;
        private CardView medicalRecordsCard;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_dashboard);

            // Initialize dashboard cards
            patientsCard = FindViewById<CardView>(Resource.Id.patientsCard);
            appointmentsCard = FindViewById<CardView>(Resource.Id.appointmentsCard);
            doctorsCard = FindViewById<CardView>(Resource.Id.doctorsCard);
            medicalRecordsCard = FindViewById<CardView>(Resource.Id.medicalRecordsCard);

            // Set up click handlers
            patientsCard.Click += (s, e) => {
                StartActivity(typeof(PatientListActivity));
            };

            appointmentsCard.Click += (s, e) => {
                StartActivity(typeof(AppointmentActivity));
            };

            doctorsCard.Click += (s, e) => {
                StartActivity(typeof(DoctorListActivity));
            };

            medicalRecordsCard.Click += (s, e) => {
                StartActivity(typeof(MedicalRecordActivity));
            };

            // Set up action bar
            SupportActionBar.Title = "Hospital Management";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnSupportNavigateUp()
        {
            // Handle back button in action bar
            OnBackPressed();
            return true;
        }

        public override void OnBackPressed()
        {
            // Show confirmation dialog before logging out
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Logout");
            alert.SetMessage("Are you sure you want to logout?");
            alert.SetPositiveButton("Yes", (senderAlert, args) => {
                base.OnBackPressed();
                StartActivity(typeof(LoginActivity));
                Finish();
            });
            alert.SetNegativeButton("No", (senderAlert, args) => {
                alert.Dispose();
            });
            alert.Show();
        }
    }
}
