namespace HMS_Project.model
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public string Dosage { get; set; } = null!;
        public DateTime DateIssued { get; set; }
        public string Duration { get; set; } = null!;

        #region Many2Many With ActiveSubstance
        public virtual ICollection<ActiveSubstance> activeSubstances { get; set; } = new HashSet<ActiveSubstance>();
        #endregion

        #region One2Many With Pharmacy
        public int? PharmacyId { get; set; }
        public virtual Pharmacy Pharmacy { get; set; } = null!;
        #endregion

        #region Many2Many With Medication
        public virtual ICollection<Medication> Medications { get; set; } = new HashSet<Medication>();
        #endregion

        #region One2One With Apointment
        public int ApointmentId { get; set; }
        public virtual Apointment Apointment { get; set; }=null!;
        #endregion

        #region One2Many With Doctor
        public long DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
        #endregion
    }
}
