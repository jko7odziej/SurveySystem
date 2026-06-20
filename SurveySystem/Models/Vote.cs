using System;

namespace SurveySystem.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int OptionId { get; set; }
        public Option? Option { get; set; }
        public int SurveyId { get; set; }
        public Survey? Survey { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}