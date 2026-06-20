using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.Options;

namespace SurveySystem.Models
{
    public class Survey
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ICollection<Option> Options { get; set; } = new List<Option>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}