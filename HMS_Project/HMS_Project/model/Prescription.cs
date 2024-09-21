namespace HMS_Project.model
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public int Dosage { get; set; }
        public DateTime DateIssued { get; set; }
        public int Duration { get; set; }
        //public int DoctorID { get; set; }

        public Pharmacy Pharmacy { get; set; } = null!;
        public ICollection<ActiveSubstance> activeSubstances {  get; set; } = new HashSet<ActiveSubstance>();

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

    }
}
