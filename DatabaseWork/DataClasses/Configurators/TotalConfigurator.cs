using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        }

        private void StartConfiguration()
        {
            TinyTablesConfigure(_builder);

            foreach (AbstractTableConfigurator tbl_configurator in _configurators)
            {
                tbl_configurator.Configure();
            }
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

            builder.Entity<TaskDesciplineCompetenceLink>()
                .HasOne(tdcl => tdcl.TaskLink)
                .WithOne(t => t.TDCLink)
                .HasForeignKey<Task_d>(t => t.IDtask);

            builder.Entity<TaskDesciplineCompetenceLink>()
                .HasOne(tdcl => tdcl.FullDCLink)
                .WithOne(dc => dc.TDCLink)
                .HasForeignKey<DisciplineCompetenceLink>(dc => dc.IDdc);
        }

        private void CompetenceDisciplineLinkConfigre(ModelBuilder builder)
        {
            builder.Entity<DisciplineCompetenceLink>()
                .HasKey(dcl => dcl.IDdc);

            builder.Entity<DisciplineCompetenceLink>()
                .HasOne(d => d.CompetenceLink)
                .WithOne(c => c.DCLink)
                .HasForeignKey<Competence>(c => c.IDcomp);

            builder.Entity<DisciplineCompetenceLink>()
                .HasOne(d => d.CompetenceLink)
                .WithOne(ds => ds.DCLink)
                .HasForeignKey<Discipline>(ds => ds.IDdis);
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
