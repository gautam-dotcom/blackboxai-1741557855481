using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
using HospitalManagement.Models;
using System;
using Android.Views;

namespace HospitalManagement
{
    [Activity(Label = "@string/add_patient", Theme = "@style/AppTheme")]
    public class AddPatientActivity : AppCompatActivity
    {
        private TextInputEditText nameInput;
        private TextInputEditText ageInput;
        private RadioGroup genderGroup;
        private TextInputEditText contactInput;
        private TextInputEditText addressInput;
        private Spinner bloodGroupSpinner;
        private TextInputEditText emergencyContactInput;
        private Button saveButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_patient);

            // Initialize views
            InitializeViews();

            // Set up action bar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = GetString(Resource.String.add_patient);

            // Set up save button click handler
            saveButton.Click += SaveButton_Click;
        }

        private void InitializeViews()
        {
            nameInput = FindViewById<TextInputEditText>(Resource.Id.inputName);
            ageInput = FindViewById<TextInputEditText>(Resource.Id.inputAge);
            genderGroup = FindViewById<RadioGroup>(Resource.Id.radioGroupGender);
            contactInput = FindViewById<TextInputEditText>(Resource.Id.inputContact);
            addressInput = FindViewById<TextInputEditText>(Resource.Id.inputAddress);
            bloodGroupSpinner = FindViewById<Spinner>(Resource.Id.spinnerBloodGroup);
            emergencyContactInput = FindViewById<TextInputEditText>(Resource.Id.inputEmergencyContact);
            saveButton = FindViewById<Button>(Resource.Id.buttonSave);

            // Set up blood group spinner
            var bloodGroups = new string[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, bloodGroups);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            bloodGroupSpinner.Adapter = adapter;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    var patient = new Patient
                    {
                        Name = nameInput.Text,
                        Age = int.Parse(ageInput.Text),
                        Gender = GetSelectedGender(),
                        ContactNumber = contactInput.Text,
                        Address = addressInput.Text,
                        BloodGroup = bloodGroupSpinner.SelectedItem.ToString(),
                        EmergencyContact = emergencyContactInput.Text,
                        RegisteredDate = DateTime.Now
                    };

                    // TODO: Save patient to database
                    SavePatient(patient);

                    Toast.MakeText(this, Resource.String.success_save, ToastLength.Short).Show();
                    Finish();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, Resource.String.error_save, ToastLength.Long).Show();
                }
            }
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(nameInput.Text))
            {
                nameInput.Error = GetString(Resource.String.error_required);
                isValid = false;
            }

            if (string.IsNullOrEmpty(ageInput.Text))
            {
                ageInput.Error = GetString(Resource.String.error_required);
                isValid = false;
            }

            if (string.IsNullOrEmpty(contactInput.Text))
            {
                contactInput.Error = GetString(Resource.String.error_required);
                isValid = false;
            }

            if (string.IsNullOrEmpty(addressInput.Text))
            {
                addressInput.Error = GetString(Resource.String.error_required);
                isValid = false;
            }

            return isValid;
        }

        private string GetSelectedGender()
        {
            int selectedId = genderGroup.CheckedRadioButtonId;
            RadioButton radioButton = FindViewById<RadioButton>(selectedId);
            return radioButton?.Text ?? "Not specified";
        }

        private void SavePatient(Patient patient)
        {
            // TODO: Implement actual database saving
            // For now, we'll just simulate saving
            System.Threading.Thread.Sleep(500);
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }
    }
}
