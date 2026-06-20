using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveySystem.Models
{
    public class Option
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        // Relacja do ankiety
        public int SurveyId { get; set; }
        public Survey? Survey { get; set; }

        // Relacja do głosów
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}