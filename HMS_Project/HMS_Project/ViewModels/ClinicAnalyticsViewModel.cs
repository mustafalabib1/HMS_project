using DALProject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    internal class ClinicAnalyticsViewModel
    {
        public ClinicAnalyticsViewModel(Clinic _clinic)
        {
            ClinicId= _clinic.ClinicId;
            TotalDoctors = _clinic.Doctors.Count;
            TotalNurses = _clinic.Nurses.Count;
            TotalAppointments = _clinic.Appointments.Count;
            UpcomingAppointments = (HashSet<Appointment>)_clinic.Appointments;
        }
        public int ClinicId { get; set; }
        HashSet<Appointment> UpcomingAppointments { get; set; } = new HashSet<Appointment>();
        public int TotalDoctors { get; set; }
        public int TotalNurses { get; set; }
        public int TotalAppointments { get; set; }

        public static explicit operator ClinicAnalyticsViewModel(Clinic _clinic)
        {
            return new ClinicAnalyticsViewModel(_clinic);
        }
    }
}
