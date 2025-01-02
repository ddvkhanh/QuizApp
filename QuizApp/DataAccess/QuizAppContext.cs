using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizApp.Models;

namespace QuizApp.DataAccess
{
    public class QuizAppContext : DbContext
    {
        public QuizAppContext(DbContextOptions<QuizAppContext> options): base (options) {}

        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizResult> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasDiscriminator<string>("QuestionType")
                .HasValue<MultipleChoiceQuestion>("MultipleChoice")
                .HasValue<SingleChoiceQuestion>("SingleChoice");

            modelBuilder.Entity<QuizResult>()
                .HasOne<Quiz>()
                .WithMany()
                .HasForeignKey(r => r.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            //Ignore CorrectAnswers column of MultipleChoice so it stores in only 1 column CorrectAnswer
            modelBuilder.Entity<MultipleChoiceQuestion>()
                .Ignore(mc => mc.CorrectAnswers);
        }

    }
}
