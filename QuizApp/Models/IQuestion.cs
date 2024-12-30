namespace QuizApp.Models
{
    public interface IQuestion
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public List<string> Options { get; set; }
        public IEnumerable<string> CorrectAnswer { get; set; }
    }
}
