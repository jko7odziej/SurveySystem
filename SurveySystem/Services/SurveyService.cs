using Microsoft.EntityFrameworkCore;
using SurveySystem.Data;
using SurveySystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveySystem.Services
{
    public record SurveyResultDto(string OptionText, int VoteCount, double Percentage);

    public class SurveyService
    {
        private readonly ApplicationDbContext _context;

        public SurveyService(ApplicationDbContext context)
        {
            _context = context;
        }

        // METODA DLA ANKIETERA: Tworzenie ankiety
        public async Task<int> CreateSurveyAsync(string title, List<string> options, string userId)
        {
            var survey = new Survey
            {
                Title = title,
                CreatedById = userId,
                Options = options.Select(text => new Option { Text = text }).ToList()
            };

            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();

            return survey.Id;
        }

        // METODA DLA RESPONDENTA: Pobieranie ankiety do głosowania
        public async Task<Survey?> GetSurveyAsync(int surveyId)
        {
            return await _context.Surveys
                .Include(s => s.Options)
                .FirstOrDefaultAsync(s => s.Id == surveyId);
        }

        // METODA DLA RESPONDENTA: Oddawanie głosu
        public async Task<bool> CastVoteAsync(int surveyId, int optionId, string userId)
        {
            bool hasVoted = await _context.Votes
                .AnyAsync(v => v.SurveyId == surveyId && v.UserId == userId);

            if (hasVoted) return false;

            var vote = new Vote
            {
                SurveyId = surveyId,
                OptionId = optionId,
                UserId = userId
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return true;
        }

        //  METODA DLA RESPONDENTA: Generowanie wyników dla wykresu
        public async Task<List<SurveyResultDto>> GetResultsAsync(int surveyId)
        {
            var totalVotes = await _context.Votes.CountAsync(v => v.SurveyId == surveyId);

            if (totalVotes == 0)
            {
                return new List<SurveyResultDto>();
            }

            var results = await _context.Options
                .Where(o => o.SurveyId == surveyId)
                .Select(o => new SurveyResultDto(
                    o.Text,
                    o.Votes.Count,
                    (double)o.Votes.Count / totalVotes * 100))
                .ToListAsync();

            return results ?? new List<SurveyResultDto>();
        }
        public async Task<List<Survey>> GetAllSurveysAsync()
        {
            return await _context.Surveys.ToListAsync();
        }
    }
}