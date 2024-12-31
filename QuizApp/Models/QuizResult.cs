using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class QuizResult 
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid QuizId { get; set; }
        public int Score { get; set; }
        public DateTime TakenAt { get; set; }
    }
}
