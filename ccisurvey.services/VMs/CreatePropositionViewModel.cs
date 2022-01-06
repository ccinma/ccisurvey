using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.services.VMs
{
	public class CreatePropositionViewModel
	{
		[Required]
		[StringLength(75, ErrorMessage = "Le libellé doit faire entre {2} et {1} charactères.", MinimumLength = 1)]
		public string Label { get; set; }
	}
}
