
namespace QuizApp.Models
{
    public class MultipleChoiceQuestion : IQuestion
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = "MultipleChoice";
        public string Description { get; set; }
        public List<string> Options { get; set; }
        public IEnumerable<string> CorrectAnswers { get; set; }


        public MultipleChoiceQuestion()
        {
            Options = new List<string>();
            CorrectAnswers = new List<string>();
        }
    }
}
