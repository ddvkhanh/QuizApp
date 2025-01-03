
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Database.Models
{
    public class SingleChoiceQuestion:Question
    {
        public SingleChoiceQuestion() {
            QuestionType = "single";
        }
        [Required]
        public string CorrectAnswer 
        {
            get => base.CorrectAnswer; 
            set => base.CorrectAnswer = value;
        }

    }
}
