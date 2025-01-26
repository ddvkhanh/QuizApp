using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Database.Models;

namespace QuizApp.Database.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        Task<List<Question>> GetQuestionsByIdsAsync(List<Guid> questionIds);
        Task SaveQuizResultAsync(QuizResult result);
    }
}
