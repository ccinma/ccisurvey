using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.services.VMs
{
	public class CreateSurveyViewModel
	{
		[Required]
		[StringLength(75, MinimumLength = 1)]
		public string Label { get; set; }

		[StringLength(500)]
		public string Description { get; set; }

		[Required]
		public bool IsMultipleChoice { get; set; }
	}
}
