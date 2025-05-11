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
                .WithOne(s => s.ProfileLink)
                .HasForeignKey<Speciality>(s => s.IDspec);
        }

        private void DisciplineConfigure(ModelBuilder builder)
        {
            builder.Entity<Discipline>()
                .HasKey(d => d.IDdis);

            builder.Entity<Discipline>()
                .HasOne(d => d.R_Profile)
                .WithOne(p => p.Subject)
                .HasForeignKey<Profile>(p => p.IDpro);
        }

        private void SpecialityConfigure(ModelBuilder builder)
        {
            builder.Entity<Speciality>()
                   .HasKey(s => s.IDspec);

            builder.Entity<Speciality>()
                   .HasOne(s => s.SGroup)
                   .WithOne(lv => lv.Spec)
                   .HasForeignKey<Level>(lv => lv.IDlv);

            builder.Entity<Speciality>()
                .HasOne(s => s.SGroup)
                .WithOne(lv => lv.Spec)
                .HasForeignKey<SpecGroup>(lv => lv.IDsg);
        }

        private void CompetenceConfigure(ModelBuilder builder)
        {
            builder.Entity<Competence>()
                .HasKey(p => p.IDcomp);

            builder.Entity<Competence>()
                .HasOne(p => p.CompType)
                .WithOne(pp => pp.Competence)
                .HasForeignKey<TypeCompetence>(pp => pp.IDtc)
                .IsRequired();

            builder.Entity<Competence>()
                .HasOne(c => c.ProfileLink)
                .WithOne(p => p.CompetenceLink)
                .HasForeignKey<Profile>(p => p.IDpro);
        }
    }
}
