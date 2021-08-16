using ApiCatalogoJogos.Entities;
using Microsoft.EntityFrameworkCore;


namespace ApiCatalogoJogos.Db
{
    public class JogoDbContext : DbContext
    {
        public JogoDbContext(DbContextOptions<JogoDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JogoMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Jogo> Jogo { get; set; }
    }
}
