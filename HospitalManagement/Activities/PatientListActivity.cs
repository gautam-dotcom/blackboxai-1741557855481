using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.FloatingActionButton;
using HospitalManagement.Models;
using HospitalManagement.Adapters;
using System.Collections.Generic;
using AndroidX.AppCompat.Widget;
using Android.Runtime;

namespace HospitalManagement
{
    [Activity(Label = "@string/patients", Theme = "@style/AppTheme")]
    public class PatientListActivity : AppCompatActivity
    {
        private RecyclerView recyclerView;
        private FloatingActionButton fabAddPatient;
        private SearchView searchView;
        private TextView emptyView;
        private ProgressBar progressBar;
        private PatientAdapter adapter;
        private List<Patient> patients;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_patient_list);

            // Initialize views
            InitializeViews();
            
            // Set up RecyclerView
            SetupRecyclerView();

            // Set up SearchView
            SetupSearchView();

            // Load patients data
            LoadPatients();

            // Set up FAB click handler
            fabAddPatient.Click += (s, e) => {
                StartActivity(typeof(AddPatientActivity));
            };

            // Set up action bar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = GetString(Resource.String.patients);
        }

        private void InitializeViews()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewPatients);
            fabAddPatient = FindViewById<FloatingActionButton>(Resource.Id.fabAddPatient);
            searchView = FindViewById<SearchView>(Resource.Id.searchViewPatients);
            emptyView = FindViewById<TextView>(Resource.Id.emptyView);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
        }

        private void SetupRecyclerView()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            patients = new List<Patient>();
            adapter = new PatientAdapter(patients);
            recyclerView.SetAdapter(adapter);

            // Add item decoration for dividers
            RecyclerView.ItemDecoration itemDecoration = 
                new DividerItemDecoration(this, DividerItemDecoration.Vertical);
            recyclerView.AddItemDecoration(itemDecoration);

            // Set up item click handling
            adapter.ItemClick += (s, position) => {
                var patient = patients[position];
                var intent = new Android.Content.Intent(this, typeof(PatientDetailsActivity));
                intent.PutExtra("patientId", patient.Id);
                StartActivity(intent);
            };
        }

        private void SetupSearchView()
        {
            searchView.QueryTextChange += (s, e) => {
                adapter.Filter(e.NewText);
                UpdateEmptyView(adapter.ItemCount == 0);
                e.Handled = true;
            };
        }

        private void LoadPatients()
        {
            // Show loading indicator
            progressBar.Visibility = ViewStates.Visible;
            recyclerView.Visibility = ViewStates.Gone;

            // TODO: Replace with actual data loading from database/API
            // For now, adding sample data
            patients.Clear();
            patients.AddRange(GetSamplePatients());

            // Update UI
            adapter.NotifyDataSetChanged();
            progressBar.Visibility = ViewStates.Gone;
            recyclerView.Visibility = ViewStates.Visible;

            UpdateEmptyView(patients.Count == 0);
        }

        private void UpdateEmptyView(bool isEmpty)
        {
            emptyView.Visibility = isEmpty ? ViewStates.Visible : ViewStates.Gone;
            recyclerView.Visibility = isEmpty ? ViewStates.Gone : ViewStates.Visible;
        }

        private List<Patient> GetSamplePatients()
        {
            // Sample data for testing
            return new List<Patient>
            {
                new Patient { 
                    Id = 1, 
                    Name = "John Doe", 
                    Age = 35, 
                    Gender = "Male",
                    ContactNumber = "123-456-7890",
                    Address = "123 Main St",
                    BloodGroup = "A+"
                },
                new Patient { 
                    Id = 2, 
                    Name = "Jane Smith", 
                    Age = 28, 
                    Gender = "Female",
                    ContactNumber = "098-765-4321",
                    Address = "456 Oak Ave",
                    BloodGroup = "O-"
                }
            };
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        protected override void OnResume()
        {
            base.OnResume();
            LoadPatients(); // Refresh list when returning to activity
        }
    }
}
