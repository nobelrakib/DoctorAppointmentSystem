using DoctorAppointmentSystem.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Contexts
{
    public class DoctorAppointmentContext : IdentityDbContext,IDoctorAppointmentContext
    {
        private string _connectionString;
        private string _migrationAssemblyName;

        public DoctorAppointmentContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        public DoctorAppointmentContext(DbContextOptions<DoctorAppointmentContext> options)
           : base(options)

        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
            //if (!dbContextOptionsBuilder.IsConfigured)
            //{
            //    dbContextOptionsBuilder.UseMySql(@"server=localhost;port=3306;database=db;uid=root;");
            //}
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Department>()
             .HasMany<Doctor>(d => d.Doctor)
             .WithOne(d => d.Department)
             .HasForeignKey(d => d.DepartmentId);
            //.OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Doctor>()
             .HasMany<Appointment>(a => a.Appointments)
             .WithOne(d => d.Doctor)
             .HasForeignKey(d => d.DoctorId);

            builder.Entity<ExtendedIdentityUser>()
            .HasOne<Doctor>(d => d.Doctor)
            .WithOne(u => u.AppUser)
            .HasForeignKey<Doctor>(d => d.UserId);

            base.OnModelCreating(builder);
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Drug> Drugs { get; set; }

        public DbSet<ExtendedIdentityUser> AppUser { get; set; }
    }
}
