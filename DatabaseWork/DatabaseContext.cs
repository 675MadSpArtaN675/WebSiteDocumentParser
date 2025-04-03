using Microsoft.EntityFrameworkCore;
using DatabaseWork.DataClasses;

namespace DatabaseWork
{
    // 
    public class DatabaseContext : DbContext
    {
        public DbSet<Task_d> tasks { get; set; } = null!;
        public DbSet<TypeTask> task_types { get; set; } = null!;
        public DbSet<SelectedItems> selectedItems { get; set; } = null!;

        private readonly string _server_ip;

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

        }
    }
}
