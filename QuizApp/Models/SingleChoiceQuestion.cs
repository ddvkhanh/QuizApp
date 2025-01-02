
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class SingleChoiceQuestion:Question
    {
        public SingleChoiceQuestion() {
            Type = "SingleChoice";
        }
        [Required]
        public string CorrectAnswer 
        {
            get => base.CorrectAnswer; 
            set => base.CorrectAnswer = value;
        }

    }
}
