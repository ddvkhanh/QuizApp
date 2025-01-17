﻿using System.ComponentModel.DataAnnotations;

namespace QuizApp.Database.Models
{
    public abstract class Question
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string QuestionType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<string> Options { get; set; }
        [Required]
        public string CorrectAnswer { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
