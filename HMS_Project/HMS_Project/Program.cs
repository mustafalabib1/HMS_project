using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace HMS_Project
{
    internal class Program
    {
		private readonly HMSdbcontextProcedures procedures;

		public Program(HMSdbcontext context , HMSdbcontextProcedures procedures)
        {
			Context = context;
			this.procedures = procedures;
		}

		public HMSdbcontext Context { get; }

		static void Main(string[] args)
        {
			#region how can get day in week 
			//DayOfWeek day = DateTime.Now.DayOfWeek;
			//Console.WriteLine($"Today is day number {day} of the week."); 
			#endregion
			//using HMSdbcontext context = new HMSdbcontext();

			#region try add patient by use view model 
			//RegisterationViewModel PatViewModle = new RegisterationViewModel()
			//{
			//    SSN = 345678990,
			//    Address = "Mansoura",
			//    FirstName = "Musrtafa",
			//    MiddleName = "Labib",
			//    LastName = "Issa",
			//    Gender = Gender.Male,
			//    DateOfBirth = new DateOnly(2004, 2, 18),
			//    Email = "Mustafa@gmail.com"
			//    ,
			//    UserPassword = "1234567890",
			//    Phone = "1234567890"
			//};
			//Patient patient = (Patient)PatViewModle;
			//context.Patients.Add(patient);
			//context.SaveChanges();
			#endregion

			#region try show patient by use view model
			//var patient =context.Patients.ToList();
			//foreach (var item in patient)
			//{
			//    var pat = (RegisterationViewModel)item;
			//    Console.WriteLine($"{pat.SSN}:::{pat.FirstName}:::{pat.Phone}");
			//} 
			#endregion

			#region show mediction and active in each one 
			//var medicition=(context.Medication.Include(m=>m.ActiveSubstances)).Select(m => (MedicationViewModel)m).ToList();
			//foreach (var item in medicition)
			//{
			//    Console.WriteLine($"{item.MedName}  {item.Strength}");
			//    foreach (var item1 in item.ActSubInMed)
			//    {
			//        Console.WriteLine($"\t{item1.ActiveSubstancesName}");
			//    }
			//}
			//Console.WriteLine();
			//Console.WriteLine(); 
			#endregion

			#region view model of Activesubstance that show substance and list of interaction and medication 
			//var ActSubs = (context.ActiveSubstances.Include(a => a.Medications).ToList()).Select(a => (ActiveSubstanceViewModel)a);
			//foreach (var item in ActSubs)
			//{
			//    Console.WriteLine("=============================================================================");
			//    Console.WriteLine($"{item.ActiveSubstancesName}");
			//    foreach (var item1 in item.Interactions)
			//    {
			//        Console.WriteLine($"\t{item1.OtherSubstanceName}");
			//    }
			//    Console.WriteLine("-------------------------------------------------------");
			//    foreach (var item1 in item.Medications)
			//    {
			//        Console.WriteLine($"\t{item1.MedName}");
			//    }
			//}
			#endregion

			#region try add active substance by use view model
			////Creating new ViewModel
			//// Step 1: Create a new ViewModel instance and load data into it
			//var viewModel = new ActiveSubstanceViewModel
			//{
			//    // Load active substances from the database without tracking to avoid tracking errors
			//    ActiveSubstancesDateReader = context.ActiveSubstances.AsNoTracking().ToHashSet(),

			//    // Load medications from the database (no AsNoTracking here, so tracking will be enabled)
			//    MedicationsDateReader = context.Medication.ToHashSet()
			//};

			//// Step 2: Loop through the active substances and print their details
			//foreach (var item in viewModel.ActiveSubstancesDateReader)
			//{
			//    Console.WriteLine($"{item.ActiveSubstancesId}\t:: {item.ActiveSubstancesName}");
			//}

			//// Separator for readability in the console output
			//Console.WriteLine("==================================================================================");

			//// Step 3: Loop through the medications and print their details
			//foreach (var item in viewModel.MedicationsDateReader)
			//{
			//    Console.WriteLine($"{item.MedicationCode}\t:: {item.MedName}");
			//}

			//// Step 4: Assign a new active substance name to the ViewModel
			//viewModel.ActiveSubstancesName = "NewSubstance";

			//// Step 5: Add an interaction for the new substance to the ViewModel
			//viewModel.Interactions = new HashSet<ActiveSubstanceInteractionViewModel>
			//{
			//    // Create a new interaction indicating that the new substance interacts with Acetaminophen
			//    new ActiveSubstanceInteractionViewModel
			//    {
			//        Interaction = "NewSubstance may interact with Acetaminophen",
			//        OtherSubstanceName = "Acetaminophen",
			//        ActSubId=1 // Example of a related ID for the interaction
			//    }
			//};

			//// Step 6: Retrieve the first medication from the MedicationsDateReader (fetched from the database earlier)
			//var med = viewModel.MedicationsDateReader.First();

			//// Step 7: Add this medication to the ViewModel's Medications property (set of medications)
			//viewModel.Medications = new HashSet<Medication>
			//{
			//    med // Adding the selected medication (directly from the database) to the ViewModel
			//};

			//// Step 8: Print the new active substance name to the console
			//Console.WriteLine("=============================================================================");
			//Console.WriteLine($"{viewModel.ActiveSubstancesName}");

			//// Step 9: Loop through and print all interactions related to the new substance
			//foreach (var item1 in viewModel.Interactions)
			//{
			//    Console.WriteLine($"\t{item1.OtherSubstanceName}"); // Printing the name of the other substance it interacts with
			//}

			//// Separator for readability in the console output
			//Console.WriteLine("-------------------------------------------------------");

			//// Step 10: Loop through and print all medications in the ViewModel
			//foreach (var item1 in viewModel.Medications)
			//{
			//    Console.WriteLine($"\t{item1.MedName}"); // Printing the medication name
			//}

			//// Step 11: Add the new active substance to the database
			//// You are casting the viewModel to ActiveSubstance which may cause issues unless properly mapped.
			//// Normally, you should map the ViewModel fields to the actual ActiveSubstance entity fields.
			//context.ActiveSubstances.Add((ActiveSubstance)viewModel);

			//// Step 12: Save changes to the database (committing the new active substance and related data)
			//context.SaveChanges();

			#endregion

			#region Try add new medicaton 
			//var MedVM = new MedicationViewModel()
			//{
			//    ActSubDateReader = context.ActiveSubstances.ToList(),
			//};
			//foreach (var item in MedVM.ActSubDateReader)
			//{
			//    Console.WriteLine($"{item.ActiveSubstancesId}\t ::{item.ActiveSubstancesName}");
			//}
			//MedVM.MedName = "Medication21";
			//MedVM.MedicationCode = "MEDC021";
			//MedVM.ActSubInMed = new List<ActiveSubstance>() { MedVM.ActSubDateReader.First(), MedVM.ActSubDateReader.Last() };
			//MedVM.Strength = 100;

			//context.Medication.Add((Medication)MedVM);
			//context.SaveChanges(); 
			#endregion

			#region Add list of ClinicSpecializationLookup
			//HashSet<ClinicSpecializationLookup> clinicSpecializations = new HashSet<ClinicSpecializationLookup>()
			//{
			//    new ClinicSpecializationLookup(){ Specialization="General"},
			//    new ClinicSpecializationLookup(){ Specialization="Cardiology"},
			//    new ClinicSpecializationLookup(){ Specialization="Dermatology"},
			//    new ClinicSpecializationLookup(){ Specialization="Neurology"},
			//    new ClinicSpecializationLookup(){ Specialization="Orthopedics"},
			//    new ClinicSpecializationLookup(){ Specialization="Pediatrics"},
			//    new ClinicSpecializationLookup(){ Specialization="Psychiatry"},
			//    new ClinicSpecializationLookup(){ Specialization="Surgery"},
			//    new ClinicSpecializationLookup(){ Specialization="Oncology"},
			//    new ClinicSpecializationLookup(){ Specialization="Gynecology"},
			//    new ClinicSpecializationLookup(){ Specialization="Endocrinology"},
			//    new ClinicSpecializationLookup(){ Specialization="Ophthalmology"},
			//    new ClinicSpecializationLookup(){ Specialization="Urology"},
			//    new ClinicSpecializationLookup(){ Specialization="Dental"},
			//};
			//context.ClinicsSpecializationLookups.AddRange(clinicSpecializations);
			//context.SaveChanges();
			#endregion

			


		}
    }
}
