using DALProject.Data.Contexts;
using DALProject.model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
	public class ActiveSubstanceViewModel
	{
		public ActiveSubstanceViewModel()
		{
		}
		public ActiveSubstanceViewModel(ActiveSubstance activeSubstance)
		{
			Id = activeSubstance.Id;
			ActiveSubstancesName = activeSubstance.ActiveSubstancesName;

			Medications = activeSubstance.Medications.ToList();

			Interactions = activeSubstance.ActSub1.Select(interaction => new ActiveSubstanceInteractionViewModel
			{
				Interaction = interaction.Interaction,
				OtherSubstanceName = interaction.ActSub2?.ActiveSubstancesName ?? "Unknown",
				ActSubId=interaction.ActiveSubstanceId2??0,
			})
			.Concat(activeSubstance.ActSub2.Select(interaction => new ActiveSubstanceInteractionViewModel
			{
				Interaction = interaction.Interaction,
				OtherSubstanceName = interaction.ActSub1?.ActiveSubstancesName ?? "Unknown",
				ActSubId = interaction.ActiveSubstanceId1 ?? 0

			}))
			.ToList();
		}
		public int Id { get; set; }

		[Required(ErrorMessage = "Active Substance is Required"), Display(Name = "Active Substance Name")]
		public string ActiveSubstancesName { get; set; } = null!;

		public HashSet<int>? MedicationId { get; set; } = new HashSet<int>();

		public List<Medication>? Medications { get; set; } = new List<Medication>();
		public List<ActiveSubstanceInteractionViewModel>? Interactions { get; set; } = new List<ActiveSubstanceInteractionViewModel>();

		public static explicit operator ActiveSubstance(ActiveSubstanceViewModel viewModel)
		{
			var activeSubstance = new ActiveSubstance
			{
				ActiveSubstancesName = viewModel.ActiveSubstancesName,
				Medications = viewModel.Medications
			};
			foreach (var Interaction in viewModel.Interactions)
			{
				activeSubstance.ActSub1.Add(new ActiveSubstanceInteraction
				{
					ActiveSubstanceId2 = Interaction.ActSubId,
					Interaction = Interaction.Interaction
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
