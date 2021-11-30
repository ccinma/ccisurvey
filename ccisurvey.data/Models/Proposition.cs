using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Models
{
	public class Proposition
	{
		public Proposition()
		{
			Participants = new List<User>();
		}

		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(75, ErrorMessage = "Le libellé doit faire entre {2} et {1} charactères.", MinimumLength = 1)]
		public string Label { get; set; }
		[Required]
		public List<User> Participants { get; set; }
	}
}
