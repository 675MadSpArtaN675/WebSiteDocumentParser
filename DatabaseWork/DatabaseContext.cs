﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataClasses;
using DatabaseWork.Interfaces;
using DatabaseWork.DataClasses.Configurators;

namespace DatabaseWork
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Task_d> Tasks { get; set; } = null!;
        public DbSet<TypeTask> TaskTypes { get; set; } = null!;

        public DbSet<Competence> Competences { get; set; } = null!;
        public DbSet<TypeCompetence> TypesOfCompetences { get; set; } = null!;

        public DbSet<SpecGroup> SpecGroups { get; set; } = null!;
        public DbSet<Level> Levels { get; set; } = null!;
        public DbSet<Speciality> Specialities { get; set; } = null!;
        public DbSet<Discipline> Disciplines { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; } = null!;

        public DbSet<SelectedItems> SelectedItems { get; set; } = null!;
        public DbSet<ItemsAccordance> ItemsAccordance { get; set; } = null!;
        public DbSet<FirstPartAccordance> FirstPartsAccordances { get; set; } = null!;
        public DbSet<SecondPartAccordance> SecondPartsAccordances { get; set; } = null!;

        public DbSet<DisciplineCompetenceLink> FullDiscipline { get; set; } = null!;
        public DbSet<TaskDesciplineCompetenceLink> FullTDC { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        private readonly string _server_ip;

        public DatabaseContext() : this("localhost")
        { }

        public DatabaseContext(string server_ip)
        {
            this._server_ip = server_ip;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host={_server_ip};Port=5432;Database=app_database;Username=application_parser;Password=31bn74jf01");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AbstractTableConfigurator[] configurator_modules = {
                new SubjectTablesConfigurator(modelBuilder),
                new TaskTablesConfigurator(modelBuilder),
            };

            TotalConfigurator configurator = new TotalConfigurator(modelBuilder, configurator_modules);

            configurator.Configure();
        }
    }
}
