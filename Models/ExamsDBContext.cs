using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorTEST.Models
{
    public partial class ExamsDBContext : DbContext
    {
        public ExamsDBContext()
        {
        }

        public ExamsDBContext(DbContextOptions<ExamsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<ExamHistory> ExamHistory { get; set; }
        public virtual DbSet<ExamHistoryAnswers> ExamHistoryAnswers { get; set; }
        public virtual DbSet<ExamHistoryDetails> ExamHistoryDetails { get; set; }
        public virtual DbSet<Exams> Exams { get; set; }
        public virtual DbSet<FlawlessView> FlawlessView { get; set; }
        public virtual DbSet<PassedPercentage> PassedPercentage { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<TakenCountByDate> TakenCountByDate { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ExamsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.AnswerText).IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Answer__Question__412EB0B6");
            });

            modelBuilder.Entity<ExamHistory>(entity =>
            {
                entity.Property(e => e.TakenDate).HasColumnType("datetime");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamHistory)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK__ExamHisto__ExamI__440B1D61");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExamHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ExamHisto__UserI__44FF419A");
            });

            modelBuilder.Entity<ExamHistoryAnswers>(entity =>
            {
                entity.HasKey(e => e.ExamAnswerId)
                    .HasName("PK__ExamHist__CD77DFF655F7AF2F");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.ExamLineId).HasColumnName("ExamLineID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.ExamHistoryAnswers)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK__ExamHisto__Answe__4E88ABD4");

                entity.HasOne(d => d.ExamLine)
                    .WithMany(p => p.ExamHistoryAnswers)
                    .HasForeignKey(d => d.ExamLineId)
                    .HasConstraintName("FK__ExamHisto__ExamL__4D94879B");
            });

            modelBuilder.Entity<ExamHistoryDetails>(entity =>
            {
                entity.HasKey(e => e.ExamLineId)
                    .HasName("PK__ExamHiso__39A09972AC6614E2");

                entity.Property(e => e.ExamLineId).HasColumnName("ExamLineID");

                entity.HasOne(d => d.ExamHistory)
                    .WithMany(p => p.ExamHistoryDetails)
                    .HasForeignKey(d => d.ExamHistoryId)
                    .HasConstraintName("FK__ExamHisot__ExamH__49C3F6B7");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ExamHistoryDetails)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__ExamHisot__Quest__4AB81AF0");
            });

            modelBuilder.Entity<Exams>(entity =>
            {
                entity.HasKey(e => e.ExamId)
                    .HasName("PK__Exams__297521A758E6B69A");

                entity.Property(e => e.ExamId).HasColumnName("ExamID");

                entity.Property(e => e.Decription).IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("TITLE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Exams__UserId__3B75D760");
            });

            modelBuilder.Entity<FlawlessView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("FlawlessView");

                entity.Property(e => e.FlawlessDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PassedPercentage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PassedPercentage");

                entity.Property(e => e.PassedPercentage1).HasColumnName("PassedPercentage");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Question__0DC06FACFF28301F");

                entity.Property(e => e.ExamId).HasColumnName("ExamID");

                entity.Property(e => e.QuestionText).IsUnicode(false);

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK__Questions__ExamI__3E52440B");
            });

            modelBuilder.Entity<TakenCountByDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TakenCountByDate");

                entity.Property(e => e.DateCreated).HasColumnType("date");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CCAC344596AE");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Users__A9D105342A32811A")
                    .IsUnique();

                entity.HasIndex(e => e.UserLogin)
                    .HasName("UQ__Users__7F8E8D5EE090A3CA")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
