using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CMX_api.Models;

public partial class CmxContext : DbContext
{
    public CmxContext()
    {
    }

    public CmxContext(DbContextOptions<CmxContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizAttendance> QuizAttendances { get; set; }

    public virtual DbSet<StudentAssignment> StudentAssignments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost;Database=CMX;Integrated security=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E77689DA3DB");

            entity.ToTable("Assignment");

            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.File).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Course).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assignmen__Cours__33D4B598");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D71A7C5052A72");

            entity.ToTable("Course");

            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Lecturer).WithMany(p => p.Courses)
                .HasForeignKey(d => d.LecturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Course__Lecturer__34C8D9D1");
        });

        modelBuilder.Entity<CourseEnrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__CourseEn__7F68771B504C8F7B");

            entity.ToTable("CourseEnrollment");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CourseEnr__Cours__35BCFE0A");

            entity.HasOne(d => d.Student).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CourseEnr__Stude__36B12243");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FAC93C296AC");

            entity.ToTable("Question");

            entity.Property(e => e.CorrectOption).HasMaxLength(1);
            entity.Property(e => e.Question1).HasColumnName("Question");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Question__QuizId__37A5467C");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PK__Quiz__8B42AE8E630794D4");

            entity.ToTable("Quiz");

            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Course).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quiz__CourseId__38996AB5");
        });

        modelBuilder.Entity<QuizAttendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__QuizAtte__8B69261C038860C0");

            entity.ToTable("QuizAttendance");

            entity.HasIndex(e => new { e.QuizId, e.StudentId }, "UC_QuizAttendance").IsUnique();

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizAttendances)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizAtten__QuizI__398D8EEE");

            entity.HasOne(d => d.Student).WithMany(p => p.QuizAttendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizAtten__Stude__3A81B327");
        });

        modelBuilder.Entity<StudentAssignment>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__StudentA__449EE12594D4395F");

            entity.ToTable("StudentAssignment");

            entity.HasIndex(e => new { e.AssignmentId, e.StudentId }, "UC_StudentAssignment").IsUnique();

            entity.Property(e => e.File).HasMaxLength(255);
            entity.Property(e => e.SubmissionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Assignment).WithMany(p => p.StudentAssignments)
                .HasForeignKey(d => d.AssignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentAs__Assig__3B75D760");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentAssignments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentAs__Stude__3C69FB99");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C9FB1834E");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
