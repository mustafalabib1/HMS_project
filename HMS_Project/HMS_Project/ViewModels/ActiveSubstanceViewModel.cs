using DALProject.Data.Contexts;
using DALProject.model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
	public class ActiveSubstanceViewModel
	{
		public ActiveSubstanceViewModel()
		{
		}
		public ActiveSubstanceViewModel(ActiveSubstance activeSubstance)
		{
			Id = activeSubstance.ActiveSubstancesId;
			ActiveSubstancesName = activeSubstance.ActiveSubstancesName;

			Medications = activeSubstance.Medications.ToHashSet();

			Interactions = activeSubstance.ActSub1.Select(interaction => new ActiveSubstanceInteractionViewModel
			{
				Interaction = interaction.Interaction,
				OtherSubstanceName = interaction.ActSub2?.ActiveSubstancesName ?? "Unknown"
			})
			.Concat(activeSubstance.ActSub2.Select(interaction => new ActiveSubstanceInteractionViewModel
			{
				Interaction = interaction.Interaction,
				OtherSubstanceName = interaction.ActSub1?.ActiveSubstancesName ?? "Unknown"
			}))
			.ToHashSet();
		}
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is Required"), Display(Name = "Active Substances Name")]
		public string ActiveSubstancesName { get; set; } = null!;
		public IEnumerable<ActiveSubstance>? ActiveSubstancesDateReader { get; set; }
		public IEnumerable<Medication>? MedicationsDateReader { get; set; }
		public ICollection<Medication> Medications { get; set; }
		public ICollection<ActiveSubstanceInteractionViewModel>? Interactions { get; set; }

		public static explicit operator ActiveSubstance(ActiveSubstanceViewModel viewModel)
		{
			var activeSubstance = new ActiveSubstance
			{
				ActiveSubstancesName = viewModel.ActiveSubstancesName,
				Medications = viewModel.Medications
			};
			foreach (var interactionViewModel in viewModel.Interactions)
			{
				activeSubstance.ActSub1.Add(new ActiveSubstanceInteraction
				{
					Interaction = interactionViewModel.Interaction,
					ActiveSubstanceId2 = interactionViewModel.ActSubId
				});
			}
			return activeSubstance;
		}

		public static explicit operator ActiveSubstanceViewModel(ActiveSubstance activeSubstance)
		{
			return new ActiveSubstanceViewModel(activeSubstance);
		}
	}
}
