
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion() {
            Type = "MultipleChoice";
        }
        
        [Required]
        public List<string> CorrectAnswer { get; set; } = new List<string>();
    }
}
