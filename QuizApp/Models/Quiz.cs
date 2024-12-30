namespace QuizApp.Models
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<IQuestion> Questions { get; set; }
        public DateTime CreatedAt { get; set; }
        public TimeSpan? TimeLimit { get; set; }

        public Quiz()
        {
            Questions = new List<IQuestion>();
        }
    }
}
