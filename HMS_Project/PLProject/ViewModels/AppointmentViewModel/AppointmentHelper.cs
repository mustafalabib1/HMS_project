using DALProject.model;
using PLProject.ViewModels.PrescriptionVM;

namespace PLProject.ViewModels.AppointmentViewModel
{
    public static class AppointmentHelper
    {
        public static Apointment ConvertApointmentCreateVMToApointment(this Apointment appointment, ApointmentCreateVM appointmentCreateVM)
        {
            DateTime selectedAppointmentDate = DateTime.Parse(appointmentCreateVM.SelectedDate);
            appointment.PatientId = appointmentCreateVM.PatientId;
            appointment.ClinicId = appointmentCreateVM.ClinicId;
            appointment.DoctorId = appointmentCreateVM.SelectedDoctorId;
            appointment.ApointmentDate = DateOnly.FromDateTime(selectedAppointmentDate);
            appointment.ApointmentTime = TimeOnly.Parse(appointmentCreateVM.SelectedTime.Split('-')[0]);

            // Return the updated appointment object
            return appointment;
        }
        public static AppointmentGenarelVM ConvertApointmentToAppointmentGenarelVM(this Apointment appointment)
        {
            var appointmentGenarelVM = new AppointmentGenarelVM();

            appointmentGenarelVM.Id = appointment.Id;
            appointmentGenarelVM.ApointmentDate = appointment.ApointmentDate;
            appointmentGenarelVM.ApointmentTime = appointment.ApointmentTime;
            appointmentGenarelVM.Patient = appointment.Patient;
            appointmentGenarelVM.Doctor = appointment.Doctor;
            appointmentGenarelVM.Clinic = appointment.Clinic;
            appointmentGenarelVM.ApointmentStatus = appointment.ApointmentStatus;
            appointmentGenarelVM.Examination = appointment.Examination;
            appointmentGenarelVM.Invoice = appointment.Invoice;
            if (appointment.Prescription is not null)
                appointmentGenarelVM.PrescriptionViewModel = appointment.Prescription.ConvertPresciptionToPrescriptionViewModel();

            // Return the updated appointment object
            return appointmentGenarelVM;
        }
        public static Apointment ConvertAppointmentGenarelVMToApointment(this Apointment appointment, AppointmentGenarelVM AppointmentGenarelVM)
        {
            appointment.Examination = AppointmentGenarelVM.Examination;
            var PrescriptionVM = AppointmentGenarelVM.PrescriptionViewModel;
            appointment.Prescription = new Prescription()
            {
                PrescriptionItems = PrescriptionVM.PrescriptionItems.Select(pi => pi.PrescriptionItemDoctorVMToPrescriptionItem()).ToList(),
                DoctorId = appointment.DoctorId
            };
            return appointment;
        }

    }
}
