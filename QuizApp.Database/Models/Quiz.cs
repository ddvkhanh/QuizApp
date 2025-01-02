using System.ComponentModel.DataAnnotations;

namespace QuizApp.Database.Models
{
    public class Quiz
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public List<Question> Questions { get; set; }
        public DateTime CreatedAt { get; set; }
        public TimeSpan? TimeLimit { get; set; }

        public Quiz()
        {
            Questions = new List<Question>();
        }
    }
}
