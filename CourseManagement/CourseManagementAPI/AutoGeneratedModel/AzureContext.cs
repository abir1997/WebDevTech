﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CourseManagementAPI.AutoGeneratedModel
{
    public partial class AzureContext : DbContext
    {
        public AzureContext()
        {
        }

        public AzureContext(DbContextOptions<AzureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseAssignment> CourseAssignments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=rmit-wdt-2021.australiasoutheast.cloudapp.azure.com;Database=s3677701;uid=s3677701;pwd=abc123;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountNumber);

                entity.ToTable("Account");

                entity.Property(e => e.AccountNumber).ValueGeneratedNever();

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Customer");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId, "IX_Courses_DepartmentID");

                entity.Property(e => e.CourseId)
                    .ValueGeneratedNever()
                    .HasColumnName("CourseID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.DepartmentId);
            });

            modelBuilder.Entity<CourseAssignment>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.InstructorId });

                entity.HasIndex(e => e.InstructorId, "IX_CourseAssignments_InstructorID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseAssignments)
                    .HasForeignKey(d => d.CourseId);

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.CourseAssignments)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostCode).HasMaxLength(4);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.InstructorId, "IX_Departments_InstructorID");

                entity.Property(e => e.DepartmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("DepartmentID");

                entity.Property(e => e.Budget).HasColumnType("money");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.InstructorId);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasIndex(e => e.CourseId, "IX_Enrollments_CourseID");

                entity.HasIndex(e => e.StudentId, "IX_Enrollments_StudentID");

                entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId);
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.Property(e => e.InstructorId)
                    .ValueGeneratedNever()
                    .HasColumnName("InstructorID");

                entity.Property(e => e.FirstMidName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.LoginId)
                    .HasMaxLength(8)
                    .HasColumnName("LoginID")
                    .IsFixedLength(true);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Login_Customer");
            });

            modelBuilder.Entity<OfficeAssignment>(entity =>
            {
                entity.HasKey(e => e.InstructorId);

                entity.Property(e => e.InstructorId)
                    .ValueGeneratedNever()
                    .HasColumnName("InstructorID");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.HasOne(d => d.Instructor)
                    .WithOne(p => p.OfficeAssignment)
                    .HasForeignKey<OfficeAssignment>(d => d.InstructorId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId)
                    .ValueGeneratedNever()
                    .HasColumnName("StudentID");

                entity.Property(e => e.FirstMidName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.TransactionAccountNumberNavigations)
                    .HasForeignKey(d => d.AccountNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.DestinationAccountNumberNavigation)
                    .WithMany(p => p.TransactionDestinationAccountNumberNavigations)
                    .HasForeignKey(d => d.DestinationAccountNumber);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}