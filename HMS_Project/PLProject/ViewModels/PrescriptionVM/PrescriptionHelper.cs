using DALProject.model;

namespace PLProject.ViewModels.PrescriptionVM
{
	public static class PrescriptionHelper
	{
		public static Prescription ConvertPrescriptionViewModelToPresciption(this Prescription prescription, PrescriptionViewModel ViewModel)
		{
			foreach (var (modelItem, vmItem) in prescription.PrescriptionItems.Zip(ViewModel.PrescriptionItems, (modelItem, vmItem) => (modelItem, vmItem)))
			{
				// Update Medications list (replace or update depending on your logic)
				modelItem.Medications = modelItem.ConvertPreItemMedVMToPrescriptionItemMedication(vmItem.Medications);
			}
			return prescription;
		}
		public static PrescriptionViewModel ConvertPresciptionToPrescriptionViewModel(this Prescription prescription)
		{
			return new PrescriptionViewModel()
			{
				PrescriptionItems = prescription.PrescriptionItems.Select(i => i.ConvertPrescriptionItemToPrescriptionItemDoctorVM()).ToList(),
				DoctorId = prescription.DoctorId,
				Patient = prescription.Apointment?.Patient,
				prescriptionId = prescription.Id
			};
		}
		public static PrescriptionItemDoctorVM ConvertPrescriptionItemToPrescriptionItemDoctorVM(this PrescriptionItem item)
		{
			var PrescriptionItemDoctorVM = new PrescriptionItemDoctorVM();

			PrescriptionItemDoctorVM.Id= item.Id;
			PrescriptionItemDoctorVM.ActiveSubstanceId = item.ActiveSubstanceId;
			PrescriptionItemDoctorVM. ActiveSubstance = item.ActiveSubstance;
			PrescriptionItemDoctorVM.FullDosage = item.FullDosage;
			PrescriptionItemDoctorVM.Medications = item.Medications.Select(m => m.ConvertPrescriptionItemMedicationToPreItemMedVM()).ToList();

			return PrescriptionItemDoctorVM;
		}
		public static List<PrescriptionItemMedication> ConvertPreItemMedVMToPrescriptionItemMedication(this PrescriptionItem PreItem,List< PreItemMedVM> itemVM)
		{
			var res=itemVM.Select(item => new PrescriptionItemMedication()
			{
				Dosage = item.Dosage,
				Duration = item.Duration,
				MedicationId = item.MedicationId,
				PrescriptionItemId = PreItem.Id,
			}).ToList();
			return res;
		}
		public static PreItemMedVM ConvertPrescriptionItemMedicationToPreItemMedVM(this PrescriptionItemMedication item)
		{
			return new PreItemMedVM()
			{
				PrescriptionItemId = item.PrescriptionItemId,
				Dosage = item.Dosage,
				Duration = item.Duration,
				Medication = item.Medication
			};
		}
		public static PrescriptionItem PrescriptionItemDoctorVMToPrescriptionItem(this PrescriptionItemDoctorVM ViewModel)
		{

			return new PrescriptionItem()
			{
				ActiveSubstanceId=ViewModel.ActiveSubstanceId,
				FullDosage=ViewModel.FullDosage,
			};
		}

	}
}
