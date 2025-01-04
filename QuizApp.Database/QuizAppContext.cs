using Microsoft.EntityFrameworkCore;
using QuizApp.Database.Models;

namespace QuizApp.Database
{
    public class QuizAppContext : DbContext
    {
        public QuizAppContext(DbContextOptions<QuizAppContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizResult> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasDiscriminator<string>("QuestionType")
                .HasValue<SingleChoiceQuestion>("single")
                .HasValue<MultipleChoiceQuestion>("multiple");

            base.OnModelCreating(modelBuilder);
        }
    }
}
