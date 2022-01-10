using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.services.VMs
{
    public class LoginViewModel
    {
		[Required(ErrorMessage = "Adresse e-mail requise.")]
		[EmailAddress(ErrorMessage = "Adresse e-mail non valide.")]
		[Display(Name = "Adresse e-mail")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Mot de passe requis.")]
		[DataType(DataType.Password)]
		[Display(Name = "Mot de passe")]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }
	}
}
