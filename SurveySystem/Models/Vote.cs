using System;

namespace SurveySystem.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacja do wybranej opcji
        public int OptionId { get; set; }
        public Option? Option { get; set; }

        // Relacja do ankiety (ułatwia sprawdzanie podwójnego głosu)
        public int SurveyId { get; set; }
        public Survey? Survey { get; set; }

        // Identyfikator respondenta, który oddał głos
        public string UserId { get; set; } = string.Empty;
    }
}