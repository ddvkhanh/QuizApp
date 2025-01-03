using System.ComponentModel.DataAnnotations;

namespace QuizApp.Database.Models
{
    public class QuizResult 
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public int Score { get; set; }

        [Required]
        public DateTime TakenAt { get; set; }
    }
}
