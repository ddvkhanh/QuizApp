using QuizApp.Database.Models;
using QuizApp.Server.Services.Interfaces;
using QuizApp.Database.Repositories.Interfaces;
using System;
using Microsoft.AspNetCore.Http.HttpResults;

namespace QuizApp.Server.Services.Implementations
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionRepository _repository;
        public QuestionsService(IQuestionRepository repository) { 
            _repository = repository;
        }
        public async Task AddQuestionAsync(Question question)
        {
            if (question == null)
            {
                throw new ArgumentException("Question data is null.");
            }

            question.CreatedAt = DateTime.Now;

            await _repository.AddQuestionAsync(question);
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            var question = await _repository.GetQuestionByIdAsync(id);
            if (question == null) 
            {
                throw new KeyNotFoundException($"Question with ID {id} not found.");
            }
            await _repository.DeleteQuestionAsync(question);
        }

        public async Task<Question> GetQuestionByIdAsync(Guid id)
        {
            return await _repository.GetQuestionByIdAsync(id);
        }

        public async Task<List<Question>> GetQuestionsAsync(bool random)
        {
            var questions = await _repository.GetQuestionsAsync();

            if (questions == null || questions.Count() == 0)
            {
                return new List<Question>();
            }

            if (random)
            {
                var randomIndex = new Random();
                for (int i = questions.Count() - 1; i > 0; i--)
                {
                    int j = randomIndex.Next(i + 1);
                    (questions[i], questions[j]) = (questions[j], questions[i]);
                }
            }
            return questions;
        }

        public async Task UpdateQuestionAsync(Guid id, Question updatedQuestion)
        {
            if (id != updatedQuestion.Id)
            {
                throw new ArgumentException("Mismatch between route ID {Id} and payload ID {PayloadId}.\", id, updatedQuestion.Id");
            }

            var existingQuestion = await _repository.GetQuestionByIdAsync(id);
            if (existingQuestion == null)
            {
                throw new KeyNotFoundException($"Question with ID {id} not found.");
            }

            // Update properties
            existingQuestion.Description = updatedQuestion.Description;
            existingQuestion.CorrectAnswer = updatedQuestion.CorrectAnswer;
            existingQuestion.Options = updatedQuestion.Options;
            existingQuestion.QuestionType = updatedQuestion.QuestionType;

            await _repository.UpdateQuestionAsync(existingQuestion);
        }
    }
}
