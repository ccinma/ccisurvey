using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.services.VMs
{
	public class AddParticipantViewModel
	{
		[Required(ErrorMessage = "Champs requis.")]
		[EmailAddress(ErrorMessage = "Adresse e-mail non valide.")]
		public string Email { get; set; }
	}
}
