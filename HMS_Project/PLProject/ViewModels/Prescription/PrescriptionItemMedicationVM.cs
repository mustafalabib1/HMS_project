using DALProject.model;

namespace PLProject.ViewModels.Presciption
{
	public class PrescriptionItemMedicationVM
	{
		public string Dosage { get; set; } = null!;
		public string Duration { get; set; } = null!;
		public int  MedicationId { get; set; }
		public virtual int PrescriptionItemId { get; set; }
	}
}
