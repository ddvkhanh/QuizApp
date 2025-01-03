using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace QuizApp.Database
{
    public class QuizAppContextFactory : IDesignTimeDbContextFactory<QuizAppContext>
    {
        public QuizAppContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<QuizAppContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));

            return new QuizAppContext(optionsBuilder.Options);
        }
    }
}
