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

            //modelBuilder.Entity<MultipleChoiceQuestion>()
            //.Property(mc => mc.CorrectAnswer)
            //.HasConversion(
            //    v => string.Join(',', v),                // Serialize List<string> to a string
            //    v => v.Split(',', StringSplitOptions.None).ToList()) // Deserialize string to List<string>
            //.Metadata.SetValueComparer(new ValueComparer<List<string>>(
            //    (c1, c2) => c1.SequenceEqual(c2),        
            //    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), 
            //    c => c.ToList()));

            //modelBuilder.Entity<SingleChoiceQuestion>()
            //    .Property(q => q.CorrectAnswer)
            //    .HasColumnName("CorrectAnswer");

            modelBuilder.Entity<QuizResult>()
                .HasOne<Quiz>()
                .WithMany()
                .HasForeignKey(r => r.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
