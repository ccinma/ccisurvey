using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Models
{
    public class User
    {
        public User()
        {
            CreatedAt = DateTime.Now;
            Surveys = new List<Survey>();
            Propositions = new List<Proposition>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public List<Survey> Surveys { get; set; }

        [Required]
        public List<Proposition> Propositions { get; set; }
    }
}
