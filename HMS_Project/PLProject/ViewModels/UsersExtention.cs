using DALProject.model;

namespace PLProject.ViewModels
{
    public static class UsersExtention
    {
        public static Nurse UpdateInfo(this Nurse nurse, NurseViewModel nurseViewModel)
        {
            nurse.AppUser.Address = nurseViewModel.Address;
            nurse.AppUser.SSN = nurseViewModel.SSN;
            nurse.AppUser.FullName = $"{nurseViewModel.FirstName.Trim()} {nurseViewModel.MiddleName.Trim()} {nurseViewModel.LastName.Trim()}";
            nurse.AppUser.Gender = nurseViewModel.Gender;
            nurse.AppUser.PhoneNumber = nurseViewModel.Phone;
            nurse.AppUser.Email = nurseViewModel.Email;
            return nurse;
        }
        public static Pharmacist UpdateInfo(this Pharmacist pharmacist, PharmacistViewModel pharmacistViewModel)
        {
            pharmacist.AppUser.Address = pharmacistViewModel.Address;
            pharmacist.AppUser.SSN = pharmacistViewModel.SSN;
            pharmacist.AppUser.FullName = $"{pharmacistViewModel.FirstName.Trim()} {pharmacistViewModel.MiddleName.Trim()} {pharmacistViewModel.LastName.Trim()}";
            pharmacist.AppUser.Gender = pharmacistViewModel.Gender;
            pharmacist.AppUser.PhoneNumber = pharmacistViewModel.Phone;
            pharmacist.AppUser.Email = pharmacistViewModel.Email;
            return pharmacist;
        }
        public static Receptionist UpdateInfo(this Receptionist receptionist, ReceptionistViewModel receptionistViewModel)
        {
            receptionist.AppUser.Address = receptionistViewModel.Address;
            receptionist.AppUser.SSN = receptionistViewModel.SSN;
            receptionist.AppUser.FullName = $"{receptionistViewModel.FirstName.Trim()} {receptionistViewModel.MiddleName.Trim()} {receptionistViewModel.LastName.Trim()}";
            receptionist.AppUser.Gender = receptionistViewModel.Gender;
            receptionist.AppUser.PhoneNumber = receptionistViewModel.Phone;
            receptionist.AppUser.Email = receptionistViewModel.Email;
            return receptionist;
        }
        public static Patient UpdateInfo(this Patient patient, PatientViewModel patientViewModel)
        {
            patient.AppUser.Address = patientViewModel.Address;
            patient.AppUser.SSN = patientViewModel.SSN;
            patient.AppUser.FullName = $"{patientViewModel.FirstName.Trim()} {patientViewModel.MiddleName.Trim()} {patientViewModel.LastName.Trim()}";
            patient.AppUser.Gender = patientViewModel.Gender;
            patient.AppUser.PhoneNumber = patientViewModel.Phone;
            patient.AppUser.Email = patientViewModel.Email;
            return patient;
        }
    }
}
