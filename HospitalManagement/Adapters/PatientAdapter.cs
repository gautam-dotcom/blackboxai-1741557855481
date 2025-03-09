using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using HospitalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement.Adapters
{
    public class PatientAdapter : RecyclerView.Adapter
    {
        private List<Patient> patients;
        private List<Patient> filteredPatients;
        public event EventHandler<int> ItemClick;

        public PatientAdapter(List<Patient> patients)
        {
            this.patients = patients;
            this.filteredPatients = new List<Patient>(patients);
        }

        public override int ItemCount => filteredPatients.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PatientViewHolder vh = holder as PatientViewHolder;
            Patient patient = filteredPatients[position];

            vh.NameText.Text = patient.Name;
            vh.AgeGenderText.Text = $"{patient.Age} years • {patient.Gender}";
            vh.ContactText.Text = patient.ContactNumber;
            vh.AddressText.Text = patient.Address;

            // Set blood group background color based on type
            vh.BloodGroupText.Text = patient.BloodGroup;
            vh.BloodGroupText.SetBackgroundResource(Resource.Drawable.blood_group_background);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.item_patient, parent, false);

            PatientViewHolder vh = new PatientViewHolder(itemView, OnClick);
            return vh;
        }

        private void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }

        public void Filter(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                filteredPatients = new List<Patient>(patients);
            }
            else
            {
                query = query.ToLower();
                filteredPatients = patients
                    .Where(p => p.Name.ToLower().Contains(query) ||
                               p.ContactNumber.Contains(query) ||
                               p.Address.ToLower().Contains(query))
                    .ToList();
            }
            NotifyDataSetChanged();
        }

        public class PatientViewHolder : RecyclerView.ViewHolder
        {
            public TextView NameText { get; private set; }
            public TextView AgeGenderText { get; private set; }
            public TextView ContactText { get; private set; }
            public TextView AddressText { get; private set; }
            public TextView BloodGroupText { get; private set; }

            public PatientViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                NameText = itemView.FindViewById<TextView>(Resource.Id.textPatientName);
                AgeGenderText = itemView.FindViewById<TextView>(Resource.Id.textAgeGender);
                ContactText = itemView.FindViewById<TextView>(Resource.Id.textContact);
                AddressText = itemView.FindViewById<TextView>(Resource.Id.textAddress);
                BloodGroupText = itemView.FindViewById<TextView>(Resource.Id.textBloodGroup);

                itemView.Click += (sender, e) => listener(AdapterPosition);
            }
        }
    }
}
