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
            builder.Entity<Task_d>()
                .HasMany(d => d.SItems)
                .WithOne(t => t.TaskLink)
                .HasForeignKey("IDtask")
                .IsRequired();

            builder.Entity<Task_d>()
                .HasMany(d => d.ItAccordance)
                .WithOne(t => t.TaskLink)
                .HasForeignKey("IDtask")
                .IsRequired();

            builder.Entity<Task_d>()
                .HasMany(d => d.FPAccordance)
                .WithOne(t => t.TaskLink)
                .HasForeignKey("IDtask")
                .IsRequired();

            builder.Entity<Task_d>()
                .HasMany(d => d.SPAccordance)
                .WithOne(t => t.TaskLink)
                .HasForeignKey("IDtask")
                .IsRequired();
        }

        private void TaskConfigure(ModelBuilder builder)
        {
            builder.Entity<Task_d>()
                .HasKey(t => t.IDtask);

            builder.Entity<TypeTask>()
                .HasMany(t => t.TaskLink)
                .WithOne(tt => tt.TaskType)
                .HasForeignKey("Idtt");
        }

    }
}
