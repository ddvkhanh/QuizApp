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
    public class ResultRepository : IResultRepository
    {
        private readonly QuizAppContext _appContext;
        public ResultRepository(QuizAppContext appContext)
        {
            _appContext = appContext;
        }
        public async Task AddResultAsync(QuizResult result)
        {
            _appContext.Results.Add(result);
            await _appContext.SaveChangesAsync();
        }

        public async Task<List<QuizResult>> GetResultsAsync()
        {
            return await _appContext.Results.ToListAsync();
        }
    }
}
