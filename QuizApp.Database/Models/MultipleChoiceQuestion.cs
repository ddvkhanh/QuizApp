
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Database.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion() {
            Type = "MultipleChoice";
        }

        public List<string> CorrectAnswers
        {
            get => CorrectAnswer?.Split(',', StringSplitOptions.None).ToList() ?? new List<string>();
            set => CorrectAnswer = string.Join(",", value);
        }
    }
}
