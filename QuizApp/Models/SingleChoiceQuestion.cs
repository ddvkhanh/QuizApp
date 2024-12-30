
namespace QuizApp.Models
{
    public class SingleChoiceQuestion:IQuestion
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = "SingleChoice";
        public string Description { get; set; }
        public List<string> Options { get; set; }
        public IEnumerable<string> CorrectAnswer { get; set; }

        public SingleChoiceQuestion()
        {
            Options = new List<string>();
        }
    }
}
