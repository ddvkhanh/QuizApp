
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Database.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion() {
            QuestionType = "multiple";
        }

        [NotMapped]
        public List<string> CorrectAnswers
        {
            get => CorrectAnswer?.Split(',', StringSplitOptions.None).ToList() ?? new List<string>();
            set => CorrectAnswer = string.Join(",", value);
        }
    }
}
