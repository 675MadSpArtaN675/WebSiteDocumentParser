using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses.Configurators
{
    public class SubjectTablesConfigurator : AbstractTableConfigurator
    {
        public SubjectTablesConfigurator(ModelBuilder builder) : base(builder) { }

        public override void Configure()
        {
            SpecialityConfigure(_builder);
            CompetenceConfigure(_builder);
            ProfileConfigure(_builder);
            DisciplineConfigure(_builder);
        }

        private void ProfileConfigure(ModelBuilder builder)
        {

            builder.Entity<Profile>()
                .HasKey(p => p.IDpro);

            builder.Entity<Profile>()
                .HasOne(p => p.Spec)
                .WithMany(s => s.ProfileLink)
                .HasForeignKey("IDspec");

            builder.Entity<Profile>().HasData(new { IDpro=1, ProTitle="None", ProYear=DateTime.Now.Year, ProAdminissionYear= DateTime.Now.Year });
        }

        private void DisciplineConfigure(ModelBuilder builder)
        {
            builder.Entity<Discipline>()
                .HasKey(d => d.IDdis);

            builder.Entity<Profile>()
                .HasMany(d => d.Subject)
                .WithOne(p => p.R_Profile)
                .HasForeignKey("IDpro");
        }

        private void SpecialityConfigure(ModelBuilder builder)
        {
            builder.Entity<Speciality>()
                   .HasKey(s => s.IDspec);

            builder.Entity<Level>()
                   .HasOne(s => s.Spec)
                   .WithOne(lv => lv.EdLevel)
                   .HasForeignKey<Speciality>("IDlv");

            builder.Entity<SpecGroup>()
                .HasOne(s => s.Spec)
                .WithOne(lv => lv.SGroup)
                .HasForeignKey<Speciality>("IDsg");
        }

        private void CompetenceConfigure(ModelBuilder builder)
        {
            builder.Entity<Competence>()
                .HasKey(p => p.IDcomp);

            builder.Entity<TypeCompetence>()
                .HasMany(p => p.Competence)
                .WithOne(pp => pp.CompType)
                .HasForeignKey("IDtc");

            builder.Entity<Profile>()
                .HasMany(c => c.CompetenceLink)
                .WithOne(p => p.ProfileLink)
                .HasForeignKey("IDpro")
                .IsRequired();
        }
    }
}
