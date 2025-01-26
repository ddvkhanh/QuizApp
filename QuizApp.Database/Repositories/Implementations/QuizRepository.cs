using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizApp.Database.Models;
using QuizApp.Database.Repositories.Interfaces;

namespace QuizApp.Database.Repositories.Implementations
{
    public class QuizRepository:IQuizRepository
    {
        private readonly QuizAppContext _context;

        public QuizRepository(QuizAppContext context) { _context = context; }

        public async Task<List<Question>> GetQuestionsByIdsAsync(List<Guid> questionIds)
        {
            return await _context.Questions
            .Where(q => questionIds.Contains(q.Id))
            .ToListAsync();
        }

        public async Task SaveQuizResultAsync(QuizResult result)
        {
            _context.Add(result);
            await _context.SaveChangesAsync();
        }

    }
}
