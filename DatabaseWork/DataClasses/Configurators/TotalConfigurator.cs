using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.Interfaces;

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseWork.DataClasses.Configurators
{
    public class TotalConfigurator : AbstractTableConfigurator
    {
        private AbstractTableConfigurator[] _configurators;

        public TotalConfigurator(ModelBuilder builder, AbstractTableConfigurator[] configurators) : base(builder)
        {
            _configurators = configurators;
        }

        public override void Configure()
        {
            StartConfiguration();
            FinalConfigure();
            AutorizationConfigure();
        }

        private void StartConfiguration()
        {
            TinyTablesConfigure(_builder);

            foreach (AbstractTableConfigurator tbl_configurator in _configurators)
            {
                tbl_configurator.Configure();
            }
        }

        private void AutorizationConfigure()
        {
            _builder.Entity<User>()
                .HasKey(u => u.IDuser);

            _builder.Entity<Role>()
                .HasKey(r => r.IDrol);

            _builder.Entity<User>()
                .HasOne(u => u.RoleLink)
                .WithMany(r => r.UserLink)
                .HasForeignKey("IDrol");

            Role[] roles = { new Role { IDrol = 2, Name = "user" }, new Role { IDrol = 1, Name = "admin" } };

            _builder.Entity<Role>().HasData(roles);
            _builder.Entity<User>().HasData(new { IDuser = 1, UserName = "admin", Password = Cryptor.HashPasswordSHA512("9524"), IDrol = 2 });
        }

        private void FinalConfigure()
        {
            CompetenceDisciplineLinkConfigre(_builder);
            FullLinkConfigure(_builder);
        }

        private void FullLinkConfigure(ModelBuilder builder)
        {
            builder.Entity<TaskDesciplineCompetenceLink>()
                .HasKey(tdcl => tdcl.IDtdc);

            builder.Entity<Task_d>()
                .HasMany(tdcl => tdcl.TDCLink)
                .WithOne(t => t.TaskLink)
                .HasForeignKey("IDtask")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DisciplineCompetenceLink>()
                .HasMany(tdcl => tdcl.TDCLink)
                .WithOne(dc => dc.FullDCLink)
                .HasForeignKey("IDdc");
        }

        private void CompetenceDisciplineLinkConfigre(ModelBuilder builder)
        {
            builder.Entity<DisciplineCompetenceLink>()
                .HasKey(dcl => dcl.IDdc);

            builder.Entity<Competence>()
                .HasMany(d => d.DCLink)
                .WithOne(c => c.CompetenceLink)
                .HasForeignKey("IDcomp");

            builder.Entity<Discipline>()
                .HasMany(d => d.DCLink)
                .WithOne(ds => ds.DisciplineLink)
                .HasForeignKey("IDdis");
        }

        private void TinyTablesConfigure(ModelBuilder builder)
        {
            builder.Entity<SpecGroup>()
                .HasKey(sg => sg.IDsg);

            builder.Entity<Level>()
                .HasKey(lv => lv.IDlv);

            builder.Entity<TypeCompetence>()
                .HasKey(tc => tc.IDtc);

            builder.Entity<TypeTask>()
                .HasKey(tt => tt.Idtt);
        }
    }
}
