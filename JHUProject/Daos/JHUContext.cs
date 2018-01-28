using JHUProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace JHUProject.Daos
{
    public class JHUContext : DbContext
    {

        public JHUContext(DbContextOptions<JHUContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Clinician> Clinicians { get; set; }
        public DbSet<Biopsy> Biopsies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Clinician>().ToTable("Clinician");
            modelBuilder.Entity<Biopsy>().ToTable("Biopsy");
        }
    }
}