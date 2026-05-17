using HelpDesk.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<ComentarioChamado> ComentariosChamado { get; set; }
    }
}