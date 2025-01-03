using Microsoft.EntityFrameworkCore;
using QuizApp.Database.Models;

namespace QuizApp.Database
{
    public class QuizAppContext : DbContext
    {
        // Constructor to accept DbContextOptions
        public QuizAppContext(DbContextOptions<QuizAppContext> options) : base(options) { }

        // DbSets for the application
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizResult> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure inheritance for questions (e.g., SingleChoice and MultipleChoice)
            modelBuilder.Entity<Question>()
                .HasDiscriminator<string>("QuestionType")
                .HasValue<SingleChoiceQuestion>("single")
                .HasValue<MultipleChoiceQuestion>("multiple");

            base.OnModelCreating(modelBuilder);
        }
    }
}
