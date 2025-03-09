using System;

namespace HospitalManagement.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public string EmergencyContact { get; set; }
        public string MedicalHistory { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime LastVisitDate { get; set; }

        public Patient()
        {
            RegisteredDate = DateTime.Now;
            LastVisitDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Name} - {Age} years old";
        }

        public string GetFullDetails()
        {
            return $"Name: {Name}\n" +
                   $"Age: {Age}\n" +
                   $"Gender: {Gender}\n" +
                   $"Contact: {ContactNumber}\n" +
                   $"Address: {Address}\n" +
                   $"Blood Group: {BloodGroup}\n" +
                   $"Emergency Contact: {EmergencyContact}";
        }

        public bool IsValidPatient()
        {
            return !string.IsNullOrEmpty(Name) &&
                   Age > 0 &&
                   !string.IsNullOrEmpty(Gender) &&
                   !string.IsNullOrEmpty(ContactNumber) &&
                   !string.IsNullOrEmpty(Address);
        }
    }
}
