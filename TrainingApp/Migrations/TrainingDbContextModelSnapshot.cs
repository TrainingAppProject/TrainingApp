﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainingApp.Services;

#nullable disable

namespace TrainingApp.Migrations
{
    [DbContext(typeof(TrainingDbContext))]
    partial class TrainingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<Guid>("CreatedID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExaminerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ExaminerSigned")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsTaskMandatory")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModifiedID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OverallGrade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<Guid>("TemplateID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TraineeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("TraineeSigned")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentElementDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssessmentGradeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssessmentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssessmentTaskID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderNo")
                        .HasColumnType("int");

                    b.Property<Guid>("TemplateElementID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentID");

                    b.ToTable("AssessmentElements");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentGradeDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssessmentElementID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AssessmentElementID")
                        .IsUnique();

                    b.ToTable("AssessmentGrades");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentGradeElementDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssessmentGradeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFail")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AssessmentGradeID");

                    b.ToTable("AssessmentGradeElements");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentTaskDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssessmentElementID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentElementID")
                        .IsUnique();

                    b.ToTable("AssessmentTasks");
                });

            modelBuilder.Entity("TrainingApp.DTOs.CompanyDTO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("TrainingApp.DTOs.GradeDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GradeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("TrainingApp.DTOs.GradeElementDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GradeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsFail")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("GradeID");

                    b.ToTable("GradeElements");
                });

            modelBuilder.Entity("TrainingApp.DTOs.TaskDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TrainingApp.DTOs.TemplateDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int>("CreatedID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GradeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsTaskMandatory")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScriptNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("GradeID");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("TrainingApp.DTOs.TemplateElementDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderNo")
                        .HasColumnType("int");

                    b.Property<Guid>("TaskID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TemplateID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TaskID");

                    b.HasIndex("TemplateID");

                    b.ToTable("TemplateElements");
                });

            modelBuilder.Entity("TrainingApp.DTOs.UserDTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("UserCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentElementDTO", b =>
                {
                    b.HasOne("TrainingApp.DTOs.AssessmentDTO", "Assessment")
                        .WithMany("AssessmentElements")
                        .HasForeignKey("AssessmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assessment");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentGradeDTO", b =>
                {
                    b.HasOne("TrainingApp.DTOs.AssessmentElementDTO", "AssessmentElement")
                        .WithOne("Grade")
                        .HasForeignKey("TrainingApp.DTOs.AssessmentGradeDTO", "AssessmentElementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssessmentElement");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentGradeElementDTO", b =>
                {
                    b.HasOne("TrainingApp.DTOs.AssessmentGradeDTO", "AssessmentGrade")
                        .WithMany("AssessmentGradeElements")
                        .HasForeignKey("AssessmentGradeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssessmentGrade");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentTaskDTO", b =>
                {
                    b.HasOne("TrainingApp.DTOs.AssessmentElementDTO", "AssessmentElement")
                        .WithOne("Task")
                        .HasForeignKey("TrainingApp.DTOs.AssessmentTaskDTO", "AssessmentElementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssessmentElement");
                });

            modelBuilder.Entity("TrainingApp.DTOs.GradeElementDTO", b =>
                {
                    b.HasOne("TrainingApp.DTOs.GradeDTO", "Grade")
                        .WithMany("Elements")
                        .HasForeignKey("GradeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");
                });

            modelBuilder.Entity("TrainingApp.DTOs.TemplateDTO", b =>
                {
                    b.HasOne("TrainingApp.DTOs.CompanyDTO", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingApp.DTOs.GradeDTO", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Grade");
                });

            modelBuilder.Entity("TrainingApp.DTOs.TemplateElementDTO", b =>
                {
                    b.HasOne("TrainingApp.DTOs.TaskDTO", "Task")
                        .WithMany()
                        .HasForeignKey("TaskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingApp.DTOs.TemplateDTO", "Template")
                        .WithMany("Elements")
                        .HasForeignKey("TemplateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("TrainingApp.DTOs.UserDTO", b =>
                {
                    b.HasOne("TrainingApp.DTOs.CompanyDTO", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentDTO", b =>
                {
                    b.Navigation("AssessmentElements");
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentElementDTO", b =>
                {
                    b.Navigation("Grade")
                        .IsRequired();

                    b.Navigation("Task")
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingApp.DTOs.AssessmentGradeDTO", b =>
                {
                    b.Navigation("AssessmentGradeElements");
                });

            modelBuilder.Entity("TrainingApp.DTOs.GradeDTO", b =>
                {
                    b.Navigation("Elements");
                });

            modelBuilder.Entity("TrainingApp.DTOs.TemplateDTO", b =>
                {
                    b.Navigation("Elements");
                });
#pragma warning restore 612, 618
        }
    }
}
