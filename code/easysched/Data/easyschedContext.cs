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

        public virtual DbSet<Access> Access { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Companylicence> Companylicence { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Licence> Licence { get; set; }
        public virtual DbSet<Phonenumber> Phonenumber { get; set; }
        public virtual DbSet<Phonetype> Phonetype { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=easysched;password=easysched2020;database=easysched", x => x.ServerVersion("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Access>(entity =>
            {
                entity.ToTable("access");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Rights)
                    .IsRequired()
                    .HasColumnName("rights")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.HasIndex(e => e.CompanyLicenseId)
                    .HasName("companyLicenseId");

                entity.HasIndex(e => e.PhoneNumberId)
                    .HasName("phoneNumberId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CompanyLicenseId).HasColumnName("companyLicenseId");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneNumberId).HasColumnName("phoneNumberId");

                entity.HasOne(d => d.CompanyLicense)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CompanyLicenseId)
                    .HasConstraintName("company_ibfk_1");

                entity.HasOne(d => d.PhoneNumber)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.PhoneNumberId)
                    .HasConstraintName("company_ibfk_2");
            });

            modelBuilder.Entity<Companylicence>(entity =>
            {
                entity.ToTable("companylicence");

                entity.HasIndex(e => e.LicenceId)
                    .HasName("licenceId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("date");

                entity.Property(e => e.LicenceId).HasColumnName("licenceId");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("date");

                entity.HasOne(d => d.Licence)
                    .WithMany(p => p.Companylicence)
                    .HasForeignKey(d => d.LicenceId)
                    .HasConstraintName("companylicence_ibfk_1");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.AccessId)
                    .HasName("accessId");

                entity.HasIndex(e => e.PhoneNumberId)
                    .HasName("phoneNumberId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessId).HasColumnName("accessId");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Employeeid)
                    .HasColumnName("employeeid")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneNumberId).HasColumnName("phoneNumberId");

                entity.HasOne(d => d.Access)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.AccessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_ibfk_2");

                entity.HasOne(d => d.PhoneNumber)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PhoneNumberId)
                    .HasConstraintName("employee_ibfk_1");
            });

            modelBuilder.Entity<Licence>(entity =>
            {
                entity.ToTable("licence");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Agreement)
                    .HasColumnName("agreement")
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Phonenumber>(entity =>
            {
                entity.ToTable("phonenumber");

                entity.HasIndex(e => e.PhonetypeId)
                    .HasName("phonetypeId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhonetypeId).HasColumnName("phonetypeId");

                entity.HasOne(d => d.Phonetype)
                    .WithMany(p => p.Phonenumber)
                    .HasForeignKey(d => d.PhonetypeId)
                    .HasConstraintName("phonenumber_ibfk_1");
            });

            modelBuilder.Entity<Phonetype>(entity =>
            {
                entity.ToTable("phonetype");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("varchar(4)")
                    .HasDefaultValueSql("'User'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
