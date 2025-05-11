using DatabaseWork.DataClasses.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses.Configurators
{
    public class TaskTablesConfigurator : AbstractTableConfigurator
    {
        public TaskTablesConfigurator(ModelBuilder builder) : base(builder) { }

        public override void Configure()
        {
            TaskConfigure(_builder);
            TasksUtilityConfigure(_builder);
        }

        private void TasksUtilityConfigure(ModelBuilder builder)
        {
            builder.Entity<SelectedItems>()
                .HasOne(d => d.TaskLink)
                .WithOne(t => t.SItems)
                .HasForeignKey<Task_d>(t => t.IDtask)
                .IsRequired();

            builder.Entity<ItemsAccordance>()
                .HasOne(d => d.TaskLink)
                .WithOne(t => t.ItAccordance)
                .HasForeignKey<Task_d>(t => t.IDtask)
                .IsRequired();

            builder.Entity<FirstPartAccordance>()
                .HasOne(d => d.TaskLink)
                .WithOne(t => t.FPAccordance)
                .HasForeignKey<Task_d>(t => t.IDtask)
                .IsRequired();

            builder.Entity<SecondPartAccordance>()
                .HasOne(d => d.TaskLink)
                .WithOne(t => t.SPAccordance)
                .HasForeignKey<Task_d>(t => t.IDtask)
                .IsRequired();
        }

        private void TaskConfigure(ModelBuilder builder)
        {
            builder.Entity<Task_d>()
                .HasKey(t => t.IDtask);

            builder.Entity<Task_d>()
                .HasOne(t => t.TaskType)
                .WithOne(tt => tt.TaskLink)
                .HasForeignKey<TypeTask>(tt => tt.Idtt);
        }

    }
}
