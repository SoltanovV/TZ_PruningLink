using PruningLink.Model.Entity;

namespace PruningLink.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Url> Urls { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=solt;database=usersdb;", new MySqlServerVersion(new Version(8, 0, 30)));
        }

    }
}
