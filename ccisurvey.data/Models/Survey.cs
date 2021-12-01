using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Models
{
	public class Survey
	{
		public Survey()
		{
			IsPublic = false;
			Propositions = new List<Proposition>() { };
			CreatedAt = DateTime.Now;
			IsClosed = false;
			IsAnonymous = false;
			IsMultipleChoice = false;
			Participants = new List<User>() { };
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(75, MinimumLength = 1)]
		public string Label { get; set; }

		[StringLength(500)]
		public string Description { get; set; }

		public bool IsPublic { get; set; }

		[Required]
		public List<Proposition> Propositions { get; set; }

		[Required]
		public User Creator { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		public DateTime EndedAt { get; set; }

		[Required]
		public bool IsClosed { get; set; }

		[Required]
		public List<User> Participants { get; set; }

		[Required]
		public bool IsAnonymous { get; set; }

		[Required]
		public bool IsMultipleChoice { get; set; }
	}
}
