using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using easysched.Models;

namespace easysched.Data
{
    public partial class easyschedContext : DbContext
    {
        public easyschedContext()
        {
        }

        public easyschedContext(DbContextOptions<easyschedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BillingAddress> BillingAddress { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeAvailability> EmployeeAvailability { get; set; }
        public virtual DbSet<Licencing> Licencing { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Phonetype> Phonetype { get; set; }
        public virtual DbSet<Priveleges> Priveleges { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Shift> Shift { get; set; }
        public virtual DbSet<Weekday> Weekday { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=password;database=easysched", x => x.ServerVersion("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillingAddress>(entity =>
            {
                entity.ToTable("billing address");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("FKBilling Ad812261");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.City)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Province)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Street)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.BillingAddress)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKBilling Ad812261");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyNumber)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("FKCompany872714");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FKCompany872714");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("FKEmployee435943");

                entity.HasIndex(e => e.PrivelegesId)
                    .HasName("FKEmployee833112");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FirstName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PrivelegesId).HasColumnName("PrivelegesID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKEmployee435943");

                entity.HasOne(d => d.Priveleges)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PrivelegesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKEmployee833112");
            });

            modelBuilder.Entity<EmployeeAvailability>(entity =>
            {
                entity.ToTable("employee availability");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("FKEmployee A983524");

                entity.HasIndex(e => e.WeekdayId)
                    .HasName("FKEmployee A593831");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.Property(e => e.WeekdayId).HasColumnName("WeekdayID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeAvailability)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKEmployee A983524");

                entity.HasOne(d => d.Weekday)
                    .WithMany(p => p.EmployeeAvailability)
                    .HasForeignKey(d => d.WeekdayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKEmployee A593831");
            });

            modelBuilder.Entity<Licencing>(entity =>
            {
                entity.ToTable("licencing");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("FKLicencing37786");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Licencing)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKLicencing37786");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("login");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("FKEmployee1312456");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKEmployee1312456");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.ToTable("phone");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("FKPhone588853");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("FKPhone263678");

                entity.HasIndex(e => e.PhoneTypeId)
                    .HasName("FKPhone530938");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.PhoneTypeId).HasColumnName("PhoneTypeID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPhone588853");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FKPhone263678");

                entity.HasOne(d => d.PhoneType)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => d.PhoneTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPhone530938");
            });

            modelBuilder.Entity<Phonetype>(entity =>
            {
                entity.ToTable("phonetype");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Type)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Priveleges>(entity =>
            {
                entity.ToTable("priveleges");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("FKSchedule876714");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSchedule876714");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("shift");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("FKShift123456");

                entity.HasIndex(e => e.ScheduleId)
                    .HasName("FKSchedule125456");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Shift)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKShift123456");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Shift)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("FKSchedule125456");
            });

            modelBuilder.Entity<Weekday>(entity =>
            {
                entity.ToTable("weekday");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
