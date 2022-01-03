using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.services.VMs
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Pseudo requis.")]
		[Display(Name = "Pseudo")]
		[StringLength(20, MinimumLength = 3, ErrorMessage ="Le pseudo ne peut contenir qu'entre 3 et 20 charactères.")]
		[RegularExpression("([a-zA-Z0-9_]+)", ErrorMessage = "Le pseudo ne peut contenir uniquement minuscules, majuscules, underscores et nombres.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Adresse e-mail requise.")]
		[EmailAddress(ErrorMessage = "Adresse e-mail non valide.")]
		[Display(Name = "Adresse e-mail")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Mot de passe requis.")]
		[DataType(DataType.Password)]
		[Display(Name = "Mot de passe")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Veuillez répéter votre mot de passe.")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
		[Display(Name = "Répéter mot de passe")]
		public string PasswordRepeat { get; set; }
	}
}
