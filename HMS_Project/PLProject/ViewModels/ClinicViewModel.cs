//using DALProject.Data.Migrations;
using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
	public class ClinicViewModel
	{
		public ClinicViewModel() { }
		public ClinicViewModel(Clinic clinic)
		{
			Id = clinic.Id;
			Name = clinic.Name;
			Phone = clinic.Phone;
			Price = clinic.Price;
			Specialization = clinic.Specialization;
			Doctors = clinic.Doctors.ToList();
			Nurses = clinic.Nurses.ToList();
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Name is Required")]
		[Display(Name = "Clinic Name")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "Phone Number Is Required")]
		[Display(Name = "Phone Number")]
		[Phone(ErrorMessage = "The phone number you entered is invalid.")]
		[MaxLength(11, ErrorMessage = "Phone Number Can be a maximum of 11 numbers")]
		[MinLength(10, ErrorMessage = "Phone Number Can be a minimum of 10 numbers")]
		public string Phone { get; set; } = null!;

		[Required(ErrorMessage = " Please Choose a Specilization for this Clinic.")]
		public string Specialization { get; set; } = null!;

		[Required(ErrorMessage = "Please specify the price.")]
		[Display(Name = "Price per Visit")]
		public double? Price { get; set; }
		public List<int>? DoctorsAddedId { get; set; } = new List<int>();
		public List<int>? NursesAddedId { get; set; } = new List<int>();
		public List<Doctor>? Doctors { get; set; }= new List<Doctor>();
		public List<Nurse>? Nurses { get; set; }= new List<Nurse>();

		public static explicit operator Clinic(ClinicViewModel _clinicViewModel)
		{
			return new Clinic()
			{
				Id = _clinicViewModel.Id,
				Name = _clinicViewModel.Name,
				Phone = _clinicViewModel.Phone,
				Price = _clinicViewModel.Price ?? default,
				Specialization = _clinicViewModel.Specialization,
				Nurses = _clinicViewModel.Nurses,
				Doctors = _clinicViewModel.Doctors,
			};
		}

		public static explicit operator ClinicViewModel(Clinic _clinic)
		{
			return new ClinicViewModel(_clinic);
		}
	}
}
