﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizApp.Database;

#nullable disable

namespace QuizApp.Database.Migrations
{
    [DbContext(typeof(QuizAppContext))]
    [Migration("20250107090334_Create-Initial-Tables")]
    partial class CreateInitialTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuizApp.Database.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("Options")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Id");

                    b.ToTable("Questions");

                    b.HasDiscriminator<string>("QuestionType").HasValue("Question");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("QuizApp.Database.Models.QuizResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<DateTime>("TakenAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("QuizApp.Database.Models.MultipleChoiceQuestion", b =>
                {
                    b.HasBaseType("QuizApp.Database.Models.Question");

                    b.HasDiscriminator().HasValue("multiple");
                });

            modelBuilder.Entity("QuizApp.Database.Models.SingleChoiceQuestion", b =>
                {
                    b.HasBaseType("QuizApp.Database.Models.Question");

                    b.HasDiscriminator().HasValue("single");
                });
#pragma warning restore 612, 618
        }
    }
}
