﻿// <auto-generated />
using System;
using LearnMS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240207114818_Migrations_5")]
    partial class Migrations_5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LearnMS.API.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("ProviderType")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PasswordResetTokenExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.Property<string>("ProviderId")
                        .HasColumnType("text");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id", "ProviderType");

                    b.HasIndex("ProviderId", "ProviderType");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("ExpirationDays")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("RenewalPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LearnMS.API.Entities.CourseItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("CourseItem");
                });

            modelBuilder.Entity("LearnMS.API.Entities.CreditCode", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid?>("AssistantId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Code");

                    b.ToTable("CreditCodes");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Exam", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("RenewalPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Lecture", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("ExpirationDays")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("RenewalPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("LearnMS.API.Entities.LectureItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LectureId")
                        .HasColumnType("uuid");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("LectureItem");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VideoSrc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("LearnMS.API.Entities.StudentCourse", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("StudentId", "CourseId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("LearnMS.API.Entities.StudentExam", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExamId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("StudentId", "ExamId");

                    b.ToTable("StudentExam");
                });

            modelBuilder.Entity("LearnMS.API.Entities.StudentLecture", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LectureId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("StudentId", "LectureId");

                    b.ToTable("StudentLecture");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("LearnMS.API.Entities.Assistant", b =>
                {
                    b.HasBaseType("User");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Assistants");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Student", b =>
                {
                    b.HasBaseType("User");

                    b.Property<decimal>("Credit")
                        .HasColumnType("numeric");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("ParentPhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Teacher", b =>
                {
                    b.HasBaseType("User");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Account", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnMS.API.Entities.CourseItem", b =>
                {
                    b.HasOne("LearnMS.API.Entities.Course", null)
                        .WithMany("Items")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LearnMS.API.Entities.CreditCode", b =>
                {
                    b.HasOne("LearnMS.API.Entities.Assistant", null)
                        .WithMany()
                        .HasForeignKey("AssistantId");

                    b.HasOne("LearnMS.API.Entities.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Exam", b =>
                {
                    b.HasOne("LearnMS.API.Entities.CourseItem", null)
                        .WithOne()
                        .HasForeignKey("LearnMS.API.Entities.Exam", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LearnMS.API.Entities.Lecture", b =>
                {
                    b.HasOne("LearnMS.API.Entities.CourseItem", null)
                        .WithOne()
                        .HasForeignKey("LearnMS.API.Entities.Lecture", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LearnMS.API.Entities.LectureItem", b =>
                {
                    b.HasOne("LearnMS.API.Entities.Lecture", null)
                        .WithMany("Items")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LearnMS.API.Entities.Lesson", b =>
                {
                    b.HasOne("LearnMS.API.Entities.LectureItem", null)
                        .WithOne()
                        .HasForeignKey("LearnMS.API.Entities.Lesson", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LearnMS.API.Entities.StudentCourse", b =>
                {
                    b.HasOne("LearnMS.API.Entities.Course", null)
                        .WithOne()
                        .HasForeignKey("LearnMS.API.Entities.StudentCourse", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnMS.API.Entities.Student", null)
                        .WithOne()
                        .HasForeignKey("LearnMS.API.Entities.StudentCourse", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LearnMS.API.Entities.StudentExam", b =>
                {
                    b.HasOne("LearnMS.API.Entities.Exam", null)
                        .WithOne()
                        .HasForeignKey("LearnMS.API.Entities.StudentExam", "ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnMS.API.Entities.Student", null)
                        .WithOne()
                        .HasForeignKey("LearnMS.API.Entities.StudentExam", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LearnMS.API.Entities.StudentLecture", b =>
                {
                    b.HasOne("LearnMS.API.Entities.Lecture", null)
                        .WithOne()
                        .HasForeignKey("LearnMS.API.Entities.StudentLecture", "LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnMS.API.Entities.Student", null)
                        .WithOne()
                        .HasForeignKey("LearnMS.API.Entities.StudentLecture", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LearnMS.API.Entities.Course", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Lecture", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}