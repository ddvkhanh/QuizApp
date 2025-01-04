namespace QuizApp.Server.Dtos
{
    public class AnswerSubmission
    {
        public Guid QuestionId { get; set; }
        public string Answer {  get; set; }
    }
}
