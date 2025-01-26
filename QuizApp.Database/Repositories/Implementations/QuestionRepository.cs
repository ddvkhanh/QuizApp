using Microsoft.EntityFrameworkCore;
using QuizApp.Database.Models;
using QuizApp.Database.Repositories.Interfaces;

namespace QuizApp.Database.Repositories.Implementations
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizAppContext _context;
        public QuestionRepository(QuizAppContext context)
        {
            _context = context;
        }
        public async Task AddQuestionAsync(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(Question question)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(Guid id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _context.Questions.OrderBy(q => q.CreatedAt).ToListAsync();
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }
    }
}
