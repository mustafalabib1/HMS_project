using DALProject.model;

namespace PLProject.ViewModels.PrescriptionVM
{
    public class PreItemMedVM
    {
        public string Dosage { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public int PrescriptionItemId { get; set; }
        public int MedicationId { get; set; }
        public virtual Medication? Medication { get; set; } = null!;
    }
}
