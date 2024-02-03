﻿// <auto-generated />
using System;
using LearnMS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LearnMS.API.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240202222927_Migrations_7")]
    partial class Migrations_7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("LearnMS.API.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "ProviderType");

                    b.HasIndex("ProviderId", "ProviderType");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ExpirationDays")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("RenewalPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LearnMS.API.Entities.CourseItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseItem");
                });

            modelBuilder.Entity("LearnMS.API.Entities.CreditCode", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AssistantId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.HasIndex("AssistantId");

                    b.HasIndex("StudentId");

                    b.ToTable("CreditCodes");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Exam", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("RenewalPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Lecture", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("RenewalPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("LearnMS.API.Entities.LectureItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LectureId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.ToTable("LectureItem");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("LearnMS.API.Entities.StudentCourse", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("LearnMS.API.Entities.StudentExam", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ExamId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId", "ExamId");

                    b.HasIndex("ExamId")
                        .IsUnique();

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentExam");
                });

            modelBuilder.Entity("LearnMS.API.Entities.StudentLecture", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LectureId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId", "LectureId");

                    b.HasIndex("LectureId")
                        .IsUnique();

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentLecture");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("LearnMS.API.Entities.Assistant", b =>
                {
                    b.HasBaseType("User");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Assistants");
                });

            modelBuilder.Entity("LearnMS.API.Entities.Student", b =>
                {
                    b.HasBaseType("User");

                    b.Property<decimal>("Credit")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ParentPhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("SchoolName")
                        .HasColumnType("TEXT");

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
