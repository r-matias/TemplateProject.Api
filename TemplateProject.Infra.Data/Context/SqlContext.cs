using Microsoft.EntityFrameworkCore;
using TemplateProject.Domain.Entities.Model;
using TemplateProject.Infra.Data.Mapping;

namespace TemplateProject.Infra.Data.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserMap().Configure);
        }
    }
}
